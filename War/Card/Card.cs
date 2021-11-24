namespace War
{
	public class Card
	{
		public CardSuit Suit { get; }
		public CardValue Value { get; }

		public bool IsTrump(CardSuit trump) => Suit.Value == trump.Value;

		public Card(Suit suit, Value value)
		{
			Suit = new CardSuit(suit);
			Value = new CardValue(value);
		}

		public override string ToString()
		{
			return $"{Suit.Value.ToString()[0]}{(int)Value.Value}";
		}
	}
}