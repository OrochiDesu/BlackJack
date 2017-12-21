using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cards.Objects;

namespace Cards.Tests
{
    [TestClass()]
    public class DeckTests
    {
        [TestMethod()]
        public void DeckCount()
        {
            Cards cards = new Cards();
            // if deck is not building 52 cards

            cards.ResetDeck();
            // should have 52 cards in cards.Deck

            if (cards.Deck.Count() < 52)
                Assert.Fail();
        }

        [TestMethod()]
        public void DeckTest()
        {
            // if cards are not built back to original structure on first deck call
            Assert.Fail();
        }

        [TestMethod()]
        public void ResetDeckTest()
        {
            // if deck doesnt reset to original structure on calling ResetDeck
            Assert.Fail();
        }

        [TestMethod()]
        public void ShuffleTest()
        {
            // if deck doesnt shuffle && differ from another shuffled simultaneously
            Assert.Fail();
        }
    }
}