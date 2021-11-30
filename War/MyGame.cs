using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LanguageExt;
using LanguageExt.UnsafeValueAccess;
using War.card;

namespace War {
  public enum PlayerNo { _1, _2 }

  public record PlayerCards(Card player1, Card player2) {
    public static IEnumerable<PlayerCards> split(CardDeck deck) {
      var (p1, p2) = deck.split2();
      var smallerSize = Math.Min(p1.Count, p2.Count);
      for (var idx = 0; idx < smallerSize; idx++)
        yield return new PlayerCards(p1.Deck[idx], p2.Deck[idx]);
    }
  }
  
  public record MyGame(
    CardSuit trump, IComparer<Card> cmp, Lst<PlayerCards> hands, Map<Card, PlayerNo> scorePile
  ) {
    Option<MyGame> next() =>
      hands.HeadOrNone().Map(cards => {
        var newScorePile = cmp.Compare(cards.player1, cards.player2).fromCmpRes() switch {
          CompareResult.LT => scorePile.Add(cards.player1, PlayerNo._2).Add(cards.player2, PlayerNo._2),
          CompareResult.EQ => scorePile.Add(cards.player1, PlayerNo._1).Add(cards.player2, PlayerNo._2),
          CompareResult.GT => scorePile.Add(cards.player1, PlayerNo._1).Add(cards.player2, PlayerNo._1),
          _ => throw new ArgumentOutOfRangeException()
        };

        return new MyGame(trump, cmp, new(hands.Tail()), newScorePile);
      });

    // NonEmpty<IEnumerable<MyGame>>
    IEnumerable<MyGame> states() {
      var current = this;

      while (true) {
        yield return current;
        var next = this.next();
        if (next.IsSome) current = next.ValueUnsafe();
        else yield break;
      }
    }

    public MyGame run() => states().Last();
    
    public string show() {
      var sb = new StringBuilder();
      sb.Append("- New round -\n");

      // Players
      //   .OrderBy(p => p.Id).ToList()
      //   .ForEach(p =>
      //     Console.WriteLine(
      //       $"Player #{p.Id} Hand: {p.HandDeck.Count} Scores: {p.ScoreDeck.Count}\tCard: {p.OpenCard}"));

      return sb.ToString();
    }
  }
}