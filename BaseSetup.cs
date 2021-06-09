using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace k
{
    public class BaseSetup{
        protected IWebDriver driver;
        public void BaseSetupBrowser(string browserName){
            if(browserName.Equals("chrome"))
                driver = new ChromeDriver();
            else
                driver = new ChromeDriver();

        }
    }
}