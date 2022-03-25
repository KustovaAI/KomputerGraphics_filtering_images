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
    

    public partial class Form1 : Form
	{
        public byte [,] pic;
        public byte[,] Mask;
        int MW, MH;
        public double _Avg = 0;
        public double _R1 = 0, _G1 = 0, _B1 = 0, _Rmax = 0, _Gmax = 0, _Bmax = 0, _Rmin = 255, _Gmin = 255, _Bmin = 255;
        Bitmap image;
        int k = 0;
        List<Bitmap> stack = new List<Bitmap>();
        List<Bitmap> stack2 = new List<Bitmap>();
        string name1, name2;
        
        public Form1()
		{
			InitializeComponent();
		}

		private void openToolStripMenuItem_Click(object sender, EventArgs e)    //открытие
		{
			OpenFileDialog dialog = new OpenFileDialog();
			dialog.Filter = "Image files|*.png;*.jpg;*.bmp|All files(*.*)|*.*";
			if (dialog.ShowDialog() == DialogResult.OK)
			{
				image = new Bitmap(dialog.FileName);
				pictureBox1.Image = image;
				pictureBox1.Refresh();
                pictureBox2.Image = image;
                pictureBox2.Refresh();
                stack.Clear();
                stack.Add(image);
            }
		}

        

        private void инверсияToolStripMenuItem_Click(object sender, EventArgs e)    //Инверсия
        {
            if (stack.Count == 0) MessageBox.Show("Загрузите изображение");
            else
            {
                _Sokritie();
                Filters filter = new InvertFilter();
                backgroundWorker1.RunWorkerAsync(filter);
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            _Avg = Parametr(ref _R1, ref _G1, ref _B1, ref _Rmax, ref _Gmax, ref _Bmax, ref _Rmin, ref _Gmin, ref _Bmin);
            Bitmap newImage = ((Filters)e.Argument).processImage(image, backgroundWorker1, _Avg, _R1, _G1, _B1, _Rmax, _Gmax, _Bmax, _Rmin, _Gmin, _Bmin);
            if (backgroundWorker1.CancellationPending != true)
                image = newImage;
                k = 0;
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled)
            {
                pictureBox1.Image = image;
                pictureBox1.Refresh();
                stack.Add(image);
                name2 = name1;
            }
            progressBar1.Value = 0;
           
        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            Bitmap newImage = ((MathMorfology)e.Argument).processImage1(image, pic, Mask, MW, MH, backgroundWorker2);
            if (backgroundWorker2.CancellationPending != true)
                image = newImage;
        }

        private void backgroundWorker2_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled)
            {
                pictureBox1.Image = image;
                pictureBox1.Refresh();
                stack.Add(image);
                name2 = name1;
            }
            progressBar1.Value = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            backgroundWorker1.CancelAsync();
        }


        private void размытиеToolStripMenuItem_Click(object sender, EventArgs e)    //размытие
        {
            if (stack.Count == 0) MessageBox.Show("Загрузите изображение");
            else
            {
                _Sokritie();
                Filters filter = new BlurFilter();
                backgroundWorker1.RunWorkerAsync(filter);
            }
        }

        private void фильтрГауссаToolStripMenuItem_Click(object sender, EventArgs e)    //фильтр гауса
        {
            if (stack.Count == 0) MessageBox.Show("Загрузите изображение");
            else
            {
                _Sokritie();
                Filters filter = new GaussianFilter();
                backgroundWorker1.RunWorkerAsync(filter);
            }
        }

        private void сепияToolStripMenuItem_Click(object sender, EventArgs e)       //фильтр GrayScale
        {
            if (stack.Count == 0) MessageBox.Show("Загрузите изображение");
            else
            {
                _Sokritie();
                Filters filter = new GrayScale();
                backgroundWorker1.RunWorkerAsync(filter);
            }
        }

        private void сепияToolStripMenuItem1_Click(object sender, EventArgs e)      //фильтр сепия
        {
            if (stack.Count == 0) MessageBox.Show("Загрузите изображение");
            else
            {
                _Sokritie();
                Filters filter = new Sepia();
                backgroundWorker1.RunWorkerAsync(filter);
                
            }
        }

        private void яркостьToolStripMenuItem_Click(object sender, EventArgs e)     //Яркость
        {
            if (stack.Count == 0) MessageBox.Show("Загрузите изображение");
            else
            {
                _Sokritie();
                Filters filter = new Brightness();
                backgroundWorker1.RunWorkerAsync(filter);
            }
        }

        

        private void тиснениеToolStripMenuItem_Click(object sender, EventArgs e)    //Тиснение
        {
            if (stack.Count == 0) MessageBox.Show("Загрузите изображение");
            else
            {
                _Sokritie();
                Filters filter = new GrayScale();
                Bitmap newImage = (filter.processImage(image));
                image = newImage;
                filter = new TisnenieFilter();
                backgroundWorker1.RunWorkerAsync(filter);
            }
        }

        private void переносToolStripMenuItem_Click(object sender, EventArgs e)     //Перенос
        {
            if (stack.Count == 0) MessageBox.Show("Загрузите изображение");
            else
            {
                _Sokritie();
                label1.Text = "Введите смещение по x и по y в пикселях";
                label1.Visible = true;
                label2.Visible = true;
                label3.Visible = true;
                textBox1.Visible = true;
                textBox2.Visible = true;
                button2.Visible = true;
            }
            
        }

        private void поворотToolStripMenuItem_Click(object sender, EventArgs e)     //поворот
        {
            if (stack.Count == 0) MessageBox.Show("Загрузите изображение");
            else
            {
                _Sokritie();
                label1.Text = "Введите центр поворота (x, y) в пикселях";
                label4.Visible = true;
                textBox3.Visible = true;
                label1.Visible = true;
                label2.Visible = true;
                label3.Visible = true;
                textBox1.Visible = true;
                textBox2.Visible = true;
                button3.Visible = true;
            }
            
        }


        private void button2_Click(object sender, EventArgs e)      //ок перенос
        {
            label1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            textBox1.Visible = false;
            textBox2.Visible = false;
            button2.Visible = false;
            int a = 0, b = 0;
            try
            {
                a = Convert.ToInt32(textBox1.Text);
                b = Convert.ToInt32(textBox2.Text);
            }
            catch
            {
                MessageBox.Show("Введите корректные данные", "Ошибка",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Filters filter = new transfer(a, b);
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void button3_Click(object sender, EventArgs e)      //ок поворот
        {
            label1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            textBox1.Visible = false;
            textBox2.Visible = false;
            button3.Visible = false;
            label4.Visible = false;
            textBox3.Visible = false;
            int a = 0, b = 0;
            double c = 0;
            try
            {
                a = Convert.ToInt32(textBox1.Text);
                b = Convert.ToInt32(textBox2.Text);
                c = (Convert.ToInt32(textBox3.Text)) * Math.PI / 180;
            }
            catch
            {
                MessageBox.Show("Введите корректные данные", "Ошибка",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Filters filter = new Turn(a, b, c);
            backgroundWorker1.RunWorkerAsync(filter);
        }


        private void стеклоToolStripMenuItem_Click(object sender, EventArgs e)      //Стекло
        {
            if (stack.Count == 0) MessageBox.Show("Загрузите изображение");
            else
            {
                _Sokritie();
                Filters filter = new Glass();
                backgroundWorker1.RunWorkerAsync(filter);
            }
        }

        private void резкостьToolStripMenuItem_Click(object sender, EventArgs e)        //Резкость
        {
            if (stack.Count == 0) MessageBox.Show("Загрузите изображение");
            else
            {
                _Sokritie();
                Filters filter = new sharpness();
                backgroundWorker1.RunWorkerAsync(filter);
            }
        }

        private void motionBlurToolStripMenuItem_Click(object sender, EventArgs e)      //Motion blur
        {
            if (stack.Count == 0) MessageBox.Show("Загрузите изображение");
            else
            {
                _Sokritie();
                Filters filter = new Motion_blur();
                backgroundWorker1.RunWorkerAsync(filter);
            }
        }

        private void поОХToolStripMenuItem_Click(object sender, EventArgs e)        //Sobela ox
        {
            if (stack.Count == 0) MessageBox.Show("Загрузите изображение");
            else
            {
                _Sokritie();
                Filters filter = new GrayScale();
                Bitmap newImage = (filter.processImage(image));
                image = newImage;
                filter = new Sobela(true);
                backgroundWorker1.RunWorkerAsync(filter);
            }
        }

        private void поОYToolStripMenuItem_Click(object sender, EventArgs e)        //Sobela oy
        {
            if (stack.Count == 0) MessageBox.Show("Загрузите изображение");
            else
            {
                _Sokritie();
                Filters filter = new GrayScale();
                Bitmap newImage = (filter.processImage(image));
                image = newImage;
                filter = new Sobela(false);
                backgroundWorker1.RunWorkerAsync(filter);
            }
        }

        private void поОсиOXToolStripMenuItem_Click(object sender, EventArgs e)     //Shar ox
        {
            if (stack.Count == 0) MessageBox.Show("Загрузите изображение");
            else
            {
                _Sokritie();
                Filters filter = new GrayScale();
                Bitmap newImage = (filter.processImage(image));
                image = newImage;
                filter = new Sharr(true);
                backgroundWorker1.RunWorkerAsync(filter);
            }
        }

        private void поОсиOYToolStripMenuItem_Click(object sender, EventArgs e)     //Sharr oy
        {
            if (stack.Count == 0) MessageBox.Show("Загрузите изображение");
            else
            {
                _Sokritie();
                Filters filter = new GrayScale();
                Bitmap newImage = (filter.processImage(image));
                image = newImage;
                filter = new Sharr(false);
                backgroundWorker1.RunWorkerAsync(filter);
            }
        }

        private void поOXToolStripMenuItem_Click(object sender, EventArgs e)        //Pruit ox
        {
            if (stack.Count == 0) MessageBox.Show("Загрузите изображение");
            else
            {
                _Sokritie();
                Filters filter = new GrayScale();
                Bitmap newImage = (filter.processImage(image));
                image = newImage;
                filter = new Pruitt(true);
                backgroundWorker1.RunWorkerAsync(filter);
            }
        }

        private void поOYToolStripMenuItem_Click(object sender, EventArgs e)        //Pruit oy
        {
            if (stack.Count == 0) MessageBox.Show("Загрузите изображение");
            else
            {
                _Sokritie();
                Filters filter = new GrayScale();
                Bitmap newImage = (filter.processImage(image));
                image = newImage;
                filter = new Pruitt(false);
                backgroundWorker1.RunWorkerAsync(filter);
            }
        }

        private void сокранитьКакToolStripMenuItem_Click(object sender, EventArgs e)        //сохранение
        {
            SaveFileDialog savedialog = new SaveFileDialog();
            savedialog.Title = "Сохранить картинку как...";
            //отображать ли предупреждение, если пользователь указывает имя уже существующего файла
            savedialog.OverwritePrompt = true;
            //отображать ли предупреждение, если пользователь указывает несуществующий путь
            savedialog.CheckPathExists = true;
            //список форматов файла, отображаемый в поле "Тип файла"
            savedialog.Filter = "Image Files(*.BMP)|*.BMP|Image Files(*.JPG)|*.JPG|Image Files(*.GIF)|*.GIF|Image Files(*.PNG)|*.PNG|All files (*.*)|*.*";
            //отображается ли кнопка "Справка" в диалоговом окне
            savedialog.ShowHelp = true;
            if (savedialog.ShowDialog() == DialogResult.OK) //если в диалоговом окне нажата кнопка "ОК"
            {
                try
                {
                    image.Save(savedialog.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                }
                catch
                {
                    MessageBox.Show("Невозможно сохранить изображение", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)      //отмена последнего действия
        {
            if (stack.Count() > 1)
            {
                stack2.Add(stack[stack.Count() - 1]);
                stack.RemoveAt(stack.Count() - 1);
                image = stack[stack.Count() - 1];
                pictureBox1.Image = image;
                pictureBox1.Refresh();
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)      //повтор последнего действия
        {
            if (stack2.Count() > 0)
            {
                stack.Add(stack2[stack2.Count() - 1]);
                image = stack2[stack2.Count() - 1];
                stack2.RemoveAt(stack2.Count() - 1);
                pictureBox1.Image = image;
                pictureBox1.Refresh();
            }
        }

        private void серыйМирToolStripMenuItem_Click(object sender, EventArgs e)    //серый мир
        {
            if (stack.Count == 0) MessageBox.Show("Загрузите изображение");
            else
            {
                _Sokritie();                
                Filters filter = new GrayWorld();
                backgroundWorker1.RunWorkerAsync(filter);
            }
            
        }

        private void видToolStripMenuItem_Click(object sender, EventArgs e) //волны 1 вид
        {
            if (stack.Count == 0) MessageBox.Show("Загрузите изображение");
            else
            {
                _Sokritie();
                Filters filter = new Waves(true);
                backgroundWorker1.RunWorkerAsync(filter);
            }
        }
                

        private void видToolStripMenuItem1_Click(object sender, EventArgs e)    //волны вид2
        {
            if (stack.Count == 0) MessageBox.Show("Загрузите изображение");
            else
            {
                _Sokritie();
                Filters filter = new Waves(false);
                backgroundWorker1.RunWorkerAsync(filter);
            }
        }

       
        private void медианныйToolStripMenuItem_Click(object sender, EventArgs e)       //медианный фильтр
        {
            if (stack.Count == 0) MessageBox.Show("Загрузите изображение");
            else
            {
                _Sokritie();
                Filters filter = new Median();
                backgroundWorker1.RunWorkerAsync(filter);
            }
        }
       
        private void светящиесяКраяToolStripMenuItem_Click(object sender, EventArgs e)      //светящиеся края
        {
            _Sokritie();
            Filters filter = new Median();
            Bitmap newImage = (filter.processImage(image));
            image = newImage;

            filter = new Sobela(true);
            newImage = (filter.processImage(image));
            image = newImage;

            filter = new Maksimum();
            newImage = (filter.processImage(image));
            image = newImage;
            backgroundWorker1.RunWorkerAsync(filter);

        }

        private void линейноеРастяжениеГистограммыToolStripMenuItem_Click(object sender, EventArgs e)   //линейное растяжение гистограммы
        {
            if (stack.Count == 0) MessageBox.Show("Загрузите изображение");
            else
            {
                _Sokritie();                
                Filters filter = new LinerStretchChart();
                backgroundWorker1.RunWorkerAsync(filter);
            }
        }

        private void идеальныйОтражательToolStripMenuItem_Click(object sender, EventArgs e)     //идеальный отражатель
        {
            if (stack.Count == 0) MessageBox.Show("Загрузите изображение");
            else
            {
                _Sokritie();                
                Filters filter = new Ideal_reflector();
                backgroundWorker1.RunWorkerAsync(filter);
            }
        }

        //подсчёт параметров изображения (максимальные, минимальные, средние значения)
        public double Parametr( ref double  R1, ref double G1, ref double B1, ref double Rmax, ref double Gmax, ref double Bmax, ref double Rmin, ref double Gmin, ref double Bmin)
        {
            double SumR = 0;
            double SumG = 0;
            double SumB = 0;
            double Avg;
            Rmax = 0;
            Gmax = 0;
            Bmax = 0; Rmin = 255; Gmin = 255; Bmin = 255;
            int n;
            for (int o = 0; o < image.Width; o++)
                for (int l = 0; l < image.Height; l++)
                {
                    SumR += image.GetPixel(o, l).R;
                    SumG += image.GetPixel(o, l).G;
                    SumB += image.GetPixel(o, l).B;
                    if (image.GetPixel(o, l).R > Rmax)
                        Rmax = image.GetPixel(o, l).R;
                    if (image.GetPixel(o, l).G > Gmax)
                        Gmax = image.GetPixel(o, l).G;
                    if (image.GetPixel(o, l).B > Bmax)
                        Bmax = image.GetPixel(o, l).B;

                    if (image.GetPixel(o, l).R < Rmin)
                        Rmin = image.GetPixel(o, l).R;
                    if (image.GetPixel(o, l).G < Gmin)
                        Gmin = image.GetPixel(o, l).G;
                    if (image.GetPixel(o, l).B < Bmin)
                        Bmin = image.GetPixel(o, l).B;

                }
            n = image.Width * image.Height;
            R1 = SumR / n;
            G1 = SumG / n;
            B1 = SumB / n;
            Avg = (R1 + G1 + B1) / 3;
            return Avg;
        }

        public void To_Byte()       //перевод картинки в битовое поле
        {
            pic = new byte[image.Width, image.Height];
            for (int i = 0; i <  image.Width; i++)
                for (int j = 0; j < image.Height; j++)
                {
                    if ((image.GetPixel(i, j).R <=50))
                        pic[i,j] = 1;
                    else pic[i,j] = 0;
                }
        }

        public void InitMask(int _MW, int _MH)      //инициализация маски
        {
            MW = _MW;
            MH = _MH;
            Mask = new byte[MH, MH];
            for (int i = 0; i < MW; i++)
                for (int j = 0; j < MH; j++)
                {
                        Mask[i, j] = 1;
                   
                }
        }

        private void delationToolStripMenuItem_Click(object sender, EventArgs e)        //delation
        {            
            if (stack.Count == 0) MessageBox.Show("Загрузите изображение");
            else
            {
                _Sokritie();
                To_Byte();
                InitMask(3, 3);
                VvodMatrix1();
                button5.Visible = true;
            }

        }

        private void erosionToolStripMenuItem_Click(object sender, EventArgs e)     //Erosion
        {
            if (stack.Count == 0) MessageBox.Show("Загрузите изображение");
            else
            {
                _Sokritie();
                To_Byte();
                InitMask(3, 3);
                VvodMatrix1();
                button4.Visible = true;
            }
        }

        private void openingToolStripMenuItem_Click(object sender, EventArgs e)     //Opening
        {
            if (stack.Count == 0) MessageBox.Show("Загрузите изображение");
            else
            {
                _Sokritie();
                To_Byte();
                InitMask(3, 3);
                VvodMatrix1();
                button6.Visible = true;

            }
        }
        
        private void gradientToolStripMenuItem_Click(object sender, EventArgs e)        //Grad
        {
            _Sokritie();
            To_Byte();
            InitMask(3, 3);
            VvodMatrix1();
            button8.Visible = true;
        }

        private void closingToolStripMenuItem_Click(object sender, EventArgs e)     //Closing
        {
            if (stack.Count == 0) MessageBox.Show("Загрузите изображение");
            else
            {
                _Sokritie();
                To_Byte();
                InitMask(3, 3);
                VvodMatrix1();
                button7.Visible = true;

            }
        }

        public void VvodMatrix1 ()      //отображение кнопок для ввода матрицы
        {
            label1.Text = "Ведите матрицу: ";
            label1.Visible = true;
            textBox4.Visible = true;
            textBox5.Visible = true;
            textBox6.Visible = true;
            textBox7.Visible = true;
            textBox8.Visible = true;
            textBox9.Visible = true;
            textBox10.Visible = true;
            textBox11.Visible = true;
            textBox12.Visible = true;
           
        }

        public void VvodMatrix2()       //Ввод значений в матрицу
        {

            try
            {

                Mask[0, 0] = Convert.ToByte(textBox4.Text);
                Mask[0, 1] = Convert.ToByte(textBox5.Text);
                Mask[0, 2] = Convert.ToByte(textBox6.Text);
                Mask[1, 0] = Convert.ToByte(textBox7.Text);
                Mask[1, 1] = Convert.ToByte(textBox8.Text);
                Mask[1, 2] = Convert.ToByte(textBox9.Text);
                Mask[2, 0] = Convert.ToByte(textBox10.Text);
                Mask[2, 1] = Convert.ToByte(textBox11.Text);
                Mask[2, 2] = Convert.ToByte(textBox12.Text);

            }
            catch
            {
                MessageBox.Show("Введите корректные данные", "Ошибка",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            label1.Visible = false;
            textBox4.Visible = false;
            textBox5.Visible = false;
            textBox6.Visible = false;
            textBox7.Visible = false;
            textBox8.Visible = false;
            textBox9.Visible = false;
            textBox10.Visible = false;
            textBox11.Visible = false;
            textBox12.Visible = false;

        }

        private void button4_Click(object sender, EventArgs e)      //button Erosion
        {
            VvodMatrix2();
            button4.Visible = false;

            MathMorfology filter = new Erosion();
            backgroundWorker2.RunWorkerAsync(filter);

        }


        private void button5_Click(object sender, EventArgs e)      //button Delation
        {
            VvodMatrix2();
            button5.Visible = false;

            MathMorfology filter = new Delation();
            backgroundWorker2.RunWorkerAsync(filter);
        }


        private void button6_Click(object sender, EventArgs e)      //button Opening
        {
            VvodMatrix2();
            button6.Visible = false;

            MathMorfology filter = new Erosion();
            Bitmap newImage = (filter.processImage1(image, pic, Mask, MW, MH));
            image = newImage;
            filter = new Delation();
            backgroundWorker2.RunWorkerAsync(filter);
        }


        private void button7_Click(object sender, EventArgs e)      //button Closing
        {
            VvodMatrix2();
            button7.Visible = false;
            MathMorfology filter = new Delation();
            Bitmap newImage = (filter.processImage1(image, pic, Mask, MW, MH));
            image = newImage;
            filter = new Erosion();
            backgroundWorker2.RunWorkerAsync(filter);
        }


        private void button8_Click(object sender, EventArgs e)      //button Grad
        {
            VvodMatrix2();
            button8.Visible = false;
            MathMorfology filter = new Delation();
            Bitmap newImage1 = (filter.processImage1(image, pic, Mask, MW, MH));            
           
            MathMorfology filter2 = new Erosion();
            Bitmap newImage2 = (filter2.processImage1(image, pic, Mask, MW, MH));
           
            image = _ToGrad(newImage1, newImage2);
            filter = new Grad();
            backgroundWorker2.RunWorkerAsync(filter);
        }

        private Bitmap _ToGrad(Bitmap _img1, Bitmap _img2)      //функция градиента
        {
            Bitmap res = _img1;
            byte[,] img_b = new byte[image.Width, image.Height];
            for (int i = 0; i < image.Width; i++)

                for (int j = 0; j < image.Height; j++)
                {
                    if ((_img1.GetPixel(i, j).R <= 50) && (_img2.GetPixel(i, j).R > 50))
                        res.SetPixel(i, j, Color.White);
                    else res.SetPixel(i, j, Color.Black);
                    
                }
            return res;
        }

        private void _Sokritie()        //сокрытие ненужных кнопок
        {
            label1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            textBox1.Visible = false;
            textBox2.Visible = false;
            textBox3.Visible = false; 
            textBox4.Visible = false;
            textBox5.Visible = false;
            textBox6.Visible = false;
            textBox7.Visible = false;
            textBox8.Visible = false;
            textBox9.Visible = false;
            textBox10.Visible = false;
            textBox11.Visible = false;
            textBox12.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
            button4.Visible = false;
            button5.Visible = false;
            button6.Visible = false;
            button7.Visible = false;
            button8.Visible = false;
        }
    }
}
