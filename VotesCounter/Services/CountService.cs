using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using VotesCounter.Model;

namespace VotesCounter.Services
{
    public static class CountService
    {
        public static List<string> CountVote(List<VoteData> items)
        {
            List<string> winners = new();
            List<Candidate> candidates = new();
            foreach (var vd in items)
            {

                for (int i = 0; i < vd.ConCount; i++)
                {
                    int[] votes = new int[vd.ConCount];
                    for (int j = 0; j < vd.BullCount; j++)
                    {
                        votes[vd.Bulletins[j, i] - 1] += 1;
                    }

                    candidates.Add(new Candidate(votes, i, vd.Names[i]));
                }

                candidates.Sort();

                foreach (var cand in candidates)
                {
                    if (cand == candidates[^1])
                    {
                        winners.Add(cand.Name);
                    }
                }
                winners.Add("\n");
            }

            return winners;
        }
    }
}
