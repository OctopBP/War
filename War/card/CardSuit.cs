using System;
using System.Collections.Generic;
using LanguageExt;

namespace War.card {
	public record CardSuit(CardSuit.Suit value) {
		public char show => value switch {
			Suit.Hearts => 'H',
			Suit.Diamonds => 'D',
			Suit.Clubs => 'C',
			Suit.Spades => 'S',
			_ => throw new ArgumentOutOfRangeException()
		};
		
		public static Eff<CardSuit> GetRandom(Random rng) =>
			EnumUtils<Suit>.random(rng).Map(suit => new CardSuit(suit));

		public static IComparer<CardSuit> comparer(CardSuit trump) => 
			Comparer<CardSuit>.Create((x, y) => {
				return res().asInt();
				
				CompareResult res() =>
					x == trump && y != trump ? CompareResult.GT
					: x != trump && y == trump ? CompareResult.LT
					: CompareResult.EQ;
			});
		
		public static readonly Arr<CardSuit> all =
			EnumUtils<Suit>.all.Map(v => new CardSuit(v));
		
		public enum Suit { Hearts, Diamonds, Clubs, Spades }
	}
}