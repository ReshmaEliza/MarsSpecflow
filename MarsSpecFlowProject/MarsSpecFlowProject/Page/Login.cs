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
        public  void loginAction(IWebDriver driver,String UserName, String Password)


        {                       
            IWebElement SignIn = driver.FindElement(By.XPath("//div/a[@class='item']"));
            SignIn.Click();
            IWebElement Email = driver.FindElement(By.XPath(" //input[@name='email']"));
            Email.SendKeys(UserName);
            IWebElement PasswordElement = driver.FindElement(By.XPath(" //input[@name='password']"));
            PasswordElement.SendKeys(Password);

            IWebElement loginButton = driver.FindElement(By.XPath("//button[contains(text(),'Login')]"));
                
                
                loginButton.Click();
                Thread.Sleep(3000);
                String loginverification_strUrl = driver.Url;
                Assert.That(loginverification_strUrl == "http://localhost:5000/Account/Profile", "Login failed");

            
        }
       


    }
}
