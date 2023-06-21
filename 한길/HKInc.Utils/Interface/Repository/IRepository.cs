using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Utils.Interface.Repository
{

    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        IEnumerable<TEntity> GetAll();

        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> condition);

        List<TEntity> FindDetached(Expression<Func<TEntity, bool>> condition);

        TEntity Insert(TEntity obj);

        TEntity Update(TEntity obj);

        void Delete(TEntity obj);

        int Save();

        void Reload();

        TEntity Detached(TEntity obj);

        DbContext GetDbContext();
    }
}
