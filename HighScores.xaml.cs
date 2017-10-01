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
    public sealed partial class HighScores : Page
    {
        public HighScores()
        {
            this.InitializeComponent();
            RetriveContent();
        }

        private async void RetriveContent()
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                HttpResponseMessage response;

                response = await httpClient.GetAsync("http://simkin.atwebpages.com/MarioTrisHighScores.php");
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    string[] words = responseBody.Split('^');
                    //scoresListBox.Items.Add(words.GetLength(0));
                    for (int i = 0; i < (words.GetLength(0) - 1); i += 2)
                    {
                        //show the player only if his score is more than 0
                        if (int.Parse(words[i + 1]) > 0)
                        scoresListBox.Items.Add(words[i] + "                     " + words[i + 1]);
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

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }
    }
}
