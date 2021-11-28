using System.Collections.Generic;

namespace War.Card
{
	public class CardSuitComparer : IComparer<CardSuit>
	{
		private CardSuit Trump { get; }

		public CardSuitComparer(CardSuit trump)
		{
			Trump = trump;
		}

		public int Compare(CardSuit x, CardSuit y)
		{
			if (ReferenceEquals(x, y)) return 0;
			if (ReferenceEquals(null, y)) return 1;
			if (ReferenceEquals(null, x)) return -1;

			bool isTrump1 = x == Trump;
			bool isTrump2 = y == Trump;
			bool boothTrumpOrNo = isTrump1 == isTrump2;

			if (boothTrumpOrNo)
				return 0;

			return isTrump1 ? 1 : -1;
		}
	}
}