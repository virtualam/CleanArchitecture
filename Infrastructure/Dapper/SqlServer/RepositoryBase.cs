using Domain.Interfaces;
using Infrastructure.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Infrastructure.Dapper
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected readonly IDbConnectionFactory _dbConnectionFactory;

        public RepositoryBase(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }
        public virtual Task Create(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual Task Delete(string id)
        {
            throw new NotImplementedException();
        }

        public virtual Task<IEnumerable<T>> FindAll(IPaginationFilter paginationFilter)
        {
            throw new NotImplementedException();
        }

        public virtual Task<IEnumerable<T>> FindByCondition(Expression<Func<T, bool>> expression, IPaginationFilter paginationFilter)
        {
            throw new NotImplementedException();
        }

        public Task Update(string id, T entity)
        {
            throw new NotImplementedException();
        }
    }
}
