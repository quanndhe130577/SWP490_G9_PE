using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.Domain.Entities;

namespace TnR_SS.Domain.Repositories
{
    public interface IRepositoryBase<T> : IDisposable where T : class
    {
        Task<T> FindAsync(params object[] value);

        IEnumerable<T> GetAll(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null,
            int? skip = null, int? take = null
            );
        IEnumerable<T> GetAllAsync();
        int CountRecords(Expression<Func<T, bool>> filter = null);

        T GetFirstOrDefault(
            Expression<Func<T, bool>> filter = null,
            string includeProperties = null
            );

        Task CreateAsync(T entity);

        void Update(T entity);

        void DeleteById(int id);

        void Delete(T entity);

    }
}
