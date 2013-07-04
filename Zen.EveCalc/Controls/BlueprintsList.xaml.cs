using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Zen.EveCalc.Controls.Models;
using Zen.EveCalc.Core;
using Zen.EveCalc.Core.DataStorage;
using Zen.EveCalc.Core.DataStorage.Raven.Repositories;
using Zen.EveCalc.DataModel;

namespace Zen.EveCalc.Controls
{
    /// <summary>
    /// Interaction logic for BlueprintsList.xaml
    /// </summary>
    public partial class BlueprintsList : UserControl, IPageControl
    {
        private readonly Func<IRepositoryWithGuid<Blueprint>> _blueprintsRepos;
        private Func<IEveItemRepository> _eveItemRepos;
        private Func<IProductionInfoRepository> _prudoctionInfoRepos;

        private Blueprints _blueprints;
        private List<Blueprint> _toRemove=new List<Blueprint>(); 

        public BlueprintsList(Func<IRepositoryWithGuid<Blueprint>> blueprintsRepos,
                              Func<IEveItemRepository> eveItemRepos, Func<IProductionInfoRepository> prudoctionInfoRepos)
        {
            _blueprintsRepos = blueprintsRepos;
            _eveItemRepos = eveItemRepos;
            _prudoctionInfoRepos = prudoctionInfoRepos;
            InitializeComponent();
        }

        public UIElement PageContent
        {
            get { return this; }
        }

        public UIElement Header
        {
            get { return new TextBlock(new Run("Чертежи")); }
        }

        public int SortOrder
        {
            get { return 1; }
        }

        public AppScope PageSope { get; set; }

        public void Init()
        {
           var newButtonContent= new StackPanel()
                {
                    Orientation = Orientation.Horizontal,
                };
            var bpTextBox = new TextBox()
                {
                    MinWidth = 100,
                    MaxWidth = 300
                };
            var button = new Button()
                {
                    Content = "Новый чертеж",
                };
            button.Click += (s, e) =>
                {
                    var bp = new Blueprint()
                        {
                            Name = string.IsNullOrEmpty(bpTextBox.Text) ? "Новый чертеж" : bpTextBox.Text.Trim(),
                            Runs = 1
                        };
                    _blueprints.Add(bp);
                    BPList.SelectedItem = bp;
                    BPList.ScrollIntoView(bp);
                    bpDetails.DownloadMaterialsClick(null, null);
                };
            newButtonContent.Children.Add(bpTextBox);
            newButtonContent.Children.Add(button);

           Commands = new List<PageCommand>()
                {
                    new PageCommand
                        {
                            Name = "Сохранить",
                            Action = p =>
                                {
                                    using (var r = _blueprintsRepos())
                                    {
                                        using (var transaction = new TransactionScope())
                                        {
                                            var toStore =
                                                _blueprints.Where(bp => _toRemove.All(tr => tr.Guid != bp.Guid));

                                            r.StoreBulk(toStore);
                                            r.SaveChanges();

                                            foreach (var toRem in _toRemove)
                                            {
                                                r.DeleteById(toRem.Id);
                                            }
                                            r.SaveChanges();
                                            transaction.Complete();
                                        }
                                    }
                                }
                        },
                    new PageCommand
                        {
                            Name = "Загрузить",
                            Action = p => Load()
                        },
                    new PageCommand
                        {
                            Name = "Новый",
                            Content=newButtonContent,
                        },
                    new PageCommand
                        {
                            Name = "Удалить",
                            Action = p =>
                                {
                                }
                        },
                    new PageCommand
                        {
                            Name = "Произвести",
                            Action = p =>
                                {
                                    var curBp = bpDetails.Blueprint;
                                    using (var repos=_prudoctionInfoRepos())
                                    {
                                        ProductionInfo prodIndo = curBp.MakeProduct();
                                        prodIndo.ProducingIn = "Kino";
                                        prodIndo.SellingIn = "Jita";                                     
                                        repos.Store(prodIndo);
                                        repos.SaveChanges();
                                    }
                                }
                        }
                }.ToArray();

            Load();
            //throw new NotImplementedException();
        }

        private void Load()
        {
            using (var r = _blueprintsRepos())
            {
                _blueprints = new Blueprints(r.GetAll().OrderBy(b=>b.Name).ToList());
                BPList.ItemsSource = _blueprints;
                _toRemove.Clear();
            }
        }

        public PageCommand[] Commands { get; private set; }
        public void Show()
        {
            Load();
        }
    }
}
