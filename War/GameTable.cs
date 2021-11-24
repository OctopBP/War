namespace War
{
	public class Players
	{
		public PlayerDecks Player1;
		public PlayerDecks Player2;

		public Players(CardDeck deck1, CardDeck deck2)
		{
			Player1 = new PlayerDecks(deck1);
			Player2 = new PlayerDecks(deck2);
		}
	}
}