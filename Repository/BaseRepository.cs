using Microsoft.EntityFrameworkCore;
using Repository.Core;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Z.EntityFramework.Plus;

namespace Repository
{
    public class BaseRepository<T,TKey> : IRepository<T> 
        where T : Entity<TKey>
        where TKey :struct
    {
        private EFDBContext _context;

        public BaseRepository(EFDBContext context)
        {
            _context = context;
        }

        public bool Add(T entity)
        {
            _context.Set<T>().Add(entity);
            var result = Save() > 0;
            _context.Entry(entity).State = EntityState.Detached;
            return result;
        }

        public void BatchAdd(T[] entityies)
        {
            _context.Set<T>().AddRange(entityies);
            Save();
        }

        public bool Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            return Save() > 0;
        }

        public int Delete(Expression<Func<T, bool>> where)
        {
            return _context.Set<T>().Where(where).Delete();
        }

        public bool Update(T entity)
        {
            var entry = this._context.Entry(entity);
            entry.State = EntityState.Modified;

            if (!this._context.ChangeTracker.HasChanges())
            {
                return true;
            }

            return Save() > 0;
        }

        public int Update(Expression<Func<T, bool>> where, Expression<Func<T, T>> entity)
        {
            return _context.Set<T>().Where(where).Update(entity);
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> exp = null)
        {
            return Filter(exp);
        }
        /// <summary>
        /// 获取分页记录
        /// </summary>
        /// <param name="pageIndex">页码（从1开始）first at 1</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="orderby">排序</param>
        /// <param name="exp">条件</param>
        /// <returns></returns>
        public IQueryable<T> Find(int pageIndex = 1, int pageSize = 10, string orderby = "", Expression<Func<T, bool>> exp = null)
        {
            pageSize = pageSize < 1 ? 0 : pageSize - 1;
            if(string.IsNullOrEmpty(orderby))
            {
                orderby = "Id descending";
            }
            return Filter(exp).
                //OrderBy(orderby).
                Skip(pageIndex * pageSize).Take(pageSize);
        }

        public T FindSingle(Expression<Func<T, bool>> exp = null)
        {
            return Filter(exp).FirstOrDefault();
        }

        public int GetCount(Expression<Func<T, bool>> exp = null)
        {
            return Filter(exp).Count();
        }

        public bool IsExist(Expression<Func<T, bool>> exp)
        {
            return _context.Set<T>().Any(exp);
        }


        public int ExecuteSql(string sql)
        {
            return _context.Database.ExecuteSqlCommand(sql);
        }


        public int Save()
        {
            return _context.SaveChanges();
        }

        private IQueryable<T> Filter(Expression<Func<T,bool>> exp)
        {
            var dbSet = _context.Set<T>().AsNoTracking().AsQueryable();

            if (exp != null) 
            {
                dbSet = dbSet.Where(exp);
            }
            return dbSet;
        }

    }
}
