using System;
using System.IO;
using Trivia;
using Xunit;

namespace TriviaTest
{
    public class GameRunnerTest
    {
        [Fact]
        public void CaracterizationTest()
        {
            for (int seed = 1; seed < 10_000; seed++)
            {
                string expectedOutput = ExtractOutput(new Random(seed), new Game());
                string actualOutput = ExtractOutput(new Random(seed), new GameBetter());
                Assert.Equal(expectedOutput, actualOutput);
            }
        }

        private string ExtractOutput(Random rand, IGame aGame)
        {
            var sw = new StringWriter();
            Console.SetOut(sw);

            aGame.Add("Chet");
            aGame.Add("Pat");
            aGame.Add("Sue");

            bool notAWinner;
            do
            {
                aGame.Roll(rand.Next(5) + 1);

                if (rand.Next(9) == 7)
                {
                    notAWinner = aGame.WrongAnswer();
                }
                else
                {
                    notAWinner = aGame.WasCorrectlyAnswered();
                }

            } while (notAWinner);


            // Restore default output stream
            var standardOutput = new StreamWriter(Console.OpenStandardOutput())
            {
                AutoFlush = true
            };
            Console.SetOut(standardOutput);

            return sw.ToString();
        }
    }
}