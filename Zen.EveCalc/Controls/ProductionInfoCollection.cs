using System.Collections.Generic;
using System.Collections.ObjectModel;
using Zen.EveCalc.DataModel;

namespace Zen.EveCalc.Controls
{
    public class ProductionInfoCollection:ObservableCollection<ProductionInfo>
    {
        public ProductionInfoCollection()
        {
            
        }

        public ProductionInfoCollection(IEnumerable<ProductionInfo> col):base(col)
        {
            
        }
    }
}