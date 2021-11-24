using System;

namespace War
{
	public class CardSuit
	{
		public Suit Value { get; }

		public CardSuit(Suit suit)
		{
			Value = suit;
		}
		
		public static CardSuit GetRandom()
		{
			Array values = Enum.GetValues(typeof(Suit));
			Random random = new Random();
			Suit randomSuit = (Suit) values.GetValue(random.Next(values.Length));
			CardSuit randomCardSuit= new CardSuit(randomSuit); 
			return randomCardSuit;
		}
	}
}