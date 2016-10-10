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
        PlayersTurn,    // waiting for player/s turn to end
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
                        Console.WriteLine($"{player.Name} would you like to [H]it or [S]tick");
                        currentGameState = GameState.PlayersTurn;
                        break;
                    case GameState.PlayersTurn:
                        ProcessPlayersTurn();
                        break;
                        
                }
            }
        }

        private void ProcessPlayersTurn()
        {
            switch (GetInputString())
            {
                case "H":
                    ProcessHit();
                    break;
                case "S":
                    ProcessStick();
                    break;
            }
        }

        private string GetInputString()
        {
            var choice = Console.ReadKey();

            return choice != null
                ? choice.Key.ToString().ToUpper()
                : null;
        }

        private void ProcessHit()
        {
            player.DealCardsToPlayer(deck.DealCards(BlackJackHit));
            player.PrintHand();
            if (HandCnt() > 21)
                ProcessStick();
        }

        private void ProcessStick()
        {
            if (HandCnt() < 21)
            {
                Console.WriteLine($"{player.Name} you have {HandCnt()}, GG");
                Console.ReadLine();
            }
            else if (HandCnt() > 21)
            {
                Console.WriteLine($"Bust {player.Name} you lose");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine($"you have {HandCnt()}, You win!");
                Console.ReadLine();
            }
        }

        private int HandCnt()
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

        private int[] GetCardValue(Card card)
        {
            var cardVal = (int)card.Val + 1;
            return cardVal > 10
                ? new[] { 10 }
                : cardVal == 1
                    ? new[] { 1, 11 }
                    : new[] { cardVal };
        }
    }
}
