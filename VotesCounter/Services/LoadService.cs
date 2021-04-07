using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VotesCounter.Model;

namespace VotesCounter.Services
{
    public static class LoadService
    {
        public static object LockObj = new object();
        public static StepData LoadData(string fileName, StepData sd)
        {
            if (File.Exists(fileName))
            {
                var LoadResult = LoadFromFileAsync(fileName, sd);
                if(sd.GetKey())sd.vdList = CreateCandidateList(LoadResult.Result, sd);
                return sd;
            }
            else
            {
                return new LoadFail("Файл не найден.\nНажмите любую клавишу...");
            }

        }

        private static async Task<IList<string>> LoadFromFileAsync(string fileName, StepData sd)
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
                sd = new LoadFail("Не удалось прочитать файл {fileName}, ошибка:");
            }
            return voteDataString;
        }

        private static StepData CreateCandidateList(IList<string> voteDataString, StepData sd)
        {
            IList<VoteData> items = new List<VoteData>();
            if (int.TryParse(voteDataString[0], out var blockCount))
            {
                if (blockCount > 0)
                {
                    IList<string> blockList = new List<string>();
                    for (int i = 2; i < voteDataString.Count; i++)
                    {
                        if (sd.GetKey())
                        {
                            // Разбиваем на блоки.
                            // Если пустая строка (разделитель).
                            if (string.IsNullOrEmpty(voteDataString[i].Trim(' ')))
                            {
                                var list = blockList.Clone();
                                CreateVoteData(list, i, sd);
                                blockList = new List<string>();
                            }
                            // Если последняя строка.
                            else if (i == voteDataString.Count - 1)
                            {
                                blockList.Add(voteDataString[i]);
                                CreateVoteData(blockList, i, sd);
                            }
                            // Читаем тело.
                            else
                            {
                                blockList.Add(voteDataString[i]);
                            }
                        }
                        else
                        {
                            return sd;
                        }
                    }
                }
                else
                {
                    sd = new CreateCandidateListFail(GetMsg("Количество блоков меньше или равно нулю", 1));
                    return sd;
                }
            }
            else
            {
                sd = new CreateCandidateListFail(GetMsg("Количество блоков не является числом.", 1));
                return sd;
            }
            return sd;
        }

        private static void CreateVoteData(IList<string> block, int lineCount, StepData sd)
        {
            int lineCurr = lineCount - block.Count;

            if (int.TryParse(block[0], out var canCount))
            {
                if (canCount > 0 && canCount <= 20)
                {
                    int bullCount = block.Count - canCount - 1;

                    string[] names = new string[canCount];
                    int[,] bulletins = new int[bullCount, canCount];

                    for (int i = 0; i < canCount; i++)
                    {
                        names[i] = block[i + 1];
                    }

                    Parallel.For(canCount + 1, block.Count, (i) =>
                    {
                        var bulletin = block[i].Split();
                        for (int j = 0; j < canCount; j++)
                        {
                            if (int.TryParse(bulletin[j], out var n))
                            {
                                bulletins[i - canCount - 1, j] = n;
                            }
                            else
                            {
                                sd = new CreateVoteDataFail(GetMsg(
                                    "Недопустимое значение количества голосов", lineCurr + i));
                                break;
                            }
                        }
                    });
                    if (sd.GetKey()) sd.AddVoteData(new VoteData(canCount, bullCount, names, bulletins));
                }
                else
                {
                    sd = new CreateVoteDataFail(GetMsg(
                        "Недопустимое значение количества голосов", lineCurr));
                }
            }
            else
            {
                sd = new CreateVoteDataFail(GetMsg(
                    "Недопустимое значение количества голосов", lineCurr));
            }
        }
        private static string GetMsg(string mtext, int lineNumber)
        {
            return string.Format("Ошибка обработки данных. {0}. стр. {1}", mtext, lineNumber + 1);
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