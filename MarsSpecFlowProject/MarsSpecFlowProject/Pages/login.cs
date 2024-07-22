using NUnit.Framework;
using OpenQA.Selenium;
using MarsSpecFlowProject.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsSpecFlowProject.Pages
{
    public class Login 
    {
        protected IWebDriver driver;
        public Login() {
            this.driver = WebdriverManager.GetDriver();

        }

        private static By SignInLocator => By.XPath("//div/a[@class='item']");
        private IWebElement SignIn;

        private static By EmailLocator => By.XPath("//input[@name='email']");
        private IWebElement Email;

        private static By PasswordElementLocator => By.XPath("//input[@name='password']");
        private IWebElement PasswordElement;
        private static By loginButtonLocator => By.XPath("//button[contains(text(),'Login')]");
        private IWebElement loginButton;




        public void loginPage(String UserName, String Password)


        {
            //Click on signin button
            SignIn = driver.FindElement(SignInLocator);
            SignIn.Click();

            //Enter username and password
            Email = driver.FindElement(EmailLocator);
            Email.SendKeys(UserName);
            PasswordElement = driver.FindElement(PasswordElementLocator);
            PasswordElement.SendKeys(Password);

            loginButton = driver.FindElement(loginButtonLocator);
            loginButton.Click();
            Thread.Sleep(3000);

            //checks if login is successful
            String loginverification_strUrl = driver.Url;
            Assert.That(loginverification_strUrl == "http://localhost:5000/Account/Profile", "Login failed");
        }
    }
}