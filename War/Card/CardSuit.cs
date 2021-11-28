using System;

namespace War.Card
{
	public record CardSuit(Suit Value)
	{
		public static CardSuit GetRandom()
		{
			Array values = Enum.GetValues(typeof(Suit));
			Random random = new();
			Suit randomSuit = (Suit) values.GetValue(random.Next(values.Length));
			CardSuit randomCardSuit = new(randomSuit);
			return randomCardSuit;
		}
	}
}