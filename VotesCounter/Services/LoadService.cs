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
            LoadFromFile(fileName);
        }

        private static List<VoteData> LoadTestData()
        {
            List<VoteData> items = new();
            items = TestData.GetTestVoteData(3, 20, 1000);
            return items;
        }

        private static async Task LoadFromFile(string fileName)
        {
            List<string> VoteDataString = new();
            try
            {
                using (StreamReader sr = new StreamReader(fileName, System.Text.Encoding.Default))
                {
                    string line;
                    while ((line = await sr.ReadLineAsync()) != null)
                    {
                        VoteDataString.Add(line);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Не удалось прочитать файл {fileName}, ошибка:");
                Console.WriteLine(e.Message);
            }
        }
    }
}
