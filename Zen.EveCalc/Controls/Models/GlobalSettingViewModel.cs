using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using Newtonsoft.Json;
using Zen.EveCalc.Core.DataStorage;
using Zen.EveCalc.DataModel;

namespace Zen.EveCalc.Controls.Models
{
    public class GlobalSettingViewModel : DependencyObject
    {
        public GlobalSettingViewModel()
        {
            ExportDB = App.Core.Resolve<ExportDBCommand>();
            ImportDB = App.Core.Resolve<ImportDBCommand>();
        }

        protected ImportDBCommand ImportDB { get; private set; }

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
                                        new PropertyMetadata(false));


    }

    public class ExportData
    {
        public EveItem[] EveItems { get; set; }
        public Blueprint[] Blueprints { get; set; }
        public ProductionInfo[] ProductionInfos { get; set; }
    }

    public class ImportDBCommand : ICommand
    {
        private readonly Func<IRepositoryWithGuid<EveItem>> _itemsRepos;
        private readonly Func<IRepositoryWithGuid<Blueprint>> _blueprintsRepos;
        private readonly Func<IRepositoryWithGuid<ProductionInfo>> _prodinfoRepos;

        public ImportDBCommand(Func<IRepositoryWithGuid<EveItem>> itemsRepos,
                               Func<IRepositoryWithGuid<Blueprint>> blueprintsRepos,
                               Func<IRepositoryWithGuid<ProductionInfo>> prodinfoRepos)
        {
            _itemsRepos = itemsRepos;
            _blueprintsRepos = blueprintsRepos;
            _prodinfoRepos = prodinfoRepos;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {/*
            var exportObject = new ExportData();
            using (var irep = _itemsRepos())
            {
                exportObject.EveItems = irep.GetAll().ToArray();
            }
            using (var brep = _blueprintsRepos())
            {
                exportObject.Blueprints = brep.GetAll().ToArray();
            }
            using (var prep = _prodinfoRepos())
            {
                exportObject.ProductionInfos = prep.GetAll().ToArray();
            }
            var res = JsonConvert.SerializeObject(exportObject, Formatting.Indented);*/
            var dlg = new OpenFileDialog()
            {
                Title = "Экспорт БД",
                Filter = "Data File *.dat|*.dat"
            };
            ExportData data;
            if (dlg.ShowDialog() == true)
            {
                using (var rdr = File.OpenText(dlg.FileName))
                {
                    data = JsonConvert.DeserializeObject<ExportData>(rdr.ReadToEnd());
                    using (var irep = _itemsRepos())
                    {
                        irep.StoreBulk(data.EveItems);
                        irep.SaveChanges();
                    }
                    using (var brep = _blueprintsRepos())
                    {
                        brep.StoreBulk(data.Blueprints);
                        brep.SaveChanges();
                    }
                    using (var prep = _prodinfoRepos())
                    {
                        prep.StoreBulk(data.ProductionInfos);
                        prep.SaveChanges();
                    }
                }
            }
            
        }

        public event EventHandler CanExecuteChanged;
    }

    public class ExportDBCommand : ICommand
    {
        private readonly Func<IRepositoryWithGuid<EveItem>> _itemsRepos;
        private readonly Func<IRepositoryWithGuid<Blueprint>> _blueprintsRepos;
        private readonly Func<IRepositoryWithGuid<ProductionInfo>> _prodinfoRepos;

        public ExportDBCommand(Func<IRepositoryWithGuid<EveItem>> itemsRepos,
                               Func<IRepositoryWithGuid<Blueprint>> blueprintsRepos,
                               Func<IRepositoryWithGuid<ProductionInfo>> prodinfoRepos)
        {
            _itemsRepos = itemsRepos;
            _blueprintsRepos = blueprintsRepos;
            _prodinfoRepos = prodinfoRepos;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var exportObject = new ExportData();
            using (var irep=_itemsRepos())
            {
                exportObject.EveItems = irep.GetAll().ToArray();
            }
            using (var brep=_blueprintsRepos())
            {
                exportObject.Blueprints = brep.GetAll().ToArray();
            }
            using (var prep=_prodinfoRepos())
            {
                exportObject.ProductionInfos = prep.GetAll().ToArray();
            }
            var res = JsonConvert.SerializeObject(exportObject,Formatting.Indented);
            var dlg = new SaveFileDialog()
                {
                    Title = "Экспорт БД",
                    Filter = "Data File *.dat|*.dat"
                };
            if (dlg.ShowDialog() == true)
            {
                using (var writer=File.CreateText(dlg.FileName))
                {
                    writer.Write(res);
                    writer.Close();
                }
            }
        }

        public event EventHandler CanExecuteChanged;
    }
}