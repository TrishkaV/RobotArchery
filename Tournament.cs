namespace RobotArchery
{
    class Tournament
    {
        internal string? RunTournament(in List<Archer> archers, in Target target)
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
    }
}