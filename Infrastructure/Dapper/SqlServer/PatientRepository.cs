using Dapper;
using Domain.Entities.Resources;
using Domain.Interfaces;
using Infrastructure.SQL;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Dapper.SqlServer
{
    public class PatientRepository : RepositoryBase<Patient>, IPatientRepository
    {
        public PatientRepository(IDbConnectionFactory dbConnectionFactory) : base(dbConnectionFactory)
        {

        }

        public override async Task Delete(string id)
        {
            using (var connection = _dbConnectionFactory.GetConnection)
            {
                string sql = $"DELETE FROM Patient WHERE PatientId = @PatientId";
                var result = await connection.QueryAsync(
                        sql,
                        new { PatientId = id }
                    );
            }
        }

        public override async Task Create(Patient entity)
        {
            using (var connection = _dbConnectionFactory.GetConnection)
            {
                string sql = $"INSERT INTO Patient (Name, DateOfBirth, Gender) VALUES (@Name, @DateOfBirth, @Gender)";
                var result = await connection.QueryAsync<Patient>(
                        sql,
                        new { Name = entity.Name, DateOfBirth = entity.DateOfBirth, Gender = entity.Gender.HasValue ? entity.Gender.Value.ToString() : null }
                    );
            }
        }

        public override async Task<IEnumerable<Patient>> FindAll(IPaginationFilter paginationFilter)
        {
            using (var connection = _dbConnectionFactory.GetConnection)
            {
                string sql = $"SELECT * FROM Patient ORDER BY CreatedDate OFFSET((@pageNumber-1)*@pageSize) ROWS FETCH NEXT @pageSize ROWS ONLY";
                var result = await connection.QueryAsync<Patient>(
                        sql,
                        new { pageNumber = paginationFilter.PageNumber, pageSize = paginationFilter.PageSize }
                    );

                return result;
            }
        }
    }
}
