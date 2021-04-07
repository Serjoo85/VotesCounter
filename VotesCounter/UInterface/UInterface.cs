using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using VotesCounter.Model;
using VotesCounter.Services;
using static System.Console;

namespace VotesCounter.UInterface
{
    public static class UInterface
    {
        private static string input;
        private static readonly Regex FileNamePattern;
        private static bool key;
        private static StepData sd;

        static UInterface()
        {
            input = "";
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
                UiLoad(input);
                UiCount();
                sd.PrintErr();
            }
        }

        private static void UiInput()
        {
            do
            {
                WriteLine("Введите имя файла:");
                input = ReadLine();
                Console.Clear();
                CheckInput(input);
            } while (!key);

            void CheckInput(string input)
            {
                key = (input.Trim(' ').Length) != 0;
                if (key) key = (!FileNamePattern.IsMatch(input));
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
                PrintResult(
                    CountService.CountVote(sd.VdList.ToList()));
            }
        }

        private static void PrintResult(List<string> result)
        {
            foreach (var line in result) WriteLine(line + "\n");
            WriteLine("Нажмите любую клавишу...");
            ReadKey();
        }
    }
}
