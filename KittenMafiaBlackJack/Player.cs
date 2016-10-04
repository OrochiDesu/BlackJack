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
        public List<Card> Hand { get; set; }

        public Player()
        {
            Hand = new List<Card>();
        }

        public void DealCardToPlayer(Card card)
        {
            Hand.Add(card);           
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
