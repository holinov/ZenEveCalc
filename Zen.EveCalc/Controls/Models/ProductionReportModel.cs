using System.Linq;
using System.Windows;
using System.Windows.Input;
using Zen.EveCalc.DataModel;

namespace Zen.EveCalc.Controls.Models
{
    public class ProductionReportModel : DependencyObject
    {
        public ProductionReportModel()
        {
            _cmd=App.Core.Resolve<SaveListCommand<ProductionInfo>>();
            _cmd.SetRows(Rows);
            SaveReports = _cmd;

        }

        public ProductionInfoCollection Rows
        {
            get { return (ProductionInfoCollection)GetValue(RowsProperty); }
            set { SetValue(RowsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Rows.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RowsProperty =
            DependencyProperty.Register("Rows", typeof(ProductionInfoCollection), typeof(ProductionReportModel), new PropertyMetadata(null,
                (s, ar) =>
                    {
                        var newRows = ar.NewValue as ProductionInfoCollection;
                        var model = (ProductionReportModel) s;
                        if (newRows != null)
                        {
                            ((ProductionReportModel) s)._cmd.SetRows(newRows);

                            model.TotalIncome = newRows.Sum(r => r.EstimatedIncome);
                            model.TotalProfit = newRows.Sum(r => r.EstimatedProfit);
                            model.TotalRealProfit = newRows.Sum(r => r.RealProfit);
                        }
                    }));

        private SaveListCommand<ProductionInfo> _cmd;

        public ICommand SaveReports { get; set; }



        public float TotalIncome
        {
            get { return (float)GetValue(TotalIncomeProperty); }
            set { SetValue(TotalIncomeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TotalIncome.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TotalIncomeProperty =
            DependencyProperty.Register("TotalIncome", typeof(float), typeof(ProductionReportModel), new PropertyMetadata(0f));




        public float TotalProfit
        {
            get { return (float)GetValue(TotalProfitProperty); }
            set { SetValue(TotalProfitProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TotalProfit.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TotalProfitProperty =
            DependencyProperty.Register("TotalProfit", typeof(float), typeof(ProductionReportModel), new PropertyMetadata(0f));





        public float TotalRealProfit
        {
            get { return (float)GetValue(TotalRealProfitProperty); }
            set { SetValue(TotalRealProfitProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TotalRealProfit.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TotalRealProfitProperty =
            DependencyProperty.Register("TotalRealProfit", typeof(float), typeof(ProductionReportModel), new PropertyMetadata(0f));



    }
}