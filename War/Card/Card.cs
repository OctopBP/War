using System.Linq;

namespace War.Card
{
	public record Card(CardSuit Suit, CardValue Value)
	{
		public Card(Suit suit, Value value) : this(new CardSuit(suit), new CardValue(value)) { }

		public int Val(CardSuit trump) => (int) Value.Value + (IsTrump(trump) ? 100 : 0);
		public override string ToString() => $"{Suit.Value.ToString().First()}{(int) Value.Value}";
		private bool IsTrump(CardSuit trump) => Suit.Value == trump.Value;
	}
}