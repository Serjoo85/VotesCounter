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
            sd = new StepData();
            input = "";
            FileNamePattern =
            new Regex(@"[\\|\0\u0001\u0002\u0003\u0004\u0005\u0006\u000e\u000f\u0010\u0011\
                u0012\u0013\u0014\u0015\u0016\u0017\u0018\u0019\u001a\u001b\u001c\u001d\u001e\u001f]");
            key = false;
        }

        public static void UIEntrance()
        {
            while (true)
            {
                Input();
                UILoad(input, sd);
                UICount(sd);

            }
        }

        private static void Input()
        {
            Console.Clear();
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
                if (!key) WriteLine("Недопустимое имя файла.\n" +
                                    "Нажмите любую кнопку...");
            }
        }

        private static void UILoad(string fileName, StepData sd)
        {
            sd = LoadService.LoadData(fileName, sd);
        }

        private static void UICount(StepData sd)
        {
            if (sd.GetKey()) CountService.CountVote(sd.VdList.ToList());
            sd.PrintErr();
            //var result = CountService.CountVote(sd);
        }
        private static void UIFaile(int x)
        {
            string msg = "";
            switch (x)
            {
                case 1:
                    msg = "";
                    break;
                case 2:
                    msg = "Файла с таким именем не существует.\nНажмите любую кнопку...";
                    break;
                case 3:
                    msg = "Файл не считан.\nНажмите любую кнопку...";
                    break;
            }
            WriteLine(msg);
            ReadLine();
            Console.Clear();
        }

        private static void PrintResult(List<string> result)
        {
            foreach (var line in result) WriteLine(line + "\n");
            WriteLine("Нажмите любую кнопку...");
            ReadLine();
        }
    }
}
