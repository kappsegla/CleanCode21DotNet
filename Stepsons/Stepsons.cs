namespace Stepsons
{
    public class Stepsons
    {
        static void Main(string[] args)
        {
            StenSaxPåse();
        }

        static void StenSaxPåse()
        {
            Random ran = new Random();

            int computerScore = 0;
            int playerScore = 0;
            int computerChoice;
            int playerInput;

            Console.WriteLine("Välkommen till Sten Sax Påse");

            while (computerScore < 3 && playerScore < 3)
            {

                Console.WriteLine("Skriv 1 för Sten, 2 för Sax och 3 för Påse");

                string input = Console.ReadLine();
                if (!int.TryParse(input, out playerInput))
                {
                    Console.WriteLine("Error fel input! Du måste skriva 1, 2 eller 3");
                    break;
                }

                computerChoice = ran.Next(3) + 1;

                if (1 == (playerInput) || 2 == (playerInput) || 3 == (playerInput))
                {
                    if (((playerInput == 1) && (computerChoice == 2)) || ((playerInput == 2) && (computerChoice == 3))
                            || ((playerInput == 3) && (computerChoice == 1)))
                    {

                        playerScore++;

                        Console.WriteLine("\nDu vann!");
                        Console.WriteLine("Du har: " + playerScore + " poäng.\nDatorn har: " + computerScore);
                    }
                    else if (((playerInput == 2) && (computerChoice == 1)) || ((playerInput == 3) && (computerChoice == 2))
                          || ((playerInput == 1) && (computerChoice == 3)))
                    {

                        computerScore++;

                        Console.WriteLine("\nDatorn vann!");
                        Console.WriteLine("Du har: " + playerScore + " poäng.\nDatorn har: " + computerScore);
                    }
                    else if (((playerInput == 1) && (computerChoice == 1)) || ((playerInput == 2) && (computerChoice == 2))
                          || ((playerInput == 3) && (computerChoice == 3)))
                    {

                        Console.WriteLine("\nOavgjort!");
                        Console.WriteLine("Du har: " + playerScore + " poäng.\nDatorn har: " + computerScore);
                    }
                }
                else
                {
                    Console.WriteLine("Error fel input! Du måste skriva 1, 2 eller 3");
                }
            }

            if (playerScore == 3)
            {
                Console.WriteLine("\nGrattis! Du fick: " + playerScore + "\nDatorn fick: " + computerScore + "\nDu vann över Datorn!\n");
            }
            else if (computerScore == 3)
            {
                Console.WriteLine("\nGrattis datorn! Datorn fick: " + computerScore + "\nDu fick: " + playerScore + "\nDatorn vann över dig!\n");
            }

        }
    }
}
