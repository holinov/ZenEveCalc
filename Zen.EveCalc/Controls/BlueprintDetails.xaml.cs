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
using Raven.Client.Linq;
using Zen.EveCalc.Controls.Models;
using Zen.EveCalc.Core.DataStorage;
using Zen.EveCalc.Core.DataStorage.Raven.Repositories;
using Zen.EveCalc.DataModel;
using Zen.EveCalc.DataModel.EveDB;

namespace Zen.EveCalc.Controls
{
    /// <summary>
    /// Interaction logic for BlueprintDetails.xaml
    /// </summary>
    public partial class BlueprintDetails : UserControl
    {
        public BlueprintDetails()
        {
            InitializeComponent();
            using (var repos=App.Core.Resolve<IRepositoryWithGuid<EveItem>>())
            {
                var mats = repos.GetAll().Select(it => new Material(it));
               Materials = new Materials(mats);
            }
//matSelect.ItemsSource = Materials;
        }


        public Blueprint Blueprint
        {
            get { return (Blueprint)GetValue(BlueprintProperty); }
            set
            {
                SetValue(BlueprintProperty, value);
            }
        }

        // Using a DependencyProperty as the backing store for Blueprint.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BlueprintProperty =
            DependencyProperty.Register("Blueprint", typeof(Blueprint), typeof(BlueprintDetails), new PropertyMetadata((
                o, args) =>
                {
                    using (var repos = App.Core.Resolve<IRepositoryWithGuid<EveItem>>())
                    {
                        var bp = ((Blueprint) args.NewValue);
                        if (bp != null)
                        {
                            bp.Materials.Blueprint = bp;
                            bp.UpdatePrices(repos);
                        }
                    }
                }));



        public Materials Materials
        {
            get { return (Materials)GetValue(MaterialsProperty); }
            set { SetValue(MaterialsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Materials.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaterialsProperty =
            DependencyProperty.Register("Materials", typeof(Materials), typeof(BlueprintDetails), new PropertyMetadata(null));



        private void AddClick(object sender, RoutedEventArgs e)
        {
            var mat = (Material)matSelect.SelectedItem;
            if (mat != null) Blueprint.Materials.Add(mat.Clone());
        }

        private void DellClick(object sender, RoutedEventArgs e)
        {
            var mat = (Material) ((Button) e.OriginalSource).Tag;
            Blueprint.Materials.Remove(mat);
        }

        public void DownloadMaterialsClick(object sender, RoutedEventArgs e)
        {
            var mats=App.LoadMaterials(Blueprint);
            if (mats != null)
            {
                var bpdto = mats.invBlueprintTypeDto;
                Blueprint.EveId = bpdto.blueprintTypeID;
                var prices = App.Core.Resolve<EveCentalApi>().GetItemPriceById(new[] { bpdto.productTypeID },true);
                Blueprint.Produces = bpdto.productPortionSize;
                Blueprint.BlueprintDto = bpdto;
                Blueprint.SellPrice = prices[bpdto.productTypeID];
                Blueprint.Materials.Clear();

                using (var repos = App.Core.Resolve<IEveItemRepository>())
                {
                    foreach (var materialDto in mats.materialDtos)
                    {
                        //var all = repos.GetAll();
                        var eveIt = repos.QuerySync.First(it => it.EveId == materialDto.materialTypeID);
                        Blueprint.Materials.Add(new Material(eveIt)
                            {
                                Ammount = materialDto.quantity
                            });
                    }

                    Blueprint.UpdatePrices(repos);
                }

            }
        }
    }
}
