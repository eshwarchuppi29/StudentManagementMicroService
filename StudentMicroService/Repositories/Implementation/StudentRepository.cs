using Dapper;
using StudentMangementSystem.Model.Models.Student;
using StudentMicroService.DatebaseFactory;
using StudentMicroService.Repositories.Interfaces;
using System.Data;

namespace StudentMicroService.Repositories.Implementation
{
    public class StudentRepository : IStudentRepository
    {
        private readonly DbConnectionFactory _connectionFactory;

        public StudentRepository(DbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            using var connection = await _connectionFactory.CreateConnectionAsync();
            return await connection.QueryAsync<Student>("SELECT * FROM Students WHERE IsArchived = 0");
            //return await connection.QueryAsync<Student>("", commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task<Student?> GetByIdAsync(Guid id)
        {
            using var connection = await _connectionFactory.CreateConnectionAsync();
            return await connection.QueryFirstOrDefaultAsync<Student>(
                "SELECT * FROM Students WHERE Id = @Id AND IsArchived = 0",
                new { Id = id });
        }

        public async Task<Student> CreateAsync(Student student)
        {
            var parameters = new DynamicParameters();
            using (var connection = await _connectionFactory.CreateConnectionAsync())
            {
                parameters.Add("@FirstName", student.FirstName, DbType.String);
                parameters.Add("@MiddleName", student.MiddleName, DbType.String);
                parameters.Add("@LastName", student.LastName, DbType.String);
                parameters.Add("@DateOfBirth", student.DateOfBirth, DbType.DateTime);
                parameters.Add("@Gender", student.Gender, DbType.Int16);
                parameters.Add("@FatherName", student.FatherName, DbType.String);
                parameters.Add("@MotherName", student.MotherName, DbType.String);
                parameters.Add("@AdharNumber", student.AdharNumber, DbType.String);
                parameters.Add("@Address", student.Address, DbType.String);
                parameters.Add("@CreatedBy", student.CreatedBy, DbType.String);

                parameters.Add("@StudentId", null, DbType.Guid, ParameterDirection.Output);
                parameters.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
                parameters.Add("@ReturnValue", null, DbType.Int32, ParameterDirection.ReturnValue);

                await connection.ExecuteAsync(sql: "sp_AddNewStudent",
                                            param: parameters,
                                            commandType: CommandType.StoredProcedure);
                int result = parameters.Get<int>("ReturnValue");
                if (result == 0)
                {
                    student.Id = parameters.Get<Guid>("StudentId");
                    return student;
                }
                else
                {
                    return null;
                }
            }
        }

        public async Task<Student> UpdateAsync(Student student)
        {
            var parameters = new DynamicParameters();
            using (var connection = await _connectionFactory.CreateConnectionAsync())
            {
                parameters.Add("@Id", student.Id, DbType.Guid);
                parameters.Add("@FirstName", student.FirstName, DbType.String);
                parameters.Add("@MiddleName", student.MiddleName, DbType.String);
                parameters.Add("@LastName", student.LastName, DbType.String);
                parameters.Add("@DateOfBirth", student.DateOfBirth, DbType.Date);
                parameters.Add("@Gender", student.Gender, DbType.Int16);
                parameters.Add("@FatherName", student.FatherName, DbType.String);
                parameters.Add("@MotherName", student.MotherName, DbType.String);
                parameters.Add("@AdharNumber", student.MotherName, DbType.String);
                parameters.Add("@Address", student.Address, DbType.String);
                parameters.Add("@UpdatedBy", student.CreatedBy, DbType.String);

                parameters.Add("@Message", null, DbType.Guid, ParameterDirection.Output);
                parameters.Add("@ReturnValue", null, DbType.Int16, ParameterDirection.ReturnValue);

                await connection.ExecuteAsync(sql: "sp_UpdateStudent",
                                            param: parameters,
                                            commandType: CommandType.StoredProcedure);

                int result = parameters.Get<int>("ReturnValue");
                if (result == 0)
                {
                    return student;
                }
                else
                {
                    return null;
                }
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
