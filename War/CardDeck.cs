using System;
using System.Collections.Generic;
using System.Linq;
using LanguageExt;

namespace War
{
	public class CardDeck
	{
		private Stck<Card> _deck;

		public int Count => _deck.Count;
		public bool IsEmpty => _deck.Count == 0;

		private CardDeck(Stck<Card> deck)
		{
			_deck = deck;
		}

		public CardDeck Push(Card card)
		{
			return new CardDeck(_deck.Push(card));
		}
		
		public (Card card, CardDeck deck) Pop()
		{
			Card card = _deck.Peek();
			Stck<Card> deck = _deck.Pop();
			return (card, new CardDeck(deck));
		}

		public static CardDeck CreateFull()
		{
			Stck<Card> deck = CreateDeck();
			return new CardDeck(deck);
		}
		
		public static CardDeck Empty()
		{
			Stck<Card> deck = Stck<Card>.Empty;
			return new CardDeck(deck);
		}

		public CardDeck Shuffle()
		{
			Stck<Card> shuffledDeck = new Stck<Card>(_deck);
			IList<Card> shuffledList = shuffledDeck.ToList().Shuffle();
			return new CardDeck(new Stck<Card>(shuffledList));
		}

		public (CardDeck partOne, CardDeck partTwo) Split()
		{
			int center = _deck.Count / 2;

			// todo: check if it works with ods count
			Stck<Card> deckOne = GetPart(0, center);
			Stck<Card> deckTwo = GetPart(center, center);

			return (new CardDeck(deckOne), new CardDeck(deckTwo));

			Stck<Card> GetPart(int index, int count)
			{
				List<Card> cards = new List<Card>();
				cards.AddRange(_deck.ToList().GetRange(index, count));
				return new Stck<Card>(cards);
			}
		}

		private static Stck<Card> CreateDeck()
		{
			Stck<Card> deck = new();

			foreach (Suit suit in (Suit[])Enum.GetValues(typeof(Suit)))
			{
				foreach (Value value in (Value[])Enum.GetValues(typeof(Value)))
				{
					Card card = new Card(suit, value);
					deck = deck.Push(card);
				}
			}

			return deck;
		}

		public override string ToString()
		{
			string[] array = _deck
				.Select(card => card.ToString())
				.ToArray();

			return string.Join(", ", array);
		}
	}
}