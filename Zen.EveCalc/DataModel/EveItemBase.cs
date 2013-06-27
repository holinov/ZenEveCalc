using System.ComponentModel;
using System.Runtime.CompilerServices;
using Raven.Imports.Newtonsoft.Json;
using Zen.EveCalc.Annotations;
using Zen.EveCalc.Core.DataStorage;
using Zen.EveCalc.DataModel.EveDB;

namespace Zen.EveCalc.DataModel
{
    public abstract class EveItemBase : HasGuidId, INotifyPropertyChanged 
    {
        private float _price;
        private string _name;
        private int _eveId;
        private bool _needsPriceUpdate;
        private float _volume;
        private string _itemType;
        private MaterialDto _materialDto;

        public int EveId
        {
            get { return _eveId; }
            set
            {
                if (value == _eveId) return;
                _eveId = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                if (value == _name) return;
                _name = value;
                OnPropertyChanged();
            }
        }

        public virtual float Price
        {
            get { return _price; }
            set { _price = value;
                NeedsPriceUpdate = true;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        public bool NeedsPriceUpdate
        {
            get { return _needsPriceUpdate; }
            set
            {
                if (value.Equals(_needsPriceUpdate)) return;
                _needsPriceUpdate = value;
                OnPropertyChanged();
            }
        }

        public float Volume
        {
            get { return _volume; }
            set
            {
                if (value.Equals(_volume)) return;
                _volume = value;
                OnPropertyChanged();
            }
        }

        public string ItemType
        {
            get { return _itemType; }
            set
            {
                if (value == _itemType) return;
                _itemType = value;
                OnPropertyChanged();
            }
        }

        public MaterialDto MaterialDto
        {
            get { return _materialDto; }
            set
            {
                if (Equals(value, _materialDto)) return;
                _materialDto = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {      
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public override string ToString()
        {
            return Name;
        } 
    }
}