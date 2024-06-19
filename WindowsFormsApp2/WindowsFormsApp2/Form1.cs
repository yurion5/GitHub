
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Drawing.Color;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;


namespace WindowsFormsApp2
{
   public partial class Form1 : Form
   {
       // объявляем API
       [DllImport("user32.dll")]
       static extern bool GetCursorPos(ref Point lpPoint);
       // глобальные переменные, в которых будут храниться координаты
       static protected long totalPixels = 0;
       static protected int currX;
       static protected int currY;
       static protected int diffX;
       static protected int diffY;

       int[,] Matrixa = new int[205, 205];

       int[][] Neuro_1 = new int[41617][];
       int _count = 41617;


       int[] Force_X = new int[2];
       int[] Force_x = new int[2];
       int[] Force_Y = new int[2];
       int[] Force_y = new int[2];


       int _Xtop = 0;
       int _Ytop = 0;

       int[,,] Pix_Matrix = new int[4, 6, 6];
       int[,] Pix_Matrix_R = new int[6, 6];
       int[,] Pix_Matrix_G = new int[6, 6];
       int[,] Pix_Matrix_B = new int[6, 6];
       int[] Pix_Mat_Zir = new int[76];
       int _count_Zir = 0;

       int _cX = 0;
       int _cY = 0;

       //Массив состояния
       int[] Sosto = new int[76];

       String text_Zir;
      
      
      
      
      
       int[][] K_Sensor = new int[11][];
       int[][] K_Motor = new int[11][];
      // int[] K_Inter = new int[11];
       int[,] K_Sinaps = new int[10000,3];
        int _sinCount = 0;


       int[][,] K_Neyro = new int[11][ , ];

        string str = "ssds,sddfs,wwws,ees";
        //string []Strp = new string[4];
       Object[,] K_neiron = new Object[1000000,6];
       

       public Form1()

