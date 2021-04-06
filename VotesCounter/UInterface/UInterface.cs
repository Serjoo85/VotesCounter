using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using VotesCounter.Data;
using VotesCounter.Services;
using static System.Console;

namespace VotesCounter.UInterface
{
    public static class UInterface
    {
        static string input;
        private static readonly Regex FileNamePattern;
        private static bool key = false;

        static UInterface()
        {
            input = "";
            FileNamePattern = 
            new Regex(@"[\\|\0\u0001\u0002\u0003\u0004\u0005\u0006\u000e\u000f\u0010\u0011\
                u0012\u0013\u0014\u0015\u0016\u0017\u0018\u0019\u001a\u001b\u001c\u001d\u001e\u001f]");
        }

        public static void UIEntrance()
        {
            while (true)
            {
                do
                {
                    Console.Clear();
                    WriteLine("Введите имя файла:");
                    input = ReadLine();
                    CheckMenuInput(input);
                } while (!key);
                UILoad(input);
            }
        }

        private static void CheckMenuInput(string input)
        {
            key = (input.Trim(' ').Length) != 0;
            if (key) key = (!FileNamePattern.IsMatch(input));
            if (!key) UIMessage(1);
        }
        private static void UILoad(string fileName)
        {
            VoteDataRepository vdr;
            if (File.Exists(fileName))
            {
                LoadService.LoadData(out vdr, fileName);
                UICount(vdr);
            }
            else
            {
                UIMessage(2);
            }
        }

        private static void UICount(VoteDataRepository vdr)
        {
            var result = CountService.CountVote(vdr.GetItems().ToList());
            PrintResult(result);

        }
        private static void UIMessage(int x)
        {
            Console.Clear();
            string msg = "";
            switch (x)
            {
                case 1:
                    msg = "Недопустимое имя файла.\nНажмите любую кнопку...";
                    break;
                case 2:
                    msg = "Файла с таким именем не существует.\n" +
                          "Нажмите любую кнопку...";
                    break;
                case 3:
                    msg = "Нажмите любую кнопку...";
                    break;
            }
            WriteLine(msg);
            ReadLine();
            Console.Clear();
        }

        private static void PrintResult(List<string> result)
        {
            foreach (var line in result) WriteLine(line);
            WriteLine("Нажмите любую кнопку...");
            ReadLine();
        }
    }
}
