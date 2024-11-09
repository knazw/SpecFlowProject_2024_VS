using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowProject_2024.PageObjects
{
    internal class RegisterResult : BasePage
    {

        public IWebElement labelResult => webDriver.FindElement(By.CssSelector("div[class='result']"));
        public IWebElement buttonContinue => webDriver.FindElement(By.XPath("//a[@class='button-1 register-continue-button']"));

        public RegisterResult(IWebDriver webDriver) : base(webDriver)
        {

        }

        public string GetRegistrationResult()
        {
            return labelResult.Text;
        }

        public MainPage clickContinue()
        {
            Click(buttonContinue);

            return new MainPage(webDriver);            
        }

    }
}
