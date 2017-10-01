using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioTris
{
    class Tturn : Block
    {
        public Tturn(IGameView view, MainGrid grid, Settings.canvas canvas) : base(view, grid)
        {
            Color = 5;
            X = 6;
            Draw(canvas);
            Right.Draw(canvas);
            Left.Draw(canvas);
            Down.Draw(canvas);

        }
    }
}
