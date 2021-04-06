using System;
using Microsoft.VisualBasic;
using VotesCounter.Data;
using VotesCounter.Services;
using static System.Console;

namespace VotesCounter
{
    [Flags]
    enum UIEnt
    {
        emp = 0b0000,
        isnum = 0b0001,
        length = 0b0010,
        big = 0b0100,
        small = 0b1000,
    }
    class Program
    {
        private static UIEnt mi = new();
        static void Main(string[] args)
        {
            VoteDataRepository vdr;
            LoadService.LoadData(out vdr, "randomfile.txt");

            Console.ReadLine();
        }

        private static void UIEntrance()
        {
            WriteLine("Выберите действие:\n" +
                      "1. Сгенерировать тестовый файл.\n" +
                      "2. Ввести имя существующего файла.\n" +
                      "3. Выйти.");
            var input = ReadLine();
            int n = 0;
            if (CheckMenuInput(input))
            {
                n = int.Parse(input);
            }
            else
            {
                UIEntFaile();
            }

            switch (n)
            {
                case 1:

                    break;
                case 2:
                    break;
                case 3:
                    break;
            }
        }

        private static bool CheckMenuInput(string input)
        {
            mi = 0;
            if (input.Length == 1)
            {
                mi = (input.Trim(' ').Length == 1) ? mi = UIEnt.emp : mi = UIEnt.length;
                if(mi == 0) mi = (Char.IsNumber(Convert.ToChar(input))) ? mi = UIEnt.emp : mi = UIEnt.isnum;
                if(mi == 0) mi = (int.Parse(input) < 0) ? mi = UIEnt.emp : mi = UIEnt.small;
                if(mi == 0) mi = (int.Parse(input) > 4) ? mi = UIEnt.emp : mi = UIEnt.big;
            }
            return (mi == 0);
        }
        private static void UserChoice()
        {

        }

        private static void UIEntFaile()
        {
            string msg = "";
            if ((mi & UIEnt.isnum) == UIEnt.isnum) msg = "Ввод не является числом.";
            else if ((mi & UIEnt.length) == UIEnt.length) msg = "Недопустимое количество символов.";
            else if ((mi & UIEnt.big) == UIEnt.big) msg = "Введенное число слишком большое.";
            else if ((mi & UIEnt.small) == UIEnt.small) msg = "Введенное число слишком маленькое.";
            msg += " Введите число от 1 до 3";
            WriteLine(msg);
        }

        private static void UIFileGen()
        {
            CreateRandomFileService

        }
    }
}
