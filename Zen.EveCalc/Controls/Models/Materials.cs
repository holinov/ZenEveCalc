using System.Collections.Generic;
using System.Collections.ObjectModel;
using Raven.Imports.Newtonsoft.Json;
using Zen.EveCalc.DataModel;

namespace Zen.EveCalc.Controls.Models
{
    public class Materials: ObservableCollection<Material>
    {
        private Blueprint _blueprint;

        public Materials()
        {
            
        }

        public Materials(IEnumerable<Material> materials):base(materials)
        {
            
        }
        public Materials(IEnumerable<Material> materials, Blueprint blueprint=null) : base(materials)
        {
            Blueprint = blueprint;
        }

        [JsonIgnore]
        public Blueprint Blueprint
        {
            get { return _blueprint; }
            set
            {
                _blueprint = value;
                foreach (var material in Items)
                {
                    material.Blueprint = Blueprint;
                }
            }
        }

        protected override void OnCollectionChanged(System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            base.OnCollectionChanged(e);
            if (Blueprint != null) Blueprint.Recount();
        }
        protected override void InsertItem(int index, Material item)
        {
            base.InsertItem(index, item);
            item.Blueprint = Blueprint;
            item.PropertyChanged += item_PropertyChanged;
        }

        void item_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (Blueprint != null) Blueprint.Recount();            
        }

        public void Add(Material[] mlist)
        {
            foreach (var material in mlist)
            {
                Add(material);
            }
        }
    }
}