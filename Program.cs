using System;
using System.Collections.Generic;
using System.Linq;

namespace RobotArchery
{
    class Program
    {
        static Target target = Target.GetTarget();
        static List<Archer> archers = new List<Archer>() { new Archer("Aaron"), new Archer("Barron"), new Archer("Caren"), new Archer("Darrin") };

        static void Main(string[] args)
        {
            var nOfIterations = GetIterationsFromUser();

            /* [Archer, round number, number of wins across simulations for round]
             */
            var scoreboard = RunTournamentsSimulation(nOfIterations);

            var observedRobot = "Darrin";
            var observedRobotIndex = archers.FindIndex(x => x.name == observedRobot);

            var observedRobotWinRatio = Convert.ToDecimal(scoreboard[observedRobotIndex]) / Convert.ToDecimal(nOfIterations);

            Console.WriteLine($"Result: {observedRobotWinRatio} \nThank you for testing this code, "
            + "press any key to exit...");
            Console.ReadKey();
        }

        private static int[] RunTournamentsSimulation(int nOfIterations)
        {
            var scoreboard = new int[archers.Count];

            Parallel.For(0, nOfIterations, new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount }, _ =>
            {
                var winnerName = Tournament();
                var winnerIndex = archers.FindIndex(x => x.name == winnerName);

                scoreboard[winnerIndex]++;
            });

            // for (int i = 0; i < nOfIterations; i++)
            // {
            // 	var winnerName = Tournament();
            // 	var winnerIndex = archers.FindIndex(x => x.name == winnerName);

            // 	scoreboard[winnerIndex]++;
            // }

            return scoreboard;
        }
        private static string? Tournament()
        {
            var currentArchers = new List<Archer>(archers);
            currentArchers.Reverse();
            var currentBestScore = new KeyValuePair<string, double>("", double.MaxValue);

            do
            {
                /* currentArchers goes from last to first and reads backwards (thus reading it forward),
				 * this allows to remove items from it without affecting the index
				 */
                for (int i = currentArchers.Count - 1; i >= 0; i--)
                {
                    if (currentArchers.Count == 1)
                        return currentBestScore.Key;

                    var hit = currentArchers[i].Shoot(target);

                    if (hit < currentBestScore.Value)
                        currentBestScore = new KeyValuePair<string, double>(currentArchers[i].name, hit);
                    else
                        currentArchers.RemoveAt(i);
                }

                if (currentArchers.Count == 0)
                    throw new Exception("Out of archers, this should not happen!");
            }
            while (currentArchers.Count != 1);

            return currentBestScore.Key;
        }
        private static int GetIterationsFromUser()
        {
            Console.WriteLine("The more iterations we run and the closest we get to the mathematical approximation" +
            " (0.18343765086). \nHow many iterations should we run (>1,000,000 is advised)? ");

            var nOfIterations = 0;

            while (nOfIterations <= 0)
            {
                Console.WriteLine("Please insert a number greater than 0");

                try
                {
                    nOfIterations = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                }
            }

            Console.WriteLine($"Running {nOfIterations} parallel simulations...");

            return nOfIterations;
        }
    }
}
