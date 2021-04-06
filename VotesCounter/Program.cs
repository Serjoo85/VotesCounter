using System;
using System.Linq;
using VotesCounter.Data;
using VotesCounter.Model;
using VotesCounter.Services;

namespace VotesCounter
{
    class Program
    {
        static void Main(string[] args)
        {

            var result = LoadService.LoadData("randomfile.txt");


            var z = CountService.CountVote(result.ToList());
            foreach (var t in z)
            {
                Console.WriteLine(t);
            }
            Console.ReadLine();
        }
    }
}