       {
           InitializeComponent();


               pictureBox1.BringToFront();

           _count = 1;
           for (int nerv = 1; nerv < 41617; nerv++)
           {
               Neuro_1[nerv] = new int[1];
               Neuro_1[nerv][0] = nerv;
           }

           for (int nex=1; nex<205; nex++)
           {
               for (int ney=1; ney<205; ney++)
               {
                   Matrixa[nex,ney] = _count;

                   _count++;
               }
           }

           // label2.Text = _count.ToString();
           _count = 1;
          
           //Матрица фокуса зрения 5*5

           for (int ze = 1; ze < 4; ze++)
           {
               for (int zey = 1; zey < 6; zey++)
               {
                   if (ze == 1)
                   {
                       dataGridView1.Rows.Add(0, 0, 0,0,0);
                   }
                   for (int zex = 1; zex < 6; zex++)
                   {
                       _count_Zir++;
                       Pix_Matrix[ze, zex, zey] = _count_Zir;
                       Pix_Mat_Zir[_count_Zir] = _count_Zir;
                  

                       DataGridViewCell cell = dataGridView1.Rows[zey-1].Cells[zex-1];
                       cell.Value = "Y";
                   }
               }
           }


//////////////////////Эрик Кандель система
          
                        //Задаём параметры 10 нейронов от 1 до 11
                         for (int nis = 0; nis <= 11; nis++)
                         {  
                                for (int sens = 0; sens <= 6; sens++)
                                {
                                    var xs=DateTime.Now;
                                    string vremia = xs.ToString("yy.MM.dd.HH.mm.ss.fff");

                                                /* dataInfo.Time Время из базы данных в формате dd.MM.yyyy hh:MM:ss:mm
                                                 * 
                                                 * DateTime enteredDate = DateTime.Parse(dataInfo.Time);
                                                 * var x = DateTime.Now;
                                                 * var time = (enteredDate - x).Duration();
                                                 */

                                    K_neiron[nis,0] = 0;    //?
                                    K_neiron[nis,1] = 3;    // Количество синапсов у текущего нейрона
                                    K_neiron[nis,2] = vremia;    // Время последнего возбуждения нейрона
                                }

                         }

                         int sinsa = 6;
                         int[] K_sinaps = new int[sinsa];
                         string sinaps = "";

                            K_sinaps[0] = 1; // Есть синапс 1 или нет 0
                            K_sinaps[1] = _sinCount; // Номер синапса от 1
                            K_sinaps[2] = 100; // Сила синапса 100 максимум
                            K_sinaps[3] = 2; // Номер нейрона получателя
                            K_sinaps[4] = 0101221123; // Время последней активности
                            K_sinaps[5] = 100; // ?? Мотивация синапса (проработать)

                         for (int sins = 0; sins < sinsa; sins++)
                         {
                              sinaps = sinaps +"."+ K_sinaps[sins].ToString();
                         }
                
            string hey = K_neiron[1,2].ToString();   // int to String
           string []Strp = sinaps.Split('.');
             int heyInt = Convert.ToInt32(Strp[5]);  //String to int
            
                //Эрик Кандель система
                            label5.Text ="";
                         for (int sis = 0; sis < 11; sis++)
                         {          
     /*                           //Создаем нейрон 
                                K_Neyro[sis] = new int[3,6];
                                 
                                    // Мета данные нейрона
                                    K_Neyro[sis][0,0] = 0; // ?
                                    K_Neyro[sis][0,1] = 2; // Количество синапсов этого нейрона

                                    // Номер синапса текущего нейрона, номера идут по порядку
                                    K_Neyro[sis][1,0] = 1; // Есть синапс 1 или нет 0
                                    K_Neyro[sis][1,1] = _sinCount; // Номер синапса от 1
                                    K_Neyro[sis][1,2] = 100; // Сила синапса 100 максимум
                                    K_Neyro[sis][1,3] = sis; // Номер нейрона получателя
                                    K_Neyro[sis][1,4] = 0101221123; // Время последней активности
                                    K_Neyro[sis][1,5] = 100; // Мотивация синапса (проработать)
*/

                                //Создаем сенсор нейрон (0 елемент - технический, далее номер синапса)
                                K_Sensor[sis] = new int[3];
                                    K_Sensor[sis][0] = 1; // Есть синапс 1 или нет 0
                                    K_Sensor[sis][1] = _sinCount; // Номер синапса [N,...]

                                        //[N,..] Номер синапса, , 
                                        K_Sinaps[_sinCount,0] = 0; //[..,0] Номер сенс нейрона
                                        K_Sinaps[_sinCount,1] = 100; //[..,1] Сила синапса
                                        K_Sinaps[_sinCount,1] = 100; //[..,0] Номер мото нейрона
                                        _sinCount++;
                                        //Второй синапс этого нейрона
                                        K_Sinaps[_sinCount,0] = 0;
                                        K_Sinaps[_sinCount,1] = 100;
                                        _sinCount++;
                                        //Третий синапс этого нейрона
                                        K_Sinaps[_sinCount,0] = 0;
                                        K_Sinaps[_sinCount,1] = 100;
                                        _sinCount++;

                                 //Создаем мото нейрон (0 елемент - технический, далее номер синапса)
                                K_Motor[sis] = new int[2];
                                 //Мото нейрону не нужны синапсы, он запускает один или несколько мото
                                     K_Motor[sis][0] = 1;
                                     K_Motor[sis][1] = 1;
                                
              
                              //K_Inter[sis] = 1;
                                  
                                  // K_sinaps[sis][0] = 0;
                                  // K_sinaps[sis][1] = 1;

                                if (sis<7)
                                {   //Strp[sis] = str.Split(",");
                                  //label5.Text = label5.Text + "  " + Strp[sis];
                                }
                               label5.Text = label5.Text + "     ." +sis + "- " +K_Sensor[sis][1].ToString();
                         }

///////////////////////////////////////






           Force_X[1] = 1;
/*           Force_x[1] = 1;
           Force_Y[1] = 1;
           Force_y[1] = 1;
*/           
               //Timer
               timer1.Interval = 20;
               timer1.Tick += new EventHandler(timer1_Tick);

               // Enable timer.
               timer1.Enabled = true;

       }

