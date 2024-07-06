using MarsSpecFlowProject.Utils;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TechTalk.SpecFlow;


namespace MarsSpecFlowProject.Page
{
     class SkillWorkflow
    {
        IWebElement Table2;
        IList<IWebElement> TableElements;
        private  readonly By TableSkillLocator = By.XPath("//div[@data-tab='second']");
        IWebElement TableSkill;
        private  string AddNewlocatorValue = "//div[@data-tab='second']//th[@class='right aligned']/div[contains(text(),'Add New')]";

        private  readonly By SLangLocator = By.XPath("//div[@class='five wide field']/input[@placeholder='Add Skill']");
        IWebElement SLang;
        private  readonly By AddButtonLocator = By.XPath("//span[@class='buttons-wrapper']/input[@value='Add']");
         IWebElement AddButton;
        IWebElement EditLevel;

      
        public void DeleteAllElements(IWebDriver driver)
        {
          
            try
            {
                               
                Table2 = GlobalVariables.TableChoice(driver, "second");
                TableElements = GlobalVariables.TableElementsChoice(driver, "second");
                int count = TableElements.Count();

                if (count > 0)
                {
                    for (int i = 0; i < count; i++)
                    {
                        IWebElement deleteButton = driver.FindElement(By.XPath("//div[@data-tab='second']//td/parent::tr//span[@class='button'][2]"));
                        WindowHandlers.ScrollToView(driver, Table2);
                        Thread.Sleep(2000);

                        deleteButton.Click();
                        String notification = WaitUtils.Notification(driver);
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


        public  void AddSkill(IWebDriver driver, String Skill, String SkillLevel)
        {
            

            
            IWebElement TableSkill = driver.FindElement(TableSkillLocator);
            //js.ExecuteScript("arguments[0].scrollIntoView(true);", Table1);
            WindowHandlers.ScrollToView(driver, TableSkill);
            Thread.Sleep(1000);
            //Click on Add New Button
            IWebElement AddNew = WaitUtils.WaitToBeClickable(driver, AddNewlocatorValue,5);



            AddNew.Click();
            String SkillViewlocatorValue = "//h3[contains(text(),'Skills')]";
            IWebElement SkillView = WaitUtils.WaitElementIsVisible(driver, SkillViewlocatorValue,5);
            WindowHandlers.ScrollToView(driver, SkillView);
            //Adding required fields
            IWebElement SLang = driver.FindElement(By.XPath("//div[@class='five wide field']/input[@placeholder='Add Skill']"));
           
            SLang.SendKeys(Skill);
           
           
            IWebElement Levelchoice = GlobalVariables.LevelValue(driver, SkillLevel);
                Levelchoice.Click();
                AddButton = driver.FindElement(AddButtonLocator);
                AddButton.Click();

                 }
        public  void UpdateSkill(IWebDriver driver, String Skill, String NewSkill, String Newlevel)
        {

            try
            {
                IWebElement RowtobeUpdated = driver.FindElement(By.XPath($"//td[contains(text(),'{Skill}')]/parent::tr//span[@class='button'][1]"));

                 WindowHandlers.ScrollToView(driver,RowtobeUpdated);
                Thread.Sleep(3000);


                RowtobeUpdated.Click();

                IWebElement EditSkill = driver.FindElement(By.XPath($"//div[@class='five wide field'][1]/input[@value='{Skill}']"));
                
                EditSkill.Clear();
                EditSkill.SendKeys(NewSkill);
                EditLevel = GlobalVariables.LevelValue(driver, Newlevel);
                 EditLevel.Click();

                IWebElement updateButton = driver.FindElement(By.XPath("//div[@class='fields']/span/input[@value='Update']"));
                updateButton.Click();
                


            }
            catch
            {
                Console.WriteLine($"Skill -  '{Skill}' which was requested to be updated is not present in the table");

            }



        }

        public static void deletelanguage(IWebDriver driver, String ElementtobeDelete)
        {

            try
            {
                //Finding Deletebutton for requested element
                IWebElement deleteButton = driver.FindElement(By.XPath($"//td[contains(text(),'{ElementtobeDelete}')]/parent::tr//span[@class='button'][2]"));
                WindowHandlers.ScrollToView(driver,deleteButton);
                Thread.Sleep(5000);
                deleteButton.Click();
                


            }
            catch
            {

                Console.WriteLine($"Language to be be deleted '{ElementtobeDelete}' was not found in the table");
            }

        }



    }
}
