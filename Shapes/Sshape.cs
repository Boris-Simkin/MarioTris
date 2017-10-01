using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioTris
{
    class Sshape : Block
    {
        public Sshape(IGameView view, MainGrid grid, Settings.canvas canvas) : base(view, grid)
        {
            Color = 4;
            X = 6;
            Draw(canvas);
            Right.Draw(canvas);
            Down.Draw(canvas);
            Down.Left.Draw(canvas);

        }
    }
}
