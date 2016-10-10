using System;

namespace KittenMafiaBlackJack
{
    public enum GameState
    {
        Initiate,   // should only announce Player/s and Dealer 
        Shuffling,  // shuffles deck
        Dealing,    // dealing cards to player/s and dealer
        Starting,   // begin game
        PlayersTurn,    // waiting for player/s turn to end
        DealersTurn     
    }

    public class BlackJack
    {
        const int BlackJackDeal = 2;                                        // only deal two cards for BlackJack
        const int BlackJackHit = 1;                                         // hit for one card

        KittenDeck deck;
        Player player;
        BlackJackDealer dealer;
        GameState currentGameState;

        public BlackJack()
        {
            deck = new KittenDeck();
            player = new BlackJackPlayer();
            dealer = new BlackJackDealer();
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
                        Console.WriteLine($"{player.Name} you currently have: {player.HandToString()}");
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
            Console.WriteLine(player.HandToString());
            if (player.HandCount() > 21)
                ProcessStick();
        }

        private void ProcessStick()
        {
            if (player.HandCount() < 21)
            {
                Console.WriteLine($"/n{player.Name} you have {player.HandCount()}, GG");
                Console.ReadLine();
            }
            else if (player.HandCount() > 21)
            {
                Console.WriteLine($"/nBust {player.Name} you lose");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine($"/nYou have {player.HandCount()}, You win!");
                Console.ReadLine();
            }
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
