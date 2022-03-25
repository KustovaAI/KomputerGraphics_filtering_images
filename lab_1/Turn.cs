using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace lab_1
{
    class Turn : Filters
    {
        int x0, y0;
        double m;
        public Turn(int _a, int _b, double _c)
        {
            x0 = _a;
            y0 = _b;
            m = _c;
        }
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
           // int x0, y0;
           // double m;
           
           // m = Math.PI / 2;
            Color sourceColor;
            if (((x - x0) * Math.Cos(m) - (y - y0) * Math.Sin(m) + x0 >= sourceImage.Width) || (((x - x0) * Math.Sin(m) + (y - y0) * Math.Cos(m) + y0 >= sourceImage.Height)) || ((x - x0) * Math.Cos(m) - (y - y0) * Math.Sin(m) + x0 <= 0) || ((x - x0) * Math.Sin(m) + (y - y0) * Math.Cos(m) + y0 <= 0))
                sourceColor = Color.Black;
            else
                sourceColor = sourceImage.GetPixel((int)((x - x0) * Math.Cos(m) - (y - y0) * Math.Sin(m) + x0), (int)((x - x0) * Math.Sin(m) + (y - y0) * Math.Cos(m) + y0));
            return Color.FromArgb(sourceColor.R, sourceColor.G, sourceColor.B);
        }
    }
}
