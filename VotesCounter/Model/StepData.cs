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
        private List<VoteData> vdList;
        private string ErrorMessage;
        public bool flag;

        public StepData()
        {
            vdList = new List<VoteData>();
        }

    }

    public class CheckInputFail: StepData
    {

    }

    public class LoadFail: StepData
    {

    }

    public class CountFail: StepData
    {

    }
}
