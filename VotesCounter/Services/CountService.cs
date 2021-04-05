using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using VotesCounter.Model;

namespace VotesCounter.Services
{
    public static class CountService
    {
        struct Candidate
        {
            public BigInteger _voteSum;
            public int _index;

            public Candidate(BigInteger voteSum, int index)
            {
                _voteSum = voteSum;
                _index = index;
            }
        }

        public static List<string> CountVote(List<VoteData> items)
        {
            BigInteger tempResult;
            List<string> winners = new();
            List<Candidate> candidates = new();
            foreach (var vd in items)
            {

                for (int i = 0; i < vd.ConCount; i++)
                {
                    tempResult = 0;
                    for (int j = 0; j < vd.BullCount; j++)
                    {
                        //var t = (BigInteger) (1 * Math.Pow(10, (double) (20 - vd.Bulletins[j, i])));
                        tempResult += BigInteger.Pow(10, (20 - vd.Bulletins[j, i]));
                    }

                    candidates.Add(new Candidate(tempResult, i));
                }

                //candidates.Sort((o1, o2) => o1._voteSum.CompareTo(o2._voteSum));
                var x  = candidates.OrderByDescending(c => c._voteSum).ToList();
                for (int i = 0; i < x.Count; i++)
                {
                    if (x[i]._voteSum == x[0]._voteSum)
                    {
                        winners.Add(vd.Names[x[i]._index]);
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
