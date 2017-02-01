using Cards;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlackJack_Tests
{
    [TestClass]
    public class DeckTests
    {
        [TestMethod]
        public void ShuffleTest()               // checks if cards in both decks match when shuffled (80%)
        {
            var testDeck1 = new Deck();
            var testDeck2 = new Deck();

            testDeck1.Shuffle(50);
            testDeck2.Shuffle(50);

            Assert.IsTrue();
        }
    }
}
