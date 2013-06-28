using System.Windows;

namespace Zen.EveCalc.Controls
{
    public class ProductionReportModel : DependencyObject
    {


        public ProductionInfoCollection Rows
        {
            get { return (ProductionInfoCollection)GetValue(RowsProperty); }
            set { SetValue(RowsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Rows.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RowsProperty =
            DependencyProperty.Register("Rows", typeof(ProductionInfoCollection), typeof(ProductionReportModel), new PropertyMetadata(null));


    }
}