using System.Collections.Generic;

namespace War.Card
{
	public class CardComparer : IComparer<Card>
	{
		private CardSuit Trump { get; }

		public CardComparer(CardSuit trump)
		{
			Trump = trump;
		}

		public int Compare(Card x, Card y)
		{
			if (ReferenceEquals(x, y)) return 0;
			if (ReferenceEquals(null, y)) return 1;
			if (ReferenceEquals(null, x)) return -1;

			int suitComparison = new CardSuitComparer(Trump).Compare(x.Suit, y.Suit);
			if (suitComparison != 0) return suitComparison;

			return new CardValueComparer().Compare(x.Value, y.Value);
		}
	}
}