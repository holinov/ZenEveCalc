using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using Zen.EveCalc.Core.DataStorage;
using Zen.EveCalc.DataModel;

namespace Zen.EveCalc.Controls.Models
{
    public class ProductionReportModel : DependencyObject
    {
        public ProductionReportModel()
        {
            _cmd=App.Core.Resolve<SaveListCommand<ProductionInfo>>();
            _cmd.SetRows(Rows);
            SaveReports = _cmd;

        }

        public ProductionInfoCollection Rows
        {
            get { return (ProductionInfoCollection)GetValue(RowsProperty); }
            set { SetValue(RowsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Rows.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RowsProperty =
            DependencyProperty.Register("Rows", typeof(ProductionInfoCollection), typeof(ProductionReportModel), new PropertyMetadata(null,
                (s, ar) =>
                    {
                        var newRows = ar.NewValue as ProductionInfoCollection;
                        if (newRows != null)
                        {
                            ((ProductionReportModel) s)._cmd.SetRows(newRows);
                        }
                    }));

        private SaveListCommand<ProductionInfo> _cmd;

        public ICommand SaveReports { get; set; }
    }

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