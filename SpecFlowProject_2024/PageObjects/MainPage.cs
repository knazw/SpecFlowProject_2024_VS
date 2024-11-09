using OpenQA.Selenium;
using SpecFlowProject_2024.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowProject_2024.PageObjects
{
    internal class MainPage : BasePage
    {


        public IWebElement iconRegister => webDriver.FindElement(By.CssSelector("a[class='ico-register']"));

        public MainPage(IWebDriver webDriver) : base(webDriver)
        {

        }

        public RegisterPage clickRegister()
        {
            iconRegister.Click();

            return new RegisterPage(webDriver);
        }



    }
}
