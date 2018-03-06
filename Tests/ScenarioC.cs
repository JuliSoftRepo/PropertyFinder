using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PropertyFinderTestFramework;

namespace Tests
{
    [TestClass]
    public class ScenarioC : TestBase
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
                    CaptureScreenshot.Capture(Driver.Instance, "ScenarioC_fail.png");
                    Assert.Fail(e.Message);
                }
            }
        }

        private void RunScenario(string url)
        {
            MainPage.NavigateTo(url);
            MainPage.FindAgents().NavigateToFindAgents();
            MainPage.FindAgents().SelectAgent("AgentDetails.txt");
            CaptureScreenshot.Capture(Driver.Instance, "ScenarioC_pass.png");
            MainPage.FindAgents().ChangeLanguage();
            CaptureScreenshot.Capture(Driver.Instance, "ScenarioC_pass_after_language_changed.png");
        }
    }
}
