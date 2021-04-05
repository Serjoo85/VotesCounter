using System;
using VotesCounter.Model;
using VotesCounter.Services;

namespace VotesCounter
{
    class Program
    {
        static void Main(string[] args)
        {
            var x = LoadServiece.LoadData();
            var r = CountService.CountVote(VoteData.Items);
            foreach (var v in r)
            {
                Console.WriteLine(v);
            }
            x.PrintAll();
            Console.ReadLine();
        }
    }
}
