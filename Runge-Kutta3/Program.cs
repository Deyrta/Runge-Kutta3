using System;

namespace RK3
{
    class Program
    {
        static void Main(string[] args)
        {
            DiffEquatation solve = new DiffEquatation();
            solve.RungeKutta3();
            solve.RungeKutta3();
            Console.ReadKey();
        }
    }

    class DiffEquatation
    {
        const int n = 1; // к-сть р-нь
        double[,] k = new double[3, n]; // k1,k2,k3
        double a = 0, b = 1; // x є [0,1]
        double[] y = new double[n] { 1 }; // dy1/dx dy2/dx dy3/dx 
        double[] f = new double[n];
        double[] z = new double[n];
        double h = 0.05;
        double eps = 1e-5;
        double x;

        private void dy() // y''' + y = 0
        {
            f[0] = y[0];
            f[1] = y[2];
            f[2] = -y[0];
        }

        public void RungeKutta3()
        {
            x = a;
            do
            {
                dy();
                for (int j = 0; j < n; j++)
                {
                    k[0, j] = h * f[j];
                    z[j] = y[j];
                    y[j] = z[j] + 0.5 * k[0, j];
                }
                x += 0.5 * h;
                dy();
                for (int j = 0; j < n; j++)
                {
                    k[1, j] = h * f[j];
                    y[j] = z[j] + 2 * k[1, j] - k[0, j];
                }
                x += 0.5 * h;
                dy();
                for (int j = 0; j < n; j++)
                {
                    k[2, j] = h * f[j];
                    y[j] = z[j] + (k[0, j] + 4 * k[1, j] + k[2, j]) / 6.0;
                }
                x += h;
                for (int i = 0; i < n; i++)
                    Console.Write($"y[{i + 1}]=" + y[i] + "\t");
                Console.WriteLine();
                Console.Write($"True y1={2.0 / 3.0 * Math.Exp(-x) + 5.0 / 3.0 * Math.Sqrt(3.0) * Math.Exp(1.0 / 2.0 * x) * Math.Sin(1.0 / 2.0 * Math.Sqrt(3.0) * x) + 1.0 / 3.0 * Math.Exp(1.0 / 2.0 * x) * Math.Cos(1.0 / 2.0 * Math.Sqrt(3.0) * x)}");
                Console.Write($"\tTrue y2={-2.0 / 3.0 * Math.Exp(-x) + 2.0 / 3.0 * Math.Sqrt(3.0) * Math.Exp(1.0 / 2.0 * x) * Math.Sin(1.0 / 2.0 * Math.Sqrt(3.0) * x) + 8.0 / 3.0 * Math.Exp(1.0 / 2.0 * x) * Math.Cos(1.0 / 2.0 * Math.Sqrt(3.0) * x)}");
                Console.WriteLine($"\tTrue y3={2.0 / 3.0 * Math.Exp(-x) - Math.Sqrt(3.0) * Math.Exp(1.0 / 2.0 * x) * Math.Sin(1.0 / 2.0 * Math.Sqrt(3.0) * x) + 7.0 / 3.0 * Math.Exp(1.0 / 2.0 * x) * Math.Cos(1.0 / 2.0 * Math.Sqrt(3.0) * x)}");
            } while (x <= b);
            //Console.WriteLine($"y1={y[0]}\ty2={y[1]}\ty3={y[2]}");
        }
    }
}