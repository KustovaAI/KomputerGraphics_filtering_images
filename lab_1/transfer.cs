using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace lab_1
{
    class transfer : Filters
    {
        int x0, y0;
        public transfer(int _a, int _b)
        {
            x0 = _a;
            y0 = _b;
        }
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {            
           // int k = 30, l = 30;

            Color sourceColor;
            if (((x + x0) >= sourceImage.Width) || ((y + y0 >= sourceImage.Height) || (x + x0) < 0) || ((y + y0 ) < 0))
                sourceColor = Color.Black;
            else
            sourceColor = sourceImage.GetPixel(x + x0, y + y0);
            return Color.FromArgb(sourceColor.R, sourceColor.G, sourceColor.B);
        }
    }
}
