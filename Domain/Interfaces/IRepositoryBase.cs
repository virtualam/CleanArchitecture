using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IRepositoryBase<T>
    {
        Task<IEnumerable<T>> FindAll(IPaginationFilter paginationFilter);
        Task<IEnumerable<T>> FindByCondition(Expression<Func<T, bool>> expression, IPaginationFilter paginationFilter);
        Task Create(T entity);
        Task Update(string id, T entity);
        Task Delete(string id);
    }
}
