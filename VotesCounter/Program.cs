using System;
using VotesCounter.Data;

namespace VotesCounter
{
    class Program
    {
        static void Main(string[] args)
        {
            TestData t = new TestData(1, 20, 1000);
            t.PrintAll();
            Console.ReadLine();
        }
    }
}
