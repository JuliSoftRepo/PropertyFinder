using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PropertyFinderTestFramework;

namespace Tests
{
    public abstract class TestBase
    {
        public List<TestRun> Runs;
       
        [TestInitialize]
        public virtual void Init()
        {
            Runs = Driver.TestConfig.TestRuns.Where(x => x.Scenario == this.GetType().Name).ToList();
            Driver.Initialize(Driver.TestConfig.Driver, Driver.TestConfig.Headless);
        }

        [TestCleanup]
        public void Close()
        {
            Driver.Close();
        }
    }
}
