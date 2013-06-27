using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
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
using Zen.EveCalc.Core.DataStorage.Raven.Repositories;
using Zen.EveCalc.DataModel;

namespace Zen.EveCalc.Controls
{
    /// <summary>
    /// Interaction logic for EveItemsList.xaml
    /// </summary>
    public partial class EveItemsList : UserControl,IPageControl
    {
        private Func<IEveItemRepository> _eveItemRepos;
        private EveItems _items;
        private List<EveItem> _itemsToDelete;

        public EveItemsList()
        {
            InitializeComponent();
        }
        public EveItemsList(Func<IEveItemRepository> eveItemRepos)
        {
            InitializeComponent();
            _eveItemRepos = eveItemRepos;
        }

        public void Init()
        {
            using (var r = EveItemRepos())
            {
                _items = new EveItems(r.GetAll().ToList());
            }
            ItemsGrid.ItemsSource = _items;
            _itemsToDelete = new List<EveItem>();
            Commands = new List<PageCommand>()
                {
                    new PageCommand
                        {
                            Name = "Сохранить",
                            Action = p =>
                                {
                                    using (var r = _eveItemRepos())
                                    {
                                        using (var transaction = new TransactionScope())
                                        {
                                            var toStore =
                                                _items.Where(it => _itemsToDelete.All(toRem => it.Guid != toRem.Guid))
                                                      .ToArray();
                                            foreach (var eveItem in toStore.Where(i => i.NeedsPriceUpdate))
                                            {
                                                r.UpdatePrice(eveItem.Guid, eveItem.Price);
                                                eveItem.NeedsPriceUpdate = false;
                                            }
                                            
                                            r.StoreBulk(toStore);
                                            r.SaveChanges();

                                            foreach (var toDelete in _itemsToDelete)
                                            {
                                                r.Delete(r.Find(toDelete.Guid));
                                            }
                                            r.SaveChanges();
                                            _itemsToDelete = new List<EveItem>();

                                            transaction.Complete();
                                        }
                                    }
                                }
                        },
                    new PageCommand
                        {
                            Name = "Загрузить",
                            Action = p =>
                                {
                                    using (var r = EveItemRepos())
                                    {
                                        _items = new EveItems(r.GetAll().ToList());
                                    }
                                    ItemsGrid.ItemsSource = _items;
                                    _itemsToDelete = new List<EveItem>();
                                }
                        },
                    new PageCommand
                        {
                            Name = "Новый",
                            Action = p =>
                                {
                                    var newItem = new EveItem()
                                        {
                                            Name = "новый пердмет"
                                        };
                                    _items.Add(newItem);
                                    ItemsGrid.SelectedItem = newItem;
                                    ItemsGrid.ScrollIntoView(newItem);
                                }
                        },
                    new PageCommand
                        {
                            Name = "Удалить",
                            Action = p =>
                                {

                                    var item = (EveItem) ItemsGrid.SelectedItem;
                                    _items.Remove(item);
                                    _itemsToDelete.Add(item);
                                }
                        }
                }.ToArray();
        }

        /*private class SaveCommand : IPageCommand
        {
            private Func<IEveItemRepository> _eveItemRepos;
            private IEnumerable<EveItem> _items;

            public SaveCommand(Func<IEveItemRepository> eveItemRepos, IEnumerable<EveItem> items)
            {
                _items = items;
                _eveItemRepos = eveItemRepos;
            }

            public void Run()
            {
                using (var r = _eveItemRepos())
                {
                    r.StoreBulk(_items);
                    r.SaveChanges();
                }
            }

            public string Name
            {
                get { return "Сохранить"; }
            }
        }*/

        public PageCommand[] Commands { get; private set; }
        public UIElement PageContent { get { return this; } }
        public UIElement Header { get{return new TextBlock(new Run("EVE DB"));} }
        public int SortOrder { get { return 0; } }
        public AppScope PageSope { get; set; }

        public Func<IEveItemRepository> EveItemRepos
        {
            get { return _eveItemRepos; }
            set { _eveItemRepos = value; }
        }
    }

    public class EveItems:ObservableCollection<EveItem>
    {
        public EveItems(){}

        public EveItems(IEnumerable<EveItem> collection):base(collection){}

        public EveItems(List<EveItem> list):base(list){}
    }
}
