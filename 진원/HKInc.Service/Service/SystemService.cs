using HKInc.Utils.Common;
using HKInc.Service.Repository;
using HKInc.Ui.Model.Context;

namespace HKInc.Service.Service
{
    public class SystemService<TEntity> : BaseRepository.BaseService<TEntity> where TEntity : class
    {
        public SystemService()
        {
            this.repository = new SystemRepository<TEntity>(ServerInfo.GetConnectString(GlobalVariable.LogInDataBase));
            HKInc.Service.BaseRepository.RepositoryList.AddService(this);
        }

        public override int Save(bool isNotifiable = false)
        {
            int changeCount = repository.Save();

            if (isNotifiable)
            {
                ProductionContext dbContext = (ProductionContext)repository.GetDbContext();
                dbContext.SendSqlNotification(typeof(TEntity).Name);
            }

            return changeCount;
        }

    }
}
