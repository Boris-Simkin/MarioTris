using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioTris
{
    class Square : Block
    {
        public Square(IGameView view, MainGrid grid, Settings.canvas canvas) : base(view, grid)
        {
            Color = 3;
            X = 6;
            Draw(canvas);
            Right.Draw(canvas);
            Down.Draw(canvas);
            Down.Right.Draw(canvas);

        }
    }
}
