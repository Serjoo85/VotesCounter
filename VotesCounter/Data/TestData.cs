using System;
using System.Collections.Generic;
using System.Linq;

namespace VotesCounter.Data
{
    public class TestData
    {
        private static Random r = new Random();

        public static void GenerateTestData()
        {
            int blocksCount = 1;

            int conCount = r.Next(1, 20);


        }

        public static string[] GetNames(int count)
        {
            return Enumerable.Range(1, count)
                .Select(i => String.Format("{0} {1}", GetName(), GetName())).ToArray();
        }

        private static string GetName()
        {
            string name = "";
            for (int i = 0; i < r.Next(4, 7); i++)
            {
                if (name == "" || name.EndsWith('.'))
                {
                    name += Char.ToUpper((char) r.Next(97, 122));
                }
                else
                {
                    name += (char) r.Next(97, 122);
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
        public static int[,] GetBulletins(int count1, int count2)
        {
            int[] x = new int[count2];
            int[,] bulletitns = new int[count1, count2];
            for (int i = 0; i < count1; i++)
            {
                for (int k = 0; k < count2; k++)
                {
                    bulletitns[i, k] = k + 1;
                }
            }

            int temp = 0;
            int rnd = 0;
            for (int i = 0; i < count1; i++)
            {
                for (int k = 0; k < r.Next(2, count2); k++)
                {

                    rnd = r.Next(0, count2);
                    if (rnd == k)
                    {
                        if (k < count2)
                        {
                            rnd += 1;
                        }
                        else
                        {
                            rnd -= 1;
                        }
                    }

                    temp = bulletitns[i, k];
                    bulletitns[i, k] = bulletitns[i, rnd];
                    bulletitns[i, rnd] = temp;
                }
            }

            return bulletitns;
        }

    }
}


