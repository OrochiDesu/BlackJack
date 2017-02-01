using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KittenMafiaBlackJack;

namespace BlackJack_Tests
{
    [TestClass]
    public class DeckTests
    {
        [TestMethod]
        public void ShuffleTest()               // checks if cards in both decks match when shuffled (80%)
        {
            var testDeck1 = new KittenDeck();
            var testDeck2 = new KittenDeck();

            testDeck1.Shuffle();
            testDeck2.Shuffle();

            var matches = testDeck1.Deck.Where((t, i) => t.Suit == testDeck2.Deck[i].Suit && t.Val == testDeck2.Deck[i].Val).Count();

            Assert.IsTrue(matches < testDeck1.Deck.Count * 0.8, "Decks do not differ and shuffle may need to be revamped");
        }

        public void ResetShuffleTest()              // shuffles to hand 1, resets deck, shuffles to hand two, compares... (80%)
        {
            var testDeck1 = new KittenDeck();
            var hand1 = new BlackJackPlayer();
            var hand2 = new BlackJackPlayer();

            testDeck1.Shuffle();
            hand1.DealCardsToPlayer(testDeck1.DealAmount(56));

            testDeck1.ResetDeck();

            testDeck1.Shuffle();
            hand2.DealCardsToPlayer(testDeck1.DealAmount(56));

            var matches = hand1.Hand.Where((t, i) => hand2.Hand[i].Val == t.Val).Count();

            Assert.IsTrue(matches < testDeck1.Deck.Count * 0.8, "Decks do not differ and 'reset' may need to be revamped");
        }
    }
}
