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
                OnPropertyChanged();
                OnPropertyChanged("TotalPrice");
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

        public float Ammount
        {
            get { return _ammount; }
            set
            {
                if (value.Equals(_ammount)) return;
                _ammount = value;
                OnPropertyChanged();
                OnPropertyChanged("TotalPrice");
                OnPropertyChanged("TotalVolume");
            }
        }

        public float TotalPrice
        {
            get { return Price*Ammount; }
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
            }
        }

        public float TotalVolume { get { return Volume*Ammount; } }
        public float Volume
        {
            get { return _volume; }
            set
            {
                if (value.Equals(_volume)) return;
                _volume = value;
                OnPropertyChanged();
                OnPropertyChanged("TotalVolume");
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