using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeleniumExtras.WaitHelpers;

namespace MarsSpecFlowProject.Utils
{
    class WaitUtils:CommonDriver
    {
      
        private static readonly By NotificationElementLocator = By.ClassName("ns-box-inner");
        static IWebElement NotificationElement;

        public  static string Notification()
            {

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            NotificationElement = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(NotificationElementLocator));
            String notification = NotificationElement.Text;
            return notification;

        }

        public static IWebElement WaitToBeClickable(string Choice, int seconds)
        {
            WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0, 0, seconds));

            
                By addLocator = By.XPath($"//div[@data-tab='{Choice}']//th[@class='right aligned']/div[contains(text(),'Add New')]");

                
                IWebElement AddNew = wait.Until(ExpectedConditions.ElementToBeClickable(addLocator));
                return AddNew;
            
            



        }

        public static IWebElement WaitElementIsVisible(string tab, int seconds)
        {
            WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0, 0, seconds));
            By Locator = By.XPath($"//h3[contains(text(),'{tab}')]");
            
            IWebElement value = wait.Until(ExpectedConditions.ElementToBeClickable(Locator));
            return value;

        }

    }
}
