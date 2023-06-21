using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace HKInc.Utils.Interface.Service
{
    public interface IService<TEntity> : IDisposable where TEntity : class
    {
        List<TEntity> GetList();

        List<TEntity> GetList(Expression<Func<TEntity, bool>> condition);

        List<TEntity> GetListDetached(Expression<Func<TEntity, bool>> condition);

        TEntity Insert(TEntity obj);

        TEntity Update(TEntity obj);

        void Delete(TEntity obj);

        int Save(bool isNotifiable = false);

        void ReLoad();

        List<Child> GetChildList<Child>(Expression<Func<Child, bool>> condition) where Child : class;

        bool GetChildFindFlag<Child>(object Key) where Child : class;

        List<Child> GetChildListDetached<Child>(Expression<Func<Child, bool>> condition) where Child : class;

        Child InsertChild<Child>(Child obj) where Child : class;

        Child UpdateChild<Child>(Child obj) where Child : class;

        void RemoveChild<Child>(Child obj) where Child : class;

        Child DetachChild<Child>(Child obj) where Child : class;

        TEntity Detached(TEntity obj);
    }
}
