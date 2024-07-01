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


        public static void Addlanguage(IWebDriver driver, String Language, String Level)
        {

            try //check if the 'Add Button is present'
            {
                //Click on Add New Button
                IWebElement AddNew = driver.FindElement(By.XPath("//th[@class='right aligned']/div[contains(text(),'Add New')]"));
                AddNew.Click();
                Thread.Sleep(1000);

                //Adding required fields
                IWebElement dLang = driver.FindElement(By.XPath("//div[@class='five wide field']/input[@placeholder='Add Language']"));
                IWebElement Langagelevel = driver.FindElement(By.XPath("//div[@class='five wide field']/select[@name='level']"));
                dLang.SendKeys(Language);
                Langagelevel.Click();
                IWebElement Levelchoice = driver.FindElement(By.XPath($"//div[@class='five wide field']/select[@name='level']/option[@value='{Level}']"));
                Levelchoice.Click();
                IWebElement AddButton = driver.FindElement(By.XPath("//div[@class='six wide field']/input[@value='Add']"));
                AddButton.Click();



            }
            catch //When Add Button not Present 
            {
                IList<IWebElement> languageElements = driver.FindElements(By.XPath("//div[@data-tab='first']//table/tbody"));
                Console.WriteLine($"Add botton not found while adding language - {Language} is not done\n");
                Console.WriteLine("The number of language elements already present is  " + languageElements.Count + "You can do up to a maximum of four selections\n");


            }
        }
        
        public void DeleteAllElements(IWebDriver driver)
        {
            try
            {
                IWebElement Table1 = driver.FindElement(By.XPath("//div[@data-tab='first']//td[1]"));
                IList<IWebElement> TableElements = driver.FindElements(By.XPath("//div[@data-tab='first']//td[1]"));
                int count = TableElements.Count();

                if (count > 0)
                {
                    for (int i = 0; i < count; i++)
                    {
                        IWebElement deleteButton = driver.FindElement(By.XPath("//div[@data-tab='first']//td/parent::tr//span[@class='button'][2]"));
                        IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                        js.ExecuteScript("arguments[0].scrollIntoView(true);", Table1);
                        Thread.Sleep(1000);

                        deleteButton.Click();
                    }



                }
                else
                {
                    Console.WriteLine("The user doesnt have any existing languages to be deleted");


                }
            }
            catch
            {
                Console.WriteLine("No elements found");
            }


        }




       

        public static void UpdateLanguage(IWebDriver driver, String Lantobeupdated, String EditedLangValue, String EditedLevelValue)
        {

            try {
                IWebElement RowtobeUpdated = driver.FindElement(By.XPath($"//td[contains(text(),'{Lantobeupdated}')]/parent::tr//span[@class='button'][1]"));


                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                js.ExecuteScript("arguments[0].scrollIntoView(true);", RowtobeUpdated);
                Thread.Sleep(3000);


                RowtobeUpdated.Click();

                IWebElement EditLang = driver.FindElement(By.XPath($"//div[@class='five wide field'][1]/input[@value='{Lantobeupdated}']"));
                EditLang.Clear();
                EditLang.SendKeys(EditedLangValue);

                IWebElement Langagelevel = driver.FindElement(By.XPath("//div[@class='five wide field'][2]/select[@name='level']"));
                Langagelevel.Click();

                IWebElement EditLevel = driver.FindElement(By.XPath($"//div[@class='five wide field'][2]/select[@name='level']/option[@value='{EditedLevelValue}']"));
                EditLevel.Click();

                IWebElement updateButton = driver.FindElement(By.XPath("//div[@class='fields']/span/input[@value='Update']"));
                updateButton.Click();
                //waitobj.WaitToBeClickable(driver, "ns-box-inner", 10, Lantobeupdated,EditedLangValue);


            }
            catch
            {
                Console.WriteLine($"Language -  {Lantobeupdated} to be updated is not present in the table");

            }



        }

        
        public void deletelanguage(IWebDriver driver, String ElementtobeDelete)
        {

            try
            {
                //Finding Deletebutton for requested element
                IWebElement deleteButton = driver.FindElement(By.XPath($"//td[contains(text(),'{ElementtobeDelete}')]/parent::tr//span[@class='button'][2]"));
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                js.ExecuteScript("arguments[0].scrollIntoView(true);", deleteButton);
                Thread.Sleep(5000);
                deleteButton.Click();
                //waitobj.WaitToBeClickable(driver, "ns-box-inner", 10, ElementtobeDelete);


            }
            catch
            {

                Console.WriteLine($"Language to be be deleted '{ElementtobeDelete}' was not found in the table");
            }

        }


        public void Sessions(IWebDriver driver,int SID )
        {


            
            SID = SID - 1;
            Window_Sessions.ActiveSession(driver, SID);
           

        }
        public void Url(IWebDriver driver)
        {
            driver.Navigate().GoToUrl("http://localhost:5000/Account/Profile");


        }


        public static string GenerateRandomString(int length)
        {          Random random = new Random();

        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());

        }
        

    }

            }

    


