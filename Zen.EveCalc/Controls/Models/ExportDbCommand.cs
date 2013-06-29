using System;
using System.IO;
using System.Linq;
using System.Windows.Input;
using Microsoft.Win32;
using Newtonsoft.Json;
using Zen.EveCalc.Core.DataStorage;
using Zen.EveCalc.DataModel;

namespace Zen.EveCalc.Controls.Models
{
    public class ExportDbCommand : ICommand
    {
        private readonly Func<IRepositoryWithGuid<EveItem>> _itemsRepos;
        private readonly Func<IRepositoryWithGuid<Blueprint>> _blueprintsRepos;
        private readonly Func<IRepositoryWithGuid<ProductionInfo>> _prodinfoRepos;

        public ExportDbCommand(Func<IRepositoryWithGuid<EveItem>> itemsRepos,
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