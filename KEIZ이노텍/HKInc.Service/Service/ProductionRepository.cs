using System;
using System.Data.Entity;
using HKInc.Ui.Model.BaseDomain;
using HKInc.Ui.Model.Context;
using HKInc.Utils.Common;

namespace HKInc.Service.Service
{
    class ProductionRepository<TEntity> : HKInc.Service.BaseRepository.BaseRepository<TEntity> where TEntity : class
    {        
        public ProductionRepository(string connectString)
        {
            db = new ProductionContext(connectString);
            dbSet = db.Set<TEntity>();
            this.connectString = connectString;
            db.Database.CommandTimeout = 180;
            //db.Configuration.LazyLoadingEnabled = false;
        }
        
        public override TEntity Insert(TEntity obj, DateTime? dateTime = null)
        {
            if (dateTime == null)
                dateTime = DateTime.Now;

            if (obj != null)
            {
                BaseDomain BaseDomain = obj as BaseDomain;
                if (BaseDomain != null)
                {
                    (obj as BaseDomain).CreateTime = (DateTime)dateTime;
                    (obj as BaseDomain).CreateId = GlobalVariable.LoginId;

                    (obj as BaseDomain).UpdateTime = (DateTime)dateTime;
                    (obj as BaseDomain).UpdateId = GlobalVariable.LoginId;
                }
                else
                {
                    MES_BaseDomain MES_BaseDomain = obj as MES_BaseDomain;
                    if(MES_BaseDomain != null)
                    {
                        (obj as MES_BaseDomain).CreateTime = (DateTime)dateTime;
                        (obj as MES_BaseDomain).CreateId = GlobalVariable.LoginId;

                        (obj as MES_BaseDomain).UpdateTime = (DateTime)dateTime;
                        (obj as MES_BaseDomain).UpdateId = GlobalVariable.LoginId;
                    }
                    else
                    {
                        (obj as MES_BaseDomain2).CreateTime = (DateTime)dateTime;
                        (obj as MES_BaseDomain2).CreateId = GlobalVariable.LoginId;

                        (obj as MES_BaseDomain2).UpdateTime = (DateTime)dateTime;
                        (obj as MES_BaseDomain2).UpdateId = GlobalVariable.LoginId;
                    }
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
                BaseDomain BaseDomain = obj as BaseDomain;
                if (BaseDomain != null)
                {
                    (obj as BaseDomain).UpdateTime = (DateTime)dateTime;
                    (obj as BaseDomain).UpdateId = GlobalVariable.LoginId;
                }
                else
                {
                    MES_BaseDomain MES_BaseDomain = obj as MES_BaseDomain;
                    if (MES_BaseDomain != null)
                    {
                        (obj as MES_BaseDomain).UpdateTime = (DateTime)dateTime;
                        (obj as MES_BaseDomain).UpdateId = GlobalVariable.LoginId;                        
                    }
                    else
                    {
                        (obj as MES_BaseDomain2).UpdateTime = (DateTime)dateTime;
                        (obj as MES_BaseDomain2).UpdateId = GlobalVariable.LoginId;
                    }
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
            db.Database.CommandTimeout = 180;
            //db.Configuration.LazyLoadingEnabled = false;
        }

    }
}
