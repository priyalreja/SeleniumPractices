using System;
using Xunit;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System.Net.Http;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;


namespace k
{
    public class UnitTest1
    {
        [Fact]
        public void ShowDeleteButton()
        {
            var driver = new ChromeDriver();
            driver.Navigate().GoToUrl(@"http://the-internet.herokuapp.com/add_remove_elements/");
            IWebElement element = driver.FindElement(By.TagName("button"));
            element.Click();
            var deleteButton = driver.FindElement(By.CssSelector(".added-manually"));
            Assert.NotNull(deleteButton);
        }
        [Fact]
        public void HideDeleteButton()
        {
            // Act
            var driver = new ChromeDriver();
            driver.Navigate().GoToUrl(@"http://the-internet.herokuapp.com/add_remove_elements/");
            IWebElement element = driver.FindElement(By.TagName("button"));
            element.Click();
            var deleteButton = driver.FindElement(By.CssSelector(".added-manually"));
            Assert.NotNull(deleteButton);
            // Action
            deleteButton.Click();
            // Assert
            Assert.Throws<OpenQA.Selenium.NoSuchElementException>(
                () => driver.FindElement(By.CssSelector(".added-manually")));
        }
    
        [Fact]
        public void AuthPopup() {
            // Act
            var driver = new ChromeDriver();
            driver.Navigate().GoToUrl(@"http://the-internet.herokuapp.com/basic_auth");
            driver.SwitchTo().Alert().SendKeys("admin");
            driver.SwitchTo().Alert().SendKeys(Keys.Tab);
            driver.SwitchTo().Alert().SendKeys("admin");
            driver.SwitchTo().Alert().SendKeys(Keys.Enter);
        } 
        
        [Fact]
        public void TestBrokenImages() {
            // Act
            var driver = new ChromeDriver();
            driver.Navigate().GoToUrl(@"http://the-internet.herokuapp.com/broken_images");
            var images = driver.FindElementsByTagName("img");
            var client = new HttpClient();
            // Action
            foreach (var image in images)
            {
                var src = image.GetProperty("src");
                var result = client.GetAsync(src).GetAwaiter().GetResult();
                // Assert
                Assert.True(result.StatusCode == System.Net.HttpStatusCode.OK);
            }
        }
        [Fact]
        public void ShowCheckboxes() {
            // Act
            var driver = new ChromeDriver();
            driver.Navigate().GoToUrl(@"http://the-internet.herokuapp.com/checkboxes");
            IWebElement elementLocator1 = driver.FindElement(By.XPath("//input[@type='checkbox'][1]"));
            IWebElement elementLocator2 = driver.FindElement(By.XPath("//*[@id='checkboxes']/input[2]"));
            
            // Action
            elementLocator1.Click();         
          //  driver.Close();
        }
        [Fact]
        public void showContextMenu() {
            // Act
            var driver = new ChromeDriver();
            driver.Navigate().GoToUrl(@"http://the-internet.herokuapp.com/context_menu");
            IWebElement elementLocator = driver.FindElement(By.XPath("//*[@id='hot-spot']"));
            Actions actions = new Actions(driver);
            
            // Action
            actions.ContextClick(elementLocator).Perform();
            driver.Close();
        }
        [Fact]
        public void DragAndDrop() {
            // Act
            var driver = new ChromeDriver();
            driver.Navigate().GoToUrl(@"http://the-internet.herokuapp.com/drag_and_drop");
           //Element which needs to drag.    		
        	IWebElement From=driver.FindElement(By.XPath("//*[@id='column-a']"));	
         
            //Element on which need to drop.		
            IWebElement To=driver.FindElement(By.XPath("//*[@id='column-b']"));		
         		
            //Using Action class for drag and drop.		
            Actions act=new Actions(driver);					

	        //Dragged and dropped.	
            act.ClickAndHold(From).MoveToElement(To).Release(To).Build().Perform();
            
           // act.DragAndDrop(From, To).Build().Perform();	
        }
        [Fact]
        public void Dropdown() {
            // Act
            var driver = new ChromeDriver();
            driver.Navigate().GoToUrl(@"http://the-internet.herokuapp.com/dropdown");
        	IWebElement element=driver.FindElement(By.Id("dropdown"));	
            SelectElement oSelect = new SelectElement(element);	

            oSelect.SelectByValue("1");
    
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
            oSelect.SelectByValue("2");
            driver.Close();
        }
                
