<Query Kind="Program">
  <NuGetReference>Humanizer.Core</NuGetReference>
  <Namespace>Humanizer</Namespace>
</Query>

void Main()
{
	//var deck = DeckOfCards.OpenNewDeck();
	//deck.Shuffle();
	//MyDeck.Dump();
	MyDeck.Shuffle(); // fresh shuffle each run.
	var myCards = MyDeck.Cards.Take(10); // Get the first 10 cards
	myCards.Dump();
	// Some grouping of the cards
	// TODO: Write a comment line under each part of the Linq statement below
	//       and identify the data types
	var result = from card in myCards
	//   card:PlayingCard     myCards:IEnumerable<PlayingCard>
				 group card by card.Suit into cardGroup
	//  card:PlayingCard   card.Suit:PlayingCard.CardSuit  cardGroup:IGrouping<..CardSuit, PlayingCard>
				 select new
				 {
				 	TheKey = cardGroup.Key,
	// TheKey:PlayingCard.CardSuit    cardGroup.Key:PlayingCard.CardSuit
					TheCards = from aCard in cardGroup
	// aCard:PlayingCard   cardGroup:IGrouping<PlayingCard.CardSuit, PlayingCard>
	//                         kinda like List<PlayingCard>
					           orderby aCard.Value
							   //         \CardValue/
							   select aCard.Value
				 };
	result.Dump();
}
static DeckOfCards MyDeck = DeckOfCards.OpenNewDeck();
// You can define other methods, fields, classes and namespaces here
// A struct is a data type that is a Value Type
public struct PlayingCard : IComparable, IComparable<PlayingCard>
{
	public readonly CardValue Value;
	public readonly CardSuit Suit; // Field
	//              datatype variablename
	//              datatype object

	public PlayingCard(CardValue cardValue, CardSuit cardSuit)
	{
		this.Value = cardValue;
		this.Suit = cardSuit;
	}
	public override string ToString()
	{
		return $"{Value.Humanize(LetterCasing.LowerCase).Humanize(LetterCasing.Title)} of {Suit.Humanize(LetterCasing.LowerCase).Humanize(LetterCasing.Title)}";
	}

	// An enum is a data type that is a Value Type
	public enum CardSuit { HEARTS, CLUBS, DIAMONDS, SPADES }
	//          datatype {   set of named values           }
	public enum CardValue { ACE, TWO, THREE, FOUR, FIVE, SIX, SEVEN, EIGHT, NINE, TEN, JACK, QUEEN, KING }

	public int CompareTo(object obj)
	{
		if (!(obj is PlayingCard))
		{
			throw new ArgumentException(nameof(obj) + " is not a " + nameof(PlayingCard));
		}

		return CompareTo((PlayingCard)obj);
	}

	public int CompareTo(PlayingCard other)
	{
		int result = 0;
		// Ordering of cards is by Suit, then by Value
		result = Suit.CompareTo(other.Suit) * 10 + Value.CompareTo(other.Value);
		return result;
	}
}
public class DeckOfCards
{
	private static Random Rnd = new Random();
	public static DeckOfCards OpenNewDeck() => new DeckOfCards();
	private IList<PlayingCard> _Cards;
	public IReadOnlyList<PlayingCard> Cards => _Cards as IReadOnlyList<PlayingCard>;
	public int Count => _Cards.Count;
	public bool IsEmpty => _Cards.Count == 0;
	private DeckOfCards()
	{
		_Cards = new List<PlayingCard>();
		var allSuits = System.Enum.GetValues(typeof(PlayingCard.CardSuit));
		var allValues = System.Enum.GetValues(typeof(PlayingCard.CardValue));
		foreach (PlayingCard.CardSuit suit in allSuits)
			foreach (PlayingCard.CardValue value in allValues)
				_Cards.Add(new PlayingCard(value, suit));
	}
	public void Shuffle()
	{
		for (int counter = 0; counter < 100; counter++)
		{
			int index = Rnd.Next(_Cards.Count);
			PlayingCard card = _Cards[0];
			_Cards.RemoveAt(0);
			_Cards.Insert(index, card);
		}
	}
	public PlayingCard DrawCard()
	{
		var topCard = _Cards[0];
		_Cards.Remove(topCard);
		return topCard;
	}
}
