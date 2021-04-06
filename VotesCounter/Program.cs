using System;
using VotesCounter.Data;
using VotesCounter.Model;
using VotesCounter.Services;

namespace VotesCounter
{
    class Program
    {
        static void Main(string[] args)
        {
            //var repository = new VoteDataRepository(LoadService.LoadData());
            //CreateRandomFileService.CreateFile(repository.GetItems());
            LoadService.LoadData("randomfile.txt");
            Console.ReadLine();
        }
    }
}
