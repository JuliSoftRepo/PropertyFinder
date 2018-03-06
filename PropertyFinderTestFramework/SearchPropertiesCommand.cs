using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace PropertyFinderTestFramework
{
    public class SearchPropertiesCommand
    {
        private const string CategoryName = "c";
        private const string TypeName = "t";
        private const string MinBedsName = "bf";
        private const string MaxBedsName = "bt";

        public void FilterBy(string searchcategory, string searchType, string location, string minBed, string maxBed)
        {
            SelectValueFromDropdown(CategoryName, searchcategory);
            SelectValueFromDropdown(TypeName, searchType);
            SearchTypeAhead(location);
            SelectValueFromDropdown(MinBedsName, minBed);
            SelectValueFromDropdown(MaxBedsName, maxBed);
            Find();
        }
        public void Find()
        {
            Driver.Instance.FindElement(By.CssSelector("#search-form-property .search [type='submit']")).Click();
        }

        public void SortBy(string text)
        {
            var sortBy = Driver.Instance.FindElement(By.CssSelector("#content [name ='search-order-by']+ div"));
            sortBy.Click();
            IWebElement value = sortBy.FindElements(By.CssSelector("li")).FirstOrDefault(x => x.Text.Contains(text));
            if (value == null)
                throw new Exception("Not Found");
            value.Click();
            Thread.Sleep(500);
        }

        public void FetchAndSavePrices(string fileName)
        {
            IReadOnlyCollection<IWebElement> searchResult = Driver.Instance.FindElements(By.CssSelector("#serp ul li[data-property-id]"));

            var result = new List<PropertyDetails>();
            foreach (var propertyPrice in searchResult)
            {
                var listingTitle = propertyPrice.FindElement(By.CssSelector("a[title]")).GetAttribute("title");
                var price = propertyPrice.FindElement(By.CssSelector("span.price")).GetAttribute("data-val");
                result.Add(new PropertyDetails { ListingTitle = listingTitle, Price = price });
            }
            FIleOutput.SaveToFile(result, fileName);
        }

        private static void SelectValueFromDropdown(string dropDownSelector, string selectorValue)
        {
            var dropdown = "#search-form-property [name='" + dropDownSelector + "'] + div";
            IWebElement select = Driver.Instance.FindElement(By.CssSelector(dropdown));
            select.Click();
            var li = string.Format("[data-value='{0}']", selectorValue);
            IWebElement value = select.FindElement(By.CssSelector(li));
            value.Click();
            Thread.Sleep(500);
        }

        private static void SearchTypeAhead(string searchWord)
        {
            Driver.Instance.FindElement(By.CssSelector("#search-form-property .typeahead")).SendKeys(searchWord);
            var dropdown = ".tt-dataset .tt-suggestion";
            IWebElement select = Driver.Instance.FindElement(By.CssSelector(dropdown));
            select.Click();
            Thread.Sleep(500);
        }
    }
}
