using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KittenMafiaBlackJack;

namespace BlackJack_Tests
{
    [TestClass]
    public class BlackJackTests
    {
        [TestMethod]
        public void ShuffleTest()
        {
            var TestDeck1 = new KittenDeck();
            var TestDeck2 = new KittenDeck();

            TestDeck1.Shuffle();
            TestDeck2.Shuffle();

            var matches = 0;

            for (int i = 0; i < TestDeck1.Deck.Count; i++)
            {
                if (TestDeck1.Deck[i].Suit == TestDeck2.Deck[i].Suit && TestDeck1.Deck[i].Val == TestDeck2.Deck[i].Val)
                {
                    matches++;
                }
            }

            Assert.IsTrue(matches < TestDeck1.Deck.Count * 0.8, "Decks differ and have been shuffled well");
        }

        public void HandCount()
        {

        }
    }
}
