using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MarioTris
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 
    public interface IGameView
    {
        void DrawSquare(int i, int j, byte color, Settings.canvas canvas);
        event EventHandler KeyLeft;
        event EventHandler KeyRight;
        event EventHandler KeyDown;
        event EventHandler KeyUp;
        event EventHandler SpaceKey;
        event EventHandler PauseButton;
        event EventHandler RefreshGame;
        
        void PlayMoveingSound();
        void PlayRotateSound();
        void PlayDropLineSound();
        void ClearGrid();
        void ClearSquare();
        void ClearNextCanvas();
        void UpdateScore(int score);
        void UpdateLevel(byte level);
        void FlyingScores(int score, int x, int y);
        void GameOver();
    }

    public sealed partial class GameView : Page, IGameView
    {
        public event EventHandler KeyLeft;
        public event EventHandler KeyRight;
        public event EventHandler KeyDown;
        public event EventHandler KeyUp;
        public event EventHandler SpaceKey;
        public event EventHandler PauseButton;
        public event EventHandler RefreshGame;


        private bool MusicOnPause;

        public GameView()
        {

            this.InitializeComponent();

            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;

            if (LoginUserInfo.username.Length > 0) loggedAsTxtBlk.Text = "Logged as:  " + LoginUserInfo.username;


            Game game = new Game(this);

        }

        public async void FlyingScores(int score, int x, int y)
        {

            TextBlock scoreTxt = new TextBlock();
            scoreTxt.Text = string.Format("+{0}", score);
            scoreTxt.FontSize = 30;
            scoreTxt.Margin = new Thickness(x * 30, y * 30, 0, 0);
            Canvas.SetZIndex(scoreTxt, 500);
            gameRectangle.Children.Add(scoreTxt);
            for (int i = 0; i < 40; i++)
            {
                scoreTxt.Margin = new Thickness(x * 30, y * 30 - i, 0, 0);
                await Task.Delay(10);
            }
            gameRectangle.Children.Remove(scoreTxt);

        }


        public void UpdateScore(int score)
        {
            scoreTxtBlk.Text = score.ToString();
        }

        public void UpdateLevel(byte level)
        {
            LevelTxtBlk.Text = level.ToString();
        }

        public void PlayMoveingSound()
        {
            MovingSound.Position = TimeSpan.Zero;
            MovingSound.Play();
        }

        public void PlayRotateSound()
        {
            RotateSound.Position = TimeSpan.Zero;
            RotateSound.Play();
        }

        public void PlayDropLineSound()
        {
            DropLineSound.Position = TimeSpan.Zero;
            DropLineSound.Play();
        }

        public void ClearNextCanvas()
        {
            nextFigureCanvas.Children.Clear();
        }

        public void ClearGrid()
        {
            gameRectangle.Children.Clear();
        }

        public void ClearSquare()
        {
            gameRectangle.Children.RemoveAt(gameRectangle.Children.Count - 1);
        }

        public void DrawSquare(int i, int j, byte color, Settings.canvas canvas)
        {
            Image img = new Image();
            switch (color)
            {
                case 1:
                    img.Source = pinkBlock.Source;
                    break;
                case 2:
                    img.Source = yellowBlock.Source;
                    break;
                case 3:
                    img.Source = greenBlock.Source;
                    break;
                case 4:
                    img.Source = redBlock.Source;
                    break;
                case 5:
                    img.Source = purpleBlock.Source;
                    break;
                case 6:
                    img.Source = blueBlock.Source;
                    break;
                case 7:
                    img.Source = orangeBlock.Source;
                    break;
            }
            img.Width = 30;
            img.Height = 30;
            Canvas.SetLeft(img, i * 30 + 2);
            Canvas.SetTop(img, j * 30 + 2);
            if (canvas == Settings.canvas.TetrisGrid)
                gameRectangle.Children.Add(img);
            else nextFigureCanvas.Children.Add(img);
        }

        private void CoreWindow_KeyDown(CoreWindow sender, KeyEventArgs e)
        {
            if (e.VirtualKey == VirtualKey.Left)
            {
                if (KeyLeft != null) KeyLeft(this, null);
            }
            if (e.VirtualKey == VirtualKey.Right)
            {
                if (KeyRight != null) KeyRight(this, null);
            }
            if (e.VirtualKey == VirtualKey.Down)
            {
                if (KeyDown != null) KeyDown(this, null);
            }
            if (e.VirtualKey == VirtualKey.Up)
            {
                if (KeyUp != null) KeyUp(this, null);
            }
            if (e.VirtualKey == VirtualKey.Space)
            {
                if (KeyUp != null) SpaceKey(this, null);
            }
            if (e.VirtualKey == VirtualKey.P)
            {
                if (KeyUp != null) pause_PointerPressed(this, null);
            }
            e.Handled = true;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var parameter = e.Parameter as string;
        }

        private void refresh_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if (RefreshGame != null) RefreshGame(this, null);
        }


        private void pause_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if (PauseButton != null) PauseButton(this, null);
        }

        private void mute_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            MusicOnPause = !MusicOnPause;
            if (MusicOnPause) backgroundMusic.Pause();
            else backgroundMusic.Play();
        }

        public void GameOver()
        {
            Window.Current.CoreWindow.KeyDown -= CoreWindow_KeyDown;
            Frame.Navigate(typeof(GameOver));
        }

        private void MainMenu_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
           if (Frame.CanGoBack)
            {
                e.Handled = true;
                Frame.GoBack();
            }
        }

    }
}

