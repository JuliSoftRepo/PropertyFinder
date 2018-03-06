using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PropertyFinderTestFramework;

namespace Tests
{
    [TestClass]
    public class ScenarioB : TestBase
    {
        [TestMethod]
        public void RunTest()
        {
            foreach (var run in Runs)
            {
                try
                {
                    RunScenario(run.Url);
                }
                catch (Exception e)
                {
                    CaptureScreenshot.Capture(Driver.Instance, "ScenarioB_fail.png");
                    Assert.Fail(e.Message);
                }
            }
        }

        private void RunScenario(string url)
        {
            MainPage.NavigateTo(url);
            MainPage.FindAgents().NavigateToFindAgents();
            MainPage.FindAgents().FilterByLanguage(LanguageValues.Hindi, LanguageValues.English, LanguageValues.Arabic);
            int agentCount = MainPage.FindAgents().GetAgentsCount();
            MainPage.FindAgents().FilterByNationality(NationalityValues.India);
            int latestAgentCount = MainPage.FindAgents().GetAgentsCount();
            Assert.IsTrue(latestAgentCount < agentCount, "The latest count is less than the previous count");
        }
    }
}
