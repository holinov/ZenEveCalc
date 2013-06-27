namespace Zen.EveCalc.DataModel.EveDB
{
    public class BlueprintDto
    {
        public int blueprintTypeID { get; set; }
        public string blueprintTypeName { get; set; }
        public int productTypeID { get; set; }
        public string productTypeName { get; set; }
        public int productCategoryID { get; set; }
        public string productIcon { get; set; }
        public int techLevel { get; set; }
        public int productionTime { get; set; }
        public int researchProductivityTime { get; set; }
        public int researchMaterialTime { get; set; }
        public int researchCopyTime { get; set; }
        public int researchTechTime { get; set; }
        public int productivityModifier { get; set; }
        public float wasteFactor { get; set; }
        public int maxProductionLimit { get; set; }
        public float productVolume { get; set; }
        public int productPortionSize { get; set; }
        public string dumpVersion { get; set; }
    }
}