
using PersonApplication.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace PersonApplication.DataAccess.Repository
{
    public abstract class GenericRepository<T> : IGenericRepository<T>
          where T : EntityBase, ISoftDelete
    {
        protected DbContext _entities;
        protected readonly IDbSet<T> _dbset;

        public GenericRepository(DbContext context)
        {
            _entities = context;
            _dbset = context.Set<T>();
        }
        public virtual T Add(T entity)
        {
            return _dbset.Add(entity);
        }
        public void Delete(object Id)
        {
            T getObjById = _dbset.Find(Id);
             _dbset.Remove(getObjById);

        }

        public T GetById(object Id)
        {
          return _dbset.Find(Id);

        //  return  _dbset.Select(x => x).Include("PersonAddress").Single(x => x.Id ==(int) Id);
                


        }

        //public virtual T Delete(T entity)
        //{
        //    return _dbset.Remove(entity);
        //}
        public virtual void Delete(T entity)
        {
          
            ISoftDelete e = entity;
            e.DeletedDate = DateTime.Now;
            e.IsDeleted = true;
            e.DeletedBy = 2;

        }
        public virtual void Edit(T entity)
        {
            _entities.Entry(entity).State = EntityState.Modified;
        }

        public virtual IQueryable<T> GetAll()
        {
            //    return _dbset.AsEnumerable<T>();
            //  return _dbset.Where(e => e.IsDeleted == false).Include("PersonAddress");
            return _dbset.AsQueryable<T>();
        }

      

        public virtual IEnumerable<T> FindBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            IEnumerable<T> query = _dbset.Where(predicate).AsEnumerable();
            return query;
        }

        public virtual void Save()
        {
            _entities.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_entities != null)
                {
                    _entities.Dispose();
                    _entities = null;
                }
            }
        }
      }
}
