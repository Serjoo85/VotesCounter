using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotesCounter.Model;

namespace VotesCounter.Services
{
    public class CountService
    {
        public static List<string> CountVote(List<VoteData> items)
        {
            List<string> winners = new();
            foreach (var vd in items)
            {
                List <Candidate> candidates = CreateCandidates(vd);
                CalculateWinners(winners, candidates);
            }
            return winners;
        }

        private static List<Candidate> CreateCandidates (VoteData vd)
        {
            List<Candidate> candidates = new();
            Parallel.For(0, vd.canCount, (i) =>
            {
                int[] votes = new int[vd.canCount];
                for (int j = 0; j < vd.BullCount; j++)
                {
                    votes[vd.Bulletins[j, i] - 1] += 1;
                }
                candidates.Add(new Candidate(votes, i, vd.Names[i]));
            });
            candidates.Sort();
            return candidates;
        }

        private static void CalculateWinners(List<string> winners, List<Candidate> candidates)
        {
            winners.AddRange(from cand in candidates where cand.CompareTo(candidates[^1]) == 0 select cand.Name);
        }
    }
}
