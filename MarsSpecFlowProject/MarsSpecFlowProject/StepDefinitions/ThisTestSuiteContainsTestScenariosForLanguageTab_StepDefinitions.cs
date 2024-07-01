using System;
using TechTalk.SpecFlow;
using MarsSpecFlowProject.Utils;
using MarsSpecFlowProject.Page;
using OpenQA.Selenium.Chrome;
using System.Reflection.Emit;
using TechTalk.SpecFlow.Assist;
using static MarsSpecFlowProject.StepDefinitions.ThisTestSuiteContainsTestScenariosForLanguageTab_StepDefinitions;
using OpenQA.Selenium;
using System.Security.Policy;
using NUnit.Framework.Internal.Execution;


namespace MarsSpecFlowProject.StepDefinitions
{
    [Binding]
    public class ThisTestSuiteContainsTestScenariosForLanguageTab_StepDefinitions : CommonDriver
    {

        Login loginobj = new Login();



        [BeforeScenario]
        public void Setup()
        {

            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            driver.Url = "http://localhost:5000/";
            driver.Manage().Window.Maximize();
            

        }


        
        
       

        [Given(@"I log into the portal with UserName '([^']*)' and Password '([^']*)'")]
        public void GivenILogIntoThePortalWithUserNameAndPassword(string UserName, string Password)
        {
            loginobj.loginAction(driver, UserName, Password);
        }


        
        [AfterScenario]
        public void Cleanup()
        {
            driver.Quit();
        }


    }
}
