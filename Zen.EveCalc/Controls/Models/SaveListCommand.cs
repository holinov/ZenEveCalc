using System;
using System.Collections.Generic;
using System.Windows.Input;
using Zen.EveCalc.Core.DataStorage;

namespace Zen.EveCalc.Controls.Models
{
    public class SaveListCommand<TEntity> : ICommand where TEntity:IHasGuidId
    {
        private Func<IRepositoryWithGuid<TEntity>> _repository;

        public SaveListCommand(Func<IRepositoryWithGuid<TEntity>> repository)
        {
            _repository = repository;
        }

        private IEnumerable<TEntity> _list;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            using (var repos=_repository())
            {
                repos.StoreBulk(_list);
            }
        }

        public event EventHandler CanExecuteChanged;

        public void SetRows(IEnumerable<TEntity> newRows)
        {
            _list = newRows;
        }
    }
}