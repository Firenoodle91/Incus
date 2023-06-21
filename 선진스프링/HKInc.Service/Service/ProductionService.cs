using HKInc.Utils.Common;

namespace HKInc.Service.Service
{
    class ProductionService<TEntity> : HKInc.Service.BaseRepository.BaseService<TEntity> where TEntity : class
    {        
        public ProductionService()
        {
            repository = new ProductionRepository<TEntity>(ServerInfo.GetConnectString(GlobalVariable.ProductionDataBase));
            HKInc.Service.BaseRepository.RepositoryList.AddService(this);
        }        
    }
 
}

