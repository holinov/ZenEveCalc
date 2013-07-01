using System.ComponentModel;
using System.Runtime.CompilerServices;
using Raven.Imports.Newtonsoft.Json;
using Zen.EveCalc.Annotations;

namespace Zen.EveCalc.DataModel
{
    public class Material : INotifyPropertyChanged
    {
        private string _name;
        private float _price;
        private string _id;
        private float _ammount;
        private Blueprint _blueprint;
        private float _volume;
        private float _totalCount;
        private int _eveId;

        public Material()
        {
            
        }
        public Material(EveItem it)
        {
            Id = it.Id;
            Name = it.Name;
            Price = it.Price;
            Ammount = 1;
        }

        public string Id
        {
            get { return _id; }
            set
            {
                if (value == _id) return;
                _id = value;
                OnPropertyChanged();
            }
        }

        public float Price
        {
            get { return _price; }
            set
            {
                if (value.Equals(_price)) return;
                _price = value;
                Recount();
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
                Recount();
            }
        }

        public float Ammount
        {
            get { return _ammount; }
            set
            {
                if (value.Equals(_ammount)) return;
                _ammount = value;
                OnPropertyChanged();
                Recount();
            }
        }

        private void Recount()
        {
            OnPropertyChanged("TotalRunPrice");
            OnPropertyChanged("TotalPrice");
            OnPropertyChanged("TotalVolume");
            OnPropertyChanged("TotalCount");
        }

        public float TotalRunPrice
        {
            get { return Price*Ammount; }
        }

        public float TotalPrice
        {
            get { return Price * TotalCount; }
        }

        [JsonIgnore]
        public Blueprint Blueprint
        {
            get { return _blueprint; }
            set
            {
                if (Equals(value, _blueprint)) return;
                _blueprint = value;
                OnPropertyChanged();
                Recount();
            }
        }

        public float TotalVolume { get { return Volume*TotalCount; } }
        public float Volume
        {
            get { return _volume; }
            set
            {
                if (value.Equals(_volume)) return;
                _volume = value;
                OnPropertyChanged();
                Recount();
            }
        }

        public float TotalCount
        {
            get { return _totalCount; }
            set
            {
                if (value.Equals(_totalCount)) return;
                _totalCount = value;
                OnPropertyChanged();
                Recount();
            }
        }

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

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (Blueprint != null) Blueprint.Recount();
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public Material Clone()
        {
            return (Material) MemberwiseClone();
        }
    }
}