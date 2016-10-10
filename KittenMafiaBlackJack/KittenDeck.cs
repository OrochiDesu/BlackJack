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

    public enum CardVal
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
        Kitten,     // wildcard
    }

    public class Card                   // what constitutes a card...
    {
        public CardSuit Suit;
        public CardVal Val;
        public int FaceVal{ get; set; }
    }

    public class KittenDeck
    {
        public List<Card> Deck;         // using a list for KittenDeck built from my card class.

        public KittenDeck()
        {
            ResetDeck();
        }

        public void Shuffle(int shuffleCounter = 5000)
        {
            Random rcg = new Random();

            for (int i = 0; i < shuffleCounter; i++)            // while this loop is less than 5000(ShuffleCounter) do the next 'for'
            {
                for (int card = 0; card < Deck.Count; card++)   // for each card in the deck(Deck.Count) 
                {
                    var rnd = rcg.Next(Deck.Count);             // Next for random usage. From deck.

                    var buffer = Deck[rnd];     // assigning buffer(empty card) a random card from deck
                    Deck[rnd] = Deck[card];     // giving the rnd all 56 cards from the deck
                    Deck[card] = buffer;        // putting buffer[56] back into the deck
                }
            }
            Console.WriteLine($"Deck Shuffled... Ready to play BlackJack");
            Console.ReadLine();
        }

        public Card[] DealCards(int cardCount = 1)      // Returns a Card
        {
            // Currently this method does not perform any validation. 
            // We need to check if there enough cards left or the game will eventually crash.
    
            var cards = Deck.Take(cardCount).ToArray();
            Deck.RemoveRange(0, cardCount);

            return cards;
        }

        public void ResetDeck()
        {
            Deck = new List<Card>();    // new list initiated

            var suitCount = Enum.GetNames(typeof(CardSuit)).Length;
            var valCount = Enum.GetNames(typeof(CardVal)).Length;
            // new technique using Enum base class to grab the 'typeof' and .length to grab the total of items in the enums

            for (int i = 0; i < suitCount; i++)         // suits
            {
                for (int o = 0; o < valCount; o++)      // face
                {
                    var card = new Card()                 // dont forget to... add new card to the list
                    {
                        Suit = (CardSuit)i,             // casting conversion
                        Val = (CardVal)o,
                        FaceVal = o + 1 > 10 ? 10 : o + 1
                    };

                    Deck.Add(card);

                    //if (o <= 8)
                    //    Deck[Deck.Count - 1].FaceVal = o <= o + 1;                   // cheeky if statement :: (Count from 0 - first card in the array, if the face value is less than or equal to 8 add 1 to the value)
                    //else
                    //    Deck[Deck.Count - 1].FaceVal = 10;                      // else leave it as ten
                }
            }
        }
    }
}