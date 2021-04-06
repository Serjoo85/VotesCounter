using System.Collections.Generic;
using VotesCounter.Model;

namespace VotesCounter.Data
{
    public class VoteDataRepository
    {
        private IList<VoteData> _Items;

        public IList<VoteData> GetItems() => _Items;

        public VoteDataRepository(IList<VoteData> items)
        {
            _Items = items;
        }
    }
}
