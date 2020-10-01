using Awesomium.Core;
using Facebook;
using GamePortal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;

namespace Agot2Client
{
    /// <summary>
    /// Логика взаимодействия для OAuthWindow.xaml
    /// </summary>
    public partial class FacebookWindow : Window
    {
        public static FacebookClient FbClient { get; private set; }

        private string appID = "952751421448742";
        private string scopes = "public_profile";//,manage_pages,publish_pages,email,publish_actions, user_posts
        private string returnURL = "https://www.facebook.com/connect/login_success.html";
        private string connectionUri;

        public FacebookWindow()
        {
            InitializeComponent();
            connectionUri = string.Format("https://www.facebook.com/dialog/oauth?client_id={0}&redirect_uri={1}&response_type=token%2Cgranted_scopes&scope={2}&display=popup", new object[] { appID, returnURL, scopes });
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

            //https://www.facebook.com/connect/login_success.html#access_token=EAANihboI4iYBAClMBUdVD1ATKH4TWRsvJJ6q9QfwN0ZBdyW04CYCGoGSv75Ffi5U22aR26zZB0QV2JPEmBJCnYtAdzyZApRr9vk60ZBIVhuHUOkNHwbAxbXcBqZBZAKoxV4zYcqth2t3MSQrUzpsFQtisFzfYjSnMDTQ1Xu93lkQZDZD&data_access_expiration_time=1566755901&expires_in=5103250&granted_scopes=email%2Cmanage_pages%2Cpages_show_list%2Cpublish_pages%2Cpublic_profile&denied_scopes=
            Regex regex = new Regex(@"(?<name>[\w]+)=(?<value>[^\x26]+)");
            foreach (Match m in regex.Matches(e.Url.ToString()))
            {
                if (m.Groups["name"].Value == "access_token")
                    App.Settings.Value.access_token = m.Groups["value"].Value;
            }
            
            if (!string.IsNullOrWhiteSpace(App.Settings.Value.access_token))
            {
                webControl.Dispose();
                DialogResult = Connect();
            }
        }

        public static bool Connect()
        {
            try
            {
                if (FacebookWindow.FbClient == null)
                    FacebookWindow.FbClient = new FacebookClient(App.Settings.Value.access_token);
                else
                    return true;

                IDictionary<string, object> me = FacebookWindow.FbClient.Get("me", new { fields = "id,first_name,last_name" }) as IDictionary<string, object>;

                App.Settings.Value.User = new user
                {
                    uid = me["id"].ToString(),
                    first_name = me["first_name"].ToString(),
                    last_name = me["last_name"].ToString(),

                    photo = string.Format("https://graph.facebook.com/{0}/picture", me["id"]),
                    isFacebook = true
                };

                return true;
            }
            catch (FacebookOAuthException exp) { SaveException(exp); }
            catch (FacebookApiException exp) { SaveException(exp); }
            catch (Exception exp)
            {
                App.WriteException(exp);
            }

            FacebookWindow.FbClient = null;
            return false;
        }

        private static void SaveException(Exception exp)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "fbException");
            Directory.CreateDirectory(path);
            File.WriteAllText(string.Format("{0}/{1}.txt", path, Guid.NewGuid()), exp.Message);
        }
    }
}
