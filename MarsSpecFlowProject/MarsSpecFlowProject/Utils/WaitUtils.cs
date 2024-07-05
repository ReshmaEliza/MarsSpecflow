using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsSpecFlowProject.Utils
{
    class WaitUtils
    {
      
        private static readonly By NotificationElementLocator = By.ClassName("ns-box-inner");
        static IWebElement NotificationElement;

        public  static string Notification(IWebDriver driver)
            {

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            NotificationElement = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(NotificationElementLocator));
            String notification = NotificationElement.Text;
            return notification;

        }


       

    }
}
