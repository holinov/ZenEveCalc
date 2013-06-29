using System;
using Zen.EveCalc.Core.DataStorage;

namespace Zen.EveCalc.DataModel
{
    public class ProductionJob : HasGuidId
    {
        private string _name;
        private int _ammount;
        private string _place;
        private Blueprint _usedBlueprintData;
        private DateTime _startDate;
        private DateTime _finishDate;

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

        public int Ammount
        {
            get { return _ammount; }
            set
            {
                if (value == _ammount) return;
                _ammount = value;
                OnPropertyChanged();
            }
        }

        public string Place
        {
            get { return _place; }
            set
            {
                if (value == _place) return;
                _place = value;
                OnPropertyChanged();
            }
        }

        public Blueprint UsedBlueprintData
        {
            get { return _usedBlueprintData; }
            set
            {
                if (Equals(value, _usedBlueprintData)) return;
                _usedBlueprintData = value;
                OnPropertyChanged();
            }
        }

        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                if (value.Equals(_startDate)) return;
                _startDate = value;
                OnPropertyChanged();
            }
        }

        public DateTime FinishDate
        {
            get { return _finishDate; }
            set
            {
                if (value.Equals(_finishDate)) return;
                _finishDate = value;
                OnPropertyChanged();
            }
        }
    }
}