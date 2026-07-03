namespace Wasserstein;

using System.Numerics;

public static class Program
{
  public static void Main(string[] args)
  {
    var P = new[] {0.6, 0.1, 0.1, 0.1, 0.1};
    var Q1 = new[] {0.1, 0.1, 0.6, 0.1, 0.1};
    var Q2 = new[] {0.1, 0.1, 0.1, 0.1, 0.6};

    var wass_p_q1 = Wasserstein(P, Q1);
    var wass_p_q2 = Wasserstein(P, Q2);

    Console.WriteLine($"Wasserstein(P, Q1) = {wass_p_q1:F4}");
    Console.WriteLine($"Wasserstein(P, Q2) = {wass_p_q2:F4}");
  }

  private static int FirstNonZero<T>(T[] vec) where T:INumber<T>
  {
    var dim = vec.Length;
    for (var i = 0; i < dim; ++i)
    {
      if (vec[i] > T.Zero)
      {
        return i;
      }
    }

    return -1;
  }

  private static T MoveDirt<T>(T[] dirt, int dirtIdx, T[] holes, int holeIdx) where  T : INumber<T>
  {
    var flow = T.Zero;
    var dist = 0;
    if (dirt[dirtIdx] <= holes[holeIdx])
    {
      flow = dirt[dirtIdx];
      dirt[dirtIdx] = T.Zero;
      holes[holeIdx] -= flow;
    }
    else if (dirt[dirtIdx] > holes[holeIdx])
    {
      flow = holes[holeIdx];
      dirt[dirtIdx] -= flow;
      holes[holeIdx] = T.Zero;
    }

    dist = Math.Abs(dirtIdx - holeIdx);

    return flow * (T)Convert.ChangeType(dist, typeof(T));
  }

  private static T Wasserstein<T>(T[] p, T[] q) where  T : INumber<T>
  {
    var dirt = (T[]) p.Clone();
    var holes = (T[]) q.Clone();
    var totalWork = T.Zero;
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
