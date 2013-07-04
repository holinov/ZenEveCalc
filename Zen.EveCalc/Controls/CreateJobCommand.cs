using System;
using System.Windows.Input;
using Zen.EveCalc.Core.DataStorage;
using Zen.EveCalc.DataModel;

namespace Zen.EveCalc.Controls
{
    public class CreateJobCommand:ICommand
    {
        private readonly Func<IRepositoryWithGuid<ProductionJob>> _prodInfoRepos;
        public bool CanExecute(object parameter)
        {
            return parameter is ProductionInfo;
        }

        public void Execute(object parameter)
        {
            /*var prodInfo=(ProductionInfoRepository)
            using (var repos=_prodInfoRepos())
            {
                
            }*/
        }

        public event EventHandler CanExecuteChanged;
    }
}