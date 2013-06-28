using System.Collections.Generic;
using System.Linq;
using Zen.EveCalc.Controls;
using Zen.EveCalc.Core.DataStorage;
using Zen.EveCalc.DataModel.EveDB;

namespace Zen.EveCalc.DataModel
{
    public class Blueprint : EveItemBase
    {
        private Materials _materials;
        private int _produces = 1;
        private int _sellPrice;
        private int _runs;
        private BlueprintDto _blueprintDto;

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

        public int SellPrice
        {
            get { return _sellPrice; }
            set
            {
                if (value == _sellPrice) return;
                _sellPrice = value;
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
            get { return Materials.Sum(m => m.TotalPrice)/Produces; }
        }

        public float TotalPrice
        {
            get { return Materials.Sum(m => m.TotalPrice)*Runs; }
        }

        public float Income
        {
            get { return SellPrice*Runs*Produces - TotalPrice; }
        }

        public float IncomePercent
        {
            get { return (SellPrice / ItemProductionPrice - 1) * 100; }
        }

        public float TotaMaterialsVolume
        {
            get { return Materials.Sum(m => m.TotalVolume)*Runs; }
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

        public void UpdatePrices(IRepositoryWithGuid<EveItem> repository)
        {
            var items = repository.Find(Materials.Select(m => m.Id)).ToDictionary(i=>i.Id,i=>i);
            foreach (var material in Materials)
            {
                material.Price = items[material.Id].Price;
                material.Volume = items[material.Id].Volume;
            }
            Recount();
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