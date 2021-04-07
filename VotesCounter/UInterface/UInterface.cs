using System.Text.RegularExpressions;
using VotesCounter.Model;
using VotesCounter.Services;
using static System.Console;

namespace VotesCounter.UInterface
{
    public static class UInterface
    {
        private static readonly Regex FileNamePattern;
        private static UiData sd;

        static UInterface()
        {
            FileNamePattern =
            new Regex(@"[\\|\0\u0001\u0002\u0003\u0004\u0005\u0006\u000e\u000f\u0010\u0011\
                u0012\u0013\u0014\u0015\u0016\u0017\u0018\u0019\u001a\u001b\u001c\u001d\u001e\u001f]");
        }

        public static void UiEntrance()
        {
            while (true)
            {
                UiInput();
                UiLoad();
                UiCount();
                sd.PrintMsg();
            }
        }

        private static void UiInput()
        {
            bool key = false;

            WriteLine("Введите имя файла:");
            sd = new UiData(ReadLine());
            CheckInput();

            void CheckInput()
            {
                key = (sd.FileName.Trim(' ').Length) != 0;
                if (key) key = (!FileNamePattern.IsMatch(sd.FileName));
                if (!key)
                {
                    sd = new CheckInputFail("Недопустимое имя файла.");
                }
            }
        }

        private static void UiLoad()
        {
            if (sd.GetKey()) sd = LoadService.LoadData(sd);
        }

        private static void UiCount()
        {
            if (sd.GetKey()) CountService.CountVote(sd);
        }
    }
}
