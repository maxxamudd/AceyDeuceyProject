// Name: Acey Deucey
// Purpose: Play the game of Acey Deucey
// Date: 9/3/2015
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acey_Deucey_Project
{
    class Program
    {
        static void Main(string[] args)
        {
            // variables
            int cash = 100;
            int wager = 0;
            int playerCard = 0;
            bool win = false;

            // create game object
            AceyDeucey game = new AceyDeucey();
            game.displayRules();

            // loop game while player has cash
            while (cash > 0)
            {
                // get wager, subtract wager from cash pool
                wager = game.getWager(cash);

                // get card choice from player
                playerCard = game.getCard();

                // shuffle and deal cards
                game.shuffle();
                game.dealCards(game);

                // check if player has won, reset wager
                win = game.checkWin(playerCard, game);
                cash = game.displayEarnings(cash, wager, win);
                wager = 0;
            }

            // exit message
            Console.WriteLine("Oh no! You're broke! Thanks for playing!");
        }
    }

    class AceyDeucey : Deck
    {
        public void displayRules()
        {
            // rules
            Console.WriteLine("Welcome to Acey Deucey!\n");
            Console.WriteLine("Rules:");
            Console.WriteLine("Place a bet on whether you will win or lose.");
            Console.WriteLine("You will be prompted to enter a card value and then two cards will be dealt.");
            Console.WriteLine("If your card's value is in between the values of the two dealt cards, you win!");
            Console.WriteLine("The value for an Ace is 1.\nThe value for a Jack is 11.\nThe value for a Queen is 12.\n" +
                               "The value for a King is 13.\n");
        }
        public int getWager(int cash)
        {
            // local variables
            string input = "";
            int wager = 0;

            // get player to place a bet
            Console.Write("You have $" + cash + ". Place your bet: $");
            input = Console.ReadLine();

            // exit if user types QUIT
            if (input == "QUIT")
            {
                Console.WriteLine("Goodbye!");
                System.Environment.Exit(1);
            }

            // if user does not quit, validate input
            int.TryParse(input, out wager);
            if (!int.TryParse(input, out wager) || wager > cash || wager < 1)
            {
                wager = -1;
            }
            while (wager == -1)
            {
                Console.WriteLine("\nPlease enter a whole number that is greater than 0 and is less than your cash total.\n");
                Console.Write("You have $" + cash + ". Place your bet: $");
                input = Console.ReadLine();

                // exit if user types QUIT
                if (input == "QUIT")
                {
                    Console.WriteLine("Goodbye!");
                    System.Environment.Exit(1);
                }

                // if user does not quit, validate input
                int.TryParse(input, out wager);
                if (!int.TryParse(input, out wager) || wager > cash || wager < 1)
                {
                    wager = -1;
                }
            }

            return wager;
        }
        public int getCard()
        {
            // local variables
            string input;
            int playerCard;

            // get player to choose a card
            Console.Write("\nChoose a card value: ");
            input = Console.ReadLine();

            // exit if user types QUIT
            if (input == "QUIT")
            {
                Console.WriteLine("Goodbye!");
                System.Environment.Exit(1);
            }

            // if user does not quit, validate input
            int.TryParse(input, out playerCard);
            if (!int.TryParse(input, out playerCard) || playerCard > 13 || playerCard < 1)
            {
                playerCard = -1;
            }
            while (playerCard == -1)
            {
                Console.WriteLine("\nPlease enter a valid card number between 1 and 13.\n");
                Console.Write("Choose a card value: ");
                input = Console.ReadLine();

                // exit if user types QUIT
                if (input == "QUIT")
                {
                    Console.WriteLine("Goodbye!");
                    System.Environment.Exit(1);
                }

                // if user does not quit, validate input
                int.TryParse(input, out playerCard);
                if (!int.TryParse(input, out playerCard) || playerCard > 13 || playerCard < 1)
                {
                    playerCard = -1;
                }
            }

            return playerCard;
        }
        public void dealCards(AceyDeucey game)
        {
            // deal two cards
            Console.WriteLine("\nDealing cards...\n");
            Console.WriteLine("First card: " + card[0].rank + " of " + card[0].Suit);
            Console.WriteLine("Second card: " + card[1].rank + " of " + card[1].Suit + "\n");
        }
        public bool checkWin(int playerCard, AceyDeucey game)
        {
            // local variable
            bool win = false;

            // if first card is greater than second card, compare to player card
            if (card[0].val > card[1].val)
            {
                if (playerCard < card[0].val && playerCard > card[1].val)
                {
                    win = true;
                }
                else
                {
                    win = false;
                }
            }
            // if first card less than second card, compare to player card
            else if (card[0].val < card[1].val)
            {
                if (playerCard > card[0].val && playerCard < card[1].val)
                {
                    win = true;
                }
                else
                {
                    win = false;
                }
            }
            // if there is no gap between the dealt cards, deal another two cards
            else if(card[0].val == card[1].val)
            {
                Console.WriteLine("Dealt cards match. Can't win without a gap!");
                game.shuffle();
                game.dealCards(game);
                win = game.checkWin(playerCard, game);
            }

            return win;
        }
        public int displayEarnings(int cash, int wager, bool win)
        {
            // check if player won or lost, adjust cash
            if(win == true)
            {
                Console.WriteLine("You won $" + wager + "!\n");
                cash += wager;
            }
            else
            {
                Console.WriteLine("You lost $" + wager + ".\n");
                cash -= wager;
            }

            return cash;
        }
    }
    class Deck
    {
        // variables
        const int DECK_SIZE = 52, HIGH_VAL = 13, LOW_VAL = 1, HIGH_SUIT = 4;
        public Card[] card;
        public Deck()
        {
            // deck index
            int x = 0;

            // create deck
            card = new Card[DECK_SIZE];

            // loop through values and suits to create 52 cards
            for (int s = 1; s < HIGH_SUIT + 1; ++s)
            {
                for (int v = 1; v < HIGH_VAL + 1; ++v)
                {
                    // create card, set value
                    card[x] = new Card();
                    card[x].Val = v;

                    // set suit
                    if (s == 1)
                    {
                        card[x].Suit = "Diamonds";
                    }
                    else if (s == 2)
                    {
                        card[x].Suit = "Clubs";
                    }
                    else if (s == 3)
                    {
                        card[x].Suit = "Hearts";
                    }
                    else if (s == 4)
                    {
                        card[x].Suit = "Spades";
                    }

                    // move to next card
                    ++x;
                }
            }
        }
        public void shuffle()
        {
            //shuffle deck
            Random rand = new Random();
            for (int i = card.Length; i > 1; i--)
            {
                int j = rand.Next(i);
                Card temp = card[j];
                card[j] = card[i - 1];
                card[i - 1] = temp;
            }
        }
    }
    class Card
    {
        public int val;
        public string rank;
        public string Suit { get; set; }
        public int Val
        {
            get
            {
                return val;
            }
            set
            {
                val = value;

                // assign card rank
                if (val == 1)
                {
                    rank = "Ace";
                }
                else if (val == 2)
                {
                    rank = "2";
                }
                else if (val == 3)
                {
                    rank = "3";
                }
                else if (val == 4)
                {
                    rank = "4";
                }
                else if (val == 5)
                {
                    rank = "5";
                }
                else if (val == 6)
                {
                    rank = "6";
                }
                else if (val == 7)
                {
                    rank = "7";
                }
                else if (val == 8)
                {
                    rank = "8";
                }
                else if (val == 9)
                {
                    rank = "9";
                }
                else if (val == 10)
                {
                    rank = "10";
                }
                else if (val == 11)
                {
                    rank = "Jack";
                }
                else if (val == 12)
                {
                    rank = "Queen";
                }
                else if (val == 13)
                {
                    rank = "King";
                }
            }
        }
    }
}
