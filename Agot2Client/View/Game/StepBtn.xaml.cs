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
    /// Interaction logic for StepBtn.xaml
    /// </summary>
    public partial class StepBtn : UserControl
    {
        static public Action DeleteStepTimer { get; private set; }

        public ExtGame Game { get; private set; }
        public StepTimer StepTimer { get; private set; }

        public StepBtn()
        {
            InitializeComponent();

            StepTimer = new StepTimer();
            DeleteStepTimer = () => StepTimer._GameTimerList.Remove(StepTimer.GameTimer);
            timerBtn.DataContext = StepTimer;

            ClientInfo.ClientGameChanging += ClientInfo_ClientGameChanging;
        }

        private void ClientInfo_ClientGameChanging(ExtGame game)
        {
            if (Game != null)
            {
                Game.ClientStepCganged -= ExtGame_ClientStepCgange;
            }
            if (game != null)
            {
                Game = game;
                Game.ClientStepCganged += ExtGame_ClientStepCgange;
            }

            StepTimer.Stop();
        }

        private void ExtGame_ClientStepCgange(ExtStep step)
        {
            if (step == null)
                StepTimer.Stop();
            else
                StepTimer.Start(step);
        }

        private void _IsFull_Button_Click(object sender, RoutedEventArgs e)
        {
            GameView.CompleteStep(Game);
        }
    }
}
