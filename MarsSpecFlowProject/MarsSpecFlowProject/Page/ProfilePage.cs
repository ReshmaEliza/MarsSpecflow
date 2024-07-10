
 using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using MarsSpecFlowProject.Utils;
using NUnit.Framework;
using SeleniumExtras.PageObjects;

namespace MarsSpecFlowProject.Page
{
    class ProfilePage:BasePage
    {
        private IWebElement TableChoice;
        private IList<IWebElement> TableElements;
        //private string choice;
        //private string WorkFlow;
        //private string tabcurrent;
        private IWebElement deleteButton;
        private IWebElement Skill_Tab;
        private IWebElement table;
        private IWebElement AddTextBox;
        private IWebElement TabView;
        private IWebElement RowtobeUpdated;
        private IWebElement EditValue;
        private IWebElement EditLevel;
        private IWebElement deleteElement;



       
        [FindsBy(How = How.XPath, Using = "//input[@value='Add']")]
        IWebElement AddButton;

        [FindsBy(How = How.XPath, Using = "//div[@class='fields']/span/input[@value='Update']")]
        IWebElement updateButton;

        public ProfilePage() : base() { }
        public  void GoToTab(String Tab)
        {



            driver.Navigate().Refresh();
            driver.Navigate().Refresh();
            Skill_Tab = GlobalVariables.NavigateToTab(Tab);
            Skill_Tab.Click();

        }


         public void DeleteAllElements()
        {
            try
            {
                Console.WriteLine(choice);
                TableChoice = GlobalVariables.TableChoice(choice);
                TableElements = GlobalVariables.TableElementsChoice(choice);
                int count = TableElements.Count();

                if (count > 0)
                {
                    for (int i = 0; i < count; i++)
                    {
                        deleteButton = GlobalVariables.DeleteButton(choice);
                        WindowHandlers.ScrollToView(TableChoice);
                        Thread.Sleep(2000);

                        deleteButton.Click();
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

        public void Add(String value, String Level)
        {



            table = GlobalVariables.table(choice);
             WindowHandlers.ScrollToView(table);
            Thread.Sleep(3000);

            //Click on Add New Button
            IWebElement AddNew = WaitUtils.WaitToBeClickable(choice, 15);
            AddNew.Click();

            Thread.Sleep(1000);
            TabView = WaitUtils.WaitElementIsVisible(tabcurrent, 15);
            
            WindowHandlers.ScrollToView(TabView);
            //Adding required fields
            

            AddTextBox = GlobalVariables.AddTextBox(WorkFlow);
                
            AddTextBox.SendKeys(value);
            IWebElement Levelchoice = GlobalVariables.LevelValue(Level);
            Levelchoice.Click();
            AddButton.Click();

        }

        public void Update(String value, String NewValue, String Newlevel)
        {

            try
            {
                
                RowtobeUpdated = GlobalVariables.rowtobechanged(value);

                WindowHandlers.ScrollToView(RowtobeUpdated);
                Thread.Sleep(3000);


                RowtobeUpdated.Click();

                

                EditValue = GlobalVariables.EditValue(value);

                EditValue.Clear();
                EditValue.SendKeys(NewValue);
                EditLevel = GlobalVariables.LevelValue(Newlevel);
                EditLevel.Click();
                updateButton.Click();



            }
            catch
            {
                Console.WriteLine($"Skill -  '{value}' which was requested to be updated is not present in the table");

            }



        }

        public void delete(String ElementtobeDelete)
        {

            try
            {
                //Finding Deletebutton for requested element
                
                deleteElement = GlobalVariables.ButtonforElementTobeDeleted(ElementtobeDelete);
                WindowHandlers.ScrollToView(deleteElement);
                Thread.Sleep(5000);
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
            WindowHandlers.ActiveSession(SID);


        }
        public void Url()
        {
            driver.Navigate().GoToUrl("http://localhost:5000/Account/Profile");


        }

    }
}


