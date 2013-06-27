namespace Zen.EveCalc.DataModel.EveDB
{
    public class MaterialDetails
    {


        public BlueprintDto invBlueprintTypeDto { get; set; }

        public MaterialDto[] materialDtos { get; set; }
        /*public object[] manufacturingRequirementDtos { get; set; }
            public object[] timeProductivityRequirementDtos { get; set; }
            public object[] copyingRequirementDtos { get; set; }
            public object[] reverseEngineeringRequirementDtos { get; set; }
            public object[] inventionRequirementDtos { get; set; }
            public object[] materialProductivityRequirementDtos { get; set; }*/
    }
}