using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Net.Http;
using System.Net;
using Windows.System;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MarioTris
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Login : Page
    {
        public Login()
        {
            this.InitializeComponent();
           
        }

        private async void submit(object sender, RoutedEventArgs e)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                HttpResponseMessage response;
                var values = new Dictionary<string, string>();
                values.Add("Username", usernameTxtBox.Text);
                values.Add("Password", passwordBox.Password);
                var content = new FormUrlEncodedContent(values);

                response = await httpClient.PostAsync("http://simkin.atwebpages.com/Login.php", content);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    if (responseBody != "false verifying")
                    {
                        LoginUserInfo.username = usernameTxtBox.Text;
                        LoginUserInfo.score = int.Parse(responseBody);


                        //usernameTxtBox.Text = responseBody;
                        if (Frame.CanGoBack)
                        {
                            Frame.GoBack();
                        }
                    }

                    else StringFromServer.Text = "Username or Password is incorrect!";
                }
                else
                {
                    StringFromServer.Text = "Error connecting to the server";
                }

            }
            catch (HttpRequestException hre)
            {
                StringFromServer.Text = "Error connecting to the server";
            }

        }

        private bool satisfyConditions()
        {
            submitBtn.IsEnabled = true;
            return ((usernameTxtBox.Text.Length > 0) && (passwordBox.Password.Length > 0));
        }

        private void usernameTxtBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (satisfyConditions())
                submitBtn.IsEnabled = true;
            else
                submitBtn.IsEnabled = false;
        }

        private void passwordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (satisfyConditions())
                submitBtn.IsEnabled = true;
            else
                submitBtn.IsEnabled = false;
        }

        private void cancleBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }

        private void passwordBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key.Equals(VirtualKey.Enter))
            {
                submit(null, null);
            }
        }

        private void registerBtn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SignIn));
        }
    }
}
