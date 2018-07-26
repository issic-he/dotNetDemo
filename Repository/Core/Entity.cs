using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Core
{
    public abstract class Entity<T> where T:struct
    {
        /// <summary>
        /// 主键
        /// </summary>
        public T Id { get; set; }

    }
}