        [Fact]
        public void DynamicControls() {
            // Act
            var driver = new ChromeDriver();
            driver.Navigate().GoToUrl(@"http://the-internet.herokuapp.com/dynamic_controls");
        	IWebElement checkbox=driver.FindElement(By.XPath("//*[@id='checkbox']/input"));	
            var Button = driver.FindElement(By.XPath("//*[@id='checkbox-example']/button"));
            if(checkbox.Displayed)
            {
                if(checkbox.Selected)
                {
                    //selected checkbox and remove checkbox by clicking on remove button
                    Button.Click();
                }
                else
                {
                    //select and remove checkbox using the button
                    checkbox.Click();
                    Button.Click();
                }
            }
            else
            {
                Button.Click();
            }
          //  driver.Close();
        }
        [Fact]
        public void LoginPage() {
            // Act
            var driver = new ChromeDriver();
            driver.Navigate().GoToUrl(@"http://the-internet.herokuapp.com/login");
        	IWebElement userName=driver.FindElement(By.Id("username"));	
            IWebElement password=driver.FindElement(By.Id("password"));	
            var loginButton = driver.FindElement(By.XPath("//*[@id='login']/button"));
            userName.SendKeys("tomsmith");
            password.SendKeys("SuperSecretPassword!");
            loginButton.Click();
        }   

        
        [Fact]
        public void StatusCode() {
            // Act
            var driver = new ChromeDriver();
            driver.Navigate().GoToUrl(@"http://the-internet.herokuapp.com/status_codes");
        	IWebElement DetailsOfStatusCode = driver.FindElement(By.XPath("//*[@id='content']/div/p[1]/a"));
            
            //Action
            if(DetailsOfStatusCode.Displayed)
                DetailsOfStatusCode.Click();

            driver.Navigate().Back();
            IWebElement StatusCode1=driver.FindElement(By.XPath("//*[@id='content']/div/ul/li[1]/a"));
            StatusCode1.Click();
            IWebElement ListOfStatusCode = driver.FindElement(By.XPath("//*[@id='content']/div/p/a"));
            ListOfStatusCode.Click();

            IWebElement StatusCode2=driver.FindElement(By.XPath("//*[@id='content']/div/ul/li[2]/a"));	
            StatusCode2.Click();
            ListOfStatusCode = driver.FindElement(By.XPath("//*[@id='content']/div/p/a"));
            ListOfStatusCode.Click();

            IWebElement StatusCode3=driver.FindElement(By.XPath("//*[@id='content']/div/ul/li[3]/a"));	
            StatusCode3.Click();
            ListOfStatusCode = driver.FindElement(By.XPath("//*[@id='content']/div/p/a"));
            ListOfStatusCode.Click();

            IWebElement StatusCode4=driver.FindElement(By.XPath("//*[@id='content']/div/ul/li[4]/a"));	
            StatusCode4.Click();
            ListOfStatusCode = driver.FindElement(By.XPath("//*[@id='content']/div/p/a"));
            ListOfStatusCode.Click();

            driver.Close();
        }   

         [Fact]
        public void ControlKeyPress() {
            // Act
            var driver = new ChromeDriver();
            driver.Navigate().GoToUrl(@"http://the-internet.herokuapp.com/key_presses");
        	IWebElement TextBox = driver.FindElement(By.XPath("//*[@id='target']"));
            
            TextBox.SendKeys(Keys.ArrowDown);
            TextBox.SendKeys(Keys.Home);
            TextBox.SendKeys(Keys.End);
            TextBox.SendKeys(Keys.Enter);
            TextBox.SendKeys("A");
            TextBox.SendKeys("u");
            TextBox.SendKeys("I");
            TextBox.SendKeys("O");

            driver.Close();
        }   

