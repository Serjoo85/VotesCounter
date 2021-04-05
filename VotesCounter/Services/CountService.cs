using System.Collections.Generic;
using System.Linq;
using VotesCounter.Model;

namespace VotesCounter.Services
{
    public static class CountService
    {
        struct Candidate
        {
            public int _voteSum;
            public int _index;

            public Candidate(int voteSum, int index)
            {
                _voteSum = voteSum;
                _index = index;
            }
        }

        public static List<string> CountVote(List<VoteData> items)
        {
            int tempResult = 0;
            List<string> winners = new();
            List<Candidate> candidates = new();
            foreach (var vd in items)
            {

                for (int i = 0; i < vd.ConCount; i++)
                {
                    tempResult = 0;
                    for (int j = 0; j < vd.BullCount; j++)
                    {
                        tempResult += vd.Bulletins[j, i];
                    }

                    candidates.Add(new Candidate(tempResult, i));
                }

                candidates.Sort((o1, o2) => o1._voteSum.CompareTo(o2._voteSum));

                for (int i = 0; i < candidates.Count; i++)
                {
                    if (candidates[i]._voteSum == candidates[0]._voteSum)
                    {
                        winners.Add(vd.Names[candidates[i]._index]);
                    }
                    else
                    {
                        winners.Add("\n");
                        break;
                    }
                }
            }

            return winners;
        }
    }
}
