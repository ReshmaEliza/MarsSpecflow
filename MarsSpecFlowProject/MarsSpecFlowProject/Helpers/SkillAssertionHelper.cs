using NUnit.Framework;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MarsSpecFlowProject.Utils;
using static MarsSpecFlowProject.StepDefinitions.ThisTestSuiteContainsTestScenariosForLanguageTab_StepDefinitions;
using TechTalk.SpecFlow;

namespace MarsSpecFlowProject.Helpers
{
    class SkillAssertionHelper
    {
        string pattern = @"^(?:$|(?=.*[a-zA-Z0-9])[a-zA-Z0-9\s-]+)$";
        IList<IWebElement> TableElements;
        IWebElement table2;
        static List<string> table_Skill = new List<string>();
        private readonly By table1Locator = By.XPath("//div[@data-tab='second']");
        String notification;
        IWebElement table1;
        public void AddDeleteSkillAssert(IWebDriver driver, string Skill)
        {
            
             notification = WaitUtils.Notification(driver);
            IWebElement table1 = driver.FindElement(table1Locator);
             TableElements = GlobalVariables.TableElementsChoice(driver,"second");
            if (Regex.IsMatch(Skill, pattern))//Checks the existance of invalid characters
            {
                if (notification.Contains("has been added to your skills"))
                {
                    SkillAssertionHelper.NotificationAdded(driver, notification, TableElements, Skill);
                }
                else if (notification.Contains("deleted"))
                {
                    Console.WriteLine($"Notification from system: {notification}");
                    SkillAssertionHelper.NotificationDeleted(notification, TableElements, Skill);
                }
                else if (notification.Contains("already exist"))
                    Console.Write($"Addition of '{Skill}' has not been done due to {notification}\n");
                else if (notification.Contains("Duplicated"))
                    Console.Write($"Addition of '{Skill}' has not been done due to {notification}\n");
                else if (notification.Contains("Please enter skill and experience level"))
                Console.Write($"Addition of '{Skill}' has not been done due to {notification}\n");
                else
                    Assert.Fail($"Failed Action due to :{notification}");
            }
            else
            {
                if(notification.Contains("invalid characters"))
                {
                    Console.Write($"Addition of '{Skill}' has not been done due to {notification}\n");
                }
               else if (notification.Contains("Please enter skill and experience level"))
                {
                    Console.Write($"Addition of '{Skill}' has not been done due to {notification}\n");
                }

                else
                Assert.Fail($"System Allowed addition of invalid characters! Notification from System :{notification}");
            }
        }

        public static void NotificationAdded(IWebDriver driver, string notification, IList<IWebElement> TableElements, String Skill)
        {
            Thread.Sleep(1000);
            int Expected_Count = TableElements.Count();
             foreach (IWebElement tableElement in TableElements)
            {
                table_Skill.Add(tableElement.Text.ToLower());
            }
            int ActualCount = table_Skill.Distinct().Count();

            if (Expected_Count == ActualCount)
            {
                 String AddedSkill = GlobalVariables.Value(driver, "second", Skill).Text;
                if (AddedSkill.Equals(Skill))
                {
                    Console.WriteLine($"TEST PASSED- Notification - {notification}");
                }
                else
                    Assert.Fail($"Element {Skill} is not added");
            }
            else
                Assert.Fail($"The system allowed the addition of duplicate entires Notification - {notification}");
        }

        public static void NotificationDeleted(string notification, IList<IWebElement> TableElements, String Skill)
        {
            
            
            // Iterate through each element and add its text to the list
            foreach (IWebElement element in TableElements)
            {
                table_Skill.Add(element.Text);

            }
            Console.WriteLine("Skills Present Currently:");
            foreach (string skill in table_Skill)
            {
                Console.WriteLine(skill);
                Assert.That(!skill.Equals(Skill), "Deletion Failed");

            }


        }

        public  void UpdateAssertions(IWebDriver driver, string Skill, string newSkill)
        {
            
             notification = WaitUtils.Notification(driver);
              table2 = GlobalVariables.TableChoice(driver,"second");


            if (Regex.IsMatch(newSkill, pattern))
            {
                if (notification.Contains("updated"))
                {
                    
                    TableElements = GlobalVariables.TableElementsChoice(driver,"second");
                     WindowHandlers.ScrollToView(driver, table2);
                    SkillAssertionHelper.NotificationUpdate(driver, notification, TableElements, Skill, newSkill);

                }
                else if (notification.Contains("already added"))
                    Console.Write($"Updation of skill '{Skill}' has not been done. Notification from system-{notification}\n");
                else if (notification.Contains("Duplicated"))
                    Console.Write($"Updation of skill '{Skill}' has not been done. Notification from system-{notification}\n");
                else if (notification.Contains("Please enter skill and experience level"))
                    Console.Write($"Addition of '{Skill}' has not been done due to {notification}\n");
                else

                    Assert.Fail($"Failed Action due to :{notification}");


            }
             else
            {
                if (notification.Contains("invalid characters"))
                {
                    Console.Write($"Addition of '{Skill}' has not been done due to {notification}\n");
                }
                else
                    Assert.Fail($"System Allowed addition of invalid characters! Notification from System :{notification}");

            }
        }

        public static void NotificationUpdate(IWebDriver driver, string notification, IList<IWebElement> TableElements, String Skill, String newSkill)
        {
            
            String UpdatedElement = GlobalVariables.Value(driver, "second", newSkill).Text;
            int Expected_Count = TableElements.Count();
            
            foreach (IWebElement tableElement in TableElements)
            {
                table_Skill.Add(tableElement.Text.ToLower());
            }

            int ActualCount = table_Skill.Distinct().Count();

            if (Expected_Count == ActualCount)
            {
                if (UpdatedElement.Equals(newSkill))
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


       public  void StringLengthAssertion(IWebDriver driver)
        {
            
             notification = WaitUtils.Notification(driver);
            
            TableElements = GlobalVariables.TableElementsChoice(driver,"second");
             Console.WriteLine($"Notification from Sysem:{notification}");

            foreach (IWebElement element in TableElements)
            {
                table_Skill.Add(element.Text);

            }

            foreach (string Entity in table_Skill)
            {
                int l = Entity.Length;
                Console.WriteLine($"length - {l}");
                if (l > 50)
                {
                    Assert.Fail("System Allowed the addition of Characters > 50");
                }



            }
        }

        public  void Stability(IWebDriver driver,int OGElementCount)
        {
            TableElements = GlobalVariables.TableElementsChoice(driver,"second");
            int ActualCount = TableElements.Count();

            if(ActualCount == OGElementCount) {
                Console.WriteLine("All Elements are added");

            }
            else
            {

                Assert.Fail($"Expected Count of Elements " + OGElementCount + " .Actual Count "+ ActualCount + "");
            }
           


            }
        }

    
}
