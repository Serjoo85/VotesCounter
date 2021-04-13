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
                UiInput();
                UiLoad();
                UiCount();
                sd.PrintMsg();
            }
        }

        private void UiInput()
        {
            bool key = false;

            WriteLine("Введите имя файла:");
            sd = new UiData(ReadLine());
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
