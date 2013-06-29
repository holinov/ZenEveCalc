using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Zen.EveCalc.Controls
{
    public class PageCommand
    {
        public Action<IPageControl> Action { get; set; } 
        public string Name { get; set; }

        public UIElement Content { get; set; }

        public ICommand Command { get; set; }
    }
}
