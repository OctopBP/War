using System;
using System.Collections.Generic;
using System.Linq;
using LanguageExt;

namespace War
{
	public record CardDeck
	{
		private readonly Lst<Card> _deck;

		public int Count => _deck.Count;
		public bool IsEmpty => _deck.Count == 0;

		private CardDeck(Lst<Card> deck)
		{
			_deck = deck;
		}

		public CardDeck WithCard(Card card)
		{
			return new CardDeck(_deck.Add(card));
		}
		
		public CardDeck WithCards(Card[] cards)
		{
			return new CardDeck(_deck.AddRange(cards));
		}
		
		public (Card card, CardDeck deck) TakeFromTop()
		{
			Card card = _deck.Last();
			Lst<Card> deck = _deck.Remove(card);
			return (card, new CardDeck(deck));
		}

		public static CardDeck CreateFull()
		{
			Lst<Card> deck = CreateDeck();
			return new CardDeck(deck);
		}
		
		public static CardDeck Empty()
		{
			Lst<Card> deck = Lst<Card>.Empty;
			return new CardDeck(deck);
		}

		public CardDeck Shuffle()
		{
			Stck<Card> shuffledDeck = new Stck<Card>(_deck);
			IList<Card> shuffledList = shuffledDeck.ToList().Shuffle();
			return new CardDeck(new Lst<Card>(shuffledList));
		}

		public Lst<CardDeck> Split(int count)
		{
			int partSize = _deck.Count / count;

			Lst<CardDeck> decks = new Lst<CardDeck>();
			for (int i = 0; i < count; i++)
			{
				Lst<Card> deckPart = GetPart(i * partSize, partSize);
				decks = decks.Add(new CardDeck(deckPart));
			}
			
			return decks;

			Lst<Card> GetPart(int index, int count)
			{
				List<Card> cards = new List<Card>();
				cards.AddRange(_deck.ToList().GetRange(index, count));
				return new Lst<Card>(cards);
			}
		}

		private static Lst<Card> CreateDeck()
		{
			Lst<Card> deck = new();

			foreach (Suit suit in (Suit[])Enum.GetValues(typeof(Suit)))
			{
				foreach (Value value in (Value[])Enum.GetValues(typeof(Value)))
				{
					Card card = new Card(suit, value);
					deck = deck.Add(card);
				}
			}

			return deck;
		}

		public override string ToString()
		{
			Arr<string> array = _deck
				.Select(card => card.ToString())
				.ToArray();

			return string.Join(", ", array);
		}
	}
}