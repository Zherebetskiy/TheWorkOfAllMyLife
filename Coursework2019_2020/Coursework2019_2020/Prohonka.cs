namespace Coursework2019_2020
{
    public static class Prohonka
    {
        public static double[] Solve(double[,] A, double[] B)
        {
            int n = B.Length;
            double[] a = new double[n];
            double[] b = new double[n];
            double[] c = new double[n];
            double[] alpha = new double[n];
            double[] beta = new double[n];
            double[] ksi = new double[n];
            double[] eta = new double[n];
            double[] y = new double[n];
            double[] x = new double[n];

            int i;
            for (i = 0; i < n - 1; ++i)
            {
                b[i] = A[i, i + 1];
            }

            for (i = 0; i < n; ++i)
            {
                c[i] = A[i, i];
                y[i] = B[i];
            }

            for (i = 1; i < n; ++i)
            {
                a[i] = A[i, i - 1];
            }

            int p = (int)((double)n / 2.0D);
            alpha[1] = -b[0] / c[0];
            beta[1] = y[0] / c[0];

            for (i = 1; i < p; ++i)
            {
                alpha[i + 1] = -b[i] / (a[i] * alpha[i] + c[i]);
                beta[i + 1] = (y[i] - a[i] * beta[i]) / (a[i] * alpha[i] + c[i]);
            }

            ksi[n - 1] = -a[n - 1] / c[n - 1];
            eta[n - 1] = y[n - 1] / c[n - 1];

            for (i = n - 2; i >= p - 1; --i)
            {
                ksi[i] = -a[i] / (c[i] + b[i] * ksi[i + 1]);
                eta[i] = (y[i] - b[i] * eta[i + 1]) / (c[i] + b[i] * ksi[i + 1]);
            }

            x[p - 1] = (alpha[p] * eta[p] + beta[p]) / (1.0D - alpha[p] * ksi[p]);

            for (i = p - 2; i >= 0; --i)
            {
                x[i] = alpha[i + 1] * x[i + 1] + beta[i + 1];
            }

            x[p] = (y[p] - b[p] * eta[p + 1]) / (b[p] * ksi[p + 1] + c[p]);

            for (i = p - 1; i < n - 1; ++i)
            {
                x[i + 1] = ksi[i + 1] * x[i] + eta[i + 1];
            }

            return x;
        }
    }
}
