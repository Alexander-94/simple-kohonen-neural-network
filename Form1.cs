using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Bitmap bmp, bmm;
        public Graphics gg;
        public SolidBrush brush; 
        public Form1 ff;
        public SolidBrush rect;

        //Входной массив
        public double [] inps = new double[60];

        //10 Массивов каждый на 1 нейрон
        public double[] N0WX = new double[60];
        public double[] N1WX = new double[60];
        public double[] N2WX = new double[60];
        public double[] N3WX = new double[60];
        public double[] N4WX = new double[60];
        public double[] N5WX = new double[60];
        public double[] N6WX = new double[60];
        public double[] N7WX = new double[60];
        public double[] N8WX = new double[60];
        public double[] N9WX = new double[60];

        //Сумма N0WX[i]*inps[i]+... для каждого нейрона
        public double[] D = new double[10]; 

        //файлы для записи обученной сети
        public string[] files = { "NW0.txt", "NW1.txt", "NW2.txt", "NW3.txt", "NW4.txt", "NW5.txt", "NW6.txt", "NW7.txt", "NW8.txt", "NW9.txt" };
              
            
        public Form1()
        {
            InitializeComponent();
        }

        public int[,] input = new int[6, 10];
        

        //открыть картинку
        private void button1_Click(object sender, EventArgs e)
        {
           
            listBox1.Items.Clear();//0
            listBox2.Items.Clear();//1
            listBox3.Items.Clear();//2
            listBox4.Items.Clear();//3
            listBox5.Items.Clear();//4
            listBox6.Items.Clear();//5
            listBox7.Items.Clear();//6
            listBox8.Items.Clear();//7
            listBox9.Items.Clear();//8
            listBox10.Items.Clear();//9

            listBox11.Items.Clear();//1-60

            for (int i = 0; i < 10; i++) D[i] = 0;
           

            button2.Enabled = true;
            openFileDialog1.Title = "Укажите тестируемый файл";
            openFileDialog1.ShowDialog();
            pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
           
            bmp = Image.FromFile(openFileDialog1.FileName) as Bitmap;

            bmm = new Bitmap(60, 100);
            gg = Graphics.FromImage((Image)bmm);
            gg.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            rect = new SolidBrush(Color.YellowGreen);

            int dx = 10, dy = 10;
            int current_pos_x = 0, current_pos_y = 0;

            //for (var i = 0; i < 10; i++) //listBox1.Items.Add(" ");
            int counter = 0;

            for (var x = 0; x <6; x++)
            {
                for (var y = 0; y <10; y++)
                {
                    
                    int n = (bmp.GetPixel(x, y).R);
                    if (n >= 250) { n = 0; }
                    else
                    {
                        n = 1;
                        gg.FillRectangle(rect, current_pos_x, current_pos_y, dx, dy);
                    }

                    inps[counter] = n;
                    counter++;
                    input[x, y] = n;
                    
                    current_pos_y += dy;
                }
                current_pos_y = 0;
             
                current_pos_x += dx;
            }

            pictureBox2.Image = (Image)bmm;

            //inps
            for (int i = 0; i < 60; i++)
            {
                listBox11.Items.Add(Convert.ToString(inps[i]));
            }

            //загрузка NWi 
            loadNWi();
            getAllD(D);
            
            //Определить максимальную D и вывести её индекс
            returnMaxD();
        }

     
        private void Form1_Load(object sender, EventArgs e)
        {
            //загрузка NWi 
            loadNWi();
        }

        //загрузка NWi в листбоксы
        public void loadNWi()
        {
            for (int i = 0; i < N0WX.Length; i++)
            {
                listBox1.Items.Add(Convert.ToString(N0WX[i]));
                listBox2.Items.Add(Convert.ToString(N1WX[i]));
                listBox3.Items.Add(Convert.ToString(N2WX[i]));
                listBox4.Items.Add(Convert.ToString(N3WX[i]));
                listBox5.Items.Add(Convert.ToString(N4WX[i]));
                listBox6.Items.Add(Convert.ToString(N5WX[i]));
                listBox7.Items.Add(Convert.ToString(N6WX[i]));
                listBox8.Items.Add(Convert.ToString(N7WX[i]));
                listBox9.Items.Add(Convert.ToString(N8WX[i]));
                listBox10.Items.Add(Convert.ToString(N9WX[i]));

                //listBox11.Items.Add(Convert.ToString(i));
            }
        }


        
        //Умножаем и суммируем
        public double[] getAllD(double[] DD)
        {
            for (int i = 0; i < 60; i++)
            {
                DD[0] = DD[0] + inps[i] * N0WX[i];
                DD[1] = DD[1] + inps[i] * N1WX[i];
                DD[2] = DD[2] + inps[i] * N2WX[i];
                DD[3] = DD[3] + inps[i] * N3WX[i];
                DD[4] = DD[4] + inps[i] * N4WX[i];
                DD[5] = DD[5] + inps[i] * N5WX[i];
                DD[6] = DD[6] + inps[i] * N6WX[i];
                DD[7] = DD[7] + inps[i] * N7WX[i];
                DD[8] = DD[8] + inps[i] * N8WX[i];
                DD[9] = DD[9] + inps[i] * N9WX[i];
            }
            //Вывод в текстбоксы D
            textBox4.Text = Convert.ToString(DD[0]);
            textBox5.Text = Convert.ToString(DD[1]);
            textBox6.Text = Convert.ToString(DD[2]);
            textBox7.Text = Convert.ToString(DD[3]);
            textBox8.Text = Convert.ToString(DD[4]);
            textBox9.Text = Convert.ToString(DD[5]);
            textBox10.Text = Convert.ToString(DD[6]);
            textBox11.Text = Convert.ToString(DD[7]);
            textBox12.Text = Convert.ToString(DD[8]);
            textBox13.Text = Convert.ToString(DD[9]);
            return DD;
        }

        public int winner_toteach = 0;
        //Получить макс D
        public double returnMaxD()
        {
            double max = D[0];
            double index = 0;
            for (int i = 0; i < D.Length; i++)
            {
                if (D[i] > max)
                {
                    max = D[i];
                    index = i;
                }
            }
            textBox2.Text = Convert.ToString(max);
            textBox1.Text = Convert.ToString(index);
            winner_toteach = Convert.ToInt32(index);
            //MessageBox.Show(Convert.ToString(index));
            return index;
        }

        //обучение
        private void button2_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;

            if (comboBox1.SelectedItem == "0")
            {
                for (int i = 0; i < N0WX.Length; i++)
                {
                    double temp = inps[i];
                    N0WX[i] += Convert.ToDouble(numericUpDown1.Value) * (temp - N0WX[i]);
                };
            }

            if (comboBox1.SelectedItem == "1")
            {
               for (int i = 0; i < N1WX.Length; i++)
               {
                   double temp1 = inps[i];
                   N1WX[i] = N1WX[i] + Convert.ToDouble(numericUpDown1.Value) * (temp1 - N1WX[i]);
               };
            }

            if (comboBox1.SelectedItem == "2")
            {
               for (int i = 0; i < N2WX.Length; i++)
               {
                   double temp2 = inps[i];
                   N2WX[i] = N2WX[i] + Convert.ToDouble(numericUpDown1.Value) * (temp2 - N2WX[i]);
               };
            }

            if (comboBox1.SelectedItem == "3")
            {
              for (int i = 0; i < N3WX.Length; i++)
              {
                  double temp3 = inps[i];
                  N3WX[i] = N3WX[i] + Convert.ToDouble(numericUpDown1.Value) * (temp3 - N3WX[i]);
              };
            }

            if (comboBox1.SelectedItem == "4")
            {
                for (int i = 0; i < N4WX.Length; i++)
                {
                    double temp4 = inps[i];
                    N4WX[i] = N4WX[i] + Convert.ToDouble(numericUpDown1.Value) * (temp4 - N4WX[i]);
                };
            }

            if (comboBox1.SelectedItem == "5")
            {
               for (int i = 0; i < N5WX.Length; i++)
               {
                   double temp5 = inps[i];
                   N5WX[i] = N5WX[i] + Convert.ToDouble(numericUpDown1.Value) * (temp5 - N5WX[i]);
               };
            }

            if (comboBox1.SelectedItem == "6")
            {
                for (int i = 0; i < N6WX.Length; i++)
                {
                    double temp6 = inps[i];
                    N6WX[i] = N6WX[i] + Convert.ToDouble(numericUpDown1.Value) * (temp6 - N6WX[i]);
                };
            }

            if (comboBox1.SelectedItem == "7")
            {
                for (int i = 0; i < N7WX.Length; i++)
                {
                    double temp7 = inps[i];
                    N7WX[i] = N7WX[i] + Convert.ToDouble(numericUpDown1.Value) * (temp7 - N7WX[i]);
                };
            }

            if (comboBox1.SelectedItem == "8")
            {
                for (int i = 0; i < N8WX.Length; i++)
                {
                    double temp8 = inps[i];
                    N8WX[i] = N8WX[i] + Convert.ToDouble(numericUpDown1.Value) * (temp8 - N8WX[i]);
                };
            }

            if (comboBox1.SelectedItem == "9")
            {
                for (int i = 0; i < N9WX.Length; i++)
                {
                    double temp9 = inps[i];
                    N9WX[i] = N9WX[i] + Convert.ToDouble(numericUpDown1.Value) * (temp9 - N9WX[i]);
                };
            }
        }

        public int c = 0;
        //Изменение файла при выборе нейронов
       
        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        //Сохранить веса
        private void button3_Click(object sender, EventArgs e)
        {
             string s = "";
             //запись массива 0 цифры      
             System.IO.File.Delete(files[0]);
             FileStream FS = new FileStream(files[0], FileMode.OpenOrCreate);
             StreamWriter SW = new StreamWriter(FS);
             for (int j = 0; j < 60; j++)
             {
                 s = Convert.ToString(N0WX[j]); 
                 SW.WriteLine(s);
             }                
             SW.Close();
             //запись массива 1 цифры 
             s = "";
             System.IO.File.Delete(files[1]);
             FileStream FS1 = new FileStream(files[1], FileMode.OpenOrCreate);
             StreamWriter SW1 = new StreamWriter(FS1);
             for (int j = 0; j < 60; j++)
             {
                 s = Convert.ToString(N1WX[j]);
                 SW1.WriteLine(s);
             }
             SW1.Close();
             //запись массива 2 цифры 
             s = "";
             System.IO.File.Delete(files[2]);
             FileStream FS2 = new FileStream(files[2], FileMode.OpenOrCreate);
             StreamWriter SW2 = new StreamWriter(FS2);
             for (int j = 0; j < 60; j++)
             {
                 s = Convert.ToString(N2WX[j]);
                 SW2.WriteLine(s);
             }
             SW2.Close();
             //запись массива 3 цифры 
             s = "";
             System.IO.File.Delete(files[3]);
             FileStream FS3 = new FileStream(files[3], FileMode.OpenOrCreate);
             StreamWriter SW3 = new StreamWriter(FS3);
             for (int j = 0; j < 60; j++)
             {
                 s = Convert.ToString(N3WX[j]);
                 SW3.WriteLine(s);
             }
             SW3.Close();
             //запись массива 4 цифры 
             s = "";
             System.IO.File.Delete(files[4]);
             FileStream FS4 = new FileStream(files[4], FileMode.OpenOrCreate);
             StreamWriter SW4 = new StreamWriter(FS4);
             for (int j = 0; j < 60; j++)
             {
                 s = Convert.ToString(N4WX[j]);
                 SW4.WriteLine(s);
             }
             SW4.Close();
             //запись массива 5 цифры 
             s = "";
             System.IO.File.Delete(files[5]);
             FileStream FS5 = new FileStream(files[5], FileMode.OpenOrCreate);
             StreamWriter SW5 = new StreamWriter(FS5);
             for (int j = 0; j < 60; j++)
             {
                 s = Convert.ToString(N5WX[j]);
                 SW5.WriteLine(s);
             }
             SW5.Close();
             //запись массива 6 цифры 
             s = "";
             System.IO.File.Delete(files[6]);
             FileStream FS6 = new FileStream(files[6], FileMode.OpenOrCreate);
             StreamWriter SW6 = new StreamWriter(FS6);
             for (int j = 0; j < 60; j++)
             {
                 s = Convert.ToString(N6WX[j]);
                 SW6.WriteLine(s);
             }
             SW6.Close();
             //запись массива 7 цифры 
             s = "";
             System.IO.File.Delete(files[7]);
             FileStream FS7 = new FileStream(files[7], FileMode.OpenOrCreate);
             StreamWriter SW7 = new StreamWriter(FS7);
             for (int j = 0; j < 60; j++)
             {
                 s = Convert.ToString(N7WX[j]);
                 SW7.WriteLine(s);
             }
             SW7.Close();
             //запись массива 8 цифры 
             s = "";
             System.IO.File.Delete(files[8]);
             FileStream FS8 = new FileStream(files[8], FileMode.OpenOrCreate);
             StreamWriter SW8 = new StreamWriter(FS8);
             for (int j = 0; j < 60; j++)
             {
                 s = Convert.ToString(N8WX[j]);
                 SW8.WriteLine(s);
             }
             SW8.Close();
             //запись массива 9 цифры 
             s = "";
             System.IO.File.Delete(files[9]);
             FileStream FS9 = new FileStream(files[9], FileMode.OpenOrCreate);
             StreamWriter SW9 = new StreamWriter(FS9);
             for (int j = 0; j < 60; j++)
             {
                 s = Convert.ToString(N9WX[j]);
                 SW9.WriteLine(s);
             }
             SW9.Close();
        }

        //Загрузить веса
        private void button4_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();//0
            listBox2.Items.Clear();//1
            listBox3.Items.Clear();//2
            listBox4.Items.Clear();//3
            listBox5.Items.Clear();//4
            listBox6.Items.Clear();//5
            listBox7.Items.Clear();//6
            listBox8.Items.Clear();//7
            listBox9.Items.Clear();//8
            listBox10.Items.Clear();//9

            listBox11.Items.Clear();//1-60
            //----------------------------
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            textBox10.Text = "";
            textBox11.Text = "";
            textBox12.Text = "";
            textBox13.Text = "";
            //----------------------------
            textBox2.Text = "";
            textBox1.Text = "";

            //Обнуляем все массивы весов
            for (int i = 0; i < 60; i++)
            {
                N0WX[i] = 0;
                N1WX[i] = 0;
                N2WX[i] = 0;
                N3WX[i] = 0;
                N4WX[i] = 0;
                N5WX[i] = 0;
                N6WX[i] = 0;
                N7WX[i] = 0;
                N8WX[i] = 0;
                N9WX[i] = 0;
            }
            //Загружаем их из файлов
            //цифра 0
            FileStream FS0 = new FileStream(files[0], FileMode.Open);
            StreamReader SR0 = new StreamReader(FS0);
            for (int j = 0; j < 60; j++)
            {
                N0WX[j] = Convert.ToDouble(SR0.ReadLine());
                
            }
            SR0.Close();
            //цифра 1
            FileStream FS1 = new FileStream(files[1], FileMode.Open);
            StreamReader SR1 = new StreamReader(FS1);
            for (int j = 0; j < 60; j++)
            {
                N1WX[j] = Convert.ToDouble(SR1.ReadLine());

            }
            SR1.Close();
            //цифра 2
            FileStream FS2 = new FileStream(files[2], FileMode.Open);
            StreamReader SR2 = new StreamReader(FS2);
            for (int j = 0; j < 60; j++)
            {
                N2WX[j] = Convert.ToDouble(SR2.ReadLine());

            }
            SR2.Close();
            //цифра 3
            FileStream FS3 = new FileStream(files[3], FileMode.Open);
            StreamReader SR3 = new StreamReader(FS3);
            for (int j = 0; j < 60; j++)
            {
                N3WX[j] = Convert.ToDouble(SR3.ReadLine());

            }
            SR3.Close();
            //цифра 4
            FileStream FS4 = new FileStream(files[4], FileMode.Open);
            StreamReader SR4 = new StreamReader(FS4);
            for (int j = 0; j < 60; j++)
            {
                N4WX[j] = Convert.ToDouble(SR4.ReadLine());

            }
            SR4.Close();
            //цифра 5
            FileStream FS5 = new FileStream(files[5], FileMode.Open);
            StreamReader SR5 = new StreamReader(FS5);
            for (int j = 0; j < 60; j++)
            {
                N5WX[j] = Convert.ToDouble(SR5.ReadLine());

            }
            SR5.Close();
            //цифра 6
            FileStream FS6 = new FileStream(files[6], FileMode.Open);
            StreamReader SR6 = new StreamReader(FS6);
            for (int j = 0; j < 60; j++)
            {
                N6WX[j] = Convert.ToDouble(SR6.ReadLine());

            }
            SR6.Close();
            //цифра 7
            FileStream FS7 = new FileStream(files[7], FileMode.Open);
            StreamReader SR7 = new StreamReader(FS7);
            for (int j = 0; j < 60; j++)
            {
                N7WX[j] = Convert.ToDouble(SR7.ReadLine());

            }
            SR7.Close();
            //цифра 8
            FileStream FS8 = new FileStream(files[8], FileMode.Open);
            StreamReader SR8 = new StreamReader(FS8);
            for (int j = 0; j < 60; j++)
            {
                N8WX[j] = Convert.ToDouble(SR8.ReadLine());

            }
            SR8.Close();
            //цифра 9
            FileStream FS9 = new FileStream(files[9], FileMode.Open);
            StreamReader SR9 = new StreamReader(FS9);
            for (int j = 0; j < 60; j++)
            {
                N9WX[j] = Convert.ToDouble(SR9.ReadLine());

            }
            SR9.Close();

            //загрузка NWi 
            loadNWi();

        }

        private void listBox10_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox9_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox8_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox7_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox6_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox5_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label23_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2();
            f.Visible = true;

        }

        private void label25_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form3 f2 = new Form3();
            f2.Visible = true;
        }

       
        
    }

}
