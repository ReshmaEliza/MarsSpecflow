using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarsSpecFlowProject.Utils;

namespace MarsSpecFlowProject.Page
{
    public class BasePage
    {

        protected IWebDriver driver;

        private readonly HttpClient httpClient;

        public BasePage() {

            this.driver = CommonDriver.getDriver();
            PageFactory.InitElements(driver, this);
        }

        protected string choice;
        protected string WorkFlow;
        protected string tabcurrent;
        protected string pattern;
        public void InitChoice(String Tab)
        {
            switch (Tab)
            {

                case "Languages":
                    this.choice = "first";
                    this.WorkFlow = "Language";
                    this.tabcurrent = Tab;
                    this.pattern = @"^(?:$|(?=.*[a-zA-Z])[a-zA-Z\s-]+)$";

                    break;
                case "Skills":
                    this.choice = "second";
                    this.WorkFlow = "Skill";
                    this.tabcurrent = Tab;
                    this.pattern = @"^(?:$|(?=.*[a-zA-Z0-9])[a-zA-Z0-9\s-]+)$";

                    break;

            }
        }
    }
}
