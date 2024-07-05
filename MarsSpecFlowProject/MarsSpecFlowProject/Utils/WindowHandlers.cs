using OpenQA.Selenium;

namespace MarsSpecFlowProject.Utils
{
    class WindowHandlers
    {

        public static void NewTab(IWebDriver driver)
        {
            // Open a new tab using JavaScript
            ((IJavaScriptExecutor)driver).ExecuteScript("window.open();");

        }
        public static void ActiveSession(IWebDriver driver,int SID)

        { // Get the updated list of window handles
            var tabs = driver.WindowHandles;

            // Switch to the new tab (third tab)
            driver.SwitchTo().Window(tabs[SID]);

           
        }
        public static void ScrollToView(IWebDriver driver,IWebElement value)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoView(true);", value);
        }
    }
}
