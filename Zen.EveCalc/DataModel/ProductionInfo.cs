using Zen.EveCalc.Core.DataStorage;

namespace Zen.EveCalc.DataModel
{
    public class ProductionInfo:HasGuidId
    {
        public string ProductName { get; set; }

        public int SellPrice { get; set; }

        public int SellAmmount { get; set; }

        public float MaterialsTransport { get; set; }

        public float ProductionTransport { get; set; }

        public float EstimatedIncome { get; set; }

        public int EstimatedProfit { get; set; }

        public int SoldOut { get; set; }

        public float RealProfit { get; set; }

        public Blueprint UsedBpData { get; set; }

        public string ProducingIn { get; set; }

        public string SellingIn { get; set; }
    }
}