using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace PropertyFinderTestFramework
{

    public class MainPage
    {
        public static void NavigateTo(string url)
        {
            Driver.Instance.Navigate().GoToUrl(url);
        }
        public static  SearchPropertiesCommand SearchProperties()
        {
            return new SearchPropertiesCommand();
        }

        public static FindAgentsCommand FindAgents()
        {
            return new FindAgentsCommand();
        }
    }

   
}