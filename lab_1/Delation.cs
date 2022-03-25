using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.ComponentModel;
namespace lab_1
{
    class Delation : MathMorfology
    {
        public override Bitmap processImage1(Bitmap sourceImage, byte[,] sourceBit, byte[,] mask, int MW, int MH, BackgroundWorker worker)
        {

            byte[,] resultBite = new byte[sourceImage.Width, sourceImage.Height];
            Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height);
            // for (int i = 0; i < Weight; i++)
            // {
            //   
            if (worker.CancellationPending)
                return null;

            //
            int yk = sourceImage.Height - (MH / 2);
            int xk = sourceImage.Width - (MW / 2);
            for (int y = MH / 2; y < yk; y++)
                for (int x = MW / 2; x < xk; x++)
                {
                    byte max = 0;
                    for (int j = -MH / 2; j <= MH / 2; j++)
                    {

                        for (int i = -MW / 2; i <= MW / 2; i++)
                            if ((mask[i + 1, j + 1] == 1) && (sourceBit[x + i, y + j] > max))
                            {
                                max = sourceBit[x + i, y + j];
                            }
                        resultBite[x, y] = max;
                    }
                }
            for (int i = 0; i < sourceImage.Width; i++)
            {
                worker.ReportProgress((int)((float)i / sourceImage.Width * 100));
                for (int j = 0; j < sourceImage.Height; j++)
                {
                    if (resultBite[i, j] == 1)
                        resultImage.SetPixel(i, j, Color.Black);
                    else resultImage.SetPixel(i, j, Color.White);
                }
            }

            return resultImage;
        }

        public override Bitmap processImage1(Bitmap sourceImage, byte[,] sourceBit, byte[,] mask, int MW, int MH)
        {

            byte[,] resultBite = new byte[sourceImage.Width, sourceImage.Height];
            Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height);
            
            int yk = sourceImage.Height - (MH / 2);
            int xk = sourceImage.Width - (MW / 2);
            for (int y = MH / 2; y < yk; y++)
                for (int x = MW / 2; x < xk; x++)
                {
                    byte max = 0;
                    for (int j = -MH / 2; j <= MH / 2; j++)
                    {

                        for (int i = -MW / 2; i <= MW / 2; i++)
                            if ((mask[i + 1, j + 1] == 1) && (sourceBit[x + i, y + j] > max))
                            {
                                max = sourceBit[x + i, y + j];
                            }
                        resultBite[x, y] = max;
                    }
                }
            for (int i = 0; i < sourceImage.Width; i++)
            {
                
                for (int j = 0; j < sourceImage.Height; j++)
                {
                    if (resultBite[i, j] == 1)
                        resultImage.SetPixel(i, j, Color.Black);
                    else resultImage.SetPixel(i, j, Color.White);
                }
            }

            return resultImage;
        }
    }
}
