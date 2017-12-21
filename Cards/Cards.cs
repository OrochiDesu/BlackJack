using Cards.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards
{
    public class Cards
    {
        private CardDeck _cardDeck = new CardDeck();
        public List<Card> Deck;

        public Cards()
        {
            Deck = _cardDeck.Deck;
        }

        public void ResetDeck()
        {
            var intSuit = Enum.GetNames(typeof(Card.CardSuit)).Length;
            var intFace = Enum.GetNames(typeof(Card.CardFace)).Length;

            for (var i = 0; i < intSuit; i++)
            {
                for (var o = 0; o < intFace; o++)
                {
                    var card = new Card()
                    {
                        Suit = (Card.CardSuit)i,
                        Face = (Card.CardFace)o
                    };
                    Deck.Add(card);
                }
            }
        }

        public void Shuffle(int shuffleCount)
        {
            var rcg = new Random();

            for (var i = 0; i < shuffleCount; i++)
            {
                for (var card = 0; card < Deck.Count; card++)
                {
                    var randomCard = rcg.Next(Deck.Count);

                    var topCard = Deck[randomCard];
                    Deck[randomCard] = Deck[card];
                    Deck[card] = topCard;
                }
            }
        }
    }
}
