using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools.V125.Network;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;


namespace MarsSpecFlowProject.Page
{
    public class Login:BasePage
    {
        //private IWebDriver driver;
        [FindsBy(How = How.XPath,Using = "//div/a[@class='item']")]
        private IWebElement SignIn;

        [FindsBy(How = How.XPath, Using = "//input[@name='email']")]
        private IWebElement Email;

        [FindsBy(How = How.XPath, Using = "//input[@name='password']")]
        private IWebElement PasswordElement;

        [FindsBy(How = How.XPath, Using = "//button[contains(text(),'Login')]")]
        IWebElement loginButton;
            public Login() : base() { }


        public void loginPage(String UserName, String Password)


        {
            SignIn.Click();
            Email.SendKeys(UserName);
            PasswordElement.SendKeys(Password);
            loginButton.Click();
            Thread.Sleep(3000);
              String loginverification_strUrl = driver.Url;
            Assert.That(loginverification_strUrl == "http://localhost:5000/Account/Profile", "Login failed");
        }
            
        
        



    }
}
