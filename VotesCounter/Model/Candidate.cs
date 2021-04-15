using System;

namespace VotesCounter.Model
{

    public class Candidate : IComparable
    {
        private readonly int[] _Votes;
        private readonly int _Index;
        private readonly string _Name;

        /// <summary>
        /// Кандидат
        /// </summary>
        /// <param name="votes">Массив голосов</param>
        /// <param name="index">Индекс</param>
        /// <param name="name">Имя</param>
        public Candidate(int[] votes, int index, string name)
        {
            _Votes = votes;
            _Index = index;
            _Name = name;
        }

        public string Name => _Name;

        public int CompareTo(object obj)
        {
            var comp = (Candidate) obj;

            for (int i = 0; i < this._Votes.Length; i++)
            {
                if (this._Votes[i] > comp._Votes[i])
                {
                    return 1;
                }
                else if(this._Votes[i] < comp._Votes[i])
                {
                    return -1;
                }
                else if (i == this._Votes.Length - 1)
                {
                    return 0;
                }
            }
            return 0;
        }
    }
}
