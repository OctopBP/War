namespace War
{
	public class PlayerDecks
	{
		public CardDeck HandDeck;
		public CardDeck ScoreDeck;

		public PlayerDecks(CardDeck deck)
		{
			HandDeck = deck;
			ScoreDeck = CardDeck.Empty();
		}
	}
}