using Zen.EveCalc.DataModel;

namespace Zen.EveCalc.Controls.Models
{
    public class ExportData
    {
        public EveItem[] EveItems { get; set; }
        public Blueprint[] Blueprints { get; set; }
        public ProductionInfo[] ProductionInfos { get; set; }
    }
}