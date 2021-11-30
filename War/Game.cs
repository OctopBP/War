using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using LanguageExt;
using War.card;

namespace War {
	public record Game(CardSuit trump, IComparer<Card> cmp) {
		private const int PlayersCount = 2;

		private CardSuit _trump;

		public void Start()
		{
			CardDeck deck = CardDeck.CreateFull();
			CardDeck shuffledDeck = deck.Shuffle();

			_trump = CardSuit.GetRandom();
			Console.WriteLine($"Trump: {_trump.value}");

			Lst<CardDeck> parts = shuffledDeck.Split(PlayersCount);
			GameState gameState = new(parts);

			GameLoop(gameState);
		}

		private void GameLoop(GameState gameState)
		{
			while (gameState.Players.All(p => !p.HandDeck.IsEmpty))
			{
				Console.ReadKey();
				gameState = Turn(gameState);
			}

			GameResult(gameState);
		}

		private GameState Turn(GameState gameState)
		{
			GameState newGameState = gameState
				.OpenCards()
				.PrintState();

			return CompareCards(newGameState);
		}

		private GameState CompareCards(GameState gameState)
		{
			return gameState.CompareCards(_trump);
		}

		private void GameResult(GameState gameState)
		{
			int maxScore = gameState.Players
				.Max(player => player.ScoreDeck.Count);

			ImmutableList<Player> winners = gameState.Players
				.Where(player => player.ScoreDeck.Count == maxScore)
				.ToImmutableList();

			Console.WriteLine(winners.Count > 1
				? $"Draw between players #{string.Join(", #", winners.Select(p => p.Id))} 🤝"
				: $"Player #{winners.First().Id} win! 🎉");
		}
	}
}