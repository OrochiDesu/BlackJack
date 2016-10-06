using System;
using System.Collections.Generic;

namespace KittenMafiaBlackJack
{
    public class Dealer : Player
    {
        public override List<Card> Hand { get; set; }
        public Dealer()
        {
            Hand = new List<Card>();
        }
    }

    public class Player
    {
        public string Name { get; set; }
        public virtual List<Card> Hand { get; set; }                        // empty list for Hand.

        public Player()
        {
            Hand = new List<Card>();                                        // new up the Hand
        }

        public void DealCardsToPlayer(Card[] card)
        {
            Hand.AddRange(card);
        }

        public void PrintHand()
        {
            Console.WriteLine($"{Name} you caurently have: ");
            foreach (Card card in Hand)
            {
                Console.WriteLine($"{card.Val} of {card.Suit} :: {card.FaceVal}");
            }
        }
    }
}
