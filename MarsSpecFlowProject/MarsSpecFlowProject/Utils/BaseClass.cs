using Gherkin.Ast;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarsSpecFlowProject.Page;
using Gherkin.CucumberMessages.Types;
using TechTalk.SpecFlow;

namespace MarsSpecFlowProject.Utils
{
     public class BaseClass
    {

        public static IWebDriver driver;

        [BeforeScenario]
        public static void Setup()
        {


            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(40);
            driver.Url = "http://localhost:5000/";
            driver.Manage().Window.Maximize();


        }

        public static IWebDriver getDriver()
        {


            if (driver == null)
            {
                Setup();
            }
            return driver;
        }



    }
}
