using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace lab_1
{
    class Glass : Filters
    {
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {            
            int x1 = Clamp((int)(x + ((double)rand.NextDouble() - 0.5) * 10), 0, sourceImage.Width - 1);
            int y1 = Clamp((int)(y + ((double)rand.NextDouble() - 0.5) * 10), 0, sourceImage.Height - 1);
            Color resultColor = sourceImage.GetPixel(x1, y1);

            return resultColor;
        }
    }
}
