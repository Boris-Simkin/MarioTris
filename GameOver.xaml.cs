using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MarioTris
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GameOver : Page
    {
        public GameOver()
        {
            this.InitializeComponent();
            if (LoginUserInfo.username.Length > 0) UploadScore();
            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;
        }


        private void CoreWindow_KeyDown(CoreWindow sender, KeyEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                Window.Current.CoreWindow.KeyDown -= CoreWindow_KeyDown;
                e.Handled = true;
                Frame.Navigate(typeof(MainPage));
            }
        }

        private void image_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                Window.Current.CoreWindow.KeyDown -= CoreWindow_KeyDown;
                e.Handled = true;
                Frame.Navigate(typeof(MainPage));
            }
        }


        public async void UploadScore()
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                HttpResponseMessage response;

                var values = new Dictionary<string, string>();
                values.Add("Username", LoginUserInfo.username);
                values.Add("Score", LoginUserInfo.score.ToString());
                var content = new FormUrlEncodedContent(values);

                response = await httpClient.PostAsync("http://simkin.atwebpages.com/MarioTrisSetScores.php", content);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    //TMPtextBlock.Text = responseBody;
                    //TMPtextBlock.Text = "ok";
                }
                else
                {
                    //TMPtextBlock.Text = "Error connetion";
                }

            }
            catch (HttpRequestException hre)
            {

            }

        }
    }
}
