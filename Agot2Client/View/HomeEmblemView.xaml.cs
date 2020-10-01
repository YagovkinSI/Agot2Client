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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Agot2Client
{
    /// <summary>
    /// Логика взаимодействия для HomeEmblem.xaml
    /// </summary>
    public partial class HomeEmblemView : UserControl
    {
        public Nullable<TimeSpan> BeginTime
        {
            get { return (Nullable<TimeSpan>)GetValue(MyPropertyProperty); }
            set { SetValue(MyPropertyProperty, value); }
        }
        public static readonly DependencyProperty MyPropertyProperty =
            DependencyProperty.Register("BeginTime", typeof(Nullable<TimeSpan>), typeof(HomeEmblemView), new PropertyMetadata(null, new PropertyChangedCallback(BeginTimePropertyChanged)));        
        static void BeginTimePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((Storyboard)((HomeEmblemView)d).Resources["Show"]).BeginTime = e.NewValue as Nullable<TimeSpan>;
        }

        
        public ImageSource Source
        {
            get { return (ImageSource)GetValue(EmblemSourceProperty); }
            set { SetValue(EmblemSourceProperty, value); }
        }
        public static readonly DependencyProperty EmblemSourceProperty =
            DependencyProperty.Register("Source", typeof(ImageSource), typeof(HomeEmblemView), new PropertyMetadata(null, new PropertyChangedCallback(SourcePropertyChanged)));
        static void SourcePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((HomeEmblemView)d).image.Source = e.NewValue as ImageSource;
        }
        
        public HomeEmblemView()
        {
            InitializeComponent();
        }
    }
}
