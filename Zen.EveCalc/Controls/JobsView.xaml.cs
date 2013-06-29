using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Zen.EveCalc.Core;

namespace Zen.EveCalc.Controls
{
    /// <summary>
    /// Interaction logic for JobsView.xaml
    /// </summary>
    public partial class JobsView : UserControl,IPageControl
    {
        
        public JobsView()
        {
            InitializeComponent();
        }

        public UIElement PageContent { get { return this; } }
        public UIElement Header { get{return new TextBlock(new Run("Работы"));} }
        public int SortOrder { get { return 4; } }
        public AppScope PageSope { get; set; }
        public void Init()
        {
            //
        }

        public PageCommand[] Commands { get; private set; }
        public void Show()
        {
            //
        }
    }
}
