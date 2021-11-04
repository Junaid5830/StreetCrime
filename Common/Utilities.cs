using System;
using System.Linq;

namespace Common
{
  public static class Utilities
  {
    public static string GenerateRandom()
    {
      var generator = new Random();
      var r = generator.Next(0, 1000000).ToString("D6");
      if (r.Distinct().Count() == 1)
      {
        r = GenerateRandom();
      }
      return r;
    }
  }
}
