using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raven.Abstractions.Data;
using Raven.Client;
using Raven.Database.Json;
using Zen.EveCalc.DataModel;

namespace Zen.EveCalc.Core.DataStorage.Raven.Repositories
{
    public class EveItemRepository : BasicRavenRepositoryWithGuid<EveItem>,IEveItemRepository
    {
        public EveItemRepository(IDocumentSession session) : base(session)
        {
        }

        public string[] GetItemTypes()
        {
            return Session.Advanced.DocumentStore.DatabaseCommands.GetTerms("EveItemIndex", "ItemType", null, 10000).ToArray();
        }

        public EveItem[] GetItemsOfType(string itemType)
        {
            return Query.Where(it => it.ItemType == itemType).ToArray();
        }

        public void UpdatePrice(Guid id, float newPrice)
        {
            var script = string.Format(@"this.Materials.$values.forEach(function(val){{
	if(val.Id===""{0}""){{
		val.Price=""{1}"";
	}}
}});", id,newPrice);
            this.Session.Advanced.DocumentStore.DatabaseCommands.UpdateByIndex("BlueprintsIndex", new IndexQuery(), new ScriptedPatchRequest()
                {
                    Script = script
                });
        }
    }
}
