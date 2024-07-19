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
     class SkillsWorkFlow:BasePage
    {



        private String notification;
        private IWebElement table;
        private IWebElement TabView;
        string pattern = @"^(?:$|(?=.*[a-zA-Z0-9])[a-zA-Z0-9\s-]+)$";
        String UpdatedElement;


        protected static List<string> table_Values = new List<string>();

        private By AddButtonLocator = By.XPath("//input[@value='Add']");
        private IWebElement AddButton;

        private static By UpdateButtonLocator = By.XPath("//input[@value='Update']");
        private IWebElement updateButton;

        private By DropDownLocator => By.XPath($"//div[@class='row']//select[@name='level']");
        IWebElement dropDown;

        private static By TabLocator => By.XPath($"//a[text() = 'Skills']");
        private IWebElement TabSelected;

        private static By deleteAllbuttonLocator => By.XPath($"//div[@data-tab='second']//td/parent::tr//span[@class='button'][2]");
        private static IWebElement deleteAllButton;
        private static By deleteElementButtonLocator(string ElementtobeDelete) => By.XPath($"//td[contains(text(),'{ElementtobeDelete}')]/parent::tr//span[@class='button'][2]");
        private IWebElement deleteElement;


        private static By AddTextBoxLocator => By.XPath($"//div[@class='row']//input[@placeholder='Add Skill']");
        private IWebElement AddTextBox;


        private static By tableLocator => By.XPath($"//div[@data-tab='second']");
        private static IWebElement TableChoice;

        private static By rowLocator(string SkillAdded) => By.XPath($"//td[contains(text(),'{SkillAdded}')]/parent::tr//span[@class='button'][1]");
        private IWebElement RowtobeUpdated;
        private static By TableElementsColoumn1_Locator => By.XPath($"//div[@data-tab='second']//td[1]");
        private static IList<IWebElement> TableElements;

        private static By EditTextBoxLocator(string NewSkill) => By.XPath($"//div[@class='fields']//div/input[@value='{NewSkill}']");
        private IWebElement EditValue;

        private static By ValueLocator(string value) => (By.XPath($"//div[@data-tab='second']//td[contains(text(),'{value}')]"));
        String AddedValue;
        public SkillsWorkFlow() : base() { }

        //Code for navigating to requested tab
        public void GoToTab(String Tab)
        {

            driver.Navigate().Refresh();
            driver.Navigate().Refresh();
            TabSelected = driver.FindElement(TabLocator);
            TabSelected.Click();

        }



        public void Add(String SkillValue, String Level)
        {

            //Go to the specified view
            table = driver.FindElement(tableLocator);
            WindowHandlers.ScrollToView(table);
            Thread.Sleep(3000);

            //Click on Add New Button
            IWebElement AddNew = WaitUtils.WaitToBeClickable("second", 15);
            AddNew.Click();

            Thread.Sleep(1000);
            TabView = WaitUtils.WaitElementIsVisible("Skills", 15);
            WindowHandlers.ScrollToView(TabView);

            //Adding required fields
            AddTextBox = driver.FindElement(AddTextBoxLocator);
            AddTextBox.SendKeys(SkillValue);

            //Select the requested dropdown and click on add button
            DropDown(Level);
            AddButton = driver.FindElement(AddButtonLocator);
            AddButton.Click();

        }

        public void Update(String SkillValue, String NewSkillValue, String Newlevel)
        {

            try //check if the element to be updated is present
            {
                //move to the the row to be updated
                RowtobeUpdated = driver.FindElement(rowLocator(SkillValue)); ;


                WindowHandlers.ScrollToView(RowtobeUpdated);
                Thread.Sleep(3000);

                //Click edit on the requested element
                RowtobeUpdated.Click();

                Thread.Sleep(2000);
                //Enter the new values
                EditValue = driver.FindElement(EditTextBoxLocator(SkillValue));
                EditValue.Clear();
                EditValue.SendKeys(NewSkillValue);
                DropDown(Newlevel);
                updateButton = driver.FindElement(UpdateButtonLocator);

                //CLick on update
                updateButton.Click();



            }
            catch // When the element to be updated is not present
            {
                Console.WriteLine($"Skill -  '{SkillValue}' which was requested to be updated is not present in the table");

            }



        }

        public void delete(String ElementtobeDelete)
        {

            try
            {
                //Finding Deletebutton for requested element

                deleteElement = driver.FindElement(deleteElementButtonLocator(ElementtobeDelete));
                WindowHandlers.ScrollToView(deleteElement);
                Thread.Sleep(5000);
                deleteElement.Click();



            }
            catch //when the requested element to be deleted i not present
            {

                Console.WriteLine($"Language to be be deleted '{ElementtobeDelete}' was not found in the table");
            }

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
                Console.WriteLine("No elements found");
            }
        }


        public void DropDown(String Level)
        {

            dropDown = driver.FindElement(DropDownLocator);
            SelectElement s = new SelectElement(dropDown);
            s.SelectByText(Level);


        }



    }
}
