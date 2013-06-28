using System.Windows;
using Zen.EveCalc.Core;

namespace Zen.EveCalc.Controls
{
    public interface IPageControl
    {
        UIElement PageContent { get; }
        UIElement Header { get; }
        int SortOrder { get; }
        AppScope PageSope { get; set; }
        void Init();
        PageCommand[] Commands { get; }
        void Show();
    }
}