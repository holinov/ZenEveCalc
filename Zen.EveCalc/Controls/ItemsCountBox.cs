namespace Zen.EveCalc.Controls
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