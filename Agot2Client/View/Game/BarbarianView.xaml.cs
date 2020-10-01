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
    /// Логика взаимодействия для BarbarianView.xaml
    /// </summary>
    public partial class BarbarianView : UserControl
    {
        public ExtGame Game { get; private set; }
        public ExtStep ClientStep { get; private set; }

        public BarbarianView()
        {
            InitializeComponent();
            ClientInfo.ClientGameChanging += ClientInfo_ClientGameChanging;
        }

        private void ClientInfo_ClientGameChanging(ExtGame game)
        {
            if (Game != null)
            {
                Game.ClientStepCganged -= ExtGame_ClientStepCganged;
            }
            if (game != null)
            {
                Game = game;
                Game.ClientStepCganged += ExtGame_ClientStepCganged;
            }
        }

        private void ExtGame_ClientStepCganged(ExtStep step)
        {
            ClientStep = step;
        }

        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            if (ClientStep == null)
                return;

            ClientStep.WCFStep.Raven.StepType = "Yes";
            GameView.CompleteStep(Game);
        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            if (ClientStep == null)
                return;

            ClientStep.WCFStep.Raven.StepType = "No";
            GameView.CompleteStep(Game);
        }
    }
}
