using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;
    
namespace lab_1
{
    class Sepia: Filters
    {
       
        
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            Color sourceColor = sourceImage.GetPixel(x, y);
            double Intensity;
            int k = 15;
            Intensity = 0.299 * sourceColor.R + 0.587 * sourceColor.G + 0.114 * sourceColor.B;
            return Color.FromArgb(
               Clamp((int)Intensity + 2 * k, 0, 255),
               Clamp((int)(Intensity + 0.5 * k), 0, 255),
               Clamp((int)Intensity - 1 * k, 0, 255));

        }
    }
}
