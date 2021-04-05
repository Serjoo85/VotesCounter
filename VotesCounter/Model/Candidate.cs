using System;

namespace VotesCounter.Model
{
    public class Candidate : IComparable
    {
        private readonly int[] _Votes;
        private readonly int _Index;
        private readonly string _Name;

        public Candidate(int[] votes, int index, string name)
        {
            _Votes = votes;
            _Index = index;
            _Name = name;
        }

        public int[] Votes => _Votes;

        public int Index => _Index;

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
                else if (i == this._Votes.Length & this._Votes[i] < comp._Votes[i])
                {
                    return 0;
                }
            }

            return 0;
        }
    }
}
