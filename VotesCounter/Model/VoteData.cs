using System.Collections.Generic;

namespace VotesCounter.Model
{
    public class VoteData
    {
        private static List<VoteData> _items = new ();
        public static List<VoteData> Items => _items;

        private int _ConCount;
        private int _BullCount;
        private string[] _Names;
        private int[,] _Bulletins;

        public int ConCount => _ConCount;

        public int BullCount => _BullCount;

        public string[] Names => _Names;

        public int[,] Bulletins => _Bulletins;

        public VoteData(int conCount, int bullCount, string[] names, int[,] bulletins)
        {
            _ConCount = conCount;
            _BullCount = bullCount;
            _Names = names;
            _Bulletins = bulletins;
        }
    }
}
