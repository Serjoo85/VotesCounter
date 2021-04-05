using VotesCounter.Data;
using VotesCounter.Model;

namespace VotesCounter.Services
{
    public static class LoadServiece
    {
        public static void LoadData()
        {

            TestData t = new TestData(1, 20, 1000);
            VoteData.CreateNewBlock(t.ConCount, t.BullCount, t.Names, t.Bulletins);
            t = new TestData(1, 20, 1000);
            VoteData.CreateNewBlock(t.ConCount, t.BullCount, t.Names, t.Bulletins);
            t = new TestData(1, 20, 1000);
            VoteData.CreateNewBlock(t.ConCount, t.BullCount, t.Names, t.Bulletins);
            t = new TestData(1, 20, 1000);
            VoteData.CreateNewBlock(t.ConCount, t.BullCount, t.Names, t.Bulletins);
            t = new TestData(1, 20, 1000);
            VoteData.CreateNewBlock(t.ConCount, t.BullCount, t.Names, t.Bulletins);


        }
    }
}
