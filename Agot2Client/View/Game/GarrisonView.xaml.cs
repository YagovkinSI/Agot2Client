using GamePortal;
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
    /// Логика взаимодействия для GarrisonView.xaml
    /// </summary>
    public partial class GarrisonView : UserControl
    {
        public GarrisonView()
        {
            InitializeComponent();
            this.Drop += _Drop;
            this.AllowDrop = true;
        }

        private void _Drop(object sender, DragEventArgs e)
        {
            e.Handled = true;
            TerrainView.UpdateGamePoint(e, this);
        }

        private void Garrison_MouseLeave(object sender, MouseEventArgs e)
        {
            e.Handled = true;

            IPosition garrison = this.DataContext as IPosition;
            if (garrison == null)
                return;

            if (Keyboard.Modifiers == ModifierKeys.Control && e.LeftButton == MouseButtonState.Pressed)
            {
                DragDrop.DoDragDrop(this, garrison, DragDropEffects.All);
                return;
            }
        }
    }
}
