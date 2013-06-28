using System;
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
using Zen.EveCalc.Core.DataStorage;
using Zen.EveCalc.Core.DataStorage.Raven.Repositories;
using Zen.EveCalc.DataModel;

namespace Zen.EveCalc.Controls
{
    /// <summary>
    /// Interaction logic for ProductionReport.xaml
    /// </summary>
    public partial class ProductionReport : UserControl,IPageControl
    {
        private Func<IProductionInfoRepository> _productionInfoRepository;
        private readonly ProductionReportModel _model;

        public ProductionReport(Func<IProductionInfoRepository> productionInfoRepository):this()
        {
            _productionInfoRepository = productionInfoRepository;
        }

        public ProductionReport()
        {
            InitializeComponent();
            _model = (ProductionReportModel)FindResource("Model");
        }

        public UIElement PageContent { get { return this; } }
        public UIElement Header { get{return new TextBlock(new Run("Отчет"));} }
        public int SortOrder { get { return 2; } }
        public AppScope PageSope { get; set; }
        public void Init()
        {
            LoadAll();
            Commands=new []
                {
                    new PageCommand()
                        {
                            Name = "Загрузить",
                            Action = p=>LoadAll()
                        }, 
                        new PageCommand()
                        {
                            Name = "Удалить",
                            Action = p =>
                                {
                                    using (var repo=_productionInfoRepository())
                                    {
                                        var cur = (ProductionInfo) ItemsGrid.SelectedItem;
                                        repo.DeleteById(cur.Id);
                                        repo.SaveChanges();
                                        _model.Rows.Remove(cur);
                                    }
                                }
                        }, 
                };
        }

        private void LoadAll()
        {
            using (var repos=_productionInfoRepository())
            {
                _model.Rows = new ProductionInfoCollection(repos.GetAll());
            }
        }

        public PageCommand[] Commands { get; private set; }
        public void Show()
        {
            LoadAll();
        }
    }
}
