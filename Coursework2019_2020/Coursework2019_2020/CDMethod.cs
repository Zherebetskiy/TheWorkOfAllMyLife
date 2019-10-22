using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework2019_2020
{
    public class CDMethod
    {
        private readonly int Lx;
        private readonly int Ly;
        private readonly int J;
        private readonly int T;
        private readonly int N;
        private readonly int M;
        private readonly double Hx;
        private readonly double Hy;
        private readonly double Tau;
        private readonly double[,] mtrMid;
        private readonly double[,] mtr;
        private readonly double[,] mtrMidB;
        private readonly double[,] mtrB;

        public CDMethod(int Lx, int Ly, int T, int J, int N, int M)
        {
            this.Lx = Lx;
            this.Ly = Ly;
            this.T = T;
            this.J = J;
            this.N = N;
            this.M = M;
            Hx = (double) Lx / N;
            Hy = (double) Ly / M;
            Tau = (double) T / J;
            mtrMid = new double[N + 1, M + 1];
            mtr = new double[N + 1, M + 1];
            mtrMidB = new double[N + 1, N + 1];
            mtrB = new double[M + 1, M + 1];
            DefineMatrix();
        }

       

        public void Solve()
        {
            for (int j = 0; j < J + 1; j++)
            {

                //from j to j+1/2
                for (int i = 1; i < N; i++)
                {
                    var another = -(Tau / (2 * Hx * Hx));
                    var main = 1 + Tau / (Hx * Hx);
                    var matrA = BuildThreeDiagonalMatrix(main, another, N - 1);

                    var vectorB = BuildVectorB(N - 1, i, mtr, Hy, j + (1 / 2));

                    var res = Prohonka.Solve(matrA, vectorB);

                    for (int q = 0; q < M; q++)
                    {
                        mtrMid[i, q] = res[q];
                    }
                }

                //from j+1/2 to j+1
                for (int k = 1; k < M; k++)
                {

                }
            }

        }

        private double[] BuildVectorB(int lenght, int index, double[,] mtr, double div, double t)
        {
            var v = new double[lenght];

            for (int i = 0; i < lenght + 1; i++)
            {
                v[i] = F(i * Hx, index * Hy, t) + (mtr[i, index - 1] - 2 * mtr[i, index] + mtr[i, index + 1]) / div;
            }

            return v;
        }

        private double[,] BuildThreeDiagonalMatrix(double main, double another, int order)
        {
            var m = new double[order, order];
            for (int i = 0; i < m.GetLength(0); i++)
            {
                m[i, i] = main;
                if (i != 0) m[i - 1, i] = another;
                if (i != m.GetLength(0) - 1) m[i + 1, i] = another;
            }

            return m;
        }

        private void DefineMatrixB(double[,] matrix)
        {
            for (int i = 0; i < mtr.GetLength(0); i++)
            {
                for (int j = 0; j < mtr.GetLength(1); j++)
                {
                    mtr[i, j] = U0(i * Hx, j * Hy);
                }
            }
        }

        private void DefineMatrix()
        {
            for (int i = 0; i < mtr.GetLength(0); i++)
            {
                for (int j = 0; j < mtr.GetLength(1); j++)
                {
                    mtr[i, j] = U0(i * Hx, j * Hy);
                }
            }
        }

        private void PrintMatrix(double[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write($"{matrix[i, j]} ");
                }
                Console.WriteLine();
            }
        }

        #region My 1-4
        private double My1(double y, double t)
        {
            return 0;
        }

        private double My2(double y, double t)
        {
            return y * Math.Cos(t);
        }

        private double My3(double x, double t)
        {
            return 0;
        }

        private double My4(double x, double t)
        {
            return x * Math.Cos(t);
        }

        private double F(double x, double y, double t)
        {
            return x * y* Math.Sin(t);
        }

        #endregion

        #region Un 
        private double U0(double x, double y)
        {
            return x * y;
        }

        private double UT(double x, double y, double t)
        {
            return x * y * Math.Cos(t);
            //return x * y + 1 + (Math.Pow(Math.E, t) - Math.Pow(Math.E, -(5 * Math.PI * Math.PI * t / 4))) / (1 + 5 * Math.PI * Math.PI / 4)
            //    * Math.Cos(Math.PI * x / 2) * Math.Sin(Math.PI * y / 4);
        }
        #endregion
    }
}
