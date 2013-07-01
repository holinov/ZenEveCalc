using System.Collections.Generic;
using System.Linq;
using Zen.EveCalc.Controls;
using Zen.EveCalc.Controls.Models;
using Zen.EveCalc.Core.DataStorage;
using Zen.EveCalc.DataModel.EveDB;

namespace Zen.EveCalc.DataModel
{
    public class Blueprint : EveItemBase
    {
        private Materials _materials;
        private int _produces = 1;
        private float _sellPrice;
        private int _runs;
        private BlueprintDto _blueprintDto;
        private float _sellPrice1;
        private bool _favorite;
        private bool _readyToCopy;

        public Blueprint()
        {
            _materials = new Materials(new List<Material>(),this);
        }

        public int Produces
        {
            get { return _produces; }
            set
            {
                if (value == _produces) return;
                _produces = value;
                OnPropertyChanged();
                Recount();
            }
        }

        public int Runs
        {
            get { return _runs; }
            set
            {
                if (value == _runs) return;
                _runs = value;
                OnPropertyChanged();
                Recount();
                foreach (var material in Materials)
                {
                    UpdateTotalAmmount(material);
                }
            }
        }

        public Materials Materials
        {
            get { return _materials; }
            set
            {
                if (Equals(value, _materials)) return;
                _materials = value;
                OnPropertyChanged();
                Recount();
            }
        }

        public float SellPrice
        {
            get { return _sellPrice1; }
            set
            {
                if (value.Equals(_sellPrice1)) return;
                _sellPrice1 = value;
                OnPropertyChanged();
                Recount();
            }
        }

        public void Recount()
        {
            OnPropertyChanged("ItemProductionPrice");
            OnPropertyChanged("TotalPrice");
            OnPropertyChanged("TotaMaterialsVolume");
            OnPropertyChanged("TotaVolume");
            OnPropertyChanged("Income");
            OnPropertyChanged("IncomePercent");
        }

        public float ItemProductionPrice
        {
            get { return Materials.Sum(m => m.TotalRunPrice) / Produces; }
        }

        public float TotalPrice
        {
            get { return Materials.Sum(m => m.TotalRunPrice) * Runs; }
        }

        public float Income
        {
            get { return SellPrice * Runs * Produces - TotalPrice; }
        }

        public float IncomePercent
        {
            get { return (SellPrice / ItemProductionPrice - 1) * 100; }
        }

        public float TotaMaterialsVolume
        {
            get { return Materials.Sum(m => m.TotalVolume); }
        }

        public float TotaVolume
        {
            get
            {
                float res = 0;
                if (BlueprintDto != null)
                {
                    res = BlueprintDto.productVolume*Runs*Produces;
                }
                return res;
            }
        }
        public BlueprintDto BlueprintDto
        {
            get { return _blueprintDto; }
            set
            {
                if (Equals(value, _blueprintDto)) return;
                _blueprintDto = value;
                OnPropertyChanged();
            }
        }

        public int TotalAmmount
        {
            get { return Runs*Produces; }
        }

        public bool Favorite
        {
            get { return _favorite; }
            set
            {
                if (value.Equals(_favorite)) return;
                _favorite = value;
                OnPropertyChanged();
            }
        }

        public bool ReadyToCopy
        {
            get { return _readyToCopy; }
            set
            {
                if (value.Equals(_readyToCopy)) return;
                _readyToCopy = value;
                OnPropertyChanged();
            }
        }

        public void UpdatePrices(IRepositoryWithGuid<EveItem> repository)
        {
            var items = repository.Find(Materials.Select(m => m.Id)).ToDictionary(i=>i.Id,i=>i);
            foreach (var material in Materials)
            {
                material.Price = items[material.Id].Price;
                material.Volume = items[material.Id].Volume;
                material.EveId = items[material.Id].EveId;
                UpdateTotalAmmount(material);
            }
            Recount();
        }

        private void UpdateTotalAmmount(Material material)
        {
            material.TotalCount = material.Ammount*this.Runs;
        }

        public ProductionInfo MakeProduct()
        {
            return new ProductionInfo()
                {
                    ProductName=Name.Replace(" Blueprint",""),
                    SellPrice = SellPrice,
                    SellAmmount = Runs*Produces,
                    MaterialsTransport = TotaMaterialsVolume,
                    ProductionTransport = TotaVolume,
                    EstimatedIncome = Income,
                    EstimatedProfit = SellPrice*Runs*Produces,
                    SoldOut=0,
                    RealProfit=0.0F,
                    UsedBpData=this
                };
        }
    }
}