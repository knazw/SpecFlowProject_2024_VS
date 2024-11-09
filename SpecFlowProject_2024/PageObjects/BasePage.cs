using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowProject_2024.PageObjects
{
    internal class BasePage
    {
        protected IWebDriver webDriver;

        public BasePage(IWebDriver webDriver)
        {
            this.webDriver = webDriver;
        }

        protected void Click(By byItem)
        {
            this.webDriver.FindElement(byItem).Click();
        }

        protected void Click(IWebElement element)
        {
            element.Click();
        }

        protected void type(IWebElement element, string text)
        {
            element.SendKeys(text);
        }
    }
}
