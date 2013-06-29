namespace Zen.EveCalc.CoreControls
{
    public class ItemsCountBox : SplittingBox
    {
        public ItemsCountBox()
        {
            Suffix = " шт";
            DigitFormat = "{0:0,0}";
        }
    }
}