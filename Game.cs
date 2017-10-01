using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace MarioTris
{
    class Game
    {
        private readonly IGameView _view;

        DispatcherTimer dispatcherTimer;

        byte nextFigure, activeFigure;
        int GameDelay;
        int totalScore;
        byte level;
        private Block tetromino;
        bool gameOnPause = true;

        public bool changeFigure = false;
        Random random;

        MainGrid mainGrid;

        public Game(IGameView view)
        {
            _view = view;
            _view.PauseButton += View_PauseButton;
            _view.RefreshGame += View_RefreshGame;
            Initiate();
            PauseStartSwitcher();
        }

        private void Initiate()
        {
            _view.ClearGrid();
            if (mainGrid == null) mainGrid = new MainGrid(_view);
            else mainGrid.ClearGrid();

            level = 1;
            GameDelay = 410;
            totalScore = 0;
            _view.UpdateLevel(level);
            _view.UpdateScore(totalScore);

            random = new Random(Guid.NewGuid().GetHashCode());
            activeFigure = nextFigure = (byte)random.Next(7);
            tetromino = InitFigure(Settings.canvas.TetrisGrid);
            nextFigure = (byte)random.Next(7);
            InitFigure(Settings.canvas.NextFigureGrid);

            if (dispatcherTimer == null)
            {
                dispatcherTimer = new DispatcherTimer();
                dispatcherTimer.Tick += TetrominoFalling;
            }
            
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, GameDelay);
        }

        private void View_RefreshGame(object sender, EventArgs e)
        {
            Initiate();
        }

        private void View_PauseButton(object sender, EventArgs e)
        {
            PauseStartSwitcher();
        }

        private void PauseStartSwitcher()
        {
            gameOnPause = !gameOnPause;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, GameDelay);
            if (gameOnPause)
            {//removing subscribtion from the keys
                _view.KeyLeft -= MoveLeft;
                _view.KeyRight -= MoveRight;
                _view.KeyDown -= MoveDown;
                _view.KeyUp -= KeyUp;
                _view.SpaceKey -= View_SpaceKey;

                dispatcherTimer.Stop();
            }
            else
            {//subscribe to the keys
                dispatcherTimer.Start(); 
                _view.KeyLeft += MoveLeft;
                _view.KeyRight += MoveRight;
                _view.KeyDown += MoveDown;
                _view.KeyUp += KeyUp;
                _view.SpaceKey += View_SpaceKey;
            }
        }

        private void KeyUp(object sender, EventArgs e)
        {
            if ((!tetromino.ReachTheGround) && (activeFigure != 1))
            {
                tetromino.ClearFigure();
                tetromino.RotateLeft();
                tetromino.DrawFigure(Settings.canvas.TetrisGrid);
            }
            _view.PlayRotateSound();
        }

        private void MoveDown(object sender, EventArgs e)
        {
            tetromino.ClearFigure();
            tetromino.MoveDown();
            tetromino.DrawFigure(Settings.canvas.TetrisGrid);
        }

        private void MoveRight(object sender, EventArgs e)
        {
            if (!tetromino.ReachTheGround)
            {
                tetromino.ClearFigure();
                tetromino.MoveRight();
                tetromino.DrawFigure(Settings.canvas.TetrisGrid);
                _view.PlayMoveingSound();
            }
        }

        private void MoveLeft(object sender, EventArgs e)
        {
            if (!tetromino.ReachTheGround)
            {
                tetromino.ClearFigure();
                tetromino.MoveLeft();
                tetromino.DrawFigure(Settings.canvas.TetrisGrid);
                _view.PlayMoveingSound();
            }
        }

        private void View_SpaceKey(object sender, EventArgs e)
        {
            while (!tetromino.ReachTheGround)
                 TetrominoFalling(this, null);
        }

        public void TetrominoFalling(object sender, object e)
        {
            
            if (tetromino.ReachTheGround)
            {
                int score = (mainGrid.FullLineHandler()) * Settings.Points;
                totalScore += score;
                if (score > 0)
                {
                    _view.FlyingScores(score, tetromino.X, tetromino.Y);
                    _view.PlayDropLineSound();
                }
                level = (byte)(totalScore / (Settings.Points * Settings.LevelDemand) + 1);
                GameDelay = 410 - 40*level;
                dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, GameDelay);
                _view.UpdateLevel(level);
                _view.UpdateScore(totalScore);
                
                tetromino = InitFigure(Settings.canvas.TetrisGrid);
                activeFigure = nextFigure;
                nextFigure = (byte)random.Next(7);
                _view.ClearNextCanvas();
                InitFigure(Settings.canvas.NextFigureGrid);
                changeFigure = false;
            }
            tetromino.ClearFigure();
            tetromino.MoveDown();
            tetromino.DrawFigure(Settings.canvas.TetrisGrid);

            if (tetromino.GameOver)
            {
                if (totalScore > LoginUserInfo.score) LoginUserInfo.score = totalScore;
                dispatcherTimer.Stop();
                _view.GameOver();
            }
        }

        //initiate new figure 
        public Block InitFigure(Settings.canvas canvas)
        {
            Block newTetromino = null;
            if (canvas == Settings.canvas.NextFigureGrid) _view.ClearNextCanvas();
            switch (nextFigure)
            {
                case 0:
                    newTetromino = new Line(_view, mainGrid, canvas);
                    break;
                case 1:
                    newTetromino = new Square(_view, mainGrid, canvas);
                    break;
                case 2:
                    newTetromino = new Tturn(_view, mainGrid, canvas);
                    break;
                case 3:
                    newTetromino = new Lshape(_view, mainGrid, canvas);
                    break;
                case 4:
                    newTetromino = new Sshape(_view, mainGrid, canvas);
                    break;
                case 5:
                    newTetromino = new Zshape(_view, mainGrid, canvas);
                    break;
                case 6:
                    newTetromino = new Jshape(_view, mainGrid, canvas);
                    break;
            }
            return newTetromino;
        }

    }
}
