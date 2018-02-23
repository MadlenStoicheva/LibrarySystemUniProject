using LibrarySystem.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entity.Repositories
{
    interface IRepository<T> where T : BaseEntity
    {
        List<T> GetAll();
        List<T> GetAll(Expression<Func<T, bool>> filter);
        T Get(int id);
        T Get(Expression<Func<T, bool>> filter);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(int id);
    }
}
