using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace lab_1
{

    class Maksimum : Filters
    {
        protected float[,] kernel = null;
        public Maksimum()
        {
            int sizeX = 5;
            int sizeY = 5;
            kernel = new float[sizeX, sizeY];

        }

        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            int radiusX = kernel.GetLength(0) / 2;
            int radiusY = kernel.GetLength(1) / 2;

            float[] arr1 = new float[25];
            float[] arr2 = new float[25];
            float[] arr3 = new float[25];
            for (int i = -radiusY; i <= radiusY; i++)
                for (int k = -radiusX; k <= radiusX; k++)
                {

                    int idX = Clamp(x + k, 0, sourceImage.Width - 1);
                    int idY = Clamp(y + i, 0, sourceImage.Height - 1);
                    Color neighborColor = sourceImage.GetPixel(idX, idY);
                    arr1[(k + radiusX) + (i + radiusY) * 5] = neighborColor.R;
                    arr2[(k + radiusX) + (i + radiusY) * 5] = neighborColor.G;
                    arr3[(k + radiusX) + (i + radiusY) * 5] = neighborColor.B;
                    
                    

                }

            Array.Sort(arr1);
            Array.Sort(arr2);
            Array.Sort(arr3);
            return Color.FromArgb(
                Clamp((int)arr1[24], 0, 255),
                Clamp((int)arr2[24], 0, 255),
                Clamp((int)arr3[24], 0, 255));
        }
    }
}
