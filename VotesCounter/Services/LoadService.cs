using VotesCounter.Data;
using VotesCounter.Model;

namespace VotesCounter.Services
{
    public static class LoadService
    {
        public static void LoadData()
        {

            TestData t = new (20, 1000);
            VoteData.CreateNewBlock(t.ConCount, t.BullCount, t.Names, t.Bulletins);
            t = new TestData(20, 1000);
            VoteData.CreateNewBlock(t.ConCount, t.BullCount, t.Names, t.Bulletins);
            t = new TestData(20, 1000);
            VoteData.CreateNewBlock(t.ConCount, t.BullCount, t.Names, t.Bulletins);
            t = new TestData(20, 1000);
            VoteData.CreateNewBlock(t.ConCount, t.BullCount, t.Names, t.Bulletins);
            t = new TestData(20, 1000);
            VoteData.CreateNewBlock(t.ConCount, t.BullCount, t.Names, t.Bulletins);


        }
    }
}
