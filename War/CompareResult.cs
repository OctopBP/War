using System;

namespace War {
  public enum CompareResult : byte { LT, EQ, GT }

  public static class CompareResultExts {
    public static CompareResult fromCmpRes(this int res) => res switch {
      < 0 => CompareResult.LT,
      > 0 => CompareResult.GT,
      _ => CompareResult.EQ
    };
    
    public static int asInt(this CompareResult res) => res switch {
      CompareResult.LT => -1,
      CompareResult.EQ => 0,
      CompareResult.GT => 1,
      _ => throw new ArgumentOutOfRangeException(nameof(res), res, null)
    };
  }
}