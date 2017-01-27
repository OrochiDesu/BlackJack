using System;

namespace Cards.Objects
{
    public class Card
    {
        public enum CardSuit
        {
            Diamonds,
            Spades,
            Hearts,
            Clubs
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
            Jack,
            Queen,
            King
        }

        public Card()
        {
            CardSuit suit;
            CardFace face;
        }

    }
}
