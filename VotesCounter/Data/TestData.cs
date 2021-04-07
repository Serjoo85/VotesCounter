using System;
using System.Collections.Generic;
using System.Linq;
using VotesCounter.Model;

namespace VotesCounter.Data
{
    public class TestData
    {
        public int ConCount;
        public int BullCount;
        public string[] Names;
        public int[,] Bulletins;

        private static readonly Random r = new Random();


        private TestData(int conCount, int bullCount)
        {
            ConCount = conCount;
            BullCount = bullCount;
            Names = GetNames(conCount);
            Bulletins = GetBulletins(bullCount, conCount);
        }

        /// <summary>
        /// Имитация загрузки из файла
        /// </summary>
        /// <param name="blocks">Количество блоков</param>
        /// <param name="conCount">Количество кандидатов</param>
        /// <param name="bullCount">Количество бюллетеней</param>
        /// <returns></returns>
        public static List<VoteData> GetTestVoteData(int blocks, int conCount, int bullCount)
        {
            var items = new List<VoteData>();
            for (int i = 0; i < blocks; i++)
            {
                items.Add(new VoteData(conCount, bullCount, GetNames(conCount), GetBulletins(bullCount, conCount)));
            }
            return items;
        }

        private static string[] GetNames(int count)
        {
            return Enumerable.Range(1, count)
                .Select(i => String.Format("{0} {1}", GetName(), GetName() + i)).ToArray();
        }

        private static string GetName()
        {
            string name = "";
            for (int i = 0; i < r.Next(4, 7); i++)
            {
                if (name == "" || name.EndsWith('.'))
                {
                    name += Char.ToUpper((char)r.Next(97, 122));
                }
                else
                {
                    name += (char)r.Next(97, 122);
                }
            }

            return name;
        }

        /// <summary>
        /// Возвращает массив бюллетеней.
        /// </summary>
        /// <param name="count1">Количество бюллетеней</param>
        /// <param name="count2">Количество кандидатов</param>
        /// <returns></returns>
        private static int[,] GetBulletins(int count1, int count2)
        {
            int[] x = new int[count2];
            int[,] bulletins = new int[count1, count2];

            // Генерация одинаковых бюллетеней (1,2,4 ..)
            for (int i = 0; i < count1; i++)
            {
                for (int j = 0; j < count2; j++)
                {
                    bulletins[i, j] = j + 1;
                }
            }

            // Перестановка чисел в каждой бюллетени.
            int temp = 0;
            int rnd1 = 0;
            int rnd2 = 0;
            for (int i = 0; i < count1; i++)
            {
                // Количество перестановок равно количеству 
                // кандидатов.
                for (int j = 0; j < r.Next(2, count2 * 25); j++)
                {
                    rnd1 = r.Next(0, count2);
                    rnd2 = r.Next(0, count2);
                    if (rnd1 == rnd2)
                    {
                        // Выход за пределы массива.
                        if (rnd2 < count2 - 1)
                        {
                            rnd2 += 1;
                        }
                        else
                        {
                            rnd2 -= 1;
                        }
                    }

                    temp = bulletins[i, rnd1];
                    bulletins[i, rnd1] = bulletins[i, rnd2];
                    bulletins[i, rnd2] = temp;
                }
            }

            return bulletins;
        }
    }
}


