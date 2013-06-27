using System;
using Raven.Client;
using Zen.EveCalc.DataModel;

namespace Zen.EveCalc.Core.DataStorage.Raven.Repositories
{
    public interface IProductionInfoRepository : IRepositoryWithGuid<ProductionInfo>
    {

    }

    public class ProductionInfoRepository : BasicRavenRepositoryWithGuid<ProductionInfo>, IProductionInfoRepository
    {
        public ProductionInfoRepository(IDocumentSession session) : base(session)
        {
        }
    }
}