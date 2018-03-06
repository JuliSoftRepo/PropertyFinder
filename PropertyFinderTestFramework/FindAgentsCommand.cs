using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace PropertyFinderTestFramework
{
    public class FindAgentsCommand
    {
            private const string LanguageXpath = "//div[@class=\'searchform_column\'][2]";
            private const string Xpath = "//div[@class=\'searchform_column searchform_column-serp\'][3]";

        public void NavigateToFindAgents()
        {
            Driver.Instance.FindElement(By.XPath("//a[@href=\'/en/find-agent\']")).Click();
        }

        public void ChangeLanguage()
        {
            Driver.Instance.FindElement(By.CssSelector(".globalswitch_language a")).Click();
        }

        public void SelectAgent(string fileName)
        {
            IReadOnlyCollection<IWebElement> agentDetails = Driver.Instance.FindElements(By.CssSelector(".tiles a[class]"));
            agentDetails.ElementAt(0).Click();
      
                var name = Driver.Instance.FindElement(By.CssSelector(".bioinfo_personal h1")).Text;
                var nationality = Driver.Instance.FindElement(By.XPath("(//span[@class=\'table_column\'])[1]")).Text;
                var languages = Driver.Instance.FindElement(By.XPath("(//span[@class=\'table_column\'])[2]")).Text;
                var licenseNo = Driver.Instance.FindElement(By.XPath("(//span[@class=\'table_column\'])[5]")).Text;
                Driver.Instance.FindElement(By.CssSelector(".tab_bar [data-qs-tab]")).Click();
                var aboutMe = Driver.Instance.FindElement(By.XPath("//div[@data-qs-content=\'tab-about\']")).Text;
                Driver.Instance.FindElement(By.CssSelector(".pane_content [data-qs] span")).Click();
                var phoneNumber = Driver.Instance.FindElement(By.XPath("//span[contains(@class,\"button_text-value\")]")).Text;
                var companyName = Driver.Instance.FindElement(By.XPath("//div[@class=\'brokerthumbnail_text\']/p[1]")).Text;
                var experience = Driver.Instance.FindElement(By.XPath("(//span[@class=\'table_column\'])[6]")).Text;
                var activeListings = Driver.Instance.FindElement(By.XPath("(//span[@class=\'table_column\'])[4]/a")).Text;
                var URL = Driver.Instance.FindElement(By.XPath("(//a[@target=\'_blank\'])[1]")).GetAttribute("href");
                var result = new AgentDetails
                {
                    Name=name, Nationality = nationality, Languages = languages, LicenseNo = licenseNo,
                    AboutMe = aboutMe, PhoneNumber = phoneNumber, CompanyName = companyName,
                    Experience = experience, NoOfActiveListings = activeListings, LinkedInURL = URL
                };
            
            FIleOutput.SaveToFile(new []{ result }, fileName);
        }

        public void FilterByLanguage(params string[] languageList)
        {           
            foreach (var language in languageList)
            {
                SelectValueFromDropdown(LanguageXpath, language);
            }
            Search();
        }

        public void FilterByNationality(string nationalityList)
        {
           SelectValueFromDropdown(Xpath, nationalityList);
        }

        public int GetAgentsCount()
        {
            string text = Driver.Instance.FindElement(By.XPath("//h1[@class=\'title\']")).Text;
            var splittedText = text.Split(' ')[0];
            return int.Parse(splittedText);
        }

        public void Search()
        {
            Driver.Instance.FindElement(By.CssSelector(".controlgroup_control  button")).Click();
        }

        private static void SelectValueFromDropdown(string dropDownSelector, string text)
        {
            IWebElement select = Driver.Instance.FindElement(By.XPath(dropDownSelector));
            select.Click();
            IWebElement value = select.FindElements(By.CssSelector(".dropdown_item")).FirstOrDefault(x => x.Text.Contains(text));
            value.Click();
            Thread.Sleep(500);
        }
    }
}
