using System;
using System.Collections.Generic;
using LanguageExt;

namespace War.card {
	public record Card(CardSuit Suit, CardValue Value) {
		public string show => $"{Suit.show}{Value.show}";

		public static IComparer<Card> comparer(CardSuit trump) =>
			CardSuit.comparer(trump).contraMap((Card c) => c.Suit)
				.andThen(CardValue.comparer.contraMap((Card c) => c.Value));

		public static readonly Arr<Card> all =
			from suit in CardSuit.all
			from value in CardValue.all
			select new Card(suit, value);
	}
}