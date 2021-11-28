using System;
using System.Linq;
using LanguageExt;
using War.Card;

namespace War
{
	public record GameState(Lst<Player> Players)
	{
		public GameState(Lst<CardDeck> decks) : this(decks.Select(deck => new Player(deck))) { }

		public GameState OpenCards()
		{
			Lst<Player> players = Players.Select(player => player.OpenCardFromTop());
			return this with { Players = players };
		}

		public GameState PrintState()
		{
			Console.WriteLine("New turn");

			Players
				.OrderBy(p => p.Id).ToList()
				.ForEach(p =>
					Console.WriteLine(
						$"Player #{p.Id} Hand: {p.HandDeck.Count} Scores: {p.ScoreDeck.Count}\tCard: {p.OpenCard}"));

			return this;
		}

		public GameState CompareCards(CardSuit trump)
		{
			int maxCard = Players
				.Select(player => player.OpenCard)
				.Max(card => card.Val(trump));

			Lst<Player> winners = new(Enumerable.Where(Players, player => player.OpenCard.Val(trump) == maxCard));

			if (winners.Count > 1)
				return OnDraw();

			return OnWin(trump, maxCard, winners);
		}

		private GameState OnDraw()
		{
			Lst<Player> players = Players.Select(player => player.OpenCardToScore());
			return this with { Players = players };
		}

		private GameState OnWin(CardSuit trump, int maxCard, Lst<Player> winners)
		{
			Lst<Player> losers = new(Players.Where(player => player.OpenCard.Val(trump) != maxCard));
			Lst<Card.Card> cards = Players.Select(p => p.OpenCard);
			Lst<Player> newWinners = new(winners.Select(p => p.CardsToScores(cards)));
			Lst<Player> players = newWinners.AddRange(losers);
			Lst<Player> newPlayers = players.Select(p => p.TakeOpenCard());

			return this with { Players = newPlayers };
		}
	}
}