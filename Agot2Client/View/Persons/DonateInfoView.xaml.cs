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
using System.Windows.Threading;

namespace Agot2Client
{
    /// <summary>
    /// Логика взаимодействия для DonateInfoView.xaml
    /// </summary>
    public partial class DonateInfoView : UserControl
    {
        public DonateInfoView()
        {
            InitializeComponent();
        }

        private void Comment_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            YandexTranslate.Translate(MainWindow.GamePortal.MasterOfDonate.DonateComment, (str) =>
            {
                MainWindow.GamePortal.MasterOfDonate.DonateComment = str;
                MainWindow.GamePortal.MasterOfDonate.OnPropertyChanged("DonateComment");
            });
        }
    }
}
