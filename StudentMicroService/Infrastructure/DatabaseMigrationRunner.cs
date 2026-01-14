using Microsoft.Data.SqlClient;
using System.Text.RegularExpressions;

namespace StudentMicroService.Infrastructure
{
    public class DatabaseMigrationRunner
    {
        private readonly string _connectionString;

        public DatabaseMigrationRunner(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Run()
        {
            Console.WriteLine(string.Format("Started: Migration Run on {0}", DateTime.Now.ToString()));
            EnsureDatabaseExists(_connectionString);

            using var conn = new SqlConnection(_connectionString);
            conn.Open();
            EnsureVersionTable(conn);
            var currentVersion = GetCurrentVersion(conn);

            Console.WriteLine("Current Version:" + currentVersion);
            var scripts = Directory.GetFiles("Infrastructure\\Database", "V*.sql")
                                   .OrderBy(x => x)
                                   .ToList();
            foreach (var script in scripts)
            {
                Console.WriteLine(string.Format("Start Script: {0}", script));
                var version = int.Parse(Path.GetFileName(script).Split("_")[0].Substring(1));
                if (version > currentVersion)
                {
                    var sql = File.ReadAllText(script);
                    var batches = Regex.Split(sql, @"^\s*GO\s*$", RegexOptions.Multiline | RegexOptions.IgnoreCase);
                    foreach (var batch in batches)
                    {
                        if (string.IsNullOrWhiteSpace(batch)) continue;

                        using var cmd = conn.CreateCommand();
                        cmd.CommandText = batch;
                        cmd.ExecuteNonQuery();
                    }
                    SaveVersion(conn, version);
                }
                Console.WriteLine(string.Format("End of Script: {0}", script));
            }
            Console.WriteLine(string.Format("Completed: Migration Run on {0}", DateTime.Now.ToString()));
        }

        private void EnsureVersionTable(SqlConnection conn)
        {
            var cmd = new SqlCommand(@"IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = '__SchemaVersions')
                                        BEGIN
                                            CREATE TABLE __SchemaVersions (
                                                Version INT PRIMARY KEY,
                                                AppliedOn DATETIME NOT NULL
                                            );
                                        END", conn);

            cmd.ExecuteNonQuery();
        }

        private int GetCurrentVersion(SqlConnection conn)
        {
            var cmd = new SqlCommand("SELECT ISNULL(MAX(Version), 0) FROM __SchemaVersions", conn);
            return (int)cmd.ExecuteScalar();
        }

        private void SaveVersion(SqlConnection conn, int version)
        {
            var cmd = new SqlCommand("INSERT INTO __SchemaVersions VALUES (@v, GETDATE())", conn);
            cmd.Parameters.AddWithValue("@v", version);
            cmd.ExecuteNonQuery();
        }

        private void EnsureDatabaseExists(string connectionString)
        {
            var builder = new SqlConnectionStringBuilder(connectionString);
            var databaseName = builder.InitialCatalog;
            builder.InitialCatalog = "master";

            using var conn = new SqlConnection(builder.ConnectionString);
            conn.Open();
            var cmd = new SqlCommand(
                $"IF DB_ID('{databaseName}') IS NULL CREATE DATABASE [{databaseName}];", conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}
