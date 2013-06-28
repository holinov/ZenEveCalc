using System.Collections.Generic;
using System.Collections.ObjectModel;
using Zen.EveCalc.DataModel;

namespace Zen.EveCalc.Controls
{
    public class EveItems:ObservableCollection<EveItem>
    {
        public EveItems(){}

        public EveItems(IEnumerable<EveItem> collection):base(collection){}

        public EveItems(List<EveItem> list):base(list){}
    }
}