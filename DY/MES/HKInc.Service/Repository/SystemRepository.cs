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
    public class SystemRepository<TEntity> : BaseRepository.BaseRepository<TEntity> where TEntity : class
    {
        public SystemRepository(string connectString)
        {           
            db = new ProductionContext(connectString);
            dbSet = db.Set<TEntity>();
            this.connectString = connectString;          
        }

        public override TEntity Insert(TEntity obj, DateTime? dateTime = null)
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

        public override TEntity Update(TEntity obj, DateTime? dateTime = null)
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

        protected override void ReloadContext()
        {            
            db = new ProductionContext(connectString);
            dbSet = db.Set<TEntity>();         
        }
    }
}
