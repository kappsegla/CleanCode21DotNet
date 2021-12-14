namespace Trivia
{
    class Player
    {
        public bool InPenaltyBox { get; set; }
        private readonly string _name;

        public Player(string name)
        {
            InPenaltyBox = false;
            _name = name;
        }

        public string Name { get { return _name; } }
    }

    public class GameBetter : IGame
    {
        List<Player> players = new List<Player>();

        int[] places = new int[6];
        int[] purses = new int[6];

        LinkedList<string> popQuestions = new LinkedList<string>();
        LinkedList<string> scienceQuestions = new LinkedList<string>();
        LinkedList<string> sportsQuestions = new LinkedList<string>();
        LinkedList<string> rockQuestions = new LinkedList<string>();

        int currentPlayer = 0;
        bool isGettingOutOfPenaltyBox;

        public GameBetter()
        {
            for (int i = 0; i < 50; i++)
            {
                popQuestions.AddLast("Pop Question " + i);
                scienceQuestions.AddLast(("Science Question " + i));
                sportsQuestions.AddLast(("Sports Question " + i));
                rockQuestions.AddLast(CreateRockQuestion(i));
            }
        }

        public string CreateRockQuestion(int index)
        {
            return "Rock Question " + index;
        }

        public bool Add(string playerName)
        {
            players.Add(new Player(playerName));
            places[HowManyPlayers()] = 0;
            purses[HowManyPlayers()] = 0;

            Console.WriteLine(playerName + " was added");
            Console.WriteLine("They are player number " + players.Count);
            return true;
        }

        public int HowManyPlayers()
        {
            return players.Count;
        }

        public void Roll(int roll)
        {
            Console.WriteLine(players[currentPlayer].Name + " is the current player");
            Console.WriteLine("They have rolled a " + roll);

            if (players[currentPlayer].InPenaltyBox)
                IsInPenaltyBox(roll);
            else
                IsNotInPenaltyBox(roll);
        }

        private void IsNotInPenaltyBox(int roll)
        {
            places[currentPlayer] = places[currentPlayer] + roll;
            if (places[currentPlayer] > 11) places[currentPlayer] = places[currentPlayer] - 12;

            Console.WriteLine(players[currentPlayer].Name
                    + "'s new location is "
                    + places[currentPlayer]);
            Console.WriteLine("The category is " + CurrentCategory());
            AskQuestion();
        }

        private void IsInPenaltyBox(int roll)
        {
            if (IsOdd(roll))
                GettingOutOfPenaltyBox(roll);
            else
                StayingInPenaltyBox();
        }

        private void StayingInPenaltyBox()
        {
            Console.WriteLine(players[currentPlayer].Name + " is not getting out of the penalty box");
            isGettingOutOfPenaltyBox = false;
        }

        private void GettingOutOfPenaltyBox(int roll)
        {
            isGettingOutOfPenaltyBox = true;

            Console.WriteLine(players[currentPlayer].Name + " is getting out of the penalty box");
            places[currentPlayer] += roll;
            if (places[currentPlayer] > 11)
                places[currentPlayer] -= 12;

            Console.WriteLine(players[currentPlayer].Name
                    + "'s new location is "
                    + places[currentPlayer]);
            Console.WriteLine("The category is " + CurrentCategory());
            AskQuestion();
        }

        private static bool IsOdd(int roll)
        {
            return roll % 2 != 0;
        }

        private void AskQuestion()
        {
            string answer = CurrentCategory() switch
            {
                "Pop" => ExtractNextQuestion(popQuestions),
                "Science" => ExtractNextQuestion(scienceQuestions),
                "Sports" => ExtractNextQuestion(sportsQuestions),
                "Rock" => ExtractNextQuestion(rockQuestions),
                _ => "",
            };
            Console.WriteLine(answer);
        }

        private string ExtractNextQuestion(LinkedList<string> questions)
        {
            string answer = questions.First();
            questions.RemoveFirst();
            return answer;
        }

        private string CurrentCategory()
        {
            return places[currentPlayer] switch
            {
                0 or 4 or 8 => "Pop",
                1 or 5 or 9 => "Science",
                2 or 6 or 10 => "Sports",
                _ => "Rock",
            };
        }

        public bool WasCorrectlyAnswered()
        {
            if (players[currentPlayer].InPenaltyBox)
            {
                if (isGettingOutOfPenaltyBox)
                {
                    Console.WriteLine("Answer was correct!!!!");
                    purses[currentPlayer]++;
                    Console.WriteLine(players[currentPlayer].Name
                            + " now has "
                            + purses[currentPlayer]
                            + " Gold Coins.");

                    bool winner = DidPlayerWin();
                    currentPlayer++;
                    if (currentPlayer == players.Count) currentPlayer = 0;

                    return winner;
                }
                else
                {
                    currentPlayer++;
                    if (currentPlayer == players.Count) currentPlayer = 0;
                    return true;
                }



            }
            else
            {

                Console.WriteLine("Answer was corrent!!!!");
                purses[currentPlayer]++;
                Console.WriteLine(players[currentPlayer].Name
                        + " now has "
                        + purses[currentPlayer]
                        + " Gold Coins.");

                bool winner = DidPlayerWin();
                currentPlayer++;
                if (currentPlayer == players.Count) currentPlayer = 0;

                return winner;
            }
        }

        public bool WrongAnswer()
        {
            Console.WriteLine("Question was incorrectly answered");
            Console.WriteLine(players[currentPlayer].Name + " was sent to the penalty box");
            players[currentPlayer].InPenaltyBox = true;

            currentPlayer++;
            if (currentPlayer == players.Count) currentPlayer = 0;
            return true;
        }


        private bool DidPlayerWin()
        {
            return !(purses[currentPlayer] == 6);
        }
    }
}