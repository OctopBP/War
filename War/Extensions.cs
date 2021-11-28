using System;
using System.Collections.Generic;
using LanguageExt;

namespace War
{
	public static class Extensions
	{
		private static readonly Random Rnd = new();

		public static Lst<T> Shuffle<T>(this Lst<T> lst)
		{
			IList<T> list = lst
				.ToList()
				.Shuffle();

			return new Lst<T>(list);
		}

		public static IList<T> Shuffle<T>(this IList<T> list)
		{
			int n = list.Count;
			while (n > 1)
			{
				n--;
				int k = Rnd.Next(n + 1);
				(list[k], list[n]) = (list[n], list[k]);
			}

			return list;
		}
	}
}