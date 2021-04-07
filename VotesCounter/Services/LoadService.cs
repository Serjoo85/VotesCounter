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
        public static IList<VoteData> LoadData(string fileName = "")
        {
            var LoadResult = LoadFromFileAsync(fileName);
            return ((CreateCandidateList(LoadResult.Result, fileName)));
        }

        private static List<VoteData> LoadTestData()
        {
            return TestData.GetTestVoteData(1,20,1000);
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
            if (int.TryParse(voteDataString[0], out var blockCount))
            {
                if (blockCount > 0)
                {
                    IList<string> blockList = new List<string>();
                    for (int i = 2; i < voteDataString.Count; i++)
                    {
                        // Разбиваем на блоки.
                        // Если пустая строка (разделитель)
                        if (string.IsNullOrEmpty(voteDataString[i].Trim(' ')))
                        {
                            var list = blockList.Clone();
                            CreateVoteData(list, i, items);
                            blockList = new List<string>();
                        }
                        // Если последняя строка
                        else if (i == voteDataString.Count - 1)
                        {
                            blockList.Add(voteDataString[i]);
                            CreateVoteData(blockList, i);
                        }
                        else
                        {
                            blockList.Add(voteDataString[i]);
                        }
                    }
                    return items;
                }
                else
                {
                    PrintMsg("Недопустимое значение количества блоков", 1);
                    return items;
                }
            }
            else
            {
                PrintMsg("Недопустимое значение количества блоков", 1);
                return items;
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
                    int[,] bulletins = new int[bullCount, conCount];

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