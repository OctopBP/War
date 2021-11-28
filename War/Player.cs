using System;
using LanguageExt;

namespace War
{
	public record Player(int Id, CardDeck HandDeck, CardDeck ScoreDeck, Card.Card OpenCard = null)
	{
		public Player(CardDeck handDeck) :
			this(new Random().Next() % 100, handDeck, new CardDeck(new Lst<Card.Card>())) { }

		public Player OpenCardToScore()
		{
			CardDeck newScoreDeck = ScoreDeck with { Deck = ScoreDeck.Deck.Add(OpenCard) };
			return this with { ScoreDeck = newScoreDeck };
		}

		public Player CardsToScores(Lst<Card.Card> cards)
		{
			CardDeck newScoreDeck = ScoreDeck with { Deck = ScoreDeck.Deck.AddRange(cards) };
			return this with { ScoreDeck = newScoreDeck };
		}

		public Player TakeOpenCard()
		{
			return this with { OpenCard = null };
		}

		public Player OpenCardFromTop()
		{
			Card.Card card = HandDeck.Deck.First();
			CardDeck newHandDeck = HandDeck with { Deck = HandDeck.Deck.Remove(card) };
			return this with { HandDeck = newHandDeck, OpenCard = card };
		}
	}
}