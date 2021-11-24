using System;

namespace War
{
	public class Game
	{
		private CardSuit _trump;
		
		public void Start()
		{
			CardDeck deck = CardDeck.CreateFull();
			deck = deck.Shuffle();
			
			_trump = CardSuit.GetRandom();
			Console.WriteLine($"Trump: {_trump.Value}");
			
			(CardDeck partOne, CardDeck partTwo) = deck.Split();
			Players players = new Players(partOne, partTwo);
			
			GameLoop(players);
		}

		private void GameLoop(Players players)
		{
			while (!players.Player1.HandDeck.IsEmpty && !players.Player2.HandDeck.IsEmpty)
			{
				Console.ReadKey();
				players = Turn(players);
			}
			
			if (players.Player1.ScoreDeck.Count > players.Player2.ScoreDeck.Count)
				Console.WriteLine($"Player 1 win! 🎉");
			else if (players.Player1.ScoreDeck.Count > players.Player2.ScoreDeck.Count)
				Console.WriteLine($"Player 2 win! 🎉");
			else
				Console.WriteLine($"Draw 🤝");
				
		}

		private Players Turn(Players players)
		{
			(Card cardOne, CardDeck deckOne) = players.Player1.HandDeck.Pop();
			(Card cardTwo, CardDeck deckTwo) = players.Player2.HandDeck.Pop();

			players.Player1.HandDeck = deckOne;
			players.Player2.HandDeck = deckTwo;
			
			Console.WriteLine($"Player 1 H: {players.Player1.HandDeck.Count} S: {players.Player1.ScoreDeck.Count} |\t{cardOne}\t{cardTwo}\t| {players.Player2.HandDeck.Count} S: {players.Player2.ScoreDeck.Count} Player 2");

			players = CompareCards(players, cardOne, cardTwo);

			return players;
		}

		private Players CompareCards(Players players, Card cardOne, Card cardTwo)
		{
			bool cardOneIsTrump = cardOne.IsTrump(_trump);
			bool cardTwoIsTrump = cardTwo.IsTrump(_trump);

			if (cardOneIsTrump == cardTwoIsTrump)
			{
				if (cardOne.Value.Value > cardTwo.Value.Value)
				{
					Player1Win(players, cardOne, cardTwo);
				}
				else if (cardOne.Value.Value < cardTwo.Value.Value)
				{
					Player2Win(players, cardOne, cardTwo);
				}
				else
				{
					Draw(players, cardOne, cardTwo);
				}
			}
			else if (cardOneIsTrump)
			{
				Player1Win(players, cardOne, cardTwo);
			}
			else
			{
				Player2Win(players, cardOne, cardTwo);
			}

			return players;
			
			void Player1Win(Players p, Card card1, Card card2)
			{
				p.Player1.ScoreDeck = p.Player1.ScoreDeck.Push(card1);
				p.Player1.ScoreDeck = p.Player1.ScoreDeck.Push(card2);
			}
			
			void Player2Win(Players p, Card card1, Card card2)
			{
				p.Player2.ScoreDeck = p.Player2.ScoreDeck.Push(card1);
				p.Player2.ScoreDeck = p.Player2.ScoreDeck.Push(card2);
			}
			
			void Draw(Players p, Card card1, Card card2)
			{
				p.Player1.ScoreDeck = p.Player1.ScoreDeck.Push(card1);
				p.Player2.ScoreDeck = p.Player2.ScoreDeck.Push(card2);
			}
		}
	}
}