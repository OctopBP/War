namespace War
{
	public record Player
	{
		public int Id { get; init; }
		public CardDeck HandDeck { get; init; }
		public CardDeck ScoreDeck { get; init; }

		public Player(int id, CardDeck deck)
		{
			Id = id;
			HandDeck = deck;
			ScoreDeck = CardDeck.Empty();
		}

		private Player(int id, CardDeck hanDeck, CardDeck scoreDeck)
		{
			Id = id;
			HandDeck = hanDeck;
			ScoreDeck = scoreDeck;
		}

		public Player ScoreWithCard(Card card)
		{
			CardDeck newScoreDeck = ScoreDeck.WithCard(card);
			return new Player(Id, HandDeck, newScoreDeck);
		}

		public Player ScoreWithCards(Card[] cards)
		{
			CardDeck newScoreDeck = ScoreDeck.WithCards(cards);
			return new Player(Id, HandDeck, newScoreDeck);
		}

		public (Card card, Player decks) TakeFromTop()
		{
			(Card card, CardDeck deck) = HandDeck.TakeFromTop();
			return (card, new Player(Id, deck, ScoreDeck));
		}
	}
}