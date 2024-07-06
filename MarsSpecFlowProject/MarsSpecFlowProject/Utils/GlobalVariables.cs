using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsSpecFlowProject.Utils
{
    class GlobalVariables
    {

        public static IWebElement Value(IWebDriver driver,String Choice,String Value)

        {
            IWebElement AddedLanguge = driver.FindElement(By.XPath($"//div[@data-tab='{Choice}']//td[contains(text(),'{Value}')]"));
                                                      
            return AddedLanguge;
        }

        public static IWebElement LevelValue(IWebDriver driver, String Level)

        {
            IWebElement Levelchoice = driver.FindElement(By.XPath($"//div[@class='five wide field']/select[@name='level']/option[@value='{Level}']"));
                                                                   
            return Levelchoice;
        }


        public static IWebElement TableChoice(IWebDriver driver, String Choice)

        {
            IWebElement TableChosen = driver.FindElement(By.XPath($"//div[@data-tab='{Choice}']//td[1]"));
            return TableChosen;
        }

        public static IList<IWebElement> TableElementsChoice(IWebDriver driver, String Choice)

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
