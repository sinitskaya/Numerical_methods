using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace laboratory
{
    public partial class Form1 : Form
    {
        int taskType;//task1-тестовая task2-основная
        const double a = 0d;
        const double b = 1d;
        const double c = 0d;
        const double d = 2d;
        int znak = -1;
        //double deleteX;
        //double deleteY;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            taskType = 1;
        }

        private void тестоваяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            taskType = 1;
            label3.Text = "Точное решение";
        }

        private void основнаяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            taskType = 2;
            label3.Text = "Двойной шаг";
        }

        double BorderConditionsTestTask(double x, double y)//ГУ
        {
            double value = 0;
            if ((x == a) || (x == b) || (y == c) || (y == d))
            {
                value = Math.Exp(Math.Pow(Math.Sin(Math.PI * x * y), 2)); //exp(sin(pixy)^2)
            }
            return value;
        }
        double BorderConditionsMainTask(double x, double y)
        {
            double value = 0;
            if (x == a)
            {
                value = Math.Pow(Math.Sin(Math.PI * y), 2);
            }
            if (x == b)
            {
                value = Math.Abs(Math.Exp(Math.Sin(Math.PI *  y)) - 1d);
            }
            if (y == c)
            {  
                value = x * (1d - x);
            }
            if (y == d)
            {
                value = x * (1d - x) * Math.Exp(x);
            }
            return value;
        }

        double PerturbationFunctionTask(double x, double y)
        {
            double value=0;
            if(taskType==1)
            {
                value = (2d * Math.Pow(Math.PI * y, 2) + 2d * Math.Pow(Math.PI * x, 2)) * (2d * Math.Pow(Math.Sin(Math.PI * x * y) * Math.Cos(Math.PI * x * y), 2) - Math.Pow(Math.Sin(Math.PI * x * y), 2) + Math.Pow(Math.Cos(Math.PI * x * y), 2)) * Math.Exp(Math.Pow(Math.Sin(Math.PI * x * y), 2));
            
            }
            if (taskType==2)
            {
                value = -1d * Math.Abs(x - y);
            }
            
            return value;
        }      

        double AccurateSolveTestTask(double x, double y)//точное решение
        {
            double value = Math.Exp(Math.Pow(Math.Sin(Math.PI * x * y), 2));
            return value;
        }

        double Max(double[,] array, int nX, int nY)//max|vij-uij|,max|vij-v2ij|
        {
            double value = double.MinValue;
            for (int i = 0; i <= nX; i++)
            {
                for (int j = 0; j <= nY; j++)
                {
                    if (value < array[i, j])
                    {
                        value = array[i, j];
                    }
                }
            }
            return value;
        }

        int[] Search(double[,] array, double elem, int nX, int nY)
        {
            int[] value = new int[2];
            for (int i = 0; i <= nX; i++)
            {
                for (int j = 0; j <= nY; j++)
                {
                    if (elem == array[i, j])
                    {
                        value[0] = i;
                        value[1] = j;
                    }
                }
            }
            return value;
        }

        double Norma(double [] x)
        {
            double scalar_product = 0; //скалярное произведение
            for (int i=0; i<x.Length; i++)
                scalar_product = scalar_product + x[i]*x[i];
            double norma = Math.Sqrt(scalar_product);
            return norma;
        }

        void Matrix_product( double  A, double  B, double  C, double [] v, double[] x, int n, int m) // A*x_s
        {// А должна быть квадратная A*x
            int k1 = n - 2, k2 = k1; double aij = 0;
            for (int i = 0; i < v.Length; i++)
                v[i] = 0;
            for (int i = 0; i < (n - 1) * (m - 1); i++)
                for (int j = 0; j < (n-1)*(m-1); j++)
                {
                    aij = 0;
                    if (i == j) // диагональ
                        aij = C; //a
                    else if ((j == i + n - 1) || (i == j + n - 1))
                    {// выше диагонали  //ниже диагонали
                        aij = A; // 1/k/k
                    }
                    else if ((j == i + 1)&&(i==k1) || (j == i - 1)&&(j==k2))
                    {//выше диагонали            //ниже диагонали
                        if ((j == i + 1) && (i == k1))
                            k1 = k1 + n - 1;
                        else if ((j == i - 1) && (j == k2))
                            k2 = k2 + n - 1;
                    }
                    else if ((j == i + 1)&&(i!=k1) || (j == i - 1)&&(j!=k2))
                    {//выше диагонали            //ниже диагонали
                        aij = B; // 1/h/h
                    }                  
                    v[i] = v[i] + aij * x[j];
                }
        }
        void G(Dictionary<int [],double> map)
        {
            Dictionary<int[], double> map1 = new Dictionary<int[],double>();
            int []v = new int[2];
            for (int i = 0; i < 2; i++)
                v[i] = 0;
            map1.Add(v, 1);//<key, value>
            //map1.TryGetValue(TKey, TValue)	//Возвращает значение, связанное с заданным ключом.
            double cvalue = map1[v];
 
        }

        void Find_lamda_max_min(double lambda_max, double lambda_min, double h, double k, int n, int m)// поиск максимального собств числа матрицы A
        {
            lambda_max = 4 / h / h * Math.Pow(Math.Cos(Math.PI / 2 / n), 2) + 4 / k / k * Math.Pow(Math.Cos(Math.PI / 2 / m), 2);
            lambda_min = 4 / h / h * Math.Pow(Math.Sin(Math.PI / 2 / n), 2) + 4 / k / k * Math.Pow(Math.Sin(Math.PI / 2 / m), 2);
        }

        double Opt_param(double h, double k, int n, int m)
        {
            double lambda_max = 0, lambda_min = 0, d_opt = 0;
            lambda_max = 4 / h / h * Math.Pow(Math.Cos(Math.PI / 2 / n), 2) + 4 / k / k * Math.Pow(Math.Cos(Math.PI / 2 / m), 2);
            lambda_min = 4 / h / h * Math.Pow(Math.Sin(Math.PI / 2 / n), 2) + 4 / k / k * Math.Pow(Math.Sin(Math.PI / 2 / m), 2);
            //Find_lamda_max_min(lambda_max, lambda_min, h, k, n, m);
            d_opt = 2.0 / (lambda_min + lambda_max);
            return d_opt;
        }  

        void Find_lamda_max1(double A, double B, double C, double[] x, int kol_iter, int n, int m)// поиск максимального собств числа матрицы A
        {
            // не нулевое начальное приближение
            double [] x_0 = new double[x.Length];
            for (int i = 0; i < x_0.Length; i++)
                x_0[i] = 0.5;
            // x_k
            double[] x_k = new double[x.Length];
            // ||x_k||
            double norma;
            // собственный нормирован вектор
            double [] e_k = new double[x.Length];
            // lamda_k
            double lambda_k = 0;
            //
            for (int i = 0; i < kol_iter; i++)
            {
                //2 // e_k = x_k/||x_k||
                norma = Norma(x); // ||x_k||
                for(int j=0; j<x.Length; j++)
                {
                    e_k[j] = (double)x[j] / norma; // x_k/||x_k||
                }
                //3 x_k = Ae_(k-1)
                double []v = new double[x.Length];
                Matrix_product(A, B, C, v, x, n, m); // Ae_(k-1)
                for (int j = 0; j < x.Length; j++)
                {
                    x_k[j] = v[j];
                }
                //4  //lambda_k = (x_k, e_(k-1))
                for (int j = 0; j < x.Length; j++)
                {
                    lambda_k = lambda_k + x_k[j] * e_k[j]; // = (x_k, e_(k-1))
                }
            }
        }

        void vector_b(int n, int m, double k, double h, double [] b, double[,] arrayV, double[] arrayX, double[] arrayY)
        {
            double[] v = new double[b.Length];
            for(int i = 0; i < v.Length; i++)
                v[i] = 0;

            for (int i = 0; i < n - 1; i++)
            {
                v[i] = arrayV[i + 1, 0] / k / k + v[i];
            }

            int s = 0;
            for (int j = 0; j < m - 1; j++)
            {
                v[s] = arrayV[0, j + 1] / h / h + v[s];
                s = s + (n - 1);
            }

            s = n - 2;
            for (int j = 0; j < m - 1; j++)
            {
                v[s] = arrayV[n, j + 1] / h / h + v[s];
                s = s + (n - 1);
            }

            s = v.Length - (n - 1);
            for (int i = 0; i < n - 1; i++)
            {
                v[s] = arrayV[i+1, m] / k / k + v[s];
                s = s + 1;
            }
            /////////////////////////////////////////
            s = 0;
            for (int j = 1; j < arrayX.Length - 1; j++)
                for (int i = 1; i < arrayX.Length - 1; i++)
                {
                    b[s] = (znak) * (PerturbationFunctionTask(arrayX[i], arrayY[j]) - v[s]);
                    s++;
                }           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int nX = Convert.ToInt32(textBox1.Text);
            int nY = Convert.ToInt32(textBox2.Text);
            int maxNumberOfIterations = Convert.ToInt32(textBox3.Text);
            double maxEPS = Convert.ToDouble(textBox4.Text);//точность метода

            double hX = (b - a) / (double)nX;
            double hY = (d - c) / (double)nY;

            double EPS = double.MinValue;//=0
            double NumberOfIterations = 0;
            bool trigger = false;//остановка метода
            double triggerV;//x{i,j}_s
            double A, B;

            double[] arrayX = new double[nX + 1];
            double[] arrayY = new double[nY + 1];

            for (int i = 0; i <= nX; i++)
            {
                arrayX[i] = hX * (double)i;
            }

            for (int i = 0; i <= nY; i++)
            {
                arrayY[i] = hY * (double)i;
            }

            double[,] arrayU = new double[nX + 1, nY + 1];
            double[,] arrayV = new double[nX + 1, nY + 1];
            double[,] differenceOfSolutions = new double[nX + 1, nY + 1];//разность решений
            /////////////////////////////////////////////////////
            double d_opt = 0;
            d_opt = Opt_param(hX, hY, nX, nY);        
            double[] X_s = new double[(nX - 1) * (nY - 1)];
            double [] v = new double[X_s.Length];
            double [] vectr_b = new double[X_s.Length];
            A = (znak) * 1.0 / hY / hY; B = (znak) * 1.0 / hX / hX; double C = (-2) * (A + B);
            for(int i=0; i<X_s.Length; i++)
            {
                X_s[i] = 0;
            }
            double[] r_s = new double [X_s.Length];
            for(int i=0; i<r_s.Length; i++)
            {
                r_s[i] = 0;
            }
            //////////////////////////////////////////////////////////////////////////////////////////
            if (taskType == 1)// тестовая
            {
                zagruzka_textBox5.Text = Convert.ToString(0);
                for (int i = 0; i <= nX; i++)
                {
                    for (int j = 0; j <= nY; j++)
                    {
                        arrayU[i, j] = AccurateSolveTestTask(arrayX[i], arrayY[j]);//точное решение
                        arrayV[i, j] = BorderConditionsTestTask(arrayX[i], arrayY[j]);//ГУ
                    }
                }
                vector_b(nX, nY, hY, hX, vectr_b, arrayV, arrayX, arrayY);

                do
                {
                    EPS = 0;
                    NumberOfIterations++;

                    Matrix_product(A, B, C, v, X_s, nX, nY); //A*x_s
                    for (int k = 0; k < X_s.Length; k++)
                    {                          //b    - A*x_s = - r_s
                        r_s[k] = vectr_b[k] - v[k];
                        X_s[k] = X_s[k] + (r_s[k]) * d_opt;
                    }

                    int s = 0;
                    for (int j = 1; j < nX; j++)//внутри
                        for (int i = 1; i < nY; i++)
                        {
                            triggerV = arrayV[i, j];//x{i,j}_s
                            EPS = Math.Max(Math.Abs(X_s[s] - triggerV), EPS);//E_s+1 = max(|x{i,j}_s-x{i,j}_s+1| , E_s)
                            s++;
                        }
                    s = 0;
                    for (int j = 1; j < nX; j++)//внутри
                        for (int i = 1; i < nY; i++)
                        {
                            arrayV[i, j] = X_s[s];//x_s+1
                            s++;
                        }

                    if ((EPS < maxEPS) || (NumberOfIterations >= maxNumberOfIterations))
                    {
                        trigger = true;//остановить метод
                    }
                    zagruzka_textBox5.Text = Convert.ToString((int)(((double)NumberOfIterations / maxNumberOfIterations) * 100));
                } while (trigger == false);
            }
            ///////////////////////////////////////////////////////////////////////////////////////
            if (taskType == 2)
            {
                for (int i = 0; i <= nX; i++)
                {
                    for (int j = 0; j <= nY; j++)
                    {
                        arrayV[i, j] = BorderConditionsMainTask(arrayX[i], arrayY[j]);//ГУ осн
                    }
                }
                vector_b(nX, nY, hY, hX, vectr_b, arrayV, arrayX, arrayY);

                do
                {
                    EPS = 0;
                    NumberOfIterations++;
                    Matrix_product(A, B, C, v, X_s, nX, nY); //A*x_s
                    for (int k = 0; k < X_s.Length; k++)
                    {                          //b    - A*x_s = - r_s
                        X_s[k] = X_s[k] + (vectr_b[k] - v[k]) * d_opt;
                    }

                    int s = 0;
                    for (int j = 1; j < nX; j++)//внутри
                        for (int i = 1; i < nY; i++)
                        {
                            triggerV = arrayV[i, j];//x{i,j}_s
                            EPS = Math.Max(Math.Abs(X_s[s] - triggerV), EPS);//E_s+1 = max(|x{i,j}_s-x{i,j}_s+1| , E_s)
                            s++;
                        }
                    s = 0;
                    for (int j = 1; j < nX; j++)//внутри
                        for (int i = 1; i < nY; i++)
                        {
                            arrayV[i, j] = X_s[s];//x_s+1
                            s++;
                        }

                    if ((EPS < maxEPS) || (NumberOfIterations >= maxNumberOfIterations))//E_s+1<E || N_iter>=N_max_iter
                    {
                        trigger = true;//остановка метода
                    }
                } while (trigger == false);
                ///////////////////////////закончисля цикл
                trigger = false;
                double[,] arrayV2 = new double[2 * nX + 1, 2 * nY + 1];
                int nXV2 = nX * 2;
                int nYV2 = nY * 2;
                double hXV2 = (b - a) / (double)nXV2;
                double hYV2 = (d - c) / (double)nYV2;
                double EPSV2 = double.MinValue;
                double NumberOfIterationsV2 = 0;
                double[] arrayXV2 = new double[nXV2 + 1];
                double[] arrayYV2 = new double[nYV2 + 1];

                d_opt = Opt_param(hXV2, hYV2, nXV2, nYV2);
                double[] XV2_s = new double[(nXV2 - 1) * (nYV2 - 1)];
                double[] vV2 = new double[XV2_s.Length];
                double[] vectr_bV2 = new double[XV2_s.Length];
                A = (znak) * 1.0 / hYV2 / hYV2; B = (znak) * 1.0 / hXV2 / hXV2;  C = (-2) * (A + B);
                for (int i = 0; i < XV2_s.Length; i++)
                {
                    XV2_s[i] = 0;
                }

                for (int i = 0; i <= nXV2; i++)
                {
                    arrayXV2[i] = hXV2 * (double)i;
                }

                for (int i = 0; i <= nYV2; i++)
                {
                    arrayYV2[i] = hYV2 * (double)i;
                }

                for (int i = 0; i <= nXV2; i++)
                {
                    for (int j = 0; j <= nYV2; j++)
                    {
                        arrayV2[i, j] = BorderConditionsMainTask(arrayXV2[i], arrayYV2[j]);//ГУ осн
                    }
                }
                vector_b(nXV2, nYV2, hYV2, hXV2, vectr_bV2, arrayV2, arrayXV2, arrayYV2);

                do
                {
                    EPSV2 = 0;
                    NumberOfIterationsV2++;
                    Matrix_product(A, B, C, vV2, XV2_s, nXV2, nYV2); //A*x_s
                    for (int k = 0; k < XV2_s.Length; k++)
                    {                          //b    - A*x_s = - r_s
                        XV2_s[k] = XV2_s[k] + (vectr_bV2[k] - vV2[k]) * d_opt;
                    }

                    int s = 0;
                    for (int j = 1; j < nXV2; j++)//внутри
                        for (int i = 1; i < nYV2; i++)
                        {
                            triggerV = arrayV2[i, j];//x{i,j}_s
                            EPSV2 = Math.Max(Math.Abs(XV2_s[s] - triggerV), EPSV2);//E_s+1 = max(|x{i,j}_s-x{i,j}_s+1| , E_s)
                            s++;
                        }
                    s = 0;
                    for (int j = 1; j < nXV2; j++)//внутри
                        for (int i = 1; i < nYV2; i++)
                        {
                            arrayV2[i, j] = XV2_s[s];//x_s+1
                            s++;
                        }
                    if ((EPSV2 < maxEPS) || (NumberOfIterationsV2 >= maxNumberOfIterations))//E_s+1<E || N_iter>=N_max_iter
                    {
                        trigger = true;//остановка метода
                    }
                } while (trigger == false);

                int tmpI = 0;
                int tmpJ = 0;
                for (int i = 0; i <= nXV2; i = i + 2)
                {
                    for (int j = 0; j <= nYV2; j = j + 2)
                    {
                        arrayU[tmpI, tmpJ] = arrayV2[i, j];
                        tmpJ++;
                    }
                    tmpJ = 0;
                    tmpI++;
                }
            }
            /////////////////////////////////////////////////////////////////////////////////////////
            dataGridView1.ColumnCount = nX + 1;
            dataGridView1.RowCount = nY + 1;
            dataGridView2.ColumnCount = nX + 1;
            dataGridView2.RowCount = nY + 1;
            dataGridView3.ColumnCount = nX + 1;
            dataGridView3.RowCount = nY + 1;

            for (int i = 0; i <= nX; i++)
            {
                for (int j = 0; j <= nY; j++)
                {
                    dataGridView1[i, nY-j].Value = Convert.ToString(Math.Round(arrayV[i, j],4));
                    dataGridView2[i, nY - j].Value = Convert.ToString(Math.Round(arrayU[i, j],4));
                    differenceOfSolutions[i, j] = Math.Abs(arrayU[i, j] - arrayV[i, j]);//разность по модулю
                    dataGridView3[i, nY-j].Value = Convert.ToString(Math.Round(differenceOfSolutions[i, j],4));//разность
                }
            }

            label10.Text = "Достигнутая точность=" + Convert.ToString(EPS) + ";";
            label11.Text = "Количество итераций=" + Convert.ToString(NumberOfIterations) + ";";
            double maxElemDifferenceOfSolutions = Max(differenceOfSolutions, nX, nY);//максимальная разность решений max|uij-vij| i От 1 до n
            if (taskType == 1)
            {
                label12.Text = "max |u-v|=" + Convert.ToString(maxElemDifferenceOfSolutions);
            }
            if (taskType == 2)
            {
                label12.Text = "max |v2-v|=" + Convert.ToString(maxElemDifferenceOfSolutions);
            }

            int[] maxElemDifferenceOfSolutionsXY = Search(differenceOfSolutions, maxElemDifferenceOfSolutions, nX, nY);//находим x y для max|uij-vij|
            label14.Text = "x=" + arrayX[maxElemDifferenceOfSolutionsXY[0]] + ";";
            label15.Text = "y=" + arrayY[maxElemDifferenceOfSolutionsXY[1]] + ";";
        }
    }
}
