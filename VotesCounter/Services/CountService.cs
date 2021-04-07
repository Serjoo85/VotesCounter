using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotesCounter.Model;

namespace VotesCounter.Services
{
    public class CountService
    {
        public static void CountVote(StepData sd)
        {
            foreach (var vd in sd.VdList)
            {
                List<Candidate> candidates = CreateCandidates(vd);
                CalculateWinners(sd, candidates);
            }
        }

        private static List<Candidate> CreateCandidates(VoteData vd)
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

        private static void CalculateWinners(StepData sd, List<Candidate> candidates)
        {
            sd.Winners.AddRange(from cand in candidates where cand.CompareTo(candidates[^1]) == 0 select cand.Name);
        }
    }
}
