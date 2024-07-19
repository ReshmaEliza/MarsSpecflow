using MarsSpecFlowProject.Utils;
using NUnit.Framework;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsSpecFlowProject.Page
{
     class LanguageWorkFlow: BasePage
    {



        //Declaring the Common Variables used for pages

        private String notification;

        protected static List<string> table_Values = new List<string>();

        string pattern = @"^(?:$|(?=.*[a-zA-Z])[a-zA-Z\s-]+)$";

        private By AddButtonLocator = By.XPath("//input[@value='Add']");
        private IWebElement AddButton;

        private static By UpdateButtonLocator = By.XPath("//input[@value='Update']");
        private IWebElement updateButton;

        private By DropDownLocator => By.XPath($"//div[@class='row']//select[@name='level']");
        IWebElement dropDown;

        protected IWebElement table;
        private static By TabLocator => By.XPath($"//a[text() = 'Languages']");
        private IWebElement TabSelected;

        private static By deleteAllbuttonLocator => By.XPath($"//div[@data-tab='first']//td/parent::tr//span[@class='button'][2]");
        private static IWebElement deleteAllButton;
        private static By deleteElementButtonLocator(string ElementtobeDelete) => By.XPath($"//td[contains(text(),'{ElementtobeDelete}')]/parent::tr//span[@class='button'][2]");
        private IWebElement deleteElement;


        private static By AddTextBoxLocator => By.XPath($"//div[@class='row']//input[@placeholder='Add Language']");
        private IWebElement AddTextBox;


        private static By tableLocator => By.XPath($"//div[@data-tab='first']");
        private static IWebElement TableChoice;

        private static By rowLocator(string LanguageAdded) => By.XPath($"//td[contains(text(),'{LanguageAdded}')]/parent::tr//span[@class='button'][1]");
        private IWebElement RowtobeUpdated;
        private static By TableElementsColoumn1_Locator => By.XPath($"//div[@data-tab='first']//td[1]");
        private static IList<IWebElement> TableElements;

        private static By EditTextBoxLocator(string NewLanguage) => By.XPath($"//div[@class='fields']//div/input[@value='{NewLanguage}']");
        private IWebElement EditValue;

        private static By ValueLocator(string value) => (By.XPath($"//div[@data-tab='first']//td[contains(text(),'{value}')]"));
        String AddedValue;
        public LanguageWorkFlow() : base()
        {


        }
        public void GoToTab()
        {


            //Refreshing data to get the correct status
            driver.Navigate().Refresh();
            driver.Navigate().Refresh();
            //Navigate to Language Tab
            TabSelected = driver.FindElement(TabLocator);
            TabSelected.Click();

        }

        public void getcurrentdata()
        {


            //Refreshing data to get the correct status
            driver.Navigate().Refresh();

        }


        public void Add(String LanguageAdded, String Level)
        {

            Thread.Sleep(3000);

            IWebElement AddNew = WaitUtils.WaitToBeClickable("first", 15);
            AddNew.Click();

            Thread.Sleep(1000);

            //Adding required fields
            //Enter the language name in text box
            AddTextBox = driver.FindElement(AddTextBoxLocator);
            AddTextBox.SendKeys(LanguageAdded);

            //Select the Level from DropDown
            DropDown(Level);

            //Confirm the entry by clicking on Add
            AddButton = driver.FindElement(AddButtonLocator);
            AddButton.Click();

        }

        public void Update(String Language, String NewLanguage, String Newlevel)
        {

            try//Check if element to be updated is present
            {
                // Search for the row to be updated
                RowtobeUpdated = driver.FindElement(rowLocator(Language));
                //Scroll into the row to be updtaed view
                WindowHandlers.ScrollToView(RowtobeUpdated);
                Thread.Sleep(3000);

                //Click on the edit button for row
                RowtobeUpdated.Click();


                //Steps to enter the updated value
                EditValue = driver.FindElement(EditTextBoxLocator(Language));
                EditValue.Clear();
                EditValue.SendKeys(NewLanguage);

                //Choose the new Language level
                DropDown(Newlevel);

                //Click on update button to confirm
                updateButton = driver.FindElement(UpdateButtonLocator);
                updateButton.Click();



            }
            catch
            {
                Console.WriteLine($"Skill - '{Language}' which was requested to be updated is not present in the table");

            }



        }

        public void delete(String ElementtobeDelete)
        {

            try
            {
                //Finding Deletebutton for requested element
                deleteElement = driver.FindElement(deleteElementButtonLocator(ElementtobeDelete));
                WindowHandlers.ScrollToView(deleteElement);
                Thread.Sleep(2000);
                deleteElement.Click();

            }
            catch
            {

                Console.WriteLine($"Language to be be deleted '{ElementtobeDelete}' was not found in the table");
            }

        }

        public void Sessions(int SID)
        {

            SID = SID - 1;

            //Navigate to the requested session
            WindowHandlers.ActiveSession(SID);


        }
        public void Url()

        {


            driver.Navigate().GoToUrl("http://localhost:5000/Account/Profile");


        }

        public void DropDown(String Level)
        {

            dropDown = driver.FindElement(DropDownLocator);
            SelectElement s = new SelectElement(dropDown);
            s.SelectByText(Level);


        }


        public static void DeleteElements()
        {

            try
            {


                TableChoice = driver.FindElement(tableLocator);
                TableElements = driver.FindElements(TableElementsColoumn1_Locator); ;
                int count = TableElements.Count();

                if (count > 0)
                {
                    for (int i = 0; i < count; i++)
                    {
                        deleteAllButton = driver.FindElement(deleteAllbuttonLocator);

                        WindowHandlers.ScrollToView(TableChoice);
                        Thread.Sleep(2000);

                        deleteAllButton.Click();
                        String notification = WaitUtils.Notification();
                        if (notification.Contains("error"))
                        {

                            Assert.Fail($"{notification}");
                            break;

                        }
                    }

                }
            }
            catch
            {
                Console.WriteLine("No elements found for deletion");
            }
        }
    }
}
