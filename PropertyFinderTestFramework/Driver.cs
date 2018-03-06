using System;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Newtonsoft.Json;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Safari;

namespace PropertyFinderTestFramework
{
    public static class Driver
    {
        public static IWebDriver Instance { get; set; }

        public static TestConfig TestConfig { get; }

        static Driver()
        {
            using (var r = new StreamReader("TestConfig.json"))
            {
                var json = r.ReadToEnd();
                TestConfig = JsonConvert.DeserializeObject<TestConfig>(json);
            }           
        }

        public static void Initialize(string driver, bool headless)
        {
            switch (driver)
            {
                case "safari":
                {
                    Instance = new SafariDriver(); break;
                }
                case "ie":
                {
                    Instance = new InternetExplorerDriver();
                    break;
                }
                case "chrome":
                {
                    var options = new ChromeOptions();
                    if (headless)
                    {
                        options.AddArgument("--headless");
                    }
                    Instance = new ChromeDriver(options);
                    break;
                }
                case "firefox":
                    Instance = new FirefoxDriver(); break;
                default: Instance = new ChromeDriver(); break;
            }
            Instance.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        public static void Close()
        {
            Instance.Close();
        }
    }
}
