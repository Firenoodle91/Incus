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
        }
        
        public override TEntity Insert(TEntity obj)
        {
            if (obj != null)
            {
                BaseDomain BaseDomain = obj as BaseDomain;
                if (BaseDomain != null)
                {
                    (obj as BaseDomain).CreateTime = DateTime.Now;
                    (obj as BaseDomain).CreateId = GlobalVariable.UserInfo.LoginId;
                    //(obj as BaseDomain).CreateClass = GlobalVariable.CurrentInstance;

                    (obj as BaseDomain).UpdateTime = DateTime.Now;
                    (obj as BaseDomain).UpdateId = GlobalVariable.UserInfo.LoginId;
                    //(obj as BaseDomain).UpdateClass = GlobalVariable.CurrentInstance;
                }
                else
                {
                    MES_BaseDomain MES_BaseDomain = obj as MES_BaseDomain;
                    if(MES_BaseDomain != null)
                    {
                        (obj as MES_BaseDomain).CreateTime = DateTime.Now;
                        (obj as MES_BaseDomain).CreateId = GlobalVariable.UserInfo.LoginId;

                        (obj as MES_BaseDomain).UpdateTime = DateTime.Now;
                        (obj as MES_BaseDomain).UpdateId = GlobalVariable.UserInfo.LoginId;

                        //(obj as MES_BaseDomain).FactCode = Factory.MasterCodeSTR.FactCode_First;
                    }
                    else
                    {
                        (obj as MES_BaseDomain2).CreateTime = DateTime.Now;
                        (obj as MES_BaseDomain2).CreateId = GlobalVariable.UserInfo.LoginId;

                        (obj as MES_BaseDomain2).UpdateTime = DateTime.Now;
                        (obj as MES_BaseDomain2).UpdateId = GlobalVariable.UserInfo.LoginId;

                        //(obj as MES_BaseDomain2).FactCode = Factory.MasterCodeSTR.FactCode_First;
                    }
                }
                dbSet.Add(obj);
            }
            return obj;
        }

        public override TEntity Update(TEntity obj)
        {
            if (obj != null)
            {
                BaseDomain BaseDomain = obj as BaseDomain;
                if (BaseDomain != null)
                {
                    (obj as BaseDomain).UpdateTime = DateTime.Now;
                    (obj as BaseDomain).UpdateId = GlobalVariable.UserInfo.LoginId;
                    //(obj as BaseDomain).UpdateClass = GlobalVariable.CurrentInstance;
                }
                else
                {
                    MES_BaseDomain MES_BaseDomain = obj as MES_BaseDomain;
                    if (MES_BaseDomain != null)
                    {
                        (obj as MES_BaseDomain).UpdateTime = DateTime.Now;
                        (obj as MES_BaseDomain).UpdateId = GlobalVariable.UserInfo.LoginId;                        
                    }
                    else
                    {
                        (obj as MES_BaseDomain2).UpdateTime = DateTime.Now;
                        (obj as MES_BaseDomain2).UpdateId = GlobalVariable.UserInfo.LoginId;
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
        }

    }
}
