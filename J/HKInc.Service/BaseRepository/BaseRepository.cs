using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity;

using HKInc.Ui.Model.BaseDomain;
using HKInc.Utils.Common;
using HKInc.Utils.Interface.Repository;

namespace HKInc.Service.BaseRepository
{
    public abstract class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected string connectString;
        protected DbContext db;
        protected DbSet<TEntity> dbSet;

        public IEnumerable<TEntity> GetAll()
        {
            return dbSet;
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> condition)
        {
            return dbSet.Where(condition);
        }

        public List<TEntity> FindDetached(Expression<Func<TEntity, bool>> condition)
        {
            List<TEntity> returnList = new List<TEntity>();

            foreach (var obj in dbSet.Where(condition))
                returnList.Add(Detached(obj));

            return returnList;
        }

        public virtual TEntity Insert(TEntity obj, DateTime? dateTime = null)
        {
            if (dateTime == null)
                dateTime = DateTime.Now;

            if (obj != null)
            {
                if (obj is BaseDomain)
                {
                    (obj as BaseDomain).CreateTime = (DateTime)dateTime;
                    (obj as BaseDomain).CreateId = GlobalVariable.LoginId;
                    //(obj as BaseDomain).CreateClass = GlobalVariable.CurrentInstance;

                    (obj as BaseDomain).UpdateTime = (DateTime)dateTime;
                    (obj as BaseDomain).UpdateId = GlobalVariable.LoginId;
                    //(obj as BaseDomain).UpdateClass = GlobalVariable.CurrentInstance;
                }
                dbSet.Add(obj);
            }
            return obj;
        }

        public virtual TEntity Update(TEntity obj, DateTime? dateTime = null)
        {
            if (dateTime == null)
                dateTime = DateTime.Now;

            if (obj != null)
            {
                if (obj is BaseDomain)
                {
                    (obj as BaseDomain).UpdateTime = (DateTime)dateTime;
                    (obj as BaseDomain).UpdateId = GlobalVariable.LoginId;
                    //(obj as BaseDomain).UpdateClass = GlobalVariable.CurrentInstance;
                }
                if (db.Entry<TEntity>(obj).State == EntityState.Detached)
                    dbSet.Attach(obj);

                db.Entry<TEntity>(obj).State = EntityState.Modified;
            }
            return obj;
        }

        public void Delete(TEntity obj)
        {
            if (obj != null)
            {
                if (db.Entry<TEntity>(obj).State == EntityState.Detached)
                    dbSet.Attach(obj);

                dbSet.Remove(obj);
            }
        }

        public int Save()
        {
            try
            {
                // Your code...
                // Could also be before try if you know the exception occurs in SaveChanges

                var test = db.SaveChanges();

                return db.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            //return db.SaveChanges();
        }

        public void Reload()
        {
            db.Dispose();
            dbSet = null;

            ReloadContext();
        }

        protected virtual void ReloadContext() { }

        public TEntity Detached(TEntity obj)
        {
            db.Entry<TEntity>(obj).State = EntityState.Detached;
            return obj;
        }

        public DbContext GetDbContext() { return db; }

        public void Dispose()
        {
            if (dbSet != null) dbSet = null;
            if (db != null)
            {
                db.Database.Connection.Close();
                db.Dispose();
                db = null;
            }
        }

    }
}
