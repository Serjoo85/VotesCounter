using System;

namespace VotesCounter.Model
{
    public class VoteData
    {
        private readonly int _ConCount;
        private readonly int _BullCount;
        private readonly string[] _Names;
        private readonly int[,] _Bulletins;

        public int ConCount => _ConCount;

        public int BullCount => _BullCount;

        public string[] Names => _Names;

        public int[,] Bulletins => _Bulletins;

        /// <summary>
        /// Создать блок
        /// </summary>
        /// <param name="conCount">Количество кандидатов</param>
        /// <param name="bullCount">Количество бюллетеней</param>
        /// <param name="names">Список имён</param>
        /// <param name="bulletins">Список бюллетеней</param>
        public VoteData(int conCount, int bullCount, string[] names, int[,] bulletins)
        {
            _ConCount = conCount;
            _BullCount = bullCount;
            _Names = names;
            _Bulletins = bulletins;
        }

        public override string ToString()
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

            return "";
        }
    }
}
