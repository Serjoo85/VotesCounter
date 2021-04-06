using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VotesCounter.Data;
using VotesCounter.Model;

namespace VotesCounter.Services
{
    public static class LoadService
    {
        public static object LockObj = new object();
        public static void LoadData( out VoteDataRepository vdr, string fileName = "")
        {
            var LoadResult = LoadFromFileAsync(fileName);
            vdr = new VoteDataRepository((CreateCandidateList(LoadResult.Result, fileName)));
        }

        private static List<VoteData> LoadTestData()
        {
            List<VoteData> items = new();
            items = TestData.GetTestVoteData(3, 20, 1000);
            return items;
        }

        private static async Task<IList<string>> LoadFromFileAsync(string fileName)
        {
            IList<string> voteDataString = new List<string>();
            try
            {
                using StreamReader sr = new StreamReader(fileName, System.Text.Encoding.Default);
                string line;
                while ((line = await sr.ReadLineAsync()) != null)
                {
                    voteDataString.Add(line);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Не удалось прочитать файл {fileName}, ошибка:");
                Console.WriteLine(e.Message);
            }

            return voteDataString;
        }

        private static IList<VoteData> CreateCandidateList(IList<string> voteDataString, string fileName)
        {
            IList<VoteData> items = new List<VoteData>();
            int blockCount;
            int lineCount = 0;
            if (int.TryParse(voteDataString[0], out blockCount))
            {
                if (blockCount > 0)
                {
                    IList<string> blockList = new List<string>();
                    foreach (var line in voteDataString)
                    {
                        lineCount++;
                        // Разбиваем на блоки.
                        if (lineCount > 2)
                        {
                            if (!string.IsNullOrEmpty(line.Trim(' ')))
                            {
                                blockList.Add(line);
                            }
                            else
                            {
                                var list = blockList.Clone();
                                CreateVoteData(list, lineCount, items);
                                blockList = new List<string>();
                            }
                        }
                    }
                    return items;
                }
                else
                {
                    PrintMsg("Недопустимое значение количества блоков", lineCount);
                    return null;
                }
            }
            else
            {
                PrintMsg("Недопустимое значение количества блоков", lineCount);
                return null;
            }
        }

        private static void CreateVoteData(IList<string> block, int lineCount, IList<VoteData> items)
        {
            int lineCurr = lineCount - block.Count;
            int conCount;
            bool flag = true;
            if (int.TryParse(block[0], out conCount))
            {
                if (conCount > 0 && conCount <= 20)
                {
                    int bullCount = block.Count - conCount - 1;

                    string[] names = new string[conCount];
                    int[,] bulletins = new int[block.Count - 1 - conCount, 20];

                    for (int i = 0; i < conCount; i++)
                    {
                        names[i] = block[i + 1];
                    }

                    Parallel.For(conCount + 1, block.Count, (i) =>
                    {
                        var bulletin = block[i].Split();
                        int n;
                        for (int j = 0; j < conCount; j++)
                        {
                            if (int.TryParse(bulletin[j], out n))
                            {
                                bulletins[i - conCount - 1, j] = n;
                            }
                            else
                            {
                                PrintMsg("Недопустимое значение количества голосов", lineCurr + i);
                                flag = false;
                                break;
                            }

                        }
                    });
                    if (flag) items.Add(new VoteData(conCount, bullCount, names, bulletins));
                }
                else
                {
                    PrintMsg("Недопустимое значение количества кандидатов", lineCurr);
                }
            }
            else
            {
                PrintMsg("Недопустимое значение количества кандидатов", lineCurr);
            }
        }
        private static void PrintMsg(string mtext, int lineNumber)
        {
            Console.WriteLine(string.Format("Ошибка обработки данных. {0}. стр. {1}", mtext, lineNumber));
        }
    }


    static class Extensions
    {
        public static IList<T> Clone<T>(this IList<T> listToClone) where T : ICloneable
        {
            return listToClone.Select(item => (T)item.Clone()).ToList();
        }
    }
}