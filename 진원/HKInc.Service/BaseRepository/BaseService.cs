using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Linq.Expressions;

using HKInc.Ui.Model.Context;
using HKInc.Utils.Common;
using HKInc.Service.Repository;
using HKInc.Utils.Interface.Repository;
using HKInc.Utils.Interface.Service;

namespace HKInc.Service.BaseRepository
{
    public abstract class BaseService<TEntity> : IService<TEntity> where TEntity : class
    {
        protected IRepository<TEntity> repository;

        public List<TEntity> GetList()
        {
            return repository.GetAll().ToList();
        }

        public List<TEntity> GetList(Expression<Func<TEntity, bool>> condition)
        {
            return repository.Find(condition).ToList();
        }

        public List<TEntity> GetListDetached(Expression<Func<TEntity, bool>> condition)
        {
            return repository.FindDetached(condition);
        }

        public TEntity Insert(TEntity obj)
        {
            return repository.Insert(obj);
        }

        public TEntity Update(TEntity obj)
        {
            obj = repository.Update(obj);
            return obj;
        }

        public void Delete(TEntity obj)
        {
            repository.Delete(obj);
        }

        public virtual int Save(bool isNotifiable = false)
        {
            int changeCount = repository.Save();

            if (isNotifiable)
            {
                //SystemContext dbContext = (SystemContext)repository.GetDbContext();
                //dbContext.SendSqlNotification(typeof(TEntity).Name);
            }

            return changeCount;
        }

        public void ReLoad()
        {
            repository.Reload();
        }

        public List<Child> GetChildList<Child>(Expression<Func<Child, bool>> condition) where Child : class
        {
            DbContext dbContext = repository.GetDbContext();            
            return dbContext.Set<Child>().Where(condition).ToList();
        }

        /// <summary>
        /// Local Find Key 
        /// </summary>
        /// <typeparam name="Child"></typeparam>
        /// <param name="Key"></param>
        /// <returns></returns>
        public bool GetChildFindFlag<Child>(object Key) where Child : class
        {
            DbContext dbContext = repository.GetDbContext();
            return dbContext.Set<Child>().Find(Key) == null ? false : true;
        }

        public List<Child> GetChildListDetached<Child>(Expression<Func<Child, bool>> condition) where Child : class
        {
            List<Child> returnList = new List<Child>();
            DbContext dbContext = repository.GetDbContext();

            foreach (var obj in dbContext.Set<Child>().Where(condition))
                returnList.Add(DetachChild<Child>(obj));

            return returnList;
        }

        public Child InsertChild<Child>(Child obj) where Child : class
        {
            DbContext dbContext = repository.GetDbContext();
            return dbContext.Set<Child>().Add(obj);
        }

        public Child UpdateChild<Child>(Child obj) where Child : class
        {
            DbContext dbContext = repository.GetDbContext();
            DbSet<Child> dbSet = dbContext.Set<Child>();

            if (obj != null)
            {
                if (dbContext.Entry<Child>(obj).State == EntityState.Detached)
                    dbSet.Attach(obj);

                dbContext.Entry<Child>(obj).State = EntityState.Modified;
            }
            return obj;
        }

        public void RemoveChild<Child>(Child obj) where Child : class
        {
            DbContext dbContext = repository.GetDbContext();
            dbContext.Set<Child>().Remove(obj);
        }

        public Child DetachChild<Child>(Child obj) where Child : class
        {
            DbContext dbContext = repository.GetDbContext();
            dbContext.Entry<Child>(obj).State = EntityState.Detached;
            return obj;
        }

        public TEntity Detached(TEntity obj)
        {
            return repository.Detached(obj);
        }
        public virtual void Dispose()
        {
            if (repository != null)
            {
                repository.Dispose();
                repository = null;
            }
        }
    }
}