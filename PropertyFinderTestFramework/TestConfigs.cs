using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyFinderTestFramework
{
    public class TestConfig
    {
        public string Driver { get; set; }
        public TestRun[] TestRuns { get; set; }
        public bool Headless { get; set; }
    }
}
