using System.Collections.Generic;
using VotesCounter.Model;

namespace VotesCounter.Data
{
    public class VoteDataRepository
    {
        private List<VoteData> _Items;

        public List<VoteData> GetItems() => _Items;

        public VoteDataRepository(List<VoteData> items)
        {
            _Items = items;
        }
    }
}
