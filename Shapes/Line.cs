using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioTris
{

    class Line : Block
    {
        public Line(IGameView view, MainGrid grid, Settings.canvas canvas) : base(view, grid)
        {
            Color = 1;
            X = 6;
            Y = 0;
            Draw(canvas);
            Left.Draw(canvas);
            Right.Draw(canvas);
            Right.Right.Draw(canvas);

        }
    }
}
