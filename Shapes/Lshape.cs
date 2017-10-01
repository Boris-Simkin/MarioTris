using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioTris
{
    class Lshape : Block
    {
        public Lshape(IGameView view, MainGrid grid, Settings.canvas canvas) : base(view, grid)
        {
            Color = 2;
            X = 6; Y = 0;
            Draw(canvas);
            Left.Draw(canvas);
            Right.Draw(canvas);
            Right.Up.Draw(canvas);

        }
    }
}
