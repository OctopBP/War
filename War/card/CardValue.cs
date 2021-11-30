using System;
using System.Collections.Generic;
using LanguageExt;

namespace War.card {
	public record CardValue(CardValue.Value value) {
		public char show => value switch {
			Value.Two => '2',
			Value.Three => '3',
			Value.Four => '4',
			Value.Five => '5',
			Value.Six => '6',
			Value.Seven => '7',
			Value.Eight => '8',
			Value.Nine => '9',
			Value.Ten => 'T',
			Value.Jack => 'J',
			Value.Queen => 'Q',
			Value.King => 'K',
			Value.Ace => 'A',
			_ => throw new ArgumentOutOfRangeException()
		};

		public static readonly Arr<CardValue> all =
			EnumUtils<Value>.all.Map(v => new CardValue(v));

		public static readonly IComparer<CardValue> comparer = 
			Comparer<CardValue>.Create((a, b) => ((int) a.value).CompareTo((int) b.value));

		public enum Value { Two = 2, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King, Ace }
	}
}