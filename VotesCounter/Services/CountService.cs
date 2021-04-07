using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotesCounter.Model;

namespace VotesCounter.Services
{
    public static class CountService
    {
        private static UiData _sd;
        public static void CountVote(UiData sd)
        {
            _sd = sd;
            foreach (var vd in _sd.VdList)
            {
                List<Candidate> candidates = CreateCandidates(vd);
                CalculateWinners(candidates);
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

        private static void CalculateWinners(List<Candidate> candidates)
        {
            _sd.Winners.AddRange(from cand in candidates where cand.CompareTo(candidates[^1]) == 0 select cand.Name);
        }
    }
}
