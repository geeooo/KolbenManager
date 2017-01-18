using KolbenService.Database.IEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KolbenService.Services.Interfaces
{
    public interface IServiceBase<T> where T : class, IEntityBase, new()
    {
        Task<List<T>> GetAll(params Expression<Func<T, object>>[] includes);

        Task<int> Count();

        Task<T> GetSingle(int id, params Expression<Func<T, object>>[] includes);

        Task<T> GetSingle(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);

        Task<List<T>> FindBy(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);

        Task<int> Add(T entity);

        Task Update(T entity);       
        Task Delete(T entity);
        Task Delete(int id);
    }
}
