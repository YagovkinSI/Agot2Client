using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Логика взаимодействия для OrderTypeMenu2.xaml
    /// </summary>
    public partial class OrderTypeMenu2 : UserControl
    {
        ExtOrder _ExtOrder;

        public OrderTypeMenu2()
        {
            InitializeComponent();
            DataContextChanged += OrderTypeMenu2_DataContextChanged;        
        }

        void OrderTypeMenu2_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            _ExtOrder = (ExtOrder)e.NewValue;
        }

        private void Item_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;

            _ExtOrder.WCFOrder.OrderType = ((ExtOrderType)((Image)e.OriginalSource).DataContext).WCFOrderType.Name;
            _ExtOrder.OnPropertyChanged("ImageName");
            
            if (_ExtOrder.Step.WCFStep.StepType == "Неожиданный_шаг")
                GameView.CompleteStep(_ExtOrder.Step.Game);

            ((Popup)this.Parent).IsOpen = false;
        }
    }
}
