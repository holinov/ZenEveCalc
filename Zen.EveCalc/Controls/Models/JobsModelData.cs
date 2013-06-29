using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Zen.EveCalc.DataModel;

namespace Zen.EveCalc.Controls.Models
{
    public class JobsModelData: DependencyObject
    {


        public ObservableCollection<ProductionInfo> Jobs
        {
            get { return (ObservableCollection<ProductionInfo>)GetValue(JobsProperty); }
            set { SetValue(JobsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Jobs.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty JobsProperty =
            DependencyProperty.Register("Jobs", typeof(ObservableCollection<ProductionInfo>), typeof(JobsModelData), new PropertyMetadata(null));

  
    }
}
