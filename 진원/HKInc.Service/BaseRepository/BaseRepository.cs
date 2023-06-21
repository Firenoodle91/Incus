﻿using System;
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

        public virtual TEntity Insert(TEntity obj)
        {
            if (obj != null)
            {
                (obj as BaseDomain3).CreateTime = DateTime.Now;
                (obj as BaseDomain3).CreateId = GlobalVariable.UserInfo.LoginId;
                (obj as BaseDomain3).CreateClass = GlobalVariable.CurrentInstance;

                (obj as BaseDomain3).UpdateTime = DateTime.Now;
                (obj as BaseDomain3).UpdateId = GlobalVariable.UserInfo.LoginId;
                (obj as BaseDomain3).UpdateClass = GlobalVariable.CurrentInstance;

                dbSet.Add(obj);
            }
            return obj;
        }

        public virtual TEntity Update(TEntity obj)
        {
            if (obj != null)
            {
                (obj as BaseDomain3).UpdateTime = DateTime.Now;
                (obj as BaseDomain3).UpdateId = GlobalVariable.UserInfo.LoginId;
                (obj as BaseDomain3).UpdateClass = GlobalVariable.CurrentInstance;

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
            return db.SaveChanges();
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