        [Fact]
        public void MultipleWindows() {
            // Act
            var driver = new ChromeDriver();
            driver.Navigate().GoToUrl(@"http://the-internet.herokuapp.com/windows");
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        	IWebElement clickHere = driver.FindElement(By.XPath("//*[@id='content']/div/a"));
            
            //Action
            string originalWindow = driver.CurrentWindowHandle;
            Assert.Equal(driver.WindowHandles.Count, 1);

            clickHere.Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(300);
                  
            foreach(string window in driver.WindowHandles)
            {
                if(originalWindow != window)
                {
                    driver.SwitchTo().Window(originalWindow);
                    break;
                }
            }
        }  

        [Fact]
        public void JavascriptsAlerts() {
            // Act
            var driver = new ChromeDriver();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));;
            driver.Navigate().GoToUrl(@"http://the-internet.herokuapp.com/javascript_alerts");
            driver.FindElement(By.XPath("//*[@id='content']/div/ul/li[1]/button")).Click();
            IAlert alert = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.AlertIsPresent());
            alert.Accept();

            driver.FindElement(By.XPath("//*[@id='content']/div/ul/li[2]/button")).Click();
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.AlertIsPresent());
            alert = driver.SwitchTo().Alert();
            alert.Dismiss();

            driver.FindElement(By.XPath("//*[@id='content']/div/ul/li[3]/button")).Click();
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.AlertIsPresent());
            alert.SendKeys("Thank You!");
            alert.Accept();

            driver.Close();

        }  

        [Fact]
        public void MouseHover() {
            // Act
            var driver = new ChromeDriver();
            driver.Navigate().GoToUrl(@"http://the-internet.herokuapp.com/hovers");
           //Element which needs to drag.    		
        	IWebElement img1=driver.FindElement(By.XPath("//*[@id='content']/div/div[1]/img"));	
         
            //Element on which need to drop.		
            IWebElement img2=driver.FindElement(By.XPath("//*[@id='content']/div/div[2]/img"));		
         		
            //Using Action class for drag and drop.		
            Actions act=new Actions(driver);					

	        //Dragged and dropped.	
            act.MoveToElement(img2).Perform();
        }

        [Fact]
        public void InfiniteScroll() {
            // Act
            var driver = new ChromeDriver();
            driver.Navigate().GoToUrl(@"http://the-internet.herokuapp.com/infinite_scroll");
           
           IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
            js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");
            
        }

        [Fact]
        public void NestedFrames() {
            // Act
            var driver = new ChromeDriver();
            driver.Navigate().GoToUrl(@"http://the-internet.herokuapp.com/nested_frames");
                                 
            /**************** Switching to the Left Frame ****************/
            //Select Parent frame
            driver.SwitchTo().ParentFrame();
            //Selecting top Frameset
            driver.SwitchTo().Frame(driver.FindElement(By.CssSelector("[name = 'frame-top']")));
            //Left Frame
            driver.SwitchTo().Frame(driver.FindElement(By.CssSelector("[name = 'frame-left']")));
 
            IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)driver;
            var currentFrame = jsExecutor.ExecuteScript("return self.name");
            Console.WriteLine(currentFrame);     
        }
        [Fact]
        public void Frames() {
            // Act
            var driver = new ChromeDriver();
            driver.Navigate().GoToUrl(@"http://the-internet.herokuapp.com/frames");
                      
            //Store the web element
            IWebElement iframeLink = driver.FindElement(By.XPath("//*[@id='content']/div/ul/li[2]/a"));
            iframeLink.Click();

            //Switch to the frame
            IWebElement bodyFrame = driver.FindElement(By.Id("mce_0_ifr"));
            driver.SwitchTo().Frame(bodyFrame);
            bodyFrame = driver.FindElement(By.XPath("//*[@id='tinymce']/p"));
            bodyFrame.Clear();
            bodyFrame.SendKeys("This is a Testing Text");
            driver.Close();
        }
    }
}


