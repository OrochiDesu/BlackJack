using System;
using System.Collections.Generic;

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

        public Card DealCard()      // Returns a Card
        {
            var card = Deck[0];     // take the first card from Deck
            Deck.RemoveAt(0);       // remove the card from the top of the list
            return card;            // Give the card you took from Deck
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
                    Deck.Add(new Card()                 // dont forget... add new card to the list
                    {
                        Suit = (CardSuit)i,             // casting conversion
                        Val = (CardVal)o
                    });
                }
            }
        }
    }
}