using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace lab_1
{
    class Motion_blur : MatrixFilter
    {
        public Motion_blur()
        {
            int sizeX = 9;
            int sizeY = 9;
            kernel = new float[sizeX, sizeY];
            for (int i = 0; i < sizeX; i++)
                for (int j = 0; j < sizeY; j++)
                    if (i == j)
                        kernel[i, j] = 1.0f / (float)(sizeX);
            else kernel[i, j] = 0;
        }
    }
}
