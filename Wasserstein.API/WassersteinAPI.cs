namespace Wasserstein.API;

using System.Numerics;

public static class WassersteinAPI
{
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

  public static T Wasserstein<T>(T[] p, T[] q) where  T : INumber<T>
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
