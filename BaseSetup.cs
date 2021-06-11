using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Newtonsoft.Json;
using System.IO;
using OpenQA.Selenium.Firefox;

namespace k
{
    public class BaseSetup{
        protected IWebDriver driver;
        public class Browser{
            public string BrowserType {get; set;}
        }
        
        public void BaseSetupBrowser(){

            var file = @"D:\k\configsetting.json";
            Browser browser = JsonConvert.DeserializeObject<Browser>(File.ReadAllText(file));
         
            if(browser.BrowserType.Equals("Chrome"))
                driver = new ChromeDriver();
            else if(browser.BrowserType.Equals("Firefox"))
                driver = new FirefoxDriver();
            
            driver.Manage().Window.Maximize();

        }
    }
}