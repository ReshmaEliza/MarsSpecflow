using NuGet.Frameworks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using OpenQA.Selenium.Chrome;
using static System.Net.Mime.MediaTypeNames;
using System.Linq.Expressions;
using MarsSpecFlowProject.Utils;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenQA.Selenium.DevTools.V124.Debugger;
using System.Text.RegularExpressions;
using static MarsSpecFlowProject.StepDefinitions.ThisTestSuiteContainsTestScenariosForLanguageTab_StepDefinitions;
using TechTalk.SpecFlow.Configuration.JsonConfig;
using System.Reflection.Emit;



namespace SpecFlowMVPMARS.Page
{
    class LanguageWorkFlow
    {

        private readonly By LanguageFieldLocator = By.XPath("//div[@class='five wide field']/input[@placeholder='Add Language']");
        IWebElement LanguageField;
        private readonly By AddNewLocator = By.XPath("//th[@class='right aligned']/div[contains(text(),'Add New')]");
        IWebElement AddNew;
        private readonly By LanguagelevelLocator = By.XPath("//div[@class='five wide field']/select[@name='level']");
        IWebElement Languagelevel;
        private readonly By AddButtonLocator = By.XPath("//div[@class='six wide field']/input[@value='Add']");
        IWebElement Addutton;
        //private readonly By Table1Locator = By.XPath("//div[@data-tab='first']//td[1]");
        IWebElement Table1;
        //private readonly By TableElementsLocator = By.XPath("//div[@data-tab='first']//td[1]");
        IList<IWebElement> TableElements;
        private readonly By updateButtonLocator = By.XPath("//div[@class='fields']/span/input[@value='Update']");
        IWebElement updateButton;


        public void Addlanguage(IWebDriver driver, String Language, String Level)
        {

            try //check if the 'Add Button is present'
            {
                //Click on Add New Button
                AddNew = driver.FindElement(AddNewLocator);
                AddNew.Click();
                Thread.Sleep(1000);

                //Adding required fields
                LanguageField = driver.FindElement(LanguageFieldLocator);
                Languagelevel = driver.FindElement(LanguagelevelLocator);
                LanguageField.SendKeys(Language);
                Languagelevel.Click();
                IWebElement Levelchoice = GlobalVariables.LevelValue(driver, Level);
                Levelchoice.Click();
                IWebElement AddButton = driver.FindElement(AddButtonLocator);
                AddButton.Click();



            }
            catch //When Add Button not Present 
            {

                
                TableElements = GlobalVariables.TableElementsChoice(driver, "first");
                Console.WriteLine($"Add botton not found while adding language - {Language} is not done\n");
                Console.WriteLine("The number of language elements already present is  " + TableElements.Count + "You can do up to a maximum of four selections\n");


            }
        }
        
        public void DeleteAllElements(IWebDriver driver)
        {
            try
            {
                 Table1 = GlobalVariables.TableChoice(driver, "first");
                  TableElements = GlobalVariables.TableElementsChoice(driver, "first");

                int count = TableElements.Count();

                if (count > 0)
                {
                    for (int i = 0; i < count; i++)
                    {
                        IWebElement deleteButton = driver.FindElement(By.XPath("//div[@data-tab='first']//td/parent::tr//span[@class='button'][2]"));
                        WindowHandlers.ScrollToView(driver, Table1);
                        Thread.Sleep(3000);

                        deleteButton.Click();
                    }



                }
                
            }
            catch
            {
                Console.WriteLine("No elements found");
            }


        }




       

        public  void UpdateLanguage(IWebDriver driver, String Lantobeupdated, String EditedLangValue, String EditedLevelValue)
        {

            try {//try when the language to be updated is present
                IWebElement RowtobeUpdated = driver.FindElement(By.XPath($"//td[contains(text(),'{Lantobeupdated}')]/parent::tr//span[@class='button'][1]"));
                WindowHandlers.ScrollToView(driver, RowtobeUpdated);
                Thread.Sleep(3000);


                RowtobeUpdated.Click();

                IWebElement EditLang = driver.FindElement(By.XPath($"//div[@class='five wide field'][1]/input[@value='{Lantobeupdated}']"));
                EditLang.Clear();
                EditLang.SendKeys(EditedLangValue);

                Languagelevel = driver.FindElement(LanguagelevelLocator);
                Languagelevel.Click();

                
                IWebElement EditLevel = GlobalVariables.LevelValue(driver, EditedLevelValue);
                EditLevel.Click();

                 updateButton = driver.FindElement(updateButtonLocator);
                updateButton.Click();
               


            }
            catch // When the language to be updtaed is not present
            {
                Console.WriteLine($"Language -  {Lantobeupdated} to be updated is not present in the table");

            }



        }

        
        public void deletelanguage(IWebDriver driver, String ElementtobeDelete)
        {

            try//try when the language to be updated is present
            {
                //Finding Deletebutton for requested element
                IWebElement deleteButton = driver.FindElement(By.XPath($"//td[contains(text(),'{ElementtobeDelete}')]/parent::tr//span[@class='button'][2]"));
                WindowHandlers.ScrollToView(driver, deleteButton);
                Thread.Sleep(3000);
                deleteButton.Click();
                


            }
            catch // when the language to be updated is not present
            {

                Console.WriteLine($"Language to be be deleted '{ElementtobeDelete}' was not found in the table");
            }

        }


        public void Sessions(IWebDriver driver,int SID )
        {


            
            SID = SID - 1;
            WindowHandlers.ActiveSession(driver, SID);
           

        }
        public void Url(IWebDriver driver)
        {
            driver.Navigate().GoToUrl("http://localhost:5000/Account/Profile");


        }


        
        

    }

            }

    


