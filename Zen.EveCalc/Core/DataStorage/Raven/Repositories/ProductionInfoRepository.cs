using Raven.Client;
using Zen.EveCalc.DataModel;

namespace Zen.EveCalc.Core.DataStorage.Raven.Repositories
{
    public class ProductionInfoRepository : BasicRavenRepositoryWithGuid<ProductionInfo>, IProductionInfoRepository
    {
        public ProductionInfoRepository(IDocumentSession session) : base(session)
        {
        }
    }
}