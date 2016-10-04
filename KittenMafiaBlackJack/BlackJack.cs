using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KittenMafiaBlackJack
{
    public enum GameState
    {
        Starting, // should only announce Player/s and Dealer 
        Shuffling, // shuffles deck
        Dealing, // dealing cards to player/s and dealer
        Waiting // waiting for player/s turn to end 
    }

    public class BlackJack
    {
        KittenDeck deck;
        Player player;
        GameState currentGameState;

        public BlackJack()
        {
            deck = new KittenDeck();
            player = new Player();
            currentGameState = GameState.Starting;
            StartGameLoop();
        }

        public void StartGameLoop()
        {
            while (true)
            {
                switch(currentGameState)
                {
                    case GameState.Starting:
                        if (SetPlayerName())
                            currentGameState = GameState.Shuffling;
                        break;
                    case GameState.Shuffling:
                        deck.Shuffle();
                        currentGameState = GameState.Dealing;
                        break;
                    case GameState.Dealing:
                        player.DealCardToPlayer(deck.DealCard());
                        player.DealCardToPlayer(deck.DealCard());
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
