using System;
using LanguageExt;

namespace War
{
	public class Game
	{
		private CardSuit _trump;
		private const int PlayersCount = 2;
		
		public void Start()
		{
			CardDeck deck = CardDeck.CreateFull();
			deck = deck.Shuffle();
			
			_trump = CardSuit.GetRandom();
			Console.WriteLine($"Trump: {_trump.Value}");
			
			Lst<CardDeck> parts = deck.Split(PlayersCount);
			GameState gameState = new GameState(parts);
			
			GameLoop(gameState);
		}

		private void GameLoop(GameState gameState)
		{
			while (gameState.Players.All(p => !p.HandDeck.IsEmpty))
			{
				Console.ReadKey();
				gameState = Turn(gameState);
			}

			if (gameState.Players[0].ScoreDeck.Count > gameState.Players[1].ScoreDeck.Count)
				Console.WriteLine($"Player 1 win! 🎉");
			else if (gameState.Players[1].ScoreDeck.Count > gameState.Players[0].ScoreDeck.Count)
				Console.WriteLine($"Player 2 win! 🎉");
			else
				Console.WriteLine($"Draw 🤝");
		}

		private GameState Turn(GameState gameState)
		{
			(Card cardOne, GameState gameState1) = gameState.PlayerTakeFromTop(0);
			(Card cardTwo, GameState gameState2) = gameState1.PlayerTakeFromTop(1);

			Console.WriteLine($"Player 1 H: {gameState.Players[0].HandDeck.Count} S: {gameState.Players[0].ScoreDeck.Count} |\t{cardOne}\t{cardTwo}\t| {gameState.Players[1].HandDeck.Count} S: {gameState.Players[1].ScoreDeck.Count} Player 2");

			GameState gameState3 = CompareCards(gameState2, cardOne, cardTwo);
			return gameState3;
		}

		private GameState CompareCards(GameState gameState, Card cardOne, Card cardTwo)
		{
			bool cardOneIsTrump = cardOne.IsTrump(_trump);
			bool cardTwoIsTrump = cardTwo.IsTrump(_trump);

			if (cardOneIsTrump == cardTwoIsTrump)
			{
				if (cardOne.Value.Value > cardTwo.Value.Value)
				{
					return CardsToScore(gameState, cardOne, cardTwo, 0, 0);
				}

				if (cardOne.Value.Value < cardTwo.Value.Value)
				{
					return CardsToScore(gameState, cardOne, cardTwo, 1, 1);
				}

				return CardsToScore(gameState, cardOne, cardTwo, 0, 1);
			}

			if (cardOneIsTrump)
			{
				return CardsToScore(gameState, cardOne, cardTwo, 0, 0);
			}

			return CardsToScore(gameState, cardOne, cardTwo, 1, 1);

			GameState CardsToScore(GameState gs, Card card1, Card card2, int cardOneTargetId,  int cardTwoTargetId)
			{
				GameState gs1 = gs.PlayerWithCard(cardOneTargetId, card1);
				GameState gs2 = gs1.PlayerWithCard(cardTwoTargetId, card2);

				return gs2;
			}
		}
	}
}