namespace Wasserstein;

public static class Program
{
  public static void Main(string[] args)
  {
    Console.WriteLine("\nBegin demo \n");

    var P = new[] {0.6, 0.1, 0.1, 0.1, 0.1};
    var Q1 = new[] {0.1, 0.1, 0.6, 0.1, 0.1};
    var Q2 = new[] {0.1, 0.1, 0.1, 0.1, 0.6};

    var wass_p_q1 = MyWasserstein(P, Q1);
    var wass_p_q2 = MyWasserstein(P, Q2);

    Console.WriteLine("Wasserstein(P, Q1) = " + wass_p_q1.ToString("F4"));
    Console.WriteLine("Wasserstein(P, Q2) = " + wass_p_q2.ToString("F4"));

    Console.WriteLine("\nEnd demo ");
  } // Main

  private static int FirstNonZero(double[] vec)
  {
    var dim = vec.Length;
    for (var i = 0; i < dim; ++i)
    {
      if (vec[i] > 0.0)
      {
        return i;
      }
    }

    return -1;
  }

  private static double MoveDirt(double[] dirt, int di, double[] holes, int hi)
  {
    var flow = 0.0;
    var dist = 0;
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

  private static double MyWasserstein(double[] p, double[] q)
  {
    var dirt = (double[]) p.Clone();
    var holes = (double[]) q.Clone();
    var totalWork = 0.0;
    while (true)
    {
      var fromIdx = FirstNonZero(dirt);
      var toIdx = FirstNonZero(holes);
      if (fromIdx == -1 || toIdx == -1)
      {
        break;
      }

      var work = MoveDirt(dirt, fromIdx, holes, toIdx);
      totalWork += work;
    }

    return totalWork;
  }
}
