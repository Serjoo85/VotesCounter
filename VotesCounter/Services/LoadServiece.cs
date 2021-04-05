using VotesCounter.Data;
using VotesCounter.Model;

namespace VotesCounter.Services
{
    public static class LoadServiece
    {
        public static VoteData LoadData()
        {
            TestData t = new TestData(1, 20, 1000);
            return new VoteData(t.ConCount, t.BullCount, t.Names, t.Bulletins);
        }
    }
}
