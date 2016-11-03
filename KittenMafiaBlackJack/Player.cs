using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace KittenMafiaBlackJack
{

    public abstract class Player
    {
        public string Name { get; set; }
        public List<Card> Hand { get; set; }                                // empty list for Hand.

        public abstract int HandCount();
        public abstract int GetFaceVal(Card card);
        public abstract string HandToString();


        public Player()
        {
            Hand = new List<Card>();                                        // new up the Hand
        }

        public bool SetPlayerName()
        {
            Name = Console.ReadLine().ToLower();
            if (Name == "ash")
            {
                RunPkmn();
                Program.Exit();
            }
            return !string.IsNullOrEmpty(Name);
        }

        public void RunPkmn()
        {
            Console.WriteLine("oh really? lets play pokemon");
            Process.Start("http://emulator.online/gameboy/pokemon-red-version/");
        }

        public virtual void DealCardsToPlayer(Card[] card)
        {
            Hand.AddRange(card);
        }
    }

    public class BlackJackPlayer : Player
    {
        public override int HandCount()                                         // moved handcount into blackjack player as faceval is blackjack specific
        {
            int handAmount = 0;

            foreach (Card card in Hand)
            {
                handAmount += GetFaceVal(card);
            }
            return handAmount;
        }

        public override int GetFaceVal(Card card)
        {
            var cardVal = (int)card.Face + 1;                                   // +1 to card value as it starts count @ 0
            return cardVal > 10 ? 10 : cardVal;                                 // if card value is over 10 in count it equals 10 (Jack, Queen, King) else its normal 
        }

        public override string HandToString()
        {
            var ret = "";                                                       // blank string var for +=
            foreach (Card card in Hand)
            {
                ret += $"{card.Face} of {card.Suit} :: {GetFaceVal(card)}\n";
            }
            return ret;
        }

        public void checkForKitten()
        {
            bool containsKitten = Hand.Any(Card => Card.Face == CardFace.Kitten);

            if (containsKitten)
            {
                Console.WriteLine($"{Name.ToUpper()} THERE'S A KITTEN IN YOUR HAND!");
                Console.WriteLine("***Kitten is scared, and runs away with your cards stuck to it***");
                Hand.Clear();
            }
        }
    }

    public class BlackJackDealer : BlackJackPlayer
    {
        public BlackJackDealer()
        {
            const string name = "House";                                        // never going to change

            Name = name;
        }
        public string PreviewHand()
        {
            string retVal = Hand.First().Face.ToString();
            string retSuit = Hand.First().Suit.ToString();
            var retAmount = GetFaceVal(Hand.First());

            return retVal + " of " + retSuit + " :: " + retAmount + ", and a face down\n";
        }
    }
}