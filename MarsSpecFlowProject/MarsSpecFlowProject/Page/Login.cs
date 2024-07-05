using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.V125.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsSpecFlowProject.Page
{
     public class Login
    {

        private readonly By SignInLocator = By.XPath("//div/a[@class='item']");
        IWebElement SignIn;
        private readonly By EmailLocator = By.XPath("//input[@name='email']");
        IWebElement Email;
      
        private readonly By PasswordLocator = By.XPath("//input[@name='password']");
        IWebElement PasswordElement;
        public void loginAction(IWebDriver driver,String UserName, String Password)


        {                       

            SignIn = driver.FindElement(SignInLocator);
            SignIn.Click();
            Email = driver.FindElement(EmailLocator);
            Email.SendKeys(UserName);
            PasswordElement = driver.FindElement(PasswordLocator);
            PasswordElement.SendKeys(Password);

            IWebElement loginButton = driver.FindElement(By.XPath("//button[contains(text(),'Login')]"));
                
                
                loginButton.Click();
                Thread.Sleep(3000);
                String loginverification_strUrl = driver.Url;
                Assert.That(loginverification_strUrl == "http://localhost:5000/Account/Profile", "Login failed");

            
        }
       


    }
}
