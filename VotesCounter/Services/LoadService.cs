using System.Collections.Generic;
using VotesCounter.Data;
using VotesCounter.Model;

namespace VotesCounter.Services
{
    public static class LoadService
    {
        public static List<VoteData> LoadData()
        {
            List<VoteData> items = new();
            items = TestData.GetTestVoteData(3, 20, 1000);
            return items;

        }
    }
}
