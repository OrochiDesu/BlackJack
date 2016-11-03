using System;
using System.Collections.Generic;
using System.Linq;

namespace KittenMafiaBlackJack
{
    public enum CardSuit
    {
        diamonds,
        hearts,
        spades,
        clubs,
    }
    public enum CardFace
    {
        Ace,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        King,
        Queen,
        Jack,
        Kitten                          //wildcard... cause its a cat
    }
    public class Card                   // what constitutes a card...
    {
        public CardSuit Suit;
        public CardFace Face;
        public int[] CardValue { get; set; }
    }
    public abstract class KittenDeck
    {
        public List<Card> Deck;                                             // using a list for KittenDeck built from my card class.

        public abstract int[] GetCardValue(CardSuit suit, CardFace face);

        public KittenDeck()
        {
            ResetDeck();
        }

        public void ResetDeck()
        {
            Deck = new List<Card>();                                        // new list initiated

            var suitCount = Enum.GetNames(typeof(CardSuit)).Length;
            var valCount = Enum.GetNames(typeof(CardFace)).Length;          // new technique using Enum base class to grab the 'typeof' and .length to grab the total of items in the enums


            for (int i = 0; i < suitCount; i++)                             // suits
            {
                for (int o = 0; o < valCount; o++)                          // face
                {
                    var suit = (CardSuit)i;                                 // casting conversion
                    var face = (CardFace)o;

                    var card = new Card()                                   
                    {
                        Suit = suit,                 
                        Face = face,
                        CardValue = GetCardValue(suit, face)
                    };
                    Deck.Add(card);                                         // dont forget to... add new card to the list
                }
            }
        }

        public void Shuffle(int shuffleCounter = 5000)
        {
            Random rcg = new Random();

            for (int i = 0; i < shuffleCounter; i++)                        // while this loop is less than 5000(ShuffleCounter) do the next 'for'
            {
                for (int card = 0; card < Deck.Count; card++)               // for each card in the deck(Deck.Count) 
                {
                    var rnd = rcg.Next(Deck.Count);                         // Next for random usage. From deck.

                    var buffer = Deck[rnd];                                 // assigning buffer(empty card) a random card from deck
                    Deck[rnd] = Deck[card];                                 // giving the rnd all 56 cards from the deck
                    Deck[card] = buffer;                                    // putting buffer[56] back into the deck
                }
            }
        }

        public Card[] DealAmount(int cardCount)                             // Returns a Card
        {
            /** Currently this method does not perform any validation. 
            We need to check if there enough cards left or the game will eventually crash. **/
            var cards = Deck.Take(cardCount).ToArray();
            Deck.RemoveRange(0, cardCount);

            return cards;
        }
    }

    public class BlackJackDeck : KittenDeck
    {
        public override int[] GetCardValue(CardSuit suit, CardFace face)
        {
            var cardVal = (int)face + 1;
            return cardVal > 10
                ? new[] { 10 }
                : cardVal == 1
                    ? new[] { 1, 11 }
                    : new[] { cardVal };                                    // use as part of compare
        }
    }
}