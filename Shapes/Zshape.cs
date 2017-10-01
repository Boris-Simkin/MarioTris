using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioTris
{
    class Zshape : Block
    {
        public Zshape(IGameView view, MainGrid grid, Settings.canvas canvas) : base(view, grid)
        {
            Color = 6;
            X = 6; Y = 0;
            Draw(canvas);
            Left.Draw(canvas);
            Down.Draw(canvas);
            Down.Right.Draw(canvas);

        }
    }
}
