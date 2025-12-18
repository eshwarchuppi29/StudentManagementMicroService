using Dapper;
using StudentMangementSystem.Model.Log.Implementation;
using StudentMangementSystem.Model.Models;
using StudentMangementSystem.Model.Models.Student;
using StudentMicroService.DatebaseFactory;
using System.Data;

namespace StudentMicroService.Repositories.Implementation
{
    public class ApiLogRepository : BaseApiLogRepository
    {
        private readonly DbConnectionFactory _connectionFactory;

        public ApiLogRepository(DbConnectionFactory connectionFactory)
        {
            this._connectionFactory = connectionFactory;
        }
        public async override Task SaveAsync(ApiLog log)
        {
            var parameters = new DynamicParameters();
            using (var connection = await _connectionFactory.CreateConnectionAsync())
            {
                parameters.Add("@TraceId", log.TraceId, DbType.String);
                parameters.Add("@RequestPath", log.RequestPath, DbType.String);
                parameters.Add("@RequestMethod", log.RequestMethod, DbType.String);
                parameters.Add("@RequestBody", log.RequestBody, DbType.String);
                parameters.Add("@StatusCode", log.StatusCode, DbType.Int16);
                parameters.Add("@ResponseBody", log.ResponseBody, DbType.String);
                parameters.Add("@ExceptionMessage", log.ExceptionMessage, DbType.String);
                parameters.Add("@ExceptionStackTrace", log.ExceptionStackTrace, DbType.String);
                parameters.Add("@ExecutionTimeMs", log.ExecutionTimeMs, DbType.Int64);

                await connection.ExecuteAsync(sql: "sp_ApiLogs",
                                            param: parameters,
                                            commandType: CommandType.StoredProcedure);
            }
        }
    }
}
