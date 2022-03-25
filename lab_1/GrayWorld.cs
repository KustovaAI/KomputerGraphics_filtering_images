using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace lab_1
{
    class GrayWorld: Filters
    {
        
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            
            Color sourceColor = sourceImage.GetPixel(x, y);
            return Color.FromArgb(
               Clamp((int)(sourceColor.R * Avg / R1), 0, 255),
               Clamp((int)(sourceColor.G * Avg / G1), 0, 255),
               Clamp((int)(sourceColor.B * Avg / B1), 0, 255));
        }
    }
}
