using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Zen.EveCalc.Controls
{
    public class SplittingBox : TextBlock
    {
        public string Suffix { get; set; }

        public float FloatValue
        {
            get { return (float)GetValue(FloatValueProperty); }
            set { SetValue(FloatValueProperty, value); }
        }

        public FormatType FormatType
        {
            get { return (FormatType) GetValue(FormatTypeProperty); }
            set { SetValue(FormatTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FloatValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FloatValueProperty =
            DependencyProperty.Register("FloatValue", typeof(float), typeof(SplittingBox), new PropertyMetadata((o, args) =>
                {
                    var val = (float) args.NewValue;
                    var mb = (SplittingBox) o;
                    switch (mb.FormatType)
                    {
                        case FormatType.None:
                            break;
                        case FormatType.PositiveGreen:
                            mb.Foreground = val > 0 ? new SolidColorBrush(Colors.Green) : new SolidColorBrush(Colors.Red);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    mb.SetValue(TextProperty, string.Format("{0:0,0.00}", val) + mb.Suffix);
                }));

        public static readonly DependencyProperty FormatTypeProperty = DependencyProperty.Register("FormatType", typeof (FormatType), typeof (SplittingBox), new PropertyMetadata(default(FormatType)));
    }
}