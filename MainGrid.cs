using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioTris
{
    class MainGrid
    {
        private readonly IGameView _view;
        public byte[,] _grid;
        int _xLength, _yLength;

        public MainGrid(IGameView view)
        {
            _xLength = Settings.GridX;
            _yLength = Settings.GridY;
            _view = view;

            _grid = new byte[_xLength, _yLength];

        }

        public void ClearGrid()
        {
            for (int i = 0; i < _xLength; i++)
            {
                for (int j = 0; j < _yLength; j++)
                {
                    _grid[i, j] = 0;
                }
            }
        }

        //This array holds all the blocks
        public byte[,] Grid
        {
            get
            {
                return _grid;
            }

        }

        private void EliminateLine(int line)
        {
            for (int y = line; y > 1; y--)
            {
                for (int x = 0; x < _xLength; x++)
                {
                    _grid[x, y] = _grid[x, y - 1];
                }
            }
        }

        //handle the full line case and return a number of destroyed lines
        public int FullLineHandler()
        {   
            bool missingSquare, refreshGrid = false;
            byte lines = 0;
            for (int y = 0; y < _yLength; y++)
            {
                missingSquare = false;
                for (int x = 0; x < _xLength; x++)
                    if (_grid[x, y] == 0) missingSquare = true;
                if (!missingSquare)
                {
                    EliminateLine(y);
                    refreshGrid = true;
                    lines++;
                }
            }

            if (refreshGrid)
            {
                _view.ClearGrid();
                for (int y = 0; y < _yLength; y++)
                    for (int x = 0; x < _xLength; x++)
                        _view.DrawSquare(x, y, _grid[x, y], Settings.canvas.TetrisGrid);
            }

            return lines;
        }

    }
}