       private void timer1_Tick(object sender, EventArgs e)
       {
           label1.Text = DateTime.Now.ToString();


           Lable_Travel();    


       }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                label6.Text = "Enter";
            }
            if (e.KeyCode == Keys.Z)
            {
                label6.Text = "a";
            }
        }

       private void button1_Click(object sender, EventArgs e)
       {
           _count++;

           if ( button1.Text == "Stop" )
           {
               button1.Text = "Start";
               timer1.Enabled = false;
           }
           else
           {
               button1.Text = "Stop";
               timer1.Enabled = true;
                           pictureBox1.Top = 204;
                           pictureBox1.Left = 204;
           }
           totalPixels = 0;
       }

       private void pictureBox1_Click(object sender, EventArgs e)
       {
           _Xtop = 204;
           _Ytop = 204;

       }

       private void MoverPic(int _X, int _Y)
       {
          // if(Force_X[1]>0)
        /*   Force_X[1] = 1;
           Force_x[1] = 1;
           Force_Y[1] = 1;
           Force_y[1] = 1;
          */
           pictureBox1.Top = pictureBox1.Top + _X;
           pictureBox1.Left = pictureBox1.Left + _Y;

           if (pictureBox1.Left <= 0 || pictureBox1.Top <= 0)
           {
               pictureBox1.Top = 204;
               pictureBox1.Left = 204;
           }

       }

       private void GetPixel_Matrix()
       {


           //Уменьшенное изображение в pictureBox3
           Bitmap imgSmall = new Bitmap(pictureBox2.Image, pictureBox2.Width / 3, pictureBox2.Height / 3);
           pictureBox3.Image = imgSmall;


           // Create a Bitmap object from an image file.
           Bitmap myBitmap = new Bitmap(pictureBox2.Image);
           int col_X = _cX - 3;
           int col_Y = _cY - 3;


           _count_Zir = 0;
           int _count_Zir_R = 0;
           int _count_Zir_G = 0;
           int _count_Zir_B = 0;
           int col_R;
           int col_G;
           int col_B;


                text_Zir = " ";
                   for (int mey = 1; mey < 6; mey++)
                   {
                       for (int mex = 1; mex < 6; mex++)
                       {

                           if (col_X <= 198 && col_Y <= 198)
                           {
                               Color pixelColor = myBitmap.GetPixel(col_X + mex - 8, col_Y + mey - 31);
                               _count_Zir_R = Pix_Matrix [1, mex, mey];
                               col_R = pixelColor.R;
                                   if (col_R>=200)
                                   {
                                      Pix_Mat_Zir[_count_Zir_R] = 1;
                                   }
                                        else
                                   {
                                      Pix_Mat_Zir[_count_Zir_R] = 0;
                                   }
                                   text_Zir =  text_Zir + " " + Pix_Mat_Zir[_count_Zir_R].ToString();
                               _count_Zir_G = Pix_Matrix [2, mex, mey];
                                col_G = pixelColor.G;
                                   if (col_G>=200)
                                   {
                                       Pix_Mat_Zir[_count_Zir_G] = 1;
                                   }
                                       else
                                   {
                                       Pix_Mat_Zir[_count_Zir_G] = 0;
                                    }
                                   text_Zir =  text_Zir + " " + Pix_Mat_Zir[_count_Zir_G].ToString();
                               _count_Zir_B = Pix_Matrix [3, mex, mey];
                               col_B = pixelColor.B;
                                   if (col_B>=200)
                                   {
                                       Pix_Mat_Zir[_count_Zir_B] = 1;
                                   }
                                       else
                                   {
                                       Pix_Mat_Zir[_count_Zir_B] = 0;
                                    }
                                   text_Zir =  text_Zir + " " + Pix_Mat_Zir[_count_Zir_B].ToString();

                               Color newColor = Color.FromArgb(pixelColor.R, 0, 0);
                               myBitmap.SetPixel(194 + mex, 194 + mey, newColor);
                           }


                            DataGridViewCell cell = dataGridView1.Rows[mey-1].Cells[mex-1];
                            cell.Value = Pix_Mat_Zir[_count_Zir_R]+" " +Pix_Mat_Zir[_count_Zir_G]+" " +Pix_Mat_Zir[_count_Zir_B];




                           

                       }
                   }


                       pictureBox2.Image = myBitmap;

           if (Pix_Mat_Zir[1]==1)
           {
               Random rnd = new Random();
               int month  = rnd.Next(-1, 1);
               MoverPic(month,-1);
           }

                           // Fill a rectangle with pixelColor.
                           // SolidBrush pixelBrush = new SolidBrush(pixelColor);
                           // e.Graphics.FillRectangle(pixelBrush, 0, 0, 100, 100);
                       /*
                           label4.Text = col_R.ToString() + " " + col_G.ToString() + " " + col_B.ToString();
                */
       }

       private void Lable_Travel()
       {


           // обновление информации происходит каждые 10 мс
           Point defPnt = new Point();
           // заполняем defPnt информацией о координатах мышки
           GetCursorPos(ref defPnt);

          
           _cX = defPnt.X;
           _cY = defPnt.Y;
           GetPixel_Matrix();
        



           // выводим информацию в окно
           label2.Text = "X = " + defPnt.X.ToString() + "  Y = " + defPnt.Y.ToString();;
           // label4.Text = Matrixa[defPnt.X, defPnt.Y].ToString();
           // если курсор перемещался
           if (diffX != defPnt.X | diffY != defPnt.Y)
           {
               // рассчитываем на сколько пикселей
               diffX = (defPnt.X - currX);
               diffY = (defPnt.Y - currY);
               if (diffX < 0)
               {
                   diffX *= -1;
               }
               if (diffY < 0)
               {
                   diffY *= -1;
               }
               // обновляем счетчик пробега мышки
               totalPixels += diffX + diffY;
               // выводим информацию о пробеге
               label3.Text = "You have traveled " + totalPixels + " pixels";
           }
           // запоминаем текущие координаты, для расчета пробега
           currX = defPnt.X;
           currY = defPnt.Y;

       }
      
       //Не используется
       private void GetPixel_Example()
       {

           // Create a Bitmap object from an image file.
           Bitmap myBitmap = new Bitmap(pictureBox2.Image);

           // Get the color of a pixel within myBitmap.
           Color pixelColor = myBitmap.GetPixel(_cX-17, _cY-40);

           // Fill a rectangle with pixelColor.
          // SolidBrush pixelBrush = new SolidBrush(pixelColor);
          // e.Graphics.FillRectangle(pixelBrush, 0, 0, 100, 100);
          int col_R = pixelColor.R;
          int col_G = pixelColor.G;
          int col_B = pixelColor.B;
           label4.Text = col_R.ToString() + " " + col_G.ToString() + " " + col_B.ToString();

       }

      
      
       Image Zoom(Image image, int k)
       {
           if (k <= 1) return image;
           Bitmap img = new Bitmap(image);
           int width = img.Width;
           int height = img.Height;
           Image zoomImg = new Bitmap(width * k, height * k);
           Graphics g = Graphics.FromImage(zoomImg);

           for (int i = 0; i < width; i++)
           for (int j = 0; j < height; j++)
           {
               Color color = img.GetPixel(i, j);
               g.FillRectangle(new SolidBrush(color), i * k, j * k, k, k);
           }

           return zoomImg;
       }



       private void button2_Click(object sender, EventArgs e)
       {

       }
   }
}

