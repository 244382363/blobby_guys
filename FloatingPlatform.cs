using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blobby_guys
{
    class FloatingPlatform : StaticGraphic
    {
        public Rectangle Surface;
        public FloatingPlatform(Texture2D txr, int xpos, int ypos):
            base(txr, xpos, ypos)
        {
            Surface = new Rectangle(xpos + 4, ypos, txr.Width - 8, 3);
        }
    }
    
    
}
