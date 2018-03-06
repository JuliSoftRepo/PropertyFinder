using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PropertyFinderTestFramework;

namespace Tests
{
    [TestClass]
    public class ScenarioA : TestBase
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
                    CaptureScreenshot.Capture(Driver.Instance, "ScenarioA_fail.png");
                    Assert.Fail(e.Message);
                }
            }
        }

        private void RunScenario(string url)
        {
            MainPage.NavigateTo(url);
            MainPage.SearchProperties().FilterBy(CategoryValues.Buy, TypeValues.Villa, "THE PEARL", BedValues.ThreeBeds, BedValues.SevenBeds);
            MainPage.SearchProperties().SortBy(SortByValues.PriceHigh);
            Thread.Sleep(2000);
            MainPage.SearchProperties().FetchAndSavePrices("ListingPrices.csv");
        }
    }
}
