using System.Collections.Generic;
using VotesCounter.Model;

namespace VotesCounter.Servies
{
    public static class CountServiece
    {
        public static object CountVote(List<VoteData> items)
        {
            foreach (var vd in items)
            {
                for (int i = 0; i < vd.BullCount; i++)
                {
                    vd.Bulletins[]
                }
            }
            return new object();
        }
    }
}
