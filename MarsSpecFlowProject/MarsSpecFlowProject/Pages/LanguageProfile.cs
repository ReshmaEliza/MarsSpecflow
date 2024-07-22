using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using MarsSpecFlowProject.Context;
using MarsSpecFlowProject.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow.Assist.ValueRetrievers;

namespace MarsSpecFlowProject.Pages
{
   public class LanguageProfile 
    {
        protected IWebDriver driver;

        protected static List<string> table_Values = new List<string>();
       
        private By AddButtonLanguageLocator = By.XPath("//input[@value='Add']");
        private IWebElement AddButton;

        private static By UpdateButtonLocator = By.XPath("//input[@value='Update']");
        private IWebElement updateButton;

        private By DropDownLocator => By.XPath($"//div[@class='row']//select[@name='level']");
        IWebElement dropDown;

        
        private static By TabLocator => By.XPath($"//a[text() = 'Languages']");
        private IWebElement TabSelected;
        private static By addLocator = By.XPath($"//div[@data-tab='first']//th[@class='right aligned']/div[contains(text(),'Add New')]");
        IWebElement AddNew;

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
        public LanguageProfile()
        {
            this.driver = WebdriverManager.GetDriver();

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

        public void Add(String LanguageAdded, String Level)
        {

            Thread.Sleep(3000);
            AddNew = driver.FindElement(addLocator);
            WaitUtils.WaitToBeClickable("Xpath",addLocator, 10);
            AddNew.Click();

            //Adding required fields
            //Enter the language name in text box
            AddTextBox = driver.FindElement(AddTextBoxLocator);
            AddTextBox.SendKeys(LanguageAdded);

            //Select the Level from DropDown
            DropDown(Level);

            //Confirm the entry by clicking on Add
            AddButton = driver.FindElement(AddButtonLanguageLocator);
            AddButton.Click();



        }


        public void delete(String ElementtobeDelete)
        {

            try
            {
                //Finding Deletebutton for requested element
                deleteElement = driver.FindElement(deleteElementButtonLocator(ElementtobeDelete));
                WindowHandlers.ScrollToView(deleteElement);
                WaitUtils.WaitToBeClickable("Xpath", deleteElementButtonLocator(ElementtobeDelete), 15);

                //Thread.Sleep(3000);
                deleteElement.Click();

            }
            catch
            {

                Console.WriteLine($"Language to be be deleted '{ElementtobeDelete}' was not found in the table");
            }

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
            //SElect the drop down
            dropDown = driver.FindElement(DropDownLocator);
            SelectElement s = new SelectElement(dropDown);
            s.SelectByText(Level);


        }


        public void DeleteAllElements()
        {

            try
            {


                TableChoice = driver.FindElement(tableLocator);
                TableElements = driver.FindElements(TableElementsColoumn1_Locator);
                int count = TableElements.Count();

                if (count > 0)
                {
                    for (int i = 0; i < count; i++)
                    {
                        deleteAllButton = driver.FindElement(deleteAllbuttonLocator);

                        WindowHandlers.ScrollToView(TableChoice);
                        Thread.Sleep(2000);

                        deleteAllButton.Click();
                      
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
