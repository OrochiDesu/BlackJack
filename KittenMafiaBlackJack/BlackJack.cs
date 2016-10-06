using System;
using System.Linq;

namespace KittenMafiaBlackJack
{
    public enum GameState
    {
        Initiate,   // should only announce Player/s and Dealer 
        Shuffling,  // shuffles deck
        Dealing,    // dealing cards to player/s and dealer
        Starting,   // begin game
        MainLoop,    // waiting for player/s turn to end
        EndGame 
    }

    public class BlackJack
    {
        const int BlackJackDeal = 2;                                        // only deal two cards for BlackJack
        const int BlackJackHit = 1;

        KittenDeck deck;
        Player player;
        Dealer dealer;
        GameState currentGameState;

        public BlackJack()
        {
            deck = new KittenDeck();
            player = new Player();
            dealer = new Dealer();
            currentGameState = GameState.Initiate;                          //gamestate will always begin at 'initiate'.
            StartGameLoop();
        }

        public void StartGameLoop()
        {
            while (true)
            {
                switch(currentGameState)
                {
                    case GameState.Initiate:
                        if (SetPlayerName())
                            currentGameState = GameState.Shuffling;
                        break;
                    case GameState.Shuffling:
                        deck.Shuffle();
                        currentGameState = GameState.Dealing;
                        break;
                    case GameState.Dealing:
                        player.DealCardsToPlayer(deck.DealCards(BlackJackDeal));
                        currentGameState = GameState.Starting;
                        break;
                    case GameState.Starting:
                        player.PrintHand();
                        currentGameState = GameState.MainLoop;
                        break;
                    case GameState.MainLoop:
                        
                }
            }
        }

        private void hitStick()
        {
            Console.WriteLine($"{player.Name} would you like to [H]it or [S]tick");
            var choice = Console.ReadKey();
            if (choice != null && choice.Key == ConsoleKey.P)
            {

                Console.WriteLine("moop");
            }
            break;
        }

        private int handCnt()
        {
            int handAmount = 0;

            foreach (Card card in player.Hand)
            {
                handAmount += card.FaceVal;
            }
            return handAmount;
        }

        private bool SetPlayerName()
        {
            Console.WriteLine("\nPlease enter your name");
            player.Name = Console.ReadLine().ToLower();            
            return !string.IsNullOrEmpty(player.Name);
        }
    }
}
