using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Agot2Client.Windows
{
    /// <summary>
    /// Логика взаимодействия для PostWindow.xaml
    /// </summary>
    public partial class PostWindow : Window
    {
        public PostModel ViewModal { get; private set; }
        public PostWindow(string filename)
        {
            ViewModal = new PostModel
            {
                FileName = filename
            };
            if (MainWindow.ClientInfo.ClientGame.WCFGame.CloseTime != null)
            {
                if (VkAuthWindow.VKAPI != null)
                {
                    ViewModal.IsPostText = true;
                    ViewModal.ProviderLogo = "/Image/vk.png";
                    ViewModal.Text = VkText(filename);
                }
                else if (FacebookWindow.FbClient != null)
                {
                    ViewModal.ProviderLogo = "/Image/fb.png";
                    ViewModal.Text = FbText(filename);
                }

                this.DataContext = ViewModal;
            }

            InitializeComponent();
        }

        private static string VkText(string filename)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[service_board_games|A Game of Thrones: The Online Board Game]\n");
            sb.Append($"http://lantsev1981.pro/{CultureInfo.CurrentUICulture.Name.Substring(0, 2)}");
            MainWindow.ClientInfo.ClientGame.ViewUserStep.Where(p => p.ExtGameUser.WCFGameUser.HomeType != null).ToList().ForEach((p) =>
            {
                sb.Append($"\n\n{App.GetResources($"homeType_{p.ExtGameUser.WCFGameUser.HomeType}")}");
                if (p.ExtGameUser.WCFGameUser.Login != null)
                {
                    if (p.ExtGameUser.GPUser.Api["isFacebook"] == "True")
                    {
                        sb.Append($" - {p.ExtGameUser.GPUser.Title} https://www.facebook.com/{p.ExtGameUser.GPUser.Api["uid"]}");
                    }
                    else
                    {
                        sb.Append($" - [id{p.ExtGameUser.GPUser.Api["uid"]}|{p.ExtGameUser.GPUser.Title}]");
                    }
                }

                var msg = p.WCFStep.Message.FirstOrDefault();
                if (!string.IsNullOrWhiteSpace(msg))
                {
                    sb.Append($"\n\t{msg}");
                }
            });
            return sb.ToString();
        }

        private static string FbText(string filename)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("A Game of Thrones: The Online Board Game\nhttps://www.facebook.com/agot.online\n");
            sb.Append($"http://lantsev1981.pro/{CultureInfo.CurrentUICulture.Name.Substring(0, 2)}");
            MainWindow.ClientInfo.ClientGame.ViewUserStep.Where(p => p.ExtGameUser.WCFGameUser.HomeType != null).ToList().ForEach((p) =>
            {
                sb.Append($"\n\n{App.GetResources($"homeType_{p.ExtGameUser.WCFGameUser.HomeType}")}");
                if (p.ExtGameUser.WCFGameUser.Login != null)
                {
                    sb.Append($" - {p.ExtGameUser.GPUser.Title} {(p.ExtGameUser.GPUser.Api["isFacebook"] == "True" ? "https://www.facebook.com/" : "https://vk.com/id")}{p.ExtGameUser.GPUser.Api["uid"]}");
                }

                var msg = p.WCFStep.Message.FirstOrDefault();
                if (!string.IsNullOrWhiteSpace(msg))
                {
                    sb.Append($"\n\t{msg}");
                }
            });
            return sb.ToString();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (VkAuthWindow.VKAPI != null)
            {
                new Task(() =>
                {
                    try
                    {
                        var photoId = VkAuthWindow.VKAPI.SendPhoto(ViewModal.FileName);
                        VkAuthWindow.VKAPI.WallPost(ViewModal.IsPostText ? ViewModal.Text : string.Empty, null, photoId);

                        App.Agot2.errorView.ShowByDispatcher(App.GetResources("text_notify"), App.GetResources("text_postComplete"));
                    }
                    catch (Exception exp)
                    {
                        App.Agot2.errorView.ShowByDispatcher(App.GetResources("text_error"), exp.Message);
                    }
                }).Start();
            }
            else if (FacebookWindow.FbClient != null)
            {
                var permissions = FacebookWindow.FbClient.Get("me", new { fields = "permissions" }).ToString();
                if (!permissions.Contains("{\"permission\":\"manage_pages\",\"status\":\"granted\"}")
                    && !permissions.Contains("{\"permission\":\"publish_pages\",\"status\":\"granted\"}"))
                {
                    App.Agot2.errorView.ShowByDispatcher(App.GetResources("text_error"), App.GetResources("text_postCanceled"));
                    return;
                }

                new Task(() =>
                  {
                      try
                      {
                          //подготовка и отправка screenshot
                          var screen = File.ReadAllBytes(ViewModal.FileName);
                          var uploader = new Facebook.FacebookMediaObject() { FileName = ViewModal.FileName, ContentType = "image/png" };
                          uploader.SetValue(screen);
                          var postInfo = new Dictionary<string, object>();
                          if (ViewModal.IsPostText)
                          {
                              postInfo.Add("massage", ViewModal.Text);
                          }

                          postInfo.Add("image", uploader);
                          var response = FacebookWindow.FbClient.Post("me/photos", postInfo);

                          App.Agot2.errorView.ShowByDispatcher(App.GetResources("text_notify"), App.GetResources("text_postComplete"));
                      }
                      catch (Exception exp)
                      {
                          App.Agot2.errorView.ShowByDispatcher(App.GetResources("text_error"), exp.Message);
                      }
                  }).Start();
            }

            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(ViewModal.FileName);
        }
    }

    public class PostModel
    {
        public string FileName { get; set; }
        public string Text { get; set; }
        public bool IsPostText { get; set; }
        public string ProviderLogo { get; set; }
    }
}
