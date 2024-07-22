using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using MarsSpecFlowProject.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace MarsSpecFlowProject.Utilities
{
    public  class AssertionUtils: WebdriverManager
    {
        
        protected string pattern1 = @"^(?:$|(?=.*[a-zA-Z])[a-zA-Z\s-]+)$";
        protected string pattern2 = @"^(?:$|(?=.*[a-zA-Z0-9])[a-zA-Z0-9\s-#]+)$";
              
        String UpdatedElement;

        protected static List<string> table_Values = new List<string>();
        protected String notification;
        private static By ValueLocator1(string value) => (By.XPath($"//div[@data-tab='first']//td[contains(text(),'{value}')]"));
        String AddedValue1;

        private static By TableElementsLanguage_Locator => By.XPath($"//div[@data-tab='first']//td[1]");
        private static IList<IWebElement> TableElementsLanguage;

        private static By TableElementsSkill_Locator => By.XPath($"//div[@data-tab='second']//td[1]");
        private static IList<IWebElement> TableElementsSkill;

        private static By AddedLanguageLocator(string value) => (By.XPath($"//div[@data-tab='first']//td[contains(text(),'{value}')]"));
        String AddedLanguage;

        private static By AddedSkillLocator(string value) => (By.XPath($"//div[@data-tab='second']//td[contains(text(),'{value}')]"));
        String AddedSkill;
        SkillProfile skillProfile = new SkillProfile();

        private static readonly By NotificationElementLocator = By.ClassName("ns-box-inner");
        static IWebElement NotificationElement;
        private static By TableLanguageLocator => By.XPath($"//div[@data-tab='first']");
        private static IWebElement TableLanguage;

        private static By TableSkillLocator => By.XPath($"//div[@data-tab='first']");
        private static IWebElement TableSkill;
        


        public void AddDeleteLanguageAssert(string Language)
        {

            //Gets the notification message
             NotificationElement = driver.FindElement(NotificationElementLocator);
              WaitUtils.WaitElementIsVisible(NotificationElementLocator,5);
            notification = NotificationElement.Text;
            driver.Navigate().Refresh();
            TableElementsLanguage = driver.FindElements(TableElementsLanguage_Locator);
         
            if (TableElementsLanguage.Count < 5) //Checks if the number of language elements is less than 5
            {
                if (Regex.IsMatch(Language, pattern1))//Checks the existance of invalid characters
                { //The following statements analyses the value of the notification and determines the test results
                    if (notification.Contains("has been added to your languages"))
                    {
                        NotificationLanguageAddedAssert(notification, TableElementsLanguage, Language);
                    }
                    else if (notification.Contains("deleted"))
                    {
                        Console.WriteLine($"Notification from system: '{notification}'");
                        NotificationLanguageDeleted(notification, TableElementsLanguage, Language);
                    }
                    else if (notification.Contains("already added"))
                    {
                        
                        Console.Write($"Addition of language - {Language} has not been done due to '{notification}'\n");
                    }
                    else if (notification.Contains("Duplicated"))
                    {
                        
                        Console.Write($"Addition - {Language} has not been done due to '{notification}'\n");
                    }
                    else if (notification.Contains("Please enter language and level"))
                    {
                        
                        Console.Write($"Addition of language '{Language}' has not been done. Notification from system - '{notification}'\n");
                    }
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
                Assert.Fail($"System allowed the addition of more than 4 languages. Number of languages in the system :{TableElementsLanguage.Count}");
            }
        }


        public void NotificationLanguageAddedAssert(string notification, IList<IWebElement> TableElements, String value)
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
                AddedLanguage = driver.FindElement(AddedLanguageLocator(value)).Text;
                if (AddedLanguage.Equals(value))
                {
                    Console.WriteLine($"TEST PASSED- Notification - {notification}");
                }
                else
                    Assert.Fail($"Element {value} is not added");
            }
            else
                Assert.Fail($"The system allowed the addition of duplicate entires Notification - {notification}");
        }
        //This method checks if the element has been deleted
        public void NotificationLanguageDeleted(string notification, IList<IWebElement> TableElements, String value)
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
            //gets the notification
            NotificationElement = driver.FindElement(NotificationElementLocator);
            WaitUtils.WaitElementIsVisible(NotificationElementLocator, 5);
            notification = NotificationElement.Text;

            driver.Navigate().Refresh();
            TableLanguage = driver.FindElement(TableLanguageLocator);

            if (Regex.IsMatch(newlanguage, pattern1))//Checks if valid patterens were entered
            {//The following statements analyses the value of the notification and determines the test results
                if (notification.Contains("updated"))
                {
                    TableElementsLanguage = driver.FindElements(TableElementsLanguage_Locator); ;

                    WindowHandlers.ScrollToView(TableLanguage);
                    NotificationLanguageUpdate(notification, TableElementsLanguage, Language, newlanguage);
                }
                else if (notification.Contains("already added"))
                {

                    Console.Write($"Updation of language '{Language}' has not been done. Notification from system-{notification}\n");
                }
                else if (notification.Contains("Duplicated"))
                {

                    Console.Write($"Updation of language '{Language}' has not been done. Notification from system-{notification}\n");
                }
                else if (notification.Contains("Please enter language and level"))
                {

                    Console.Write($"Updation of language '{Language}' has not been done. Notification from system-{notification}\n");
                }

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

        public void NotificationLanguageUpdate(string notification, IList<IWebElement> TableElements, String Value, String newValue)
        {

            UpdatedElement = driver.FindElement(AddedLanguageLocator(newValue)).Text;
            int Expected_Count = TableElements.Count();
            //Checks the presence of duplicate values
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

        public void AddDeleteSkillAssert(string Skill)
        {
            //retrieves the notification
            NotificationElement = driver.FindElement(NotificationElementLocator);
            WaitUtils.WaitElementIsVisible(NotificationElementLocator, 5);
            notification = NotificationElement.Text;


            TableElementsSkill = driver.FindElements(TableElementsSkill_Locator); 
            if (Regex.IsMatch(Skill, pattern2))//Checks the existance of invalid characters
            {//The following statements analyses the value of the notification and determines the test results
                if (notification.Contains("has been added to your skills"))
                {
                    NotificationSkillAddedAssert(notification, TableElementsSkill, Skill);
                }
                else if (notification.Contains("deleted"))
                {
                    Console.WriteLine($"Notification from system: {notification}");
                    NotificationSkillDeleted(notification, TableElementsSkill, Skill);
                }
                else if (notification.Contains("already exist"))
                {

                    skillProfile.cancelButton();
                    Console.Write($"Addition of '{Skill}' has not been done due to {notification}\n");
                }
                else if (notification.Contains("Duplicated"))
                {

                    skillProfile.cancelButton();
                    Console.Write($"Addition of '{Skill}' has not been done due to {notification}\n");
                }
                else if (notification.Contains("Please enter skill and experience level"))
                {

                    skillProfile.cancelButton();
                    Console.Write($"Addition of '{Skill}' has not been done due to {notification}\n");
                }
                else
                    Assert.Fail($"Failed Action due to :{notification}");
            }
            else
            {
                if (notification.Contains("invalid characters"))
                    {
                        skillProfile.cancelButton();
                        Console.Write($"Addition of '{Skill}' has not been done due to {notification}\n");
                    }
                    else
                        Assert.Fail($"System Allowed addition of invalid characters! Notification from System :{notification}");


                }
        }


       
       

        //This method checks if the addition of element is successful
        public void NotificationSkillAddedAssert(string notification, IList<IWebElement> TableElements, String value)
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
                AddedSkill = driver.FindElement(AddedSkillLocator(value)).Text;
                if (AddedSkill.Equals(value))
                {
                    Console.WriteLine($"TEST PASSED- Notification - {notification}");
                }
                else
                    Assert.Fail($"Element {value} is not added");
            }
            else
                Assert.Fail($"The system allowed the addition of duplicate entires Notification - {notification}");
        }



        public void NotificationSkillDeleted(string notification, IList<IWebElement> TableElements, String value)
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


      
        public void UpdateAssertionsSkill(string Skill, string newSkill)
        {
            //retrives the notification
            NotificationElement = driver.FindElement(NotificationElementLocator);
            WaitUtils.WaitElementIsVisible(NotificationElementLocator, 5);
            notification = NotificationElement.Text;

            TableSkill = driver.FindElement(TableElementsSkill_Locator);


            if (Regex.IsMatch(newSkill, pattern2))//check if valid elements are entered
            {
                if (notification.Contains("updated"))
                {//The following statements analyses the value of the notification and determines the test results

                    TableElementsSkill = driver.FindElements(TableElementsSkill_Locator);
                    WindowHandlers.ScrollToView(TableSkill);
                    NotificationSkillUpdate(notification, TableElementsSkill, Skill, newSkill);

                }
                else if (notification.Contains("already added"))
                {
                    skillProfile.cancelButton();
                    Console.Write($"Updation of skill '{Skill}' has not been done. Notification from system-{notification}\n");
                }
                else if (notification.Contains("Duplicated"))
                {
                    skillProfile.cancelButton();

                    Console.Write($"Updation of skill '{Skill}' has not been done. Notification from system-{notification}\n");
                }
                else if (notification.Contains("Please enter skill and experience level"))
                {
                    skillProfile.cancelButton();
                    Console.Write($"Addition of '{Skill}' has not been done due to {notification}\n");
                }
                else

                    Assert.Fail($"Failed Action due to :{notification}");


            }
            else
            {
                if (notification.Contains("invalid characters"))
                {
                    skillProfile.cancelButton();
                    Console.Write($"Addition of '{Skill}' has not been done due to {notification}\n");
                }
                else
                    Assert.Fail($"System Allowed addition of invalid characters! Notification from System :{notification}");

            }
        }

        public void NotificationSkillUpdate(string notification, IList<IWebElement> TableElements, String Value, String newValue)
        {

            String UpdatedElement = driver.FindElement(AddedSkillLocator(newValue)).Text;
            int Expected_Count = TableElements.Count();
            //Checks the presence of duplicate values
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

        public void StringLengthAssertion_Skill()
        {
            //retrieves the notification
            NotificationElement = driver.FindElement(NotificationElementLocator);
            WaitUtils.WaitElementIsVisible(NotificationElementLocator, 5);
            notification = NotificationElement.Text;

            TableElementsSkill = driver.FindElements(TableElementsSkill_Locator); ;
            int count = TableElementsSkill.Count();
            Console.WriteLine($"Notification from Sysem:{notification}");

            foreach (IWebElement element in TableElementsSkill)
            {
                table_Values.Add(element.Text);

            }

            foreach (string value in table_Values)
            {
                int l = value.Length;
                //checks if the length of characters is greater than 50
                if (l > 50)
                {
            Assert.Fail("System Allowed the addition of Characters > 50");
                    Thread.Sleep(1000);                 }

            }
        }
        public void StringLengthAssertion_Language()
        {
            //retrieves the notification
            NotificationElement = driver.FindElement(NotificationElementLocator);
            WaitUtils.WaitElementIsVisible(NotificationElementLocator, 5);
            notification = NotificationElement.Text;

            TableElementsLanguage = driver.FindElements(TableElementsLanguage_Locator); ;
            int count = TableElementsLanguage.Count();
            Console.WriteLine($"Notification from Sysem:{notification}");

            foreach (IWebElement element in TableElementsLanguage)
            {
                table_Values.Add(element.Text);

            }

            foreach (string value in table_Values)
            {
                int l = value.Length;
                //checks if the length of characters is greater than 50
                if (l< 50)
                {
                    skillProfile.cancelButton();
                    Assert.Pass("The system stoopped the addition of characters >50");
                }
                else 
                {
                  Assert.Fail("System Allowed the addition of Characters > 50");
                    
                }

            }
        }



        public void Stability(int OGElementCount)
        {
            TableElementsSkill = driver.FindElements(TableElementsSkill_Locator);
            int ActualCount = TableElementsSkill.Count();
            //checks if all the requested elements are added
            if (ActualCount == OGElementCount)
            {
                Console.WriteLine("All Elements are added");

            }
            else
            {

                Assert.Fail($"Expected Count of Elements " + OGElementCount + " .Actual Count " + ActualCount + "");
            }
        }

       



    }
}
