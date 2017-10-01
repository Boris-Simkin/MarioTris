using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioTris
{
    class Jshape : Block
    {
        public Jshape(IGameView view, MainGrid grid, Settings.canvas canvas) : base(view, grid)
        {
            Color = 7;
            X = 6; Y = 0;
            Draw(canvas);
            Left.Draw(canvas);
            Right.Draw(canvas);
            Right.Down.Draw(canvas);

        }
    }
}
