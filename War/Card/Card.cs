namespace War.Card
{
	public class Card
	{
		public CardSuit Suit { get; }
		public CardValue Value { get; }

		
		public int Val(CardSuit trump) =>
			(int) Value.Value + (IsTrump(trump) ? 100 : 0);
		
		public bool IsTrump(CardSuit trump) =>
			Suit.Value == trump.Value;

		public Card(Suit suit, Value value)
		{
			Suit = new CardSuit(suit);
			Value = new CardValue(value);
		}

		public bool IsHigherThen(Card card, CardSuit trump)
		{
			if (IsBoothTrumpOrNot(this, card, trump))
				return Value.Value > card.Value.Value;

			return IsTrump(trump);
		}
		
		public bool IsEqual(Card card, CardSuit trump) =>
			IsBoothTrumpOrNot(this, card, trump) && SameSuit(this, card);
		
		private bool SameSuit(Card card1,Card card2) =>
			card1.Suit.Value == card2.Suit.Value;

		private bool SameValue(Card card1,Card card2) =>
			card1.Value.Value == card2.Value.Value;

		private bool IsBoothTrumpOrNot(Card card1, Card card2, CardSuit trump) =>
			card1.IsTrump(trump) == card2.IsTrump(trump);
		
		public bool Equals(Card card) =>
			SameSuit(this, card) && SameValue(this, card);
		
		public override string ToString() =>
			$"{Suit.Value.ToString()[0]}{(int)Value.Value}";
	}
}