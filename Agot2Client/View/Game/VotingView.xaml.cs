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

namespace Agot2Client
{
    /// <summary>
    /// Логика взаимодействия для VotingView.xaml
    /// </summary>
    public partial class VotingView : UserControl
    {
        public ExtGame Game { get; private set; }
        public VotingView()
        {
            InitializeComponent();
            ClientInfo.ClientGameChanging += ClientInfo_ClientGameChanging;
        }

        private void ClientInfo_ClientGameChanging(ExtGame game)
        {
            Game = game;
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            GameView.CompleteStep(Game);
        }
    }
}
