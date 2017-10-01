using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace MarioTris
{
    class Block
    {
        private readonly IGameView _view;
        private MainGrid _grid;
        public bool ReachTheGround;

        //constructor
        public Block(IGameView view, MainGrid grid)
        {
            _grid = grid;
            _view = view;
        }

        //the coordinates of each block and the figure
        public int X { get; set; }
        public int Y { get; set; }

        public byte Color { get; set; }
        public bool GameOver { get; private set; }

        private Block parent;
        private Block left, right, up, down;
        private static bool movable = true;

        private void CopyShapeToTheGrid()
        {
            if (left != null) left.CopyShapeToTheGrid();
            if (right != null) right.CopyShapeToTheGrid();
            if (up != null) up.CopyShapeToTheGrid();
            if (down != null) down.CopyShapeToTheGrid();
            _grid.Grid[X, Y] = Color;
        }

        public void DrawFigure(Settings.canvas canvas)
        {
            if (left != null) left.DrawFigure(canvas);
            if (right != null) right.DrawFigure(canvas);
            if (up != null) up.DrawFigure(canvas);
            if (down != null) down.DrawFigure(canvas);
            _view.DrawSquare(X, Y, Color, canvas);
        }

        public void ClearFigure()
        {
            if (left != null) left.ClearFigure();
            if (right != null) right.ClearFigure();
            if (up != null) up.ClearFigure();
            if (down != null) down.ClearFigure();
            _view.ClearSquare();
        }

        #region figure movement
        public void MoveRight()
        {

            if (left != null) left.MoveRight();
            if (right != null) right.MoveRight();
            if (up != null) up.MoveRight();
            if (down != null) down.MoveRight();
            X++;
            if ((X > 12) || (_grid.Grid[X, Y] != 0)) movable = false;
            if ((parent == null) && !(movable))
            {
                movable = true;
                MoveLeft();
            }
        }

        public void MoveLeft()
        {
            if (left != null) left.MoveLeft();
            if (right != null) right.MoveLeft();
            if (up != null) up.MoveLeft();
            if (down != null) down.MoveLeft();
            X--;
            if ((X < 0) || (_grid.Grid[X,Y] != 0)) movable = false;

            if ((parent == null) && ! (movable))
            {
                movable = true;
                MoveRight();
            }
        }

        private void MoveUp()
        {
            if (left != null) left.MoveUp();
            if (right != null) right.MoveUp();
            if (up != null) up.MoveUp();
            if (down != null) down.MoveUp();
            Y--;
        }

        public void MoveDown()
        {
            if (left != null) left.MoveDown();
            if (right != null) right.MoveDown();
            if (up != null) up.MoveDown();
            if (down != null) down.MoveDown();
            Y++;

            if (Y > Settings.GridY - 1) movable = false;
            else if (_grid.Grid[X, Y] != 0) movable = false;
  
            if ((parent == null) && !(movable))
            {
                if (Y <= 1) GameOver = true;
                movable = true;
                MoveUp();
                ReachTheGround = true;
                CopyShapeToTheGrid();
            }
        }
        

        public void RotateLeft()
        {
            if (left != null)
            {
                left.X = X;
                left.Y = Y + 1;
                left.RotateLeft();
            }
            if (right != null)
            {
                right.X = X;
                right.Y = Y - 1;
                right.RotateLeft();
            }
            if (up != null)
            {
                up.X = X - 1;
                up.Y = Y;
                up.RotateLeft();
            }
            if (down != null)
            {
                down.X = X + 1;
                down.Y = Y;
                down.RotateLeft();
            }
            Block temp = up;
            up = left;
            left = down;
            down = right;
            right = temp;
            if ((X < 0) || (Y < 0) || (X > Settings.GridX - 1)) movable = false;
            else if ((_grid.Grid[X, Y] != 0)) movable = false;
            if ((parent == null) && !(movable))
            {
                movable = true;
                RotateRihgt();
            }
        }

        public void RotateRihgt()
        {
            if (left != null)
            {
                left.X = X;
                left.Y = Y - 1;
                left.RotateRihgt();
            }
            if (right != null)
            {
                right.X = X;
                right.Y = Y + 1;
                right.RotateRihgt();
            }
            if (up != null)
            {
                up.X = X + 1;
                up.Y = Y;
                up.RotateRihgt();
            }
            if (down != null)
            {
                down.X = X - 1;
                down.Y = Y;
                down.RotateRihgt();
            }
            Block temp = up;
            up = right;
            right = down;
            down = left;
            left = temp;

            if ((X < 0) || (X > 12) || (Y < 0) || (_grid.Grid[X, Y] != 0)) movable = false;
            if ((parent == null) && !(movable))
            {
                movable = true;
                RotateLeft();
            }
        }
        #endregion

        #region nodes
        public Block Left
        {
            get
            {
                if (left == null)
                {
                    left = new Block(_view, _grid);
                    left.parent = this;
                    left.Color = Color;
                    left.X = X - 1;
                    left.Y = Y;
                }
                return left;
            }
        }

        public Block Right
        {
            get
            {
                if (right == null)
                {
                    right = new Block(_view, _grid);
                    right.Color = Color;
                    right.parent = this;
                    right.X = X + 1;
                    right.Y = Y;
                }
                return right;
            }
        }
        public Block Up
        {
            get
            {
                if (up == null)
                {
                    up = new Block(_view, _grid);
                    up.Color = Color;
                    up.parent = this;
                    up.X = X;
                    up.Y = Y - 1;
                }
                return up;
            }
        }      
        public Block Down
        {
            get
            {
                if (down == null)
                {
                    down = new Block(_view, _grid);
                    down.Color = Color;
                    down.parent = this;
                    down.X = X;
                    down.Y = Y + 1;
                }
                return down;
            }
        }
        #endregion

        //Drawing one block on the tetris canvas
        public void Draw(Settings.canvas canvas)
        {
            _view.DrawSquare(X, Y, Color, canvas);
        }

    }
}
