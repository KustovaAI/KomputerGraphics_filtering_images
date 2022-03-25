using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.ComponentModel;

namespace lab_1
{
    abstract class MathMorfology
    {
        public abstract Bitmap processImage1(Bitmap sourceImage, byte[,] sourceBit, byte[,] mask, int MW, int MH, BackgroundWorker worker);
        public abstract Bitmap processImage1(Bitmap sourceImage, byte[,] sourceBit, byte[,] mask, int MW, int MH);

    }
}
