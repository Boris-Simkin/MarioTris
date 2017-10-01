using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioTris
{
    public static class Settings
    {
        static public int GridX = 13;
        static public int GridY = 25;
        static public int Points = 200;
        static public int LevelDemand = 4;

        public enum canvas
        {
            TetrisGrid,
            NextFigureGrid
        }
    }
}
