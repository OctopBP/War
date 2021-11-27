using System;
using LanguageExt;

namespace War
{
	public record Player
	{
		public int Id { get; }
		public CardDeck HandDeck { get; }
		public CardDeck ScoreDeck { get; }
		public Card.Card OpenCard { get; }

		public Player(CardDeck handDeck)
		{
			Id = new Random().Next() % 100;
			HandDeck = handDeck;
			ScoreDeck = CardDeck.Empty();
		}

		private Player(int id, CardDeck handDeck, CardDeck scoreDeck)
		{
			Id = id;
			HandDeck = handDeck;
			ScoreDeck = scoreDeck;
		}
		
		private Player(int id, CardDeck handDeck, CardDeck scoreDeck, Card.Card openCard)
		{
			Id = id;
			HandDeck = handDeck;
			ScoreDeck = scoreDeck;
			OpenCard = openCard;
		}

		public Player OpenCardToScore()
		{
			CardDeck newScoreDeck = ScoreDeck.WithCard(OpenCard);
			return new Player(Id, HandDeck, newScoreDeck);
		}

		public Player ScoreWithCards(Lst<Card.Card> cards)
		{
			CardDeck newScoreDeck = ScoreDeck.WithCards(cards);
			return new Player(Id, HandDeck, newScoreDeck);
		}

		public Player TakeOpenCard()
		{
			return new Player(Id, HandDeck, ScoreDeck, null);
		}

		public Player OpenCardFromTop()
		{
			(Card.Card card, CardDeck deck) = HandDeck.TakeFromTop();
			return new Player(Id, deck, ScoreDeck, card);
		}
	}
}