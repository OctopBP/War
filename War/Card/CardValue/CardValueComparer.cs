using System.Collections.Generic;

namespace War.Card
{
	public class CardValueComparer : IComparer<CardValue>
	{
		public int Compare(CardValue x, CardValue y)
		{
			if (ReferenceEquals(x, y)) return 0;
			if (ReferenceEquals(null, y)) return 1;
			if (ReferenceEquals(null, x)) return -1;
			return x.Value.CompareTo(y.Value);
		}
	}
}