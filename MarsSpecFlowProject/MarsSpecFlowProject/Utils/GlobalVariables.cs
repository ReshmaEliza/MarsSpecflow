using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsSpecFlowProject.Utils
{
    class GlobalVariables:CommonDriver
    {


        public static IWebElement NavigateToTab(String Tab)

        {
            IWebElement Skill_Tab = driver.FindElement(By.XPath($"//a[text() = '{Tab}']"));
            return Skill_Tab;
        }

        public static IWebElement AddNew(String choice)

        {
            //IWebElement Skill_Tab = driver.FindElement(By.XPath($"//a[text() = '{Tab}']"));
            IWebElement AddNew = driver.FindElement(By.XPath($"//div[@data-tab='{choice}']//th[@class='right aligned']/div[contains(text(),'Add New')]"));
            return AddNew;
        }

        public static IWebElement DeleteButton(String Choice)

        {
            IWebElement deletebutton = driver.FindElement(By.XPath($"//div[@data-tab='{Choice}']//td/parent::tr//span[@class='button'][2]"));

            return deletebutton;
        }
       
        public static IWebElement ButtonforElementTobeDeleted(String ElementtobeDelete)
        {

             IWebElement deleteButton =  driver.FindElement(By.XPath($"//td[contains(text(),'{ElementtobeDelete}')]/parent::tr//span[@class='button'][2]"));
            return deleteButton;
        }
        public static IWebElement Value(String Choice,String Value)

        {
            IWebElement AddedLanguge = driver.FindElement(By.XPath($"//div[@data-tab='{Choice}']//td[contains(text(),'{Value}')]"));
                                                      
            return AddedLanguge;
        }

        

        public static IWebElement LevelValue(String Level)

        {
            IWebElement Levelchoice = driver.FindElement(By.XPath($"//div[@class='five wide field']/select[@name='level']/option[@value='{Level}']"));
                                                                   
            return Levelchoice;
        }


        public static IWebElement TableChoice(String Choice)

        {
            IWebElement TableChosen = driver.FindElement(By.XPath($"//div[@data-tab='{Choice}']//td[1]"));
            return TableChosen;
        }

        public static IWebElement table (String Choice)

        {
            IWebElement table = driver.FindElement(By.XPath($"//div[@data-tab='{Choice}']"));

            return table;
        }

        public static IWebElement rowtobechanged(String value)

        {
            IWebElement row = driver.FindElement(By.XPath($"//td[contains(text(),'{value}')]/parent::tr//span[@class='button'][1]"));

            return row;
        }

        public static IWebElement AddTextBox(String workflow)

        {
            IWebElement AddTextBox = driver.FindElement(By.XPath($"//div[@class='five wide field']/input[@placeholder='Add {workflow}']")); 
            return AddTextBox;
        }

        

        public static IWebElement EditValue(String value)

        {
            IWebElement EditValue = driver.FindElement(By.XPath($"//div[@class='five wide field'][1]/input[@value='{value}']"));
            return EditValue;
        }
        public static IList<IWebElement> TableElementsChoice(String Choice)

        {
            IList<IWebElement> TableElementsChosen = driver.FindElements(By.XPath($"//div[@data-tab='{Choice}']//td[1]")) ;
            return TableElementsChosen;
        }
        public static string GenerateRandomString(int length)
        {
            Random random = new Random();

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());

        }



    }
}
