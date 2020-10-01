using GamePortal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace Agot2Client
{
    /// <summary>
    /// Логика взаимодействия для AvatarView.xaml
    /// </summary>
    public partial class AvatarViewMin : UserControl
    {
        private GamePersonItemViewModel _ViewModel;

        public AvatarViewMin()
        {
            InitializeComponent();
            DataContextChanged += AvatarView_DataContextChanged;
        }

        private void AvatarView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            _ViewModel = e.NewValue as GamePersonItemViewModel;
        }

        private void Mute_Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.GamePortal.UserMute(_ViewModel.User);
        }
    }
}
