using Cards.Objects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Cards.Tests
{
    [TestClass()]
    public class DeckTests
    {
        [TestMethod()]
        public void DeckCount()
        {
            Cards cards = new Cards();

            cards.ResetDeck();
            
            // if deck is not building 52 cards
            if (cards.Deck.Count() < 52)
                Assert.Fail();
        }

        [TestMethod()]
        public void DeckTest()
        {
            Cards cards = new Cards();

            cards.ResetDeck();

            // if cards are not in order on first deck call
            if (cards.Deck[0].Face != Card.CardFace.Ace && cards.Deck[0].Suit != Card.CardSuit.Diamonds)
            Assert.Fail();
        }

        [TestMethod()]
        public void ResetDeckTest()
        {
            Cards cards = new Cards();

            cards.Shuffle(2);

            cards.ResetDeck();
            cards.Deck.ToArray();

            // if deck doesnt reset to original structure on calling ResetDeck
            if (cards.Deck[0].Face != Card.CardFace.Ace && cards.Deck[0].Suit != Card.CardSuit.Diamonds)
                Assert.Fail();
            if (cards.Deck[14].Face != Card.CardFace.Ace && cards.Deck[14].Suit != Card.CardSuit.Spades)
                Assert.Fail();
            if (cards.Deck[27].Face != Card.CardFace.Ace && cards.Deck[27].Suit != Card.CardSuit.Hearts)
                Assert.Fail();
            if (cards.Deck[40].Face != Card.CardFace.Ace && cards.Deck[40].Suit != Card.CardSuit.Clubs)
                Assert.Fail();
        }

        [TestMethod()]
        public void ShuffleTest()
        {
            Random rng = new Random();
            var randomNo = rng.Next(51);

            Cards cards = new Cards();
            Cards pllCards = new Cards();

            pllCards.ResetDeck();
            cards.ResetDeck();

            pllCards.Shuffle(1);
            cards.Shuffle(1);

            // if deck doesnt shuffle && differ from another shuffled simultaneously
            if (cards.Deck[randomNo] == pllCards.Deck[randomNo])
                Assert.Fail();
        }
    }
}