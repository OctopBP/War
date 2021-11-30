using System;
using System.Collections.Generic;

namespace War {
  public static class ComparerExts {
    // map: A => B
    // contraMap: B => A
    
    public static IComparer<Wider> contraMap<Narrower, Wider>(
      this IComparer<Narrower> cmp, Func<Wider, Narrower> mapper
    ) => Comparer<Wider>.Create((a, b) => cmp.Compare(mapper(a), mapper(b)));
    
    public static IComparer<A> andThen<A>(this IComparer<A> cmp1, IComparer<A> cmp2) =>
      Comparer<A>.Create((a, b) => {
        var res1 = cmp1.Compare(a, b);
        return res1 == 0 ? cmp2.Compare(a, b) : res1;
      });
  }
}