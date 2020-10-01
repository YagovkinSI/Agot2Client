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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.ComponentModel;

namespace Agot2Client
{
    /// <summary>
    /// Логика взаимодействия для CloudsView.xaml
    /// </summary>
    public partial class CloudsView : UserControl
    {
        public CloudsView()
        {
            InitializeComponent();
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            cloudsAnimation.SpeedRatio = (1920 / this.ActualWidth);
        }
    }
}
