using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KittenMafiaBlackJack
{
    public class Dealer : Player
    {
        public Dealer()
        {

        }

        public void AnalyseHand()
        {

        }
    }

    public class Player
    {
        public string Name { get; set; }
        public List<Card> Hand { get; set; }                    // empty list for Hand

        public Player()
        {
            Hand = new List<Card>();                            // new up the Hand
        }

        public void DealCardToPlayer(Card card)
        {
            Hand.Add(card);                                     // Add a card from DealCard method in KittenDeck (takes in a card in parethesis, Deal Card returns... a card) 
        }

        public void PrintHand()
        {
            foreach (Card i in Hand)
            {
                Console.WriteLine($"{i.Val} of {i.Suit}");
            }
        }
    }
}
