using NUnit.Framework;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using static MarsSpecFlowProject.StepDefinitions.ThisTestSuiteContainsTestScenariosForLanguageTab_StepDefinitions;
using System.Diagnostics;
using TechTalk.SpecFlow;
using System.Runtime.Intrinsics.X86;

namespace MarsSpecFlowProject.Helpers
{
    class AssertionHelpers
    {

        public static void AddDeleteLanguageAssert(IWebDriver driver, string Language)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            IWebElement notificationElement = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.ClassName("ns-box-inner")));
            String notification = notificationElement.Text;
            IWebElement table1 = driver.FindElement(By.XPath("//div[@data-tab='first']"));
            string pattern = @"^(?:$|(?=.*[a-zA-Z])[a-zA-Z\s-]+)$";
            driver.Navigate().Refresh();
            IList<IWebElement> TableElements = driver.FindElements(By.XPath("//div[@data-tab='first']//td[1]"));


            if (TableElements.Count < 5) //Checks if the number of language elements is less than 5
            {
                if (Regex.IsMatch(Language, pattern))//Checks the existance of invalid characters
                {
                    if (notification.Contains("has been added to your languages"))
                    {
                        AssertionHelpers.NotificationAdded(driver, notification, TableElements, Language);
                    }
                    else if (notification.Contains("deleted"))
                    {
                        Console.WriteLine($"Notification from system: '{notification}'");
                        AssertionHelpers.NotificationDeleted(notification, TableElements, Language);
                    }
                    else if (notification.Contains("already added"))
                        Console.Write($"Addition/Updation of language - {Language} has not been done due to '{notification}'\n");
                    else if (notification.Contains("Duplicated"))
                        Console.Write($"Addition/Updation - {Language} has not been done due to '{notification}'\n");
                    else if (notification.Contains("Please enter language and level"))
                        Console.Write($"Updation of language '{Language}' has not been done. Notification from system - '{notification}'\n");
                    else
                        Assert.Fail($"Failed Action due to :{notification}");
                }
                else
                {
                    if (notification.Contains("invalid characters"))
                    {
                        Console.Write($"Addition of '{Language}' has not been done due to {notification}\n");
                    }
                    else
                        Assert.Fail($"System Allowed addition of invalid characters! Notification from System :{notification}");
                }
            }
            else
            {
                Assert.Fail($"System allowed the addition of more than 4 languages. Number of languages in the system :{TableElements.Count}");
            }
        }


        public static void UpdateAssertions(IWebDriver driver, string Language, string newlanguage)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            IWebElement notificationElement = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.ClassName("ns-box-inner")));
            String notification = notificationElement.Text;
            IWebElement table1 = driver.FindElement(By.XPath("//div[@data-tab='first']"));
            string pattern = @"^(?:$|(?=.*[a-zA-Z])[a-zA-Z\s-]+)$";
             if (Regex.IsMatch(newlanguage, pattern))
            {
                if (notification.Contains("updated"))
                {
                    IList<IWebElement> TableElements = driver.FindElements(By.XPath("//div[@data-tab='first']//td[1]"));
                    js.ExecuteScript("arguments[0].scrollIntoView(true);", table1);
                    AssertionHelpers.NotificationUpdate(driver, notification, TableElements, Language, newlanguage);
                }
                else if (notification.Contains("already added"))
                    Console.Write($"Updation of language '{Language}' has not been done. Notification from system-{notification}\n");
                else if (notification.Contains("Duplicated"))
                    Console.Write($"Updation of language '{Language}' has not been done. Notification from system-{notification}\n");
                else if (notification.Contains("Please enter language and level"))
                    Console.Write($"Updation of language '{Language}' has not been done. Notification from system-{notification}\n");
                else if (notification.Contains("Please enter language and level"))
                    Console.Write($"Updation of language '{Language}' has not been done. Notification from system-{notification}\n");
                else

                    Assert.Fail($"Failed Action due to :{notification}");


            }




            else

            {
                if (notification.Contains("invalid characters"))
                {
                    Console.Write($"Addition of '{Language}' has not been done due to {notification}\n");
                }
                else
                    Assert.Fail($"System Allowed addition of invalid characters! Notification from System :{notification}");
            }


        }


        public static void StringLengthAssertion(IWebDriver driver)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            IWebElement notificationElement = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.ClassName("ns-box-inner")));
            String notification = notificationElement.Text;
            IList<IWebElement> TableElements = driver.FindElements(By.XPath("//div[@data-tab='first']//td[1]"));
            int count = TableElements.Count();
            List<string> table_languages = new List<string>();
            Console.WriteLine($"Notification from Sysem:{notification}");

            foreach (IWebElement element in TableElements)
            {
                table_languages.Add(element.Text);

            }

            foreach (string language in table_languages)
            {
                int l = language.Length;
                if (l > 50)
                {
                    Assert.Fail("System Allowed the addition of Characters > 50");
                }



            }
        }

        public static void NotificationAdded(IWebDriver driver, string notification, IList<IWebElement> TableElements, String Language)
        {
            Thread.Sleep(1000);
            int Expected_Count = TableElements.Count();
            List<string> table_languages = new List<string>();
            
            
                foreach (IWebElement tableElement in TableElements)
                {
                    table_languages.Add(tableElement.Text.ToLower());
                }


                int ActualCount = table_languages.Distinct().Count();

                if (Expected_Count == ActualCount)
                {
                String AddedLanguge = driver.FindElement(By.XPath($"//td[contains(text(),'{Language}')]")).Text;
                if (AddedLanguge.Equals(Language))
                {
                    Console.WriteLine($"TEST PASSED- Notification - {notification}");
                }
                else
                    Assert.Fail($"Element {Language} is not added");
                }
                else
                    Assert.Fail($"The system allowed the addition of duplicate entiresNotification - {notification}");
            
            

        }


        public static void NotificationDeleted(string notification, IList<IWebElement> TableElements, String Language)
        {
            List<string> languages = new List<string>();

            // Iterate through each element and add its text to the list
            foreach (IWebElement element in TableElements)
            {
                languages.Add(element.Text);

            }
            Console.WriteLine("Languages Present Currently:");
            foreach (string language in languages)
            {
                Console.WriteLine(language);
                Assert.That(!language.Equals(Language), "Deletion Failed");

            }


        }

        public static void NotificationUpdate(IWebDriver driver, string notification, IList<IWebElement> TableElements, String Language, String newlanguage)
        {
            String UpdatedElement = driver.FindElement(By.XPath($"//td[contains(text(),'{newlanguage}')]")).Text;
            int Expected_Count = TableElements.Count();
            List<string> table_languages = new List<string>();
            
            
                foreach (IWebElement tableElement in TableElements)
                {
                    table_languages.Add(tableElement.Text.ToLower());
                }

                int ActualCount = table_languages.Distinct().Count();

                if (Expected_Count == ActualCount)
                {
                    if (UpdatedElement.Equals(newlanguage))
                    {
                        Console.WriteLine($"TEST PASSED- Notification - {notification}");
                    }
                    else
                    {
                        Assert.Fail("Element not added in the table");
                    }
                }
                else
                    Assert.Fail($"The system allowed the addition of duplicate entires.Notification from system - {notification}");
            }
        }
    
    

}
