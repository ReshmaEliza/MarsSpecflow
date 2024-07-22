using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsSpecFlowProject.Utilities
{
     class WindowHandlers:WebdriverManager
    {
        public static void NewTab()
        {
            // Open a new tab using JavaScript
            ((IJavaScriptExecutor)driver).ExecuteScript("window.open();");

        }
        public static void ActiveSession(int SID)

        { // Get the updated list of window handles
            var tabs = driver.WindowHandles;

            // Switch to the new tab 
            driver.SwitchTo().Window(tabs[SID]);


        }
        public static void ScrollToView(IWebElement value)
        {
            //scroll to reqested view
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoView(true);", value);
            
        }
    }
}
