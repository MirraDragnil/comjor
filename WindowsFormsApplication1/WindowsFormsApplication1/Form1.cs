using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        int[,] matrix;
        int st, towns;
        string opt_path = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            opt_path = "";
            towns = Convert.ToInt32(textBox1.Text);
            string str = richTextBox1.Text;
            if (towns > 6)
                MessageBox.Show("Слишком большое количество городов! Введите другое значение!", "Ошибка ввода", MessageBoxButtons.OK);
            else
                if (towns < 2)
                MessageBox.Show("Слишком малое количество городов! Введите другое значение!", "Ошибка ввода", MessageBoxButtons.OK);
            matrix = new int[towns, towns];

            for (int i = 0; i < towns; i++)
                for (int j = 0; j < towns; j++)
                {
                    int a;
                    if (j == towns - 1)
                    {
                        if (str.IndexOf(" ") < str.IndexOf("\n"))
                            a = str.IndexOf(" ");
                        else
                            a = str.IndexOf("\n");
                    }
                    else a = str.IndexOf(" ");
                    if ((i == towns - 1) && (j == towns - 1))
                    {
                        if (a != -1)
                            matrix[i, j] = Convert.ToInt32(str.Substring(0, a));
                        else
                            matrix[i, j] = Convert.ToInt32(str);
                    }
                    else
                        matrix[i, j] = Convert.ToInt32(str.Substring(0, a));
                    str = str.Remove(0, a + 1);
                }
            MessageBox.Show(f1().ToString() + "\n"+opt_path, "Минимальный путь", MessageBoxButtons.OK);

        }

        public int f1()
        {
            int min_g = 0;
            for (int j = 0; j < towns; j++)
            {
                int[] mass = new int[towns];
                for (int i = 0; i < towns; i++)
                    mass[i] = -1;
                st = j;
                //mass[j] = 0;
                int step = 0;
                string path = "";
                int min_yz = f2(j, mass, step, ref path);
                if (min_yz != -1)
                    if (min_g == 0)
                    {
                        min_g = min_yz;
                        opt_path = path;
                    }
                    else
                        if (min_g > min_yz)
                        {
                            min_g = min_yz;
                            opt_path = path;
                        }
            }
            return min_g;
        }

        public int f2(int i, int[] p, int step, ref string path)
        {
            int min = 0;
            string opt_path_yz = "";
            if (step == towns)
            {
                path = (i+1).ToString();
                return 0;
            }
            else
                for (int j = 0; j < towns; j++)
                {
                    if (((j != st) || (step == towns - 1)) && (p[j] == -1) && (matrix[i, j] != 0))
                    {
                        p[j] = step + 1;
                        int a = f2(j, p, step + 1, ref path);
                        if (a != -1)
                        {
                            int b = a + matrix[i, j];
                            if (min == 0)
                            {
                                min = b;
                                path = (i+1).ToString() + " -> " + path; //(i+1) для привычного отображения городов; 
                                opt_path_yz = path;
                            }
                            else
                                if (min > b)
                                {
                                    min = b;
                                    path = (i+1).ToString() +" -> "+ path;
                                    opt_path_yz = path;
                                }
                            
                        }
                        p[j] = -1;

                    }
                }
            if (min == 0)
                return -1;
            else
            {
                path = opt_path_yz;
                return min;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
