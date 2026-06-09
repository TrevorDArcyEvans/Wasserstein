using System;
namespace Wasserstein
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine("\nBegin demo \n");

      double[] P = new double[]
        { 0.6, 0.1, 0.1, 0.1, 0.1 };
      double[] Q1 = new double[]
        { 0.1, 0.1, 0.6, 0.1, 0.1 };
      double[] Q2 = new double[]
        { 0.1, 0.1, 0.1, 0.1, 0.6 };

      double wass_p_q1 = MyWasserstein(P, Q1);
      double wass_p_q2 = MyWasserstein(P, Q2);

      Console.WriteLine("Wasserstein(P, Q1) = " +
                        wass_p_q1.ToString("F4"));
      Console.WriteLine("Wasserstein(P, Q2) = " +
                        wass_p_q2.ToString("F4"));

      Console.WriteLine("\nEnd demo ");
      Console.ReadLine();
    }  // Main

    static int FirstNonZero(double[] vec)
    {
      int dim = vec.Length;
      for (int i = 0; i < dim; ++i)
        if (vec[i] > 0.0)
          return i;
      return -1;
    }

    static double MoveDirt(double[] dirt, int di,
      double[] holes, int hi)
    {
      double flow = 0.0;
      int dist = 0;
      if (dirt[di] <= holes[hi])
      {
        flow = dirt[di];
        dirt[di] = 0.0;
        holes[hi] -= flow;
      }
      else if (dirt[di] > holes[hi])
      {
        flow = holes[hi];
        dirt[di] -= flow;
        holes[hi] = 0.0;
      }
      dist = Math.Abs(di - hi);
      return flow * dist;
    }

    static double MyWasserstein(double[] p, double[] q)
    {
      double[] dirt = (double[])p.Clone();
      double[] holes = (double[])q.Clone();
      double totalWork = 0.0;
      while (true)
      {
        int fromIdx = FirstNonZero(dirt);
        int toIdx = FirstNonZero(holes);
        if (fromIdx == -1 || toIdx == -1)
          break;
        double work = MoveDirt(dirt, fromIdx,
          holes, toIdx);
        totalWork += work;
      }
      return totalWork;
    }
  }  // Program
}  // ns
