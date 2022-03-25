using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace lab_1
{
    class Ideal_reflector : Filters
    {

        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {

            Color sourceColor = sourceImage.GetPixel(x, y);
            return Color.FromArgb(
               Clamp((int)(sourceColor.R * 255 / Rmax), 0, 255),
               Clamp((int)(sourceColor.G * 255 / Gmax), 0, 255),
               Clamp((int)(sourceColor.B * 255 / Bmax), 0, 255));

        }
    }
}
