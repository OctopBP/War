using System.Linq;

namespace War.Card
{
	public record Card(CardSuit Suit, CardValue Value)
	{
		public Card(Suit suit, Value value) : this(new CardSuit(suit), new CardValue(value)) { }
		public override string ToString() => $"{Suit.Value.ToString().First()}{(int) Value.Value}";
	}
}