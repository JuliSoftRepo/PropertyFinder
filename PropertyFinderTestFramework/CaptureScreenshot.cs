using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace PropertyFinderTestFramework
{
    public static class CaptureScreenshot
    {
        public static void Capture(IWebDriver driver, string screenShotName)
        {
            ITakesScreenshot ts = (ITakesScreenshot)driver;
            Screenshot screenshot = ts.GetScreenshot();
            screenshot.SaveAsFile(screenShotName);
        }

    }
}
