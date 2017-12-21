using System.Collections.Generic;

namespace Cards.Objects
{
    public class CardDeck
    {
        // may need to use to implement game mechanics
        private readonly List<Card> _systemDeck;
        public List<Card> Deck = new List<Card>();

        public CardDeck()
        {
            _systemDeck = Deck;
        }
    }
}
