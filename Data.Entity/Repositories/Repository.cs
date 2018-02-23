using LibrarySystem.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entity.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private System.Data.Entity.DbContext db;
        private System.Data.Entity.DbSet<T> dbSet;

        public Repository()
        {
            db = new LibrarySystemContext();
            dbSet = db.Set<T>();
        }
        public List<T> GetAll()
        {
            return dbSet.ToList();
        }

        public List<T> GetAll(System.Linq.Expressions.Expression<Func<T, bool>> filter)
        {
            return dbSet.Where(filter).ToList();
        }

        public T Get(int id)
        {
            return dbSet.Find(id);
        }

        public T Get(System.Linq.Expressions.Expression<Func<T, bool>> filter)
        {
            return dbSet.SingleOrDefault(filter);
        }

        public void Insert(T entity)
        {
            db.Entry(entity).State = EntityState.Added;
            db.SaveChanges();

        }

        public void Update(T entity)
        {
            db.Entry(entity).State = EntityState.Modified;
            db.SaveChanges();

        }

        public T GetById(int id)
        {
            return dbSet.Find(id);
        }


        public void Save(T entity)
        {
            if (entity.Id > 0)
            {
                Update(entity);
            }
            else
            {
                Insert(entity);
            }

            db.SaveChanges();
        }

        public void Delete(int id)
        {
            T entity = GetById(id);
            Delete(entity);
        }
        public void Delete(T entity)
        {
            db.Entry(entity).State = EntityState.Deleted;
            db.SaveChanges();
        }
    }
}
