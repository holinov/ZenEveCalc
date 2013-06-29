using System.ComponentModel;
using Zen.EveCalc.Core.DataStorage;

namespace Zen.EveCalc.DataModel
{
    public class ProductionInfo:HasGuidId
    {
        private string _productName;
        private float _sellPrice;
        private int _sellAmmount;
        private float _materialsTransport;
        private float _productionTransport;
        private float _estimatedIncome;
        private float _estimatedProfit;
        private int _soldOut;
        private float _realProfit;
        private Blueprint _usedBpData;
        private string _producingIn;
        private string _sellingIn;

        public string ProductName
        {
            get { return _productName; }
            set
            {
                if (value == _productName) return;
                _productName = value;
                OnPropertyChanged();
            }
        }

        public float SellPrice
        {
            get { return _sellPrice; }
            set
            {
                if (value .Equals(_sellPrice)) return;
                _sellPrice = value;
                OnPropertyChanged();
                RefreshRealProfit();
            }
        }

        public int SellAmmount
        {
            get { return _sellAmmount; }
            set
            {
                if (value == _sellAmmount) return;
                _sellAmmount = value;
                OnPropertyChanged();
            }
        }

        public float MaterialsTransport
        {
            get { return _materialsTransport; }
            set
            {
                if (value.Equals(_materialsTransport)) return;
                _materialsTransport = value;
                OnPropertyChanged();
            }
        }

        public float ProductionTransport
        {
            get { return _productionTransport; }
            set
            {
                if (value.Equals(_productionTransport)) return;
                _productionTransport = value;
                OnPropertyChanged();
            }
        }

        public float EstimatedIncome
        {
            get { return _estimatedIncome; }
            set
            {
                if (value.Equals(_estimatedIncome)) return;
                _estimatedIncome = value;
                OnPropertyChanged();
            }
        }

        public float EstimatedProfit
        {
            get { return _estimatedProfit; }
            set
            {
                if (value .Equals(_estimatedProfit)) return;
                _estimatedProfit = value;
                OnPropertyChanged();
            }
        }

        public int SoldOut
        {
            get { return _soldOut; }
            set
            {
                if (value == _soldOut) return;
                _soldOut = value;
                OnPropertyChanged();
                RefreshRealProfit();
            }
        }

        public float RealProfit
        {
            get { return _realProfit; }
            set
            {
                if (value.Equals(_realProfit)) return;
                _realProfit = value;
                OnPropertyChanged();
            }
        }

        public Blueprint UsedBpData
        {
            get { return _usedBpData; }
            set
            {
                if (Equals(value, _usedBpData)) return;
                _usedBpData = value;
                OnPropertyChanged();
            }
        }

        public string ProducingIn
        {
            get { return _producingIn; }
            set
            {
                if (value == _producingIn) return;
                _producingIn = value;
                OnPropertyChanged();
            }
        }

        public string SellingIn
        {
            get { return _sellingIn; }
            set
            {
                if (value == _sellingIn) return;
                _sellingIn = value;
                OnPropertyChanged();
            }
        }

        private void RefreshRealProfit()
        {
            RealProfit = SellPrice*SoldOut;
        }
    }
}