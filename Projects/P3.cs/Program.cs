using System;
using System.Collections.Generic;

class Catcade
{
    static int totalTokens = 10;  // Starting tokens
    static int slotTokensEarned = 0;
    static int hideSeekTokensEarned = 0;
    static bool slotFirstTime = true;
    static bool hideSeekFirstTime = true;

    static void Main()
    {
        int choice;
        do
        {
            Console.WriteLine("\nWelcome to the Catcade! Choose an option:");
            Console.WriteLine("1. Play Slot Machine");
            Console.WriteLine("2. Play Hide 'n Seek");
            Console.WriteLine("3. Check Tokens");
            Console.WriteLine("4. Cat Adoption Center");
            Console.WriteLine("5. Exit");
            Console.Write("Enter choice: ");
            if (int.TryParse(Console.ReadLine(), out choice))
            {
                switch (choice)
                {
                    case 1:
                        PlaySlotMachine();
                        break;
                    case 2:
                        PlayHideAndSeek();
                        break;
                    case 3:
                        Console.WriteLine($"You currently have {totalTokens} tokens.");
                        break;
                    case 4:
                        if (totalTokens >= 20)
                        {
                            CatAdoptionCenter();
                        }
                        else
                        {
                            Console.WriteLine("You need at least 20 tokens to enter the Cat Adoption Center.");
                        }
                        break;
                    case 5:
                        Console.WriteLine("Thank you for visiting the Catcade! Goodbye.");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Please enter a valid number.");
            }

        } while (choice != 5);
    }

    static void PlaySlotMachine()
    {
        if (slotFirstTime)
        {
            Console.WriteLine("Welcome to the Slot Machine! Win tokens by matching symbols.");
            slotFirstTime = false;
        }

        if (slotTokensEarned >= 6)
        {
            Console.WriteLine("You have already earned the maximum of 6 tokens from the Slot Machine.");
            return;
        }

        Random random = new Random();
        string[] symbols = { "=^.^=", "★", "♥", "♣", "♦", "♠" };

        Console.Write("Enter your token wager: ");
        if (int.TryParse(Console.ReadLine(), out int wager) && wager > 0 && wager <= totalTokens)
        {
            totalTokens -= wager;
            string[] roll = { symbols[random.Next(symbols.Length)], symbols[random.Next(symbols.Length)], symbols[random.Next(symbols.Length)] };
            Console.WriteLine($"You rolled: {roll[0]} {roll[1]} {roll[2]}");

            int tokensWon = CalculateSlotPayout(roll, wager);
            slotTokensEarned += tokensWon;
            slotTokensEarned = Math.Min(slotTokensEarned, 6);  // Cap at 6 tokens
            totalTokens += tokensWon;
            Console.WriteLine($"You won {Math.Min(tokensWon, 6)} tokens. Total tokens: {Math.Min(totalTokens, 20)}");
        }
        else
        {
            Console.WriteLine("Invalid wager amount. Please try again.");
        }
    }

    static int CalculateSlotPayout(string[] roll, int wager)
    {
        if (roll[0] == roll[1] && roll[1] == roll[2])
        {
            return wager * 3;
        }
        else if (roll[0] == roll[1] || roll[1] == roll[2] || roll[0] == roll[2])
        {
            return wager * 2;
        }
        else if (Array.Exists(roll, symbol => symbol == "=^.^="))
        {
            return wager;
        }
        return 0;
    }

    static void PlayHideAndSeek()
    {
        if (hideSeekFirstTime)
        {
            Console.WriteLine("Welcome to Hide 'n Seek! Find the cat to earn tokens.");
            hideSeekFirstTime = false;
        }

        if (hideSeekTokensEarned >= 6)
        {
            Console.WriteLine("You have already earned the maximum of 6 tokens from Hide 'n Seek.");
            return;
        }

        string[] hidingSpots = { "plant", "couch", "fridge", "balcony", "closet", "bathtub", "sink" };
        Random random = new Random();
        string catLocation = hidingSpots[random.Next(hidingSpots.Length)];

        int attempts = 4;
        string previousGuess = "";
        bool won = false;

        for (int i = 0; i < attempts; i++)
        {
            Console.Write("Guess where the cat is hiding (plant, couch, fridge, balcony, closet, bathtub, sink): ");
            string guess = Console.ReadLine().ToLower();

            if (!Array.Exists(hidingSpots, spot => spot == guess))
            {
                Console.WriteLine("Invalid guess. Please try again.");
                i--;
                continue;
            }

            if (guess == catLocation)
            {
                Console.WriteLine("You found the cat!");
                won = true;
                break;
            }
            else
            {
                Console.WriteLine(i == 0 ? "Room temperature..." : CompareGuesses(guess, previousGuess, catLocation));
                previousGuess = guess;
            }
        }

        if (won)
        {
            int tokensWon = 3;
            hideSeekTokensEarned += tokensWon;
            hideSeekTokensEarned = Math.Min(hideSeekTokensEarned, 6);  // Cap at 6 tokens
            totalTokens += tokensWon;
            Console.WriteLine($"You won {tokensWon} tokens! Total tokens: {totalTokens}");
        }
        else
        {
            Console.WriteLine("You ran out of attempts. You lose all tokens earned in Hide 'n Seek.");
            totalTokens -= hideSeekTokensEarned;
            hideSeekTokensEarned = 0;
        }
    }

    static string CompareGuesses(string currentGuess, string previousGuess, string catLocation)
    {
        return (string.Compare(currentGuess, catLocation) < 0 && string.Compare(previousGuess, catLocation) < 0) ||
               (string.Compare(currentGuess, catLocation) > 0 && string.Compare(previousGuess, catLocation) > 0)
            ? "Hotter!"
            : "Colder!";
    }

    static void CatAdoptionCenter()
    {
        Console.WriteLine("\nWelcome to the Cat Adoption Center!");
        
        string[] cats = { "Fluffy", "Whiskers", "Shadow" };
        Console.WriteLine("What characteristic do you prefer in a cat? (1) Friendly, (2) Quiet, (3) Playful:");
        int choice = int.Parse(Console.ReadLine());
        string chosenCat = cats[choice - 1];

        Console.WriteLine($"Congratulations! You've been matched with {chosenCat}!");

        Dictionary<string, int> items = new Dictionary<string, int>
        {
            { "Collar", 3 },
            { "Bell Collar", 5 },
            { "Sparkly Collar", 8 },
            { "Toy Mouse", 2 },
            { "Feather Wand", 3 },
            { "Laser Pointer", 4 },
            { "Cat Food Small Bag", 5 },
            { "Cat Food Large Bag", 10 },
            { "Treats", 6 }
        };

        int remainingTokens = totalTokens;
        List<string> purchasedItems = new List<string>();

        while (remainingTokens > 0)
        {
            Console.WriteLine("\nHere are items available to purchase for your cat:");
            foreach (var item in items)
            {
                Console.WriteLine($"{item.Key} - {item.Value} tokens");
            }

            Console.WriteLine($"You have {remainingTokens} tokens remaining. Enter the item you want to buy or type 'done' to finish:");
            string itemChoice = Console.ReadLine();

            if (itemChoice.ToLower() == "done")
                break;

            if (items.ContainsKey(itemChoice) && items[itemChoice] <= remainingTokens)
            {
                purchasedItems.Add(itemChoice);
                remainingTokens -= items[itemChoice];
                Console.WriteLine($"You bought a {itemChoice} for {items[itemChoice]} tokens.");
            }
            else
            {
                Console.WriteLine("Invalid item or not enough tokens.");
            }
        }

        Console.WriteLine("\nSummary:");
        Console.WriteLine($"Cat: {chosenCat}");
        Console.WriteLine("Purchased items:");
        foreach (var item in purchasedItems)
        {
            Console.WriteLine($"- {item}");
        }

        Console.WriteLine("Thank you for adopting a cat and visiting the Catcade!");
    }
}




