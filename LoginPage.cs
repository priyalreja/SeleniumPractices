using System;
using OpenQA.Selenium;

namespace k
{
     public class LoginPage {
            protected IWebDriver driver;
            private By usernameBy = By.Id("username");
            private By passwordBy = By.Id("password");
            private By signinBy = By.ClassName("radius");

            public LoginPage(IWebDriver driver){
                this.driver = driver;
            }

            public void loginValidUser(String userName, String password) {
                driver.FindElement(usernameBy).SendKeys(userName);
                driver.FindElement(passwordBy).SendKeys(password);
                driver.FindElement(signinBy).Click();
            }
        }
}