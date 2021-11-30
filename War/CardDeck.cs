using System;
using System.Linq;
using LanguageExt;
using War.card;

namespace War {
	public record CardDeck(Arr<Card> Deck) {
		public int Count => Deck.Count;
		public bool IsEmpty => Count == 0;
		
		public static Eff<CardDeck> shuffled(Random rng) =>
			Eff<CardDeck>.Effect(() => new CardDeck(new Arr<Card>(Card.all.OrderBy(_ => rng.Next()))));

		public Lst<CardDeck> Split(int numberOfParts) {
			var partSize = Deck.Count / numberOfParts;
			return new(
				Enumerable.Range(0, numberOfParts)
					.Select(partIndex => new CardDeck(new Arr<Card>(Deck.Skip(partSize * partIndex).Take(partSize))))
			);
		}

		public (CardDeck player1, CardDeck player2) split2() {
			var lst = Split(2);
			return (lst[0], lst[1]);
		}

		public string show => string.Join(", ", Deck.Select(_ => _.show));
	}
}