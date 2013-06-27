using System;
using Zen.EveCalc.DataModel;

namespace Zen.EveCalc.Core.DataStorage.Raven.Repositories
{
    public interface IEveItemRepository : IRepositoryWithGuid<EveItem>
    {
        string[] GetItemTypes();
        EveItem[] GetItemsOfType(string itemType);
        void UpdatePrice(Guid id, float newPrice);
    }
}