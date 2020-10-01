using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Agot2Client
{
    static public class SendStepTask
    {
        static public bool IsBusy { get; private set; }

        static public void AddTask(ExtGame game)
        {
            if (IsBusy)
                return;

            IsBusy = true;
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                App.TaskFactory.StartNew(() =>
                {
                    try
                    {
                        game.ClientStep.WCFStep.IsFull = true;
                        game.GameService.SendStep(game.ClientStep.WCFStep);
                        game.ClientStep = null;
                    }
                    catch (Exception exp)
                    {
                        MainWindow.ClientInfo.OnLineStatus = false;
                        App.Agot2.errorView.ShowByDispatcher(App.GetResources("text_error"), App.GetResources("error_sendStep") + exp.Message);
                    }

                    IsBusy = false;
                });
            }), DispatcherPriority.ApplicationIdle);
        }
    }
}
