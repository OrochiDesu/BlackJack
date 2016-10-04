using System;

namespace KittenMafiaBlackJack
{
    public enum GameState
    {
        Initiate,   // should only announce Player/s and Dealer 
        Shuffling,  // shuffles deck
        Dealing,    // dealing cards to player/s and dealer
        Starting,   // begin game
        Waiting     // waiting for player/s turn to end 
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
                        if(player.Hand.Count == 0)
                        {
                            player.DealCardToPlayer(deck.DealCard(), BlackJackDeal);
                        }
                        else
                        {
                            player.DealCardToPlayer(deck.DealCard(), BlackJackHit);
                        }
                        player.PrintHand();
                        Console.ReadLine();
                        break;
                    
                }
            }
        }

        private bool SetPlayerName()
        {
            Console.WriteLine("\nPlease enter your name");
            player.Name = Console.ReadLine().ToLower();            
            return !string.IsNullOrEmpty(player.Name);
        }
    }
}
