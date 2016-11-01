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
        DealersTurn,
        Ending     
    }

    public class BlackJack
    {
        const int BlackJackDeal = 2;                                        // only deal two cards for BlackJack
        const int BlackJackHit = 1;                                         // hit for one card

        KittenDeck deck;
        BlackJackPlayer player;
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
                        Console.WriteLine("\nPlease enter your name");
                        player.SetPlayerName();
                        Console.WriteLine($"{player.Name} I'm ready to play Kitten BlackJack! \nPlease press the return key...");
                        Console.ReadLine();
                        currentGameState = GameState.Shuffling;
                        break;
                    case GameState.Shuffling:
                        Console.WriteLine("Lets begin!");
                        Console.WriteLine("Everyday I'm shuffling..");
                        deck.Shuffle();
                        Console.ReadLine();
                        currentGameState = GameState.Dealing;
                        break;
                    case GameState.Dealing:
                        Console.WriteLine("Top deals, being dealt...");
                        dealer.DealCardsToPlayer(deck.DealAmount(BlackJackDeal));
                        player.DealCardsToPlayer(deck.DealAmount(BlackJackDeal));
                        Console.ReadLine();
                        currentGameState = GameState.Starting;
                        break;
                    case GameState.Starting:
                        StartGame();
                        currentGameState = GameState.PlayersTurn;
                        break;
                    case GameState.PlayersTurn:
                        ReadPlayerTurn();
                        if (player.HandCount() == 21)
                            currentGameState = GameState.Ending;
                        else
                            currentGameState = GameState.DealersTurn;
                        break;
                    case GameState.DealersTurn:
                        ProcessDealersTurn();
                        currentGameState = GameState.Ending;
                        break;
                    case GameState.Ending:
                        Console.WriteLine(CompareHands());
                        RestartGame();
                        break;
                }
            }
        }

        private void StartGame()
        {
            Console.WriteLine($"{player.Name} you currently have:\n{player.HandToString()}\nDealer Has {dealer.PreviewHand()}\n{player.Name} would you like to [H]it or [S]tick?");
        }

        private void ReadPlayerTurn()
        {
            switch (KittenTools.GetInputString())
            {
                case "H":
                    ProcessHit();
                    break;
                case "S":
                    Console.WriteLine(ProcessStick());
                    break;
            }
        }

        private void ProcessHit()
        {
            player.DealCardsToPlayer(deck.DealAmount(BlackJackHit));
            player.checkForSpecial();
            Console.WriteLine($"\n{player.Name} you have {player.HandCount()}");
            Console.WriteLine(player.HandToString());

            if (player.HandCount() < 21)
            {
                Console.WriteLine($"\n{player.Name} would you like to [H]it or [S]tick");
                ReadPlayerTurn();
            }
            else if (player.HandCount() > 21)
            {
                currentGameState = GameState.Ending;
            }
            else if (player.HandCount() == 21)
            {
                currentGameState = GameState.Ending;
            }
            else if (KittenTools.GetInputString() == "s" && player.HandCount() < dealer.GetFaceVal(dealer.Hand[0]))             // hope this works =s (choosing stick when your hand is less than the dealers preview)
                currentGameState = GameState.Ending;
        }

        private string ProcessStick()
        {
            return $"{player.Name} you currently have: {player.HandToString()}"
                + $"{player.HandCount()}"
                + "\n\n***DEALERS TURN***";
        }

        private void ProcessDealersTurn()
        {
            if (dealer.HandCount() < 17)
            {
                Console.WriteLine($"\nDealer has {dealer.HandCount()}");
                Console.WriteLine(dealer.HandToString());
                Console.WriteLine("Dealer hits!, Press Enter...");
                Console.ReadLine();
                dealer.DealCardsToPlayer(deck.DealAmount(BlackJackHit));
            }
            if (dealer.HandCount() > 21)
                CompareHands();
        }

        private void RestartGame()
        {
            switch (KittenTools.GetInputString())
            {
                case "Y":
                    deck.ResetDeck();
                    player.Hand.Clear();
                    dealer.Hand.Clear();
                    currentGameState = GameState.Shuffling;
                    break;
                case "N":
                    Program.Exit();
                    break;
            }
        }        
    }
}
