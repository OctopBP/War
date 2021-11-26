using LanguageExt;
using LanguageExt.UnsafeValueAccess;

namespace War
{
	public record GameState
	{
		public readonly Lst<Player> Players;

		public GameState(Lst<CardDeck> decks)
		{
			Lst<Player> players = new Lst<Player>();
			for (int i = 0; i < decks.Count; i++)
			{
				Player player = new Player(i, decks[i]);
				players = players.Add(player);
			}
			
			Players = players;
		}
		
		public GameState(Lst<Player> players)
		{
			Players =  new Lst<Player>(players);
		}

		public GameState PlayerWithCard(int id, Card card)
		{
			Option<Player> player = Players.Find(p => p.Id.Equals(id));
			if (player.IsNone) return this;
			
			Player newPlayer = player.ValueUnsafe().ScoreWithCard(card);
			GameState newGameState = ReplacePlayer(player.ValueUnsafe(), newPlayer);
			return newGameState;
		}
		
		public (Card card, GameState gameState) PlayerTakeFromTop(int id)
		{
			Option<Player> player = Players.Find(p => p.Id.Equals(id));
			if (player.IsNone) return (null, this);
			
			(Card card, Player newPlayer) = player.ValueUnsafe().TakeFromTop();
			GameState newGameState = ReplacePlayer(player.ValueUnsafe(), newPlayer);
			return (card, newGameState);
		}

		private GameState ReplacePlayer(Player player, Player newPlayer)
		{
			Lst<Player> otherPlayers = Players.Remove(player);
			Lst<Player> newPlayers = otherPlayers.Add(newPlayer);
			GameState newGameState = new GameState(newPlayers);
			return newGameState;
		}
	}
}