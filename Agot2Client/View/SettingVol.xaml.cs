using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Agot2Client.View
{
    /// <summary>
    /// Логика взаимодействия для SettingVol.xaml
    /// </summary>
    public partial class SettingVol : UserControl
    {
        public SettingVol()
        {
            InitializeComponent();
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (e.OldValue == 0) return;

            test.Stop();
            test.Position = TimeSpan.Zero;
            test.Play();
        }

        private void Slider_MouseLeave(object sender, MouseEventArgs e)
        {
            test.Stop();
        }
    }
}
