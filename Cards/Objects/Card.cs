namespace Cards.Objects
{
    public class Card
    {
        public CardSuit Suit;
        public CardFace Face;

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
    }
}
