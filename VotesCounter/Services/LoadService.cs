using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using VotesCounter.Data;
using VotesCounter.Model;

namespace VotesCounter.Services
{
    public static class LoadService
    {
        public static void LoadData(string fileName = "")
        {
            var x = LoadFromFile(fileName);
            CreateCandidateList(x.Result, fileName);
        }

        private static List<VoteData> LoadTestData()
        {
            List<VoteData> items = new();
            items = TestData.GetTestVoteData(3, 20, 1000);
            return items;
        }

        private static async Task<IList<string>> LoadFromFile(string fileName)
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

        private static List<VoteData> CreateCandidateList(IList<string> voteDataString, string fileName)
        {
            List<VoteData> items = new();
            int blockCount;
            int lineCount = 0;
            IList<string> blockList = new List<string>();

            blockCount = Convert.ToInt32(voteDataString[0]);
            foreach (var line in voteDataString)
            {
                lineCount++;

                if (lineCount > 2)
                {
                    if (!string.IsNullOrEmpty(line.Trim(' ')))
                    {
                        blockList.Add(line);
                    }
                    else
                    {
                        items.Add(CreateVoteData(blockList, lineCount));
                        blockList = new List<string>();
                    }
                }
            }

            return items;

            void PrintMsg(string mtext)
            {
                Console.WriteLine("Ошибка обработки данных. " + mtext);
            }


            VoteData CreateVoteData(IList<string> block, int lineCount)
            {
                int lineCurr = lineCount - block.Count;
                int conCount = int.Parse(block[0]);
                int bullCount = block.Count - conCount - 1;

                string[] names = new string[conCount];
                int[,] bulletins = new int[block.Count - 1 - conCount, 20];

                for(int i = 0; i < conCount; i++)
                {
                    names[i] = block[i + 1];
                }

                for (int i = conCount + 1; i < block.Count; i++)
                {
                    var bulletin = block[i].Split();
                    for (int j = 0; j < conCount; j++)
                    {
                        bulletins[i - conCount - 1, j] = int.Parse(bulletin[j]);
                    }
                }

                return new VoteData(conCount, bullCount, names, bulletins);
            }
        }
    }
}