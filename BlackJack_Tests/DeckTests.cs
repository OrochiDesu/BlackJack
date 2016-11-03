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
            var TestDeck1 = new KittenDeck();
            var TestDeck2 = new KittenDeck();

            TestDeck1.Shuffle();
            TestDeck2.Shuffle();

            var matches = 0;

            for (int i = 0; i < TestDeck1.Deck.Count; i++)
            {
                if (TestDeck1.Deck[i].Suit == TestDeck2.Deck[i].Suit && TestDeck1.Deck[i].Face == TestDeck2.Deck[i].Face) 
                {
                    matches++;
                }
            }

            Assert.IsTrue(matches < TestDeck1.Deck.Count * 0.8, "Decks do not differ and shuffle may need to be revamped");
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

            var matches = 0;

            for (int i = 0; i < hand1.Hand.Count; i++)
            {
                if (hand1.Hand[i].Suit == hand1.Hand[i].Suit && hand2.Hand[i].Face == hand1.Hand[i].Face)
                {
                    matches++;
                }
            }

            Assert.IsTrue(matches < testDeck1.Deck.Count * 0.8, "Decks do not differ and 'reset' may need to be revamped");
        }
    }
}
