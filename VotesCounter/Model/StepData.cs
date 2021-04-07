using System;
using System.Collections.Generic;

namespace VotesCounter.Model
{
    public class StepData
    {
        private IList<VoteData> _VdList;
        public IList<VoteData> VdList => _VdList;

        private List<string> _Winners;
        public List<string> Winners => _Winners;

        private string _FileName;
        public string FileName
        {
            get => _FileName;
            set => _FileName = value;
        }

        public StepData(string fileName)
        {
            _VdList = new List<VoteData>();
            _Winners = new List<string>();
            _FileName = fileName;
            Console.Clear();
        }

        protected StepData()
        {

        }

        public void AddVoteData(VoteData vd)
        {
            _VdList.Add(vd);
        }

        protected string Message;

        public void PrintMsg()
        {
            if (Message != null)
            {
                Console.WriteLine(Message);

            }
            else
            {
                foreach (var line in _Winners) Console.WriteLine(line);
            }
            Console.WriteLine("\nНажмите любую клавишу...");
            Console.ReadKey();
            Console.Clear();
        }
        public virtual bool GetKey() => true;
    }

    public class LoadFail : StepData
    {
        public LoadFail(string msg) => Message = msg;
        public override bool GetKey() => false;
    }

    public class CreateCandidateListFail : StepData
    {
        public CreateCandidateListFail(string msg) => Message = msg;
        public override bool GetKey() => false;
    }

    public class CreateVoteDataFail : StepData
    {
        public CreateVoteDataFail(string msg) => Message = msg;

        public override bool GetKey() => false;
    }

    public class CheckInputFail : StepData
    {
        public CheckInputFail(string msg) => Message = msg;

        public override bool GetKey() => false;
    }
}
