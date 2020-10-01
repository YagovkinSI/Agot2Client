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
    /// Логика взаимодействия для ErrorView.xaml
    /// </summary>
    public partial class ErrorView : UserControl
    {
        DispatcherTimer _Timer = new DispatcherTimer(DispatcherPriority.ApplicationIdle);
        public ErrorView()
        {
            InitializeComponent();
            _Timer.Tick += (s, a) =>
            {
                _Timer.Stop();
                this.Visibility = Visibility.Collapsed;
            };
        }

        public void ShowByDispatcher(string type, string text, int second = 10)
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                try
                {
                    this.typeName.Text = type;
                    this.text.Text = text;
                    this.Visibility = Visibility.Visible;

                    _Timer.Stop();
                    _Timer.Interval = TimeSpan.FromSeconds(second);
                    _Timer.Start();
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message);
                }
            }), DispatcherPriority.ApplicationIdle);
        }
    }
}
