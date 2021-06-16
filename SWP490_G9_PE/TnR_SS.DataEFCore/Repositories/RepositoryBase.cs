using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using TnR_SS.Domain.Repositories;

namespace TnR_SS.DataEFCore.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected readonly TnR_SSContext _context;
        internal DbSet<T> dbSet;

        public RepositoryBase(TnR_SSContext context)
        {
            _context = context;
            this.dbSet = context.Set<T>();
        }
        public virtual async Task CreateAsync(T entity)
        {
            Type t = entity.GetType();
            PropertyInfo createdAt = t.GetProperty("CreatedAt");
            if (createdAt is not null)
            {
                createdAt.SetValue(entity, DateTime.Now);
            }


            PropertyInfo updatedAt = t.GetProperty("UpdatedAt");
            if (updatedAt is not null)
            {
                updatedAt.SetValue(entity, DateTime.Now);
            }

            await dbSet.AddAsync(entity);
        }

        public void Dispose() => _context.Dispose();

        public virtual async Task<T> FindAsync(params object[] value)
        {
            return await dbSet.FindAsync(value);
        }

        public virtual int CountRecords(Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query.Count();
        }

        public virtual IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = null,
            int? skip = null, int? take = null)
        {
            if (take != null && take <= 0)
            {
                return new List<T>();
            }

            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query.Include(includeProperty);
                }
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (skip != null && take != null)
            {
                return query.Skip((int)skip).Take((int)take);
            }

            return query;
        }

        public virtual T GetFirstOrDefault(Expression<Func<T, bool>> filter = null, string includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            // Include properties will be comma spereated
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query.Include(includeProperty);
                }
            }

            return query.FirstOrDefault();
        }

        public virtual void DeleteById(int id)
        {
            T entityToRemove = dbSet.Find(id);
            Delete(entityToRemove);
        }

        public virtual void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public virtual void Update(T entity)
        {
            Type t = entity.GetType();
            PropertyInfo updatedAt = t.GetProperty("UpdatedAt");
            if (updatedAt is not null)
            {
                updatedAt.SetValue(entity, DateTime.Now);
            }
            dbSet.Update(entity);
        }

        public IEnumerable<T> GetAllAsync()
        {
            return (IEnumerable<T>)dbSet.AsAsyncEnumerable();
        }
    }
}
