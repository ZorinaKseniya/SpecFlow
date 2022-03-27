using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using TechTalk.SpecFlow.Assist.ValueRetrievers;

namespace SpecFlowProject.Hooks
{
    [Binding]
    public static class Hooks1
    {
        [BeforeScenario]
        public static void BeforeTestRun()
        {
            Service.Instance.ValueRetrievers.Register(new NullValueRetriever("<null>"));
        }
    }
}
