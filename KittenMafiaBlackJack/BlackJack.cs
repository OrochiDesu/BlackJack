using System;
using System.Diagnostics;
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
        DealersTurn,
        Ending     
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
                        Console.WriteLine("\nPlease enter your name");
                        SetPlayerName();
                        Console.WriteLine($"{player.Name} please press the return key...");
                        Console.ReadLine();
                        currentGameState = GameState.Shuffling;
                        break;
                    case GameState.Shuffling:
                        Console.WriteLine("Everyday I'm shuffling");
                        deck.Shuffle();
                        currentGameState = GameState.Dealing;
                        break;
                    case GameState.Dealing:
                        Console.WriteLine("Top deals, being dealt...");
                        dealer.DealCardsToPlayer(deck.DealAmount(BlackJackDeal));
                        player.DealCardsToPlayer(deck.DealAmount(BlackJackDeal));
                        currentGameState = GameState.Starting;
                        break;
                    case GameState.Starting:
                        StartGame();
                        currentGameState = GameState.PlayersTurn;
                        break;
                    case GameState.PlayersTurn:
                        ReadPlayerTurn();
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

        private bool SetPlayerName()
        {
            player.Name = Console.ReadLine().ToLower();
            if (player.Name == "ash")
            {
                RunPkmn();
                Program.Exit();
            }
                return !string.IsNullOrEmpty(player.Name);
        }
        private void RunPkmn()
        {
            Console.WriteLine("oh really? lets play pokemon");
            Process.Start("http://emulator.online/gameboy/pokemon-red-version/");
        }
        private void StartGame()
        {
            Console.WriteLine($"{player.Name} you currently have:\n{player.HandToString()}\nDealer Has {dealer.PreviewHand()}\n{player.Name} would you like to [H]it or [S]tick?");
        }
        private void checkForSpecial()
        {
            bool containsAce = player.Hand.Any(Card => Card.Val == CardVal.Ace);
            bool containsKitten = player.Hand.Any(Card => Card.Val == CardVal.Kitten);

            if (containsAce)
            {
                Console.WriteLine($"{player.Name} would you like your ace to be [a]-1 or [b]-11");
                var decision = GetInputString();

                switch (decision)
                {
                    case "a":
                        
                    default:
                        break;
                }
            }
        }
        private void ReadPlayerTurn()
        {
            switch (GetInputString())
            {
                case "H":
                    ProcessHit();
                    break;
                case "S":
                    EndPlayerTurn();
                    break;
            }
        }
        public string GetInputString()
        {
            var choice = Console.ReadKey();

            return choice != null
                ? choice.Key.ToString().ToUpper()
                : null;
        }
        private void ProcessDealersTurn()
        {
            if (dealer.HandCount() < 17)
            {
                Console.WriteLine("dealer hits!");
                dealer.DealCardsToPlayer(deck.DealAmount(BlackJackHit));
                ProcessDealersTurn();
            }
        }
        private void ProcessHit()
        {
            player.DealCardsToPlayer(deck.DealAmount(BlackJackHit));
            checkForSpecial();
            Console.WriteLine($"\n{player.Name} you have {player.HandCount()}");
            Console.WriteLine(player.HandToString());
            if (player.HandCount() > 21)
            {
                GameOver();
                currentGameState = GameState.Ending;
            }
            else if (player.HandCount() < 21)
            {
                Console.WriteLine($"\n{player.Name} would you like to [H]it or [S]tick");
                ReadPlayerTurn();
            }
        }
        private string GameOver()
        {
            return $"game over {player.Name}"
                + $"you have {player.HandToString()}"
                + player.HandCount();
        }
        private void ProcessStick()
        {
            EndPlayerTurn();
            currentGameState = GameState.DealersTurn;
        }
        private string EndPlayerTurn()
        {
            return $"{player.Name} you currently have: {player.HandToString()}"
                + $"\n{player.HandCount()}"
                + "\nDealers Turn";
        }
        private string CompareHands()
        {
            var retMsg = "";
            if (dealer.HandCount() > player.HandCount())
            {
                retMsg = $"\n{player.Name} you have {player.HandToString()}"
                            + $"\n{player.HandCount()}"
                            + $"\n\nDealer has {dealer.HandToString()}"
                            + $"\n{dealer.HandCount()}"
                            + $"\nYou lose {player.Name}"
                            + "Try Again? [Y]es, [N]o";
            }
            else if (player.HandCount() > dealer.HandCount())
            {
                retMsg = $"\n{player.Name} you have {player.HandToString()}"
                            + $"\n{player.HandCount()}"
                            + $"\n\nDealer has {dealer.HandToString()}"
                            + $"\n{dealer.HandCount()}"
                            + $"\nYou win {player.Name}!"
                            + "Play Again? [Y]es, [N]o";
            }
            return retMsg;
        }
        private void RestartGame()
        {
            switch (GetInputString())
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
