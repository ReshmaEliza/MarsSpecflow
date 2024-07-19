using MarsSpecFlowProject.Utils;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsSpecFlowProject.Page
{
     public class BasePage
    {

        protected static IWebDriver driver;
      

        public BasePage()
        {

            driver = BaseClass.getDriver();

        }


            }
}
