using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace lab_1
{
    class GrayScale : Filters
    {
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            
            Color sourceColor = sourceImage.GetPixel(x, y);
            double Intensity;
            Intensity = 0.299 * sourceColor.R + 0.587 * sourceColor.G + 0.114 * sourceColor.B;
            return Color.FromArgb(
               Clamp((int)Intensity, 0, 255),
               Clamp((int)Intensity, 0, 255),
               Clamp((int)Intensity, 0, 255));

        }
    }
}
