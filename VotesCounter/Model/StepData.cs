using System;
using System.Collections.Generic;

namespace VotesCounter.Model
{
    public class StepData
    {
        private IList<VoteData> _VdList;
        public IList<VoteData> VdList => _VdList;

        public StepData()
        {
            _VdList = new List<VoteData>();
        }

        public void AddVoteData(VoteData vd)
        {
            _VdList.Add(vd);
        }

        protected string ErrorMessage;

        public void PrintErr()
        {
            if (ErrorMessage != null)
            {
                Console.WriteLine(ErrorMessage + "\nНажмите любую клавишу...");
                Console.ReadKey();
                Console.Clear();
            }
        }

        public virtual string GetName() => nameof(StepData);
        public virtual bool GetKey() => true;

    }

    public class LoadFail : StepData
    {
        public LoadFail(string msg) => ErrorMessage = msg;

        public override string GetName() => nameof(LoadFail);
        public override bool GetKey() => false;
    }

    public class CreateCandidateListFail : StepData
    {
        public CreateCandidateListFail(string msg) => ErrorMessage = msg;

        public override string GetName() => nameof(CreateCandidateListFail);
        public override bool GetKey() => false;
    }

    public class CreateVoteDataFail : StepData
    {
        public CreateVoteDataFail(string msg) => ErrorMessage = msg;

        public override string GetName() => nameof(CreateVoteDataFail);
        public override bool GetKey() => false;
    }
}
