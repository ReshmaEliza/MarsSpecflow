using MarsSpecFlowProject.Page;
using MarsSpecFlowProject.Utils;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace MarsSpecFlowProject.Helpers
{
    class Assertions:BasePage
    {
       
        private IList<IWebElement> TableElements;
        private static List<string> table_Values = new List<string>();
        private String notification;
       
        private IWebElement table;
        

        public Assertions() : base() { }

      
        public void AddDeleteLanguageAssert(string Language)
        {


             notification = WaitUtils.Notification();
            driver.Navigate().Refresh();
            TableElements = GlobalVariables.TableElementsChoice(choice);
            if (TableElements.Count < 5) //Checks if the number of language elements is less than 5
            {
                if (Regex.IsMatch(Language, pattern))//Checks the existance of invalid characters
                {
                    if (notification.Contains("has been added to your languages"))
                    {
                        NotificationAddedAssert(notification, TableElements, Language);
                    }
                    else if (notification.Contains("deleted"))
                    {
                        Console.WriteLine($"Notification from system: '{notification}'");
                        NotificationDeleted(notification, TableElements, Language);
                    }
                    else if (notification.Contains("already added"))
                        Console.Write($"Addition/Updation of language - {Language} has not been done due to '{notification}'\n");
                    else if (notification.Contains("Duplicated"))
                        Console.Write($"Addition - {Language} has not been done due to '{notification}'\n");
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

        
        public void AddDeleteSkillAssert(string Skill)
        {

            notification = WaitUtils.Notification();
            //IWebElement table1 = driver.FindElement(table1Locator);
            TableElements = GlobalVariables.TableElementsChoice(choice);
            if (Regex.IsMatch(Skill, pattern))//Checks the existance of invalid characters
            {
                if (notification.Contains("has been added to your skills"))
                {
                    NotificationAddedAssert(notification, TableElements, Skill);
                }
                else if (notification.Contains("deleted"))
                {
                    Console.WriteLine($"Notification from system: {notification}");
                    NotificationDeleted(notification, TableElements, Skill);
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
                if (notification.Contains("invalid characters"))
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

        public  void NotificationAddedAssert(string notification, IList<IWebElement> TableElements, String value)
        {
            Thread.Sleep(1000);
            int Expected_Count = TableElements.Count();
            foreach (IWebElement tableElement in TableElements)
            {
                table_Values.Add(tableElement.Text.ToLower());
            }
            int ActualCount = table_Values.Distinct().Count();

            if (Expected_Count == ActualCount)
            {
                String AddedValue = GlobalVariables.Value(choice,value).Text;
                if (AddedValue.Equals(value))
                {
                    Console.WriteLine($"TEST PASSED- Notification - {notification}");
                }
                else
                    Assert.Fail($"Element {value} is not added");
            }
            else
                Assert.Fail($"The system allowed the addition of duplicate entires Notification - {notification}");
        }

        public  void NotificationDeleted(string notification, IList<IWebElement> TableElements, String value)
        {


            // Iterate through each element and add its text to the list
            foreach (IWebElement element in TableElements)
            {
                table_Values.Add(element.Text);

            }
            Console.WriteLine("Skills Present Currently:");
            foreach (string Value in table_Values)
            {
                Console.WriteLine(Value);
                Assert.That(!Value.Equals(value), "Deletion Failed");

            }


        }

        public void UpdateAssertionsLanguage(string Language, string newlanguage)
        {

            notification = WaitUtils.Notification();
             table = GlobalVariables.TableChoice(choice);

            if (Regex.IsMatch(newlanguage, pattern))
            {
                if (notification.Contains("updated"))
                {
                    //IList<IWebElement> TableElements = driver.FindElements(By.XPath("//div[@data-tab='first']//td[1]"));
                    TableElements = GlobalVariables.TableElementsChoice(choice);

                    WindowHandlers.ScrollToView(table);
                    NotificationUpdate(notification, TableElements, Language, newlanguage);
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

        public void UpdateAssertionsSkill(string Skill, string newSkill)
        {

            notification = WaitUtils.Notification();
            table = GlobalVariables.TableChoice(choice);


            if (Regex.IsMatch(newSkill, pattern))
            {
                if (notification.Contains("updated"))
                {

                    TableElements = GlobalVariables.TableElementsChoice(choice);
                    WindowHandlers.ScrollToView(table);
                   NotificationUpdate(notification, TableElements, Skill, newSkill);

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


        public void StringLengthAssertion()
        {

            notification = WaitUtils.Notification();
            TableElements = GlobalVariables.TableElementsChoice(choice);
            int count = TableElements.Count();
            Console.WriteLine($"Notification from Sysem:{notification}");

            foreach (IWebElement element in TableElements)
            {
                table_Values.Add(element.Text);

            }

            foreach (string value in table_Values)
            {
                int l = value.Length;
                if (l > 50)
                {
                    Assert.Fail("System Allowed the addition of Characters > 50");
                }

            }
        }



        public void Stability(int OGElementCount)
        {
            TableElements = GlobalVariables.TableElementsChoice(choice);
            int ActualCount = TableElements.Count();

            if (ActualCount == OGElementCount)
            {
                Console.WriteLine("All Elements are added");

            }
            else
            {

                Assert.Fail($"Expected Count of Elements " + OGElementCount + " .Actual Count " + ActualCount + "");
            }
        }

        public  void NotificationUpdate(string notification, IList<IWebElement> TableElements, String Value, String newValue)
        {

            String UpdatedElement = GlobalVariables.Value(choice, newValue).Text;
            int Expected_Count = TableElements.Count();

            foreach (IWebElement tableElement in TableElements)
            {
                table_Values.Add(tableElement.Text.ToLower());
            }

            int ActualCount = table_Values.Distinct().Count();

            if (Expected_Count == ActualCount)
            {
                if (UpdatedElement.Equals(newValue))
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

