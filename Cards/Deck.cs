using System;
using System.Collections.Generic;
using Cards.Objects;

namespace Cards
{
    public class Deck
    {
        private readonly CardDeck _cardDeck = new CardDeck {Deck = new List<Card>()};
        public Deck()
        {
            ResetDeck();
        }

        public void ResetDeck()
        {
            var intSuit = Enum.GetNames(typeof(Card.CardSuit)).Length;
            var intFace = Enum.GetNames(typeof(Card.CardFace)).Length;

            for (var i = 0; i < intSuit; i++)
            {
                for (var j = 0; j < intFace; j++)
                {
                    var card = new Card()
                    {
                        Suit = (Card.CardSuit)i,
                        Face = (Card.CardFace)j
                    };
                    _cardDeck.Deck.Add(card);
                }
            }
        }

        public void Shuffle(int shuffleCount)
        {
            var rcg = new Random();

            for (var i = 0; i < shuffleCount; i++)
            {
                for (var card = 0; card < _cardDeck.Deck.Count; card++)
                {
                    var randomCard = rcg.Next(_cardDeck.Deck.Count);

                    var topCard = _cardDeck.Deck[randomCard];
                    _cardDeck.Deck[randomCard] = _cardDeck.Deck[card];
                    _cardDeck.Deck[card] = topCard;
                }
            }
        }
    }
}
