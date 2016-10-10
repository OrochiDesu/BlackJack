using System.Collections.Generic;

namespace KittenMafiaBlackJack
{
    public class BlackJackDealer : BlackJackPlayer
    {
        public BlackJackDealer()
        {
        }
    }

    public abstract class Player
    {
        public string Name { get; set; }
        public List<Card> Hand { get; set; }                                // empty list for Hand.

        public Player()
        {
            Hand = new List<Card>();                                        // new up the Hand
        }

        public abstract int HandCount();
        public abstract int GetFaceVal(Card card);
        public abstract string HandToString();

        public virtual void DealCardsToPlayer(Card[] card)
        {
            Hand.AddRange(card);
        }
    }

    public class BlackJackPlayer : Player                           
    {
        public override int HandCount()                             // moved handcount into blackjack player as faceval is blackjack specific
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
            var cardVal = (int)card.Val + 1;                        // +1 to card value as it starts count @ 0
            return cardVal > 10 ? 10 : cardVal;                     // if card value is over 10 in count it equals 10 (Jack, Queen, King) else its normal 
        }

        public override string HandToString()
        {
            var ret = "";                                           // blank string var for +=
            foreach (Card card in Hand)
            {
                ret = $"{card.Val} of {card.Suit} :: {GetFaceVal(card)}\n";
            }
            return ret;
        }
    }


}
