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
			Console.WriteLine("- New round -");

			Players
				.OrderBy(p => p.Id).ToList()
				.ForEach(p =>
					Console.WriteLine(
						$"Player #{p.Id} Hand: {p.HandDeck.Count} Scores: {p.ScoreDeck.Count}\tCard: {p.OpenCard}"));

			return this;
		}

		public GameState CompareCards(CardSuit trump)
		{
			CardComparer comparer = new(trump);

			Card.Card maxCard = Players
				.Select(player => player.OpenCard)
				.OrderBy(card => card, comparer)
				.Last();

			Lst<Player> winners =
				new(Players.Where(player => comparer.Compare(player.OpenCard, maxCard) == 0));

			if (winners.Count > 1)
				return OnDraw();

			return OnWin(winners);
		}

		private GameState OnDraw()
		{
			Console.WriteLine($"Draw in this round");

			Lst<Player> players = Players.Select(player => player.OpenCardToScore());
			return this with { Players = players };
		}

		private GameState OnWin(Lst<Player> winners)
		{
			Lst<Player> losers = new(Players.Where(player => !winners.Contains(player)));
			Lst<Card.Card> cards = Players.Select(p => p.OpenCard);
			Lst<Player> newWinners = new(winners.Select(p => p.CardsToScores(cards)));
			Lst<Player> players = newWinners.AddRange(losers);
			Lst<Player> newPlayers = players.Select(p => p.TakeOpenCard());

			Console.WriteLine(
				$"#{string.Join(", #", winners.Select(p => p.Id))} get {cards.Count} score{(cards.Count > 1 ? "s" : "")}");

			return this with { Players = newPlayers };
		}
	}
}