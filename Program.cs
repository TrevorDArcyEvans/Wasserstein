namespace Wasserstein;

using Wasserstein.API;

public static class Program
{
  public static void Main(string[] args)
  {
    var P = new[] {0.6, 0.1, 0.1, 0.1, 0.1};
    var Q1 = new[] {0.1, 0.1, 0.6, 0.1, 0.1};
    var Q2 = new[] {0.1, 0.1, 0.1, 0.1, 0.6};

    var wass_p_q1 = WassersteinAPI.Wasserstein(P, Q1);
    var wass_p_q2 = WassersteinAPI.Wasserstein(P, Q2);

    Console.WriteLine($"Wasserstein(P, Q1) = {wass_p_q1:F4}");
    Console.WriteLine($"Wasserstein(P, Q2) = {wass_p_q2:F4}");
  }
}
