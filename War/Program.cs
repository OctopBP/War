using System;
using LanguageExt;
using War.card;

namespace War {
	class Program {
		static readonly Eff<MyGame> createGame =
			from rng in Eff<Random>.Effect(() => new Random())
			from trump in CardSuit.GetRandom(rng)
			from shuffledDeck in CardDeck.shuffled(rng)
			let hands = new Lst<PlayerCards>(PlayerCards.split(shuffledDeck))
			select new MyGame(trump, Card.comparer(trump), hands, Map<Card, PlayerNo>.Empty);
		
		static void Main(string[] args) {
			createGame.Map(game => game.run()).Run().Match(
				Succ: game => {},
				Fail: err => {}
			);
		}
	}
}