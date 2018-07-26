using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Repository.Interface
{
    public interface IRepository<T> where T: class
    {

        int GetCount(Expression<Func<T, bool>> exp = null);

        bool IsExist(Expression<Func<T, bool>> exp);

        bool Add(T entity);

        void BatchAdd(T[] entityies);

        bool Update(T entity);

        /// <summary>
        /// 按需更新
        /// </summary>
        /// <param name="where"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Update(Expression<Func<T, bool>> where, Expression<Func<T, T>> entity);

        bool Delete(T entity);

        int Delete(Expression<Func<T, bool>> where);

        IQueryable<T> Find(Expression<Func<T, bool>> exp = null);

        IQueryable<T> Find(int pageIndex = 1, int pageSize = 10, string orderby = ""
            , Expression<Func<T, bool>> exp = null);

        T FindSingle(Expression<Func<T, bool>> exp = null);

        int ExecuteSql(string sql);


    }
}
