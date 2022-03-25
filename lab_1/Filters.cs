using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.ComponentModel;

namespace lab_1
{
	abstract class Filters
	{
        public
            Random rand = new Random();
        public    double Avg;
        public double R1, G1, B1, Rmax = 0, Gmax = 0, Bmax = 0, Rmin = 255, Gmin = 255, Bmin = 255;
        public double Brightmax = 0, Brightmin = 255;
        public Form1 f = new Form1();

        protected abstract Color calculateNewPixelColor(Bitmap sourceImage, int x, int y);

		public Bitmap processImage(Bitmap sourceImage, BackgroundWorker worker, double _Avg, double _R1, double _G1, double _B1, double _Rmax, double _Gmax, double _Bmax, double _Rmin, double _Gmin, double _Bmin)
		{

			Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height);
			for (int i = 0; i < sourceImage.Width; i++)
			{
                worker.ReportProgress((int)((float)i / resultImage.Width * 100));
                if (worker.CancellationPending)
                    return null;
                Avg = _Avg; R1 = _R1; G1 = _G1; B1 = _B1; Rmax = _Rmax; Gmax = _Gmax; Bmax = _Bmax; Rmin = _Rmin; Gmin = _Gmin; Bmin = _Bmin;
                //
				for (int j = 0; j < sourceImage.Height; j++)
				{
					resultImage.SetPixel(i, j, calculateNewPixelColor(sourceImage, i, j));
				}
			}
			return resultImage;
		}

        

        public Bitmap processImage(Bitmap sourceImage)
        {
            Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height);
            for (int i = 0; i < sourceImage.Width; i++)
            {
                for (int j = 0; j < sourceImage.Height; j++)
                {
                    resultImage.SetPixel(i, j, calculateNewPixelColor(sourceImage, i, j));
                }
            }
            return resultImage;
        }

            public int Clamp(int value, int min, int max)
		{
			if (value < min)
				return min;
			if (value > max)
				return max;
			return value;
		}

        

	}


}
