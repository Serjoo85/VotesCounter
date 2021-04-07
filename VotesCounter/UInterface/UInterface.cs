using System;
using System.Text.RegularExpressions;
using VotesCounter.Model;
using VotesCounter.Services;
using static System.Console;

namespace VotesCounter.UInterface
{
    public static class UInterface
    {
        private static string fileName;
        private static readonly Regex FileNamePattern;
        private static bool key;
        private static StepData sd;

        static UInterface()
        {
            fileName = "";
            FileNamePattern =
            new Regex(@"[\\|\0\u0001\u0002\u0003\u0004\u0005\u0006\u000e\u000f\u0010\u0011\
                u0012\u0013\u0014\u0015\u0016\u0017\u0018\u0019\u001a\u001b\u001c\u001d\u001e\u001f]");
            key = false;
        }

        public static void UiEntrance()
        {
            while (true)
            {
                sd = new StepData();
                UiInput();
                UiLoad(fileName);
                UiCount();
                sd.PrintMsg();
            }
        }

        private static void UiInput()
        {
            do
            {
                WriteLine("Введите имя файла:");
                fileName = ReadLine();
                Console.Clear();
                CheckInput(fileName);
            } while (!key);

            void CheckInput(string fileName)
            {
                key = (fileName.Trim(' ').Length) != 0;
                if (key) key = (!FileNamePattern.IsMatch(fileName));
                if (!key)
                {
                    WriteLine("Недопустимое имя файла.\n" +
                                    "Нажмите любую клавишу...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }

        private static void UiLoad(string fileName)
        {
            sd = LoadService.LoadData(fileName, sd);
        }

        private static void UiCount()
        {
            if (sd.GetKey())
            {
                CountService.CountVote(sd);
            }
        }
    }
}
