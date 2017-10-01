using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MarioTris
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SignIn : Page
    {
        public SignIn()
        {
            this.InitializeComponent();
        }

        private void passwordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (satisfyConditions())
                submitBtn.IsEnabled = true;
            else
                submitBtn.IsEnabled = false;
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (satisfyConditions())
                submitBtn.IsEnabled = true;
            else
                submitBtn.IsEnabled = false;
        }

        private bool satisfyConditions()
        {
            return ((usernameTxtBox.Text.Length >= 2) &&
                (firstNameTxtBox.Text.Length >= 2) &&
                (lastNameTxtBox.Text.Length >= 2) &&
                (passwordBox.Password.Length >= 4) &&
                (passwordBox.Password == confirmPasswordBox.Password));
        }

        private async void submit(object sender, RoutedEventArgs e)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                HttpResponseMessage response;
                var values = new Dictionary<string, string>();
                values.Add("Username", usernameTxtBox.Text);
                values.Add("FName", firstNameTxtBox.Text);
                values.Add("LName", lastNameTxtBox.Text);
                values.Add("Password", passwordBox.Password);
                var content = new FormUrlEncodedContent(values);

                response = await httpClient.PostAsync("http://simkin.atwebpages.com/SignIn.php", content);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    if (responseBody == "created")
                    {
                        if (Frame.CanGoBack)
                        {
                            Frame.GoBack();
                        }
                    }
                    else
                    {
                        StringFromServer.Text = responseBody;
                    }

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

        private void cancleBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }
    }
}
