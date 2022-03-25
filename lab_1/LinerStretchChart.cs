using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace lab_1
{
    class LinerStretchChart : Filters
    {
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            Color sourceColor = sourceImage.GetPixel(x, y);
            
            return Color.FromArgb(
               Clamp((int)((sourceColor.R - Rmin)* 255 / (Rmax - Rmin) ), 0, 255),
               Clamp((int)((sourceColor.G - Gmin) * 255 / (Gmax - Gmin)), 0, 255),
               Clamp((int)((sourceColor.B - Bmin) * 255 / (Bmax - Bmin)), 0, 255));
        }
    }
}

