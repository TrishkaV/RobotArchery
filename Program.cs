using ShellProgressBar;

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

            var options = new ProgressBarOptions
            {
                ProgressCharacter = '─',
                ProgressBarOnBottom = true
            };

            Console.WriteLine($"Running {nOfIterations} parallel simulations...");
            using (var pbar = new ProgressBar(nOfIterations, "", options))
            {
                Parallel.For(0, nOfIterations, new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount }, _ =>
                {
                    var winnerName = new Tournament().RunTournament(in archers, in target);
                    var winnerIndex = archers.FindIndex(x => x.name == winnerName);

                    lock (scoreboard)
                        scoreboard[winnerIndex]++;

                    pbar.Tick($" -- Simulations run: {scoreboard.Sum()}");
                });
            }

            // for (int i = 0; i < nOfIterations; i++)
            // {
            // 	var winnerName = Tournament();
            // 	var winnerIndex = archers.FindIndex(x => x.name == winnerName);

            // 	scoreboard[winnerIndex]++;
            // }

            return scoreboard;
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

            return nOfIterations;
        }
    }
}
