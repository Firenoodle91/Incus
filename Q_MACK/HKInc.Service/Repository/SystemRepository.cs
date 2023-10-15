using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

using HKInc.Ui.Model.Context;
using HKInc.Ui.Model.BaseDomain;
using HKInc.Utils.Common;
using HKInc.Utils.Enum;
using HKInc.Utils.Interface.Repository;

namespace HKInc.Service.Repository
{
    //class SystemRepository<TEntity> : IRepository<TEntity> where TEntity : class
    //{
    //    private string connectString;
    //    private SystemContext db;
    //    private DbSet<TEntity> dbSet;        

    //    public SystemRepository(string connectString)
    //    {
    //        db = new SystemContext(connectString);
    //        dbSet = db.Set<TEntity>();
    //        this.connectString = connectString;
    //    }
    //    public IEnumerable<TEntity> GetAll()
    //    {
    //        return dbSet;            
    //    }

    //    public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> condition)
    //    {
    //        return dbSet.Where(condition);          
    //    }

    //    public TEntity Insert(TEntity obj)
    //    {
    //        if (obj != null)
    //        {
    //            (obj as BaseDomain).CreateTime = DateTime.Now;
    //            (obj as BaseDomain).CreateId = GlobalVariable.UserInfo.LoginId;
    //            (obj as BaseDomain).CreateClass = GlobalVariable.CurrentInstance;

    //            (obj as BaseDomain).UpdateTime = DateTime.Now;
    //            (obj as BaseDomain).UpdateId = GlobalVariable.UserInfo.LoginId;
    //            (obj as BaseDomain).UpdateClass = GlobalVariable.CurrentInstance;

    //            dbSet.Add(obj);
    //        }
    //        return obj;
    //    }

    //    public TEntity Update(TEntity obj)
    //    {
    //        if (obj != null)
    //        {
    //            (obj as BaseDomain).UpdateTime = DateTime.Now;
    //            (obj as BaseDomain).UpdateId = GlobalVariable.UserInfo.LoginId;
    //            (obj as BaseDomain).UpdateClass = GlobalVariable.CurrentInstance;

    //            if (db.Entry<TEntity>(obj).State == EntityState.Detached)
    //                dbSet.Attach(obj);

    //            db.Entry<TEntity>(obj).State = EntityState.Modified;
    //        }
    //        return obj;
    //    }

    //    public void Delete(TEntity obj)
    //    {
    //        if (obj != null)
    //        {
    //            if(db.Entry<TEntity>(obj).State == EntityState.Detached)
    //                dbSet.Attach(obj);

    //            dbSet.Remove(obj);
    //        }
    //    }

    //    public void Save()
    //    {

    //    }

    //    public void Reload()
    //    {            
    //        db.Dispose();
    //        dbSet = null;

    //        db = new SystemContext(connectString);
    //        dbSet = db.Set<TEntity>();
    //    }

    //    public TEntity Detached(TEntity obj)
    //    {
    //        db.Entry<TEntity>(obj).State = EntityState.Detached;
    //        return obj;
    //    }        

    //    public DbContext GetDbContext() { return db; }

    //    public void Dispose()
    //    {
    //        if (db != null) db.Dispose();
    //        if (dbSet != null) dbSet = null;
    //    }

    //}
    public class SystemRepository<TEntity> : BaseRepository.BaseRepository<TEntity> where TEntity : class
    {
        public SystemRepository(string connectString)
        {
           
                db = new ProductionContext(connectString);
                dbSet = db.Set<TEntity>();
                this.connectString = connectString;
          
        }

        public override TEntity Insert(TEntity obj)
        {
            if (obj != null)
            {
                if (obj is BaseDomain3)
                {
                    (obj as BaseDomain3).CreateTime = DateTime.Now;
                    (obj as BaseDomain3).CreateId = GlobalVariable.UserInfo.LoginId;
                    (obj as BaseDomain3).CreateClass = GlobalVariable.CurrentInstance;

                    (obj as BaseDomain3).UpdateTime = DateTime.Now;
                    (obj as BaseDomain3).UpdateId = GlobalVariable.UserInfo.LoginId;
                    (obj as BaseDomain3).UpdateClass = GlobalVariable.CurrentInstance;
                }
                dbSet.Add(obj);
            }
            return obj;
        }

        public override TEntity Update(TEntity obj)
        {
            if (obj != null)
            {
                if (obj is BaseDomain3)
                {
                    (obj as BaseDomain3).UpdateTime = DateTime.Now;
                    (obj as BaseDomain3).UpdateId = GlobalVariable.UserInfo.LoginId;
                    (obj as BaseDomain3).UpdateClass = GlobalVariable.CurrentInstance;
                }
                if (db.Entry<TEntity>(obj).State == EntityState.Detached)
                    dbSet.Attach(obj);

                db.Entry<TEntity>(obj).State = EntityState.Modified;
            }
            return obj;
        }

        protected override void ReloadContext()
        {
            
                db = new ProductionContext(connectString);
                dbSet = db.Set<TEntity>();
         
        }
    }
    }
