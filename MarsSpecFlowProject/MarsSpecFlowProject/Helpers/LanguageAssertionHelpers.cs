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
using MarsSpecFlowProject.Utils;

namespace MarsSpecFlowProject.Helpers
{
    class LanguageAssertionHelpers
    {
        string pattern = @"^(?:$|(?=.*[a-zA-Z])[a-zA-Z\s-]+)$";
         IList<IWebElement> TableElements;
       static List<string> table_languages = new List<string>();

        public void AddDeleteLanguageAssert(IWebDriver driver, string Language)
        {
            
            
            String notification = WaitUtils.Notification(driver);
            driver.Navigate().Refresh();
            TableElements = GlobalVariables.TableElementsChoice(driver,"first");
            if (TableElements.Count < 5) //Checks if the number of language elements is less than 5
            {
                if (Regex.IsMatch(Language, pattern))//Checks the existance of invalid characters
                {
                    if (notification.Contains("has been added to your languages"))
                    {
                        LanguageAssertionHelpers.NotificationAdded(driver, notification, TableElements, Language);
                    }
                    else if (notification.Contains("deleted"))
                    {
                        Console.WriteLine($"Notification from system: '{notification}'");
                        LanguageAssertionHelpers.NotificationDeleted(notification, TableElements, Language);
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


        public  void UpdateAssertions(IWebDriver driver, string Language, string newlanguage)
        {
            
            String notification = WaitUtils.Notification(driver);
            IWebElement table1 = GlobalVariables.TableChoice(driver, "first");

            if (Regex.IsMatch(newlanguage, pattern))
            {
                if (notification.Contains("updated"))
                {
                    IList<IWebElement> TableElements = driver.FindElements(By.XPath("//div[@data-tab='first']//td[1]"));
            
                    WindowHandlers.ScrollToView(driver, table1);
                    LanguageAssertionHelpers.NotificationUpdate(driver, notification, TableElements, Language, newlanguage);
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


        public  void StringLengthAssertion(IWebDriver driver)
        {
            
            String notification = WaitUtils.Notification(driver);
            TableElements = GlobalVariables.TableElementsChoice(driver, "first");
            int count = TableElements.Count();
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

        public  static void NotificationAdded(IWebDriver driver, string notification, IList<IWebElement> TableElements, String Language)
        {
            Thread.Sleep(1000);
            int Expected_Count = TableElements.Count();
                       
            
                foreach (IWebElement tableElement in TableElements)
                {
                    table_languages.Add(tableElement.Text.ToLower());
                }


                int ActualCount = table_languages.Distinct().Count();
                
                if (Expected_Count == ActualCount)
                {
                
                 String AddedLanguage = GlobalVariables.Value(driver,"first",Language).Text;
                if (AddedLanguage.Equals(Language))
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
            

            // Iterate through each element and add its text to the list
            foreach (IWebElement element in TableElements)
            {
                table_languages.Add(element.Text);

            }
            Console.WriteLine("Languages Present Currently:");
            foreach (string language in table_languages)
            {
                Console.WriteLine(language);
                Assert.That(!language.Equals(Language), "Deletion Failed");

            }


        }

        public static void NotificationUpdate(IWebDriver driver, string notification, IList<IWebElement> TableElements, String Language, String newlanguage)
        {
            
            String UpdatedElement = GlobalVariables.Value(driver,"first", newlanguage).Text;
            int Expected_Count = TableElements.Count();
               foreach (IWebElement tableElement in TableElements)
                {
                    table_languages.Add(tableElement.Text.ToLower());
                }

                int ActualCount = table_languages.Distinct().Count();

                if (Expected_Count == ActualCount)//Checks for duplicate values
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
