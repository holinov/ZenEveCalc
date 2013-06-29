using System.Windows;
using System.Windows.Input;

namespace Zen.EveCalc.Controls.Models
{
    public class GlobalSettingViewModel : DependencyObject
    {
        public GlobalSettingViewModel()
        {
            ExportDB = App.Core.Resolve<ExportDbCommand>();
            ImportDB = App.Core.Resolve<ImportDbCommand>();
        }

        protected ImportDbCommand ImportDB { get; private set; }

        public ICommand ExportDB { get; private set; }

        public bool ShowReportsSummary
        {
            get { return (bool) GetValue(ShowReportsSummaryProperty); }
            set { SetValue(ShowReportsSummaryProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ShowReportsSummary.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowReportsSummaryProperty =
            DependencyProperty.Register("ShowReportsSummary", typeof (bool),
                                        typeof (GlobalSettingViewModel),
                                        new PropertyMetadata(true));


    }
}