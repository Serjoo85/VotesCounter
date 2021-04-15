using System.Text.RegularExpressions;
using VotesCounter.Model;
using VotesCounter.Services;
using static System.Console;

namespace VotesCounter.UInterface
{
    public class UInterface
    {
        private readonly Regex FileNamePattern;
        private UiData sd;

        public void UiEntrance()
        {
            while (true)
            {
                //UiTestGeneration();
                UiInput();
                UiLoad();
                UiCount();
                sd.PrintMsg();
            }
        }

        private void UiTestGeneration()
        {
            TestDataService.CreateFile(TestData.GetTestVoteData(1, 4, 6));
        }

        private void UiInput()
        {
            bool key = false;

            //WriteLine("Введите имя файла:");
            sd = new UiData("test.txt");
            CheckInput();

            void CheckInput()
            {
                key = (sd.FileName.Trim(' ').Length) != 0;
                if (!key)
                {
                    sd = new CheckInputFail("Недопустимое имя файла.");
                }
            }
        }

        private void UiLoad()
        {
            if (sd.GetKey()) sd = LoadService.LoadData(sd);
        }

        private void UiCount()
        {
            if (sd.GetKey()) CountService.CountVote(sd);
        }
    }

}
