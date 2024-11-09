using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace SpecFlowProject_2024
{
    internal class WebdriverReferenceHandler
    {

        public static IWebDriver webDriver;
        //protected WebDriverManager.DriverManager webDriverManager;

        public static void GetBrowserInstance(string browserName, bool headless)
        {
            
            switch (browserName)
            {
                case "chrome":
                    new DriverManager().SetUpDriver(new ChromeConfig(), version: "Latest");

                    webDriver = new ChromeDriver();
                    break;
                case "chromeheadless":
                    new DriverManager().SetUpDriver(new ChromeConfig(), version: "Latest");
                    var options = new ChromeOptions();
                    options.AddArgument("headless");

                    webDriver = new ChromeDriver(options);

                    break;
                default:
                    new DriverManager().SetUpDriver(new ChromeConfig(), version: "Latest");
                    webDriver = new ChromeDriver();
                    break;
            }
        }
    }
}
