using System;
using System.Collections.Generic;

namespace VotesCounter.Model
{
    public class VoteData
    {
        private static List<VoteData> _items = new();
        public static List<VoteData> Items => _items;


        private readonly int _ConCount;
        private readonly int _BullCount;
        private readonly string[] _Names;
        private readonly int[,] _Bulletins;

        public int ConCount => _ConCount;

        public int BullCount => _BullCount;

        public string[] Names => _Names;

        public int[,] Bulletins => _Bulletins;

        private VoteData(int conCount, int bullCount, string[] names, int[,] bulletins)
        {
            _ConCount = conCount;
            _BullCount = bullCount;
            _Names = names;
            _Bulletins = bulletins;
        }


        /// <summary>
        /// Один блок из файла
        /// </summary>
        /// <param name="conCount">Количество кандидатов</param>
        /// <param name="bullCount">Количество бюллетеней</param>
        /// <param name="names">Список имён</param>
        /// <param name="bulletins">Список бюллетеней</param>
        public static void CreateNewBlock(int conCount, int bullCount, string[] names, int[,] bulletins)
        {
            _items.Add(new VoteData(conCount, bullCount, names, bulletins));
        }
        /// <summary>
        /// Сбрасывает загруженные данные
        /// </summary>
        public static void Reset()
        {
            _items = new List<VoteData>();
        }
        public void PrintAll()
        {
            foreach (var n in Names)
            {
                Console.WriteLine(n);
            }
            for (int i = 0; i < this.BullCount; i++)
            {
                for (int j = 0; j < this.ConCount; j++)
                {
                    if (Bulletins[i, j] == 1) Console.ForegroundColor = ConsoleColor.Yellow;
                    if (Bulletins[i, j] == 2) Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(Bulletins[i, j].ToString("00") + " ");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.Write("\n");
            }
        }
    }
}
