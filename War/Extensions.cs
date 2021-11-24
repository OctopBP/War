using System;
using System.Collections.Generic;
using System.Linq;

namespace War
{
	public static class Extensions
	{
		private static Random rng = new Random();  

		public static Stack<T> Shuffle<T>(this Stack<T> stack)
		{
			List<T> list = stack.ToList();
			list.Shuffle();
			return new Stack<T>(list);
		}
		
		public static IList<T> Shuffle<T>(this IList<T> list)
		{
			int n = list.Count;  
			while (n > 1) {  
				n--;  
				int k = rng.Next(n + 1);  
				(list[k], list[n]) = (list[n], list[k]);
			}

			return list;
		}
	}
}