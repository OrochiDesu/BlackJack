using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Tests
{
    [TestClass()]
    public class DeckTests
    {
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