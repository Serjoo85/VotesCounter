using System;
using VotesCounter.Model;
using VotesCounter.Services;

namespace VotesCounter
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                VoteData.Reset();
                LoadServiece.LoadData();
                var r = CountService.CountVote(VoteData.Items);
                foreach (var v in r)
                {
                    Console.WriteLine(v);
                }

                Console.ReadLine();
                Console.Clear();
            }
        }
    }
}
