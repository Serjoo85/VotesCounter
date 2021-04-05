using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VotesCounter.Model
{
    public class Candidate
    {
        private int[] _Votes;
        private int _Index;

        public Candidate(int[] votes, int index)
        {
            _Votes = votes;
            _Index = index;
        }

        public int[] Votes => _Votes;

        public int Index => _Index;
    }
}
