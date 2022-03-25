using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.ComponentModel;
namespace lab_1
{
    class Grad : MathMorfology
    {
        public override Bitmap processImage1(Bitmap sourceImage, byte[,] sourceBit, byte[,] mask, int MW, int MH, BackgroundWorker worker)
        {            
            if (worker.CancellationPending)
                return null;            //
            
            for (int i = 0; i < sourceImage.Width; i++)
            {
                worker.ReportProgress((int)((float)i / sourceImage.Width * 100));
                for (int j = 0; j < sourceImage.Height; j++)
                {
                }
            }

            return sourceImage;
        }

        public override Bitmap processImage1(Bitmap sourceImage, byte[,] sourceBit, byte[,] mask, int MW, int MH)
        {
            
            return sourceImage;
        }
    }
}
