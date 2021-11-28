using System;
using System.Collections.Generic;
using System.Linq;
using LanguageExt;
using War.Card;

namespace War
{
	public record CardDeck(Lst<Card.Card> Deck)
	{
		public int Count => Deck.Count;
		public bool IsEmpty => Count == 0;

		public static CardDeck CreateFull()
		{
			Lst<Card.Card> deck = CreateDeck();
			return new CardDeck(deck);
		}

		public CardDeck Shuffle()
		{
			return this with { Deck = Deck.Shuffle() };
		}

		public Lst<CardDeck> Split(int count)
		{
			int partSize = Deck.Count / count;

			Lst<CardDeck> decks = new();
			for (int i = 0; i < count; i++)
			{
				Lst<Card.Card> deckPart = GetPart(i * partSize, partSize);
				decks = decks.Add(new CardDeck(deckPart));
			}

			return decks;

			Lst<Card.Card> GetPart(int index, int parts)
			{
				List<Card.Card> cards = new();
				cards.AddRange(Deck.ToList().GetRange(index, parts));
				return new Lst<Card.Card>(cards);
			}
		}

		private static Lst<Card.Card> CreateDeck()
		{
			Suit[] suits = (Suit[]) Enum.GetValues(typeof(Suit));
			Value[] values = (Value[]) Enum.GetValues(typeof(Value));

			Lst<Card.Card> deck = suits.Aggregate(deck,
				(current1, suit) =>
					values.Aggregate(current1, (current, value) => current.Add(new Card.Card(suit, value))));


			return deck;
		}

		public override string ToString()
		{
			Arr<string> array = Deck
				.Select(card => card.ToString())
				.ToArray();

			return string.Join(", ", array);
		}
	}
}