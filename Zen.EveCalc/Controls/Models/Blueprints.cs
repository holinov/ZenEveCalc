using System.Collections.Generic;
using System.Collections.ObjectModel;
using Zen.EveCalc.DataModel;

namespace Zen.EveCalc.Controls.Models
{
    public class Blueprints : ObservableCollection<Blueprint>
    {
        public Blueprints()
        {
        }

        public Blueprints(IEnumerable<Blueprint> collection) : base(collection)
        {
        }

        public Blueprints(List<Blueprint> list) : base(list)
        {
        }
    }
}