using System.Collections.Generic;
using VotesCounter.Model;

namespace VotesCounter.Interfaces
{
    public interface ITestData
    {
        List<VoteData> GetTestVoteData(int blocks, int conCount, int bullCount);
    }
}
