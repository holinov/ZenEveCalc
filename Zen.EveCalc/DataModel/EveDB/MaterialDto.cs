namespace Zen.EveCalc.DataModel.EveDB
{
    public class MaterialDto
    {
        public int materialTypeID { get; set; }
        public int materialTypeCategoryID { get; set; }
        public int materialTypeGroupID { get; set; }
        public string materialTypeIcon { get; set; }
        public string materialTypeName { get; set; }
        public float materialVolume { get; set; }
        public int quantity { get; set; }
    }
}