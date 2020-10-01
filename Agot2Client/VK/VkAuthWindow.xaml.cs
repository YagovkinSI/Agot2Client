using Awesomium.Core;
using GamePortal;
using System;
using System.Text.RegularExpressions;
using System.Windows;

namespace Agot2Client
{
    /// <summary>
    /// Interaction logic for VkAuthWindow.xaml
    /// </summary>
    public partial class VkAuthWindow : Window
    {
        public static VKAPI VKAPI { get; private set; }

        private string connectionUri = string.Format("https://api.vk.com/oauth/authorize?client_id={0}&scope={1}&display=popup&response_type=token", VKAPI.client_id, VKAPI.Scope);

        public VkAuthWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {            
            webControl.TitleChanged += (s, ee) => { Title = webControl.Title; };
            webControl.LoadingFrame += (s, ee) => { Title = webControl.Title; progress.Visibility = Visibility.Visible; };
            webControl.DocumentReady += WebControl_DocumentReady;

            webControl.Source = connectionUri.ToUri();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            webControl.Source = connectionUri.ToUri();
        }
        
        private void WebControl_DocumentReady(object sender, Awesomium.Core.DocumentReadyEventArgs e)
        {

            progress.Visibility = Visibility.Hidden;
            webControl.Focus();

            string user_id = null;
            //https://oauth.vk.com/blank.html#access_token=349631229a0ffc79fbbec53ff3ab42fc32dd6c5ed875c5a3681dfa2d01ce1d0b6c8efcb9b78dbd6d0540d&expires_in=86400&user_id=147751339
            Regex regex = new Regex(@"(?<name>[\w]+)=(?<value>[^\x26]+)");
            foreach (Match m in regex.Matches(e.Url.ToString()))
            {
                if (m.Groups["name"].Value == "access_token")
                    App.Settings.Value.access_token = m.Groups["value"].Value;

                if (m.Groups["name"].Value == "user_id")
                    user_id = m.Groups["value"].Value;
            }

            if (!string.IsNullOrWhiteSpace(App.Settings.Value.access_token)&& !string.IsNullOrWhiteSpace(user_id))
            {
                App.Settings.Value.User = new user() { uid = user_id, isFacebook = false };
                webControl.Dispose();
                DialogResult = Connect();
            }
        }

        public static bool Connect()
        {
            try
            {
                if (VkAuthWindow.VKAPI == null)
                    VkAuthWindow.VKAPI = new VKAPI();
                else
                    return true;

                bool result = VkAuthWindow.VKAPI.UpdateProfile();
                if (result)
                    return true;
            }
            catch (Exception exp)
            {
                App.WriteException(exp);
            }

            VkAuthWindow.VKAPI = null;
            return false;
        }
    }
}
