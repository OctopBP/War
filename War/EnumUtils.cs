using System;
using System.Linq;
using LanguageExt;

namespace War {
  public static class EnumUtils<A> where A : Enum {
    public static readonly Arr<A> all = new(Enum.GetValues(typeof(A)).Cast<A>());

    public static Eff<A> random(Random rng) => 
      Eff<A>.Effect(() => all[rng.Next(all.Length)]);
  }
}