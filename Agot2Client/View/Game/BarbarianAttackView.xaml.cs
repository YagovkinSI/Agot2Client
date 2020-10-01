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
    /// Логика взаимодействия для BarbarianAttackView.xaml
    /// </summary>
    public partial class BarbarianAttackView : UserControl
    {
        public ExtGame Game { get; private set; }
        public ExtStep ClientStep { get; private set; }

        public BarbarianAttackView()
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

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (ClientStep == null)
                return;

            var user = (ExtGameUserInfo)((FrameworkElement)sender).DataContext;
            ClientStep.WCFStep.Raven.StepType = user.Step.ExtGameUser.WCFGameUser.HomeType;
            GameView.CompleteStep(ClientStep.Game);
        }
    }
}
