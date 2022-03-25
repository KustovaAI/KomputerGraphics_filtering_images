using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace lab_1
{
    class Waves : Filters
    {
        bool type;
        public Waves(bool ok)
        {
            type = ok;
        }
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            Color resultColor;
            int x1, y1 = y;
            if (type == true)
            {
                //Waves_1
                  y1 = y;
                  x1 = Clamp((int)(x + 20 * Math.Sin(2 * Math.PI * y / 60)), 0, sourceImage.Width - 1); ;
                  resultColor = sourceImage.GetPixel(x1, y1);
            }
            else
            {
                //Waves_2
                x1 = Clamp((int)(x + 20 * Math.Sin(2 * Math.PI * x / 30)), 0, sourceImage.Width - 1);
                resultColor = sourceImage.GetPixel(x1, y1);
            }
            return resultColor;
        }
    }
}
