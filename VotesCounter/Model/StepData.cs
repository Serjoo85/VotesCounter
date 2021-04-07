using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Console;

namespace VotesCounter.Model
{
    public class StepData
    {
        private IList<VoteData> _VdList;
        public IList<VoteData> VdList => _VdList;

        public void AddVoteData(VoteData vd)
        {
            _VdList.Add(vd);
        }

        protected string ErrorMessage;

        public void PrintErr()
        {
            Console.WriteLine(ErrorMessage);
        }

        public virtual string GetName() => nameof(StepData);
        public virtual bool GetKey() => true;

    }

    public class CheckInputFail: StepData
    {

    }

    public class LoadFail: StepData
    {
        public LoadFail(string msg) => ErrorMessage = msg;

        public override string GetName() => nameof(LoadFail);
        public override bool GetKey() => false;

    }



    public class CreateCandidateListFail: StepData
    {
        public CreateCandidateListFail(string msg) => ErrorMessage = msg;

        public override string GetName() => nameof(CreateCandidateListFail);
        public override bool GetKey() => false;
    }

    public class CountFail: StepData
    {
        public CountFail(string msg) => ErrorMessage = msg;
        public override string GetName() => nameof(CountFail);
        public override bool GetKey() => false;
    }

    public class CreateVoteDataFail: StepData
    {
        public CreateVoteDataFail(string msg) => ErrorMessage = msg;

        public override string GetName() => nameof(CreateVoteDataFail);
        public override bool GetKey() => false;
    }

    
}
