using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Agot2Client
{
    /// <summary>
    /// Логика взаимодействия для AwardsForm.xaml
    /// </summary>
    public partial class AwardsForm : UserControl
    {
        public AwardsForm()
        {
            InitializeComponent();
        }

        private void okBtnClick(object sender, RoutedEventArgs e)
        {
            App.Agot2.awardsForm.Visibility = Visibility.Collapsed;
        }
    }
}
