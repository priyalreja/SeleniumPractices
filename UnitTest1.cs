using System;
using Xunit;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System.Net.Http;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Firefox;

namespace k
{
    public class UnitTest1: BaseSetup
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
            BaseSetupBrowser();
            driver.Navigate().GoToUrl(@"http://the-internet.herokuapp.com/drag_and_drop");
           
           //Element which needs to drag and dropped to.	
        	IWebElement drag = driver.FindElement(By.Id("column-a"));		
            IWebElement drop = driver.FindElement(By.Id("column-b"));		
         	var location = drop.Location;
             var size = drop.Size;
            //Using Action class for drag and drop.		
            Actions act=new Actions(driver);	
            var offsetX = location.X + (size.Width/2);
            var offsetY = location.Y + (size.Height/2);

            //act.DragAndDrop(drag,drop).Build().Perform();
          // act.DragAndDropToOffset(drag, offsetX, offsetY).Build().Perform();
         //  Assert.Equal(drop.Text, "A");
           act.ClickAndHold(drag).MoveByOffset(offsetX, offsetY).Build().Perform();
           act.Release(drop).Perform(); 
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
        public void NestedFrames() {
            // Act
            var driver = new ChromeDriver();
            driver.Navigate().GoToUrl(@"http://the-internet.herokuapp.com/nested_frames");
                                 
            /**************** Switching to the Left Frame ****************/
            driver.SwitchTo().ParentFrame();
            driver.SwitchTo().Frame(driver.FindElement(By.CssSelector("[name = 'frame-top']")));
            driver.SwitchTo().Frame(driver.FindElement(By.CssSelector("[name = 'frame-left']")));
 
            IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)driver;
            var currentFrame = jsExecutor.ExecuteScript("return self.name");
            Console.WriteLine(currentFrame);     

            /**************** Switching to the Middle Frame ****************/
            driver.SwitchTo().ParentFrame();
            driver.SwitchTo().Frame(driver.FindElement(By.CssSelector("[name = 'frame-middle']")));
 
            jsExecutor = (IJavaScriptExecutor)driver;
            currentFrame = jsExecutor.ExecuteScript("return self.name");
            Console.WriteLine(currentFrame);

            /**************** Switching to the Right Frame ****************/
            driver.SwitchTo().ParentFrame();
            driver.SwitchTo().Frame(driver.FindElement(By.CssSelector("[name = 'frame-right']")));
 
            jsExecutor = (IJavaScriptExecutor)driver;
            currentFrame = jsExecutor.ExecuteScript("return self.name");
            Console.WriteLine(currentFrame);

            /**************** Switching to the Bottom Frame ****************/
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(By.CssSelector("[name = 'frame-bottom']")));
 
            jsExecutor = (IJavaScriptExecutor)driver;
            currentFrame = jsExecutor.ExecuteScript("return self.name");
            Console.WriteLine(currentFrame);
            driver.Close();
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

        [Fact]
        public void ChalllengingDOM() {
            // Act
            var driver = new ChromeDriver(); 
            driver.Navigate().GoToUrl(@"http://the-internet.herokuapp.com/challenging_dom");
            driver.Manage().Window.Maximize();
            
            IWebElement editButton = driver.FindElement(By.XPath("//td[text()='Definiebas4']/..//a[@href='#edit'][last()]"));
            Assert.NotNull(editButton);

            IWebElement deleteButton = driver.FindElement(By.XPath("//td[text()='Definiebas4']/../td[last()]/a[@href='#delete']"));
            Assert.NotNull(deleteButton);
            driver.Close();
        }
        [Fact]
        public void InfiniteScroll() {
            // Act
            var driver = new ChromeDriver();
            driver.Navigate().GoToUrl(@"http://the-internet.herokuapp.com/infinite_scroll");
            driver.Manage().Window.Maximize();

            //Action
            int OriginalCount = driver.FindElements(By.XPath("//div[@class='jscroll-added']")).Count;

            IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
            js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
            int IncrementedCount = driver.FindElements(By.XPath("//div[@class='jscroll-added']")).Count;
            Assert.NotEqual(OriginalCount, IncrementedCount);
            driver.Close();
        }

        [Fact]
        public void notificationMessages()
        {
            //Act
            var driver = new ChromeDriver();
            driver.Navigate().GoToUrl(@"http://the-internet.herokuapp.com/notification_message_rendered");
            driver.Manage().Window.Maximize();

            //Action
            driver.FindElement(By.XPath("//a[@href='/notification_message']")).Click();

            //TearDown
            driver.Close();
        }
        [Fact]
        public void FileDownload()
        {
            //Act
            var driver = new ChromeDriver();
            driver.Navigate().GoToUrl(@"http://the-internet.herokuapp.com/download");
            driver.Manage().Window.Maximize();

            //Action
            driver.FindElement(By.XPath("//*[@id='content']/div/a[1]")).Click();            
        }
          [Fact]
        public void FileUpload()
        {
            //Act
            var driver = new ChromeDriver();
            driver.Navigate().GoToUrl(@"http://the-internet.herokuapp.com/upload");
            driver.Manage().Window.Maximize();

            var fileUpload1 = driver.FindElement(By.XPath("//input[@id='file-upload']"));
            fileUpload1.SendKeys("C:\\Users\\priyalr\\Downloads\\luminoslogo.png");

            driver.FindElement(By.XPath("//input[@class='button']")).Click();
        }

        [Fact]
        public void JqueryMenus()
        {
            //Act
            var driver = new ChromeDriver();
            driver.Navigate().GoToUrl(@"http://the-internet.herokuapp.com/jqueryui/menu#");
            driver.Manage().Window.Maximize();

            //Action
            var enabled = driver.FindElement(By.XPath("//*[@id='ui-id-2']"));
            enabled.Click();

            var downloads = driver.FindElement(By.XPath("//*[@id='ui-id-4']"));
            downloads.Click();
        }

        [Fact]
        public void ShadowDOM()
        {
            //Act
            var driver = new ChromeDriver();
            driver.Navigate().GoToUrl(@"http://the-internet.herokuapp.com/shadowdom");
            driver.Manage().Window.Maximize();

            //Action
            var block1= driver.FindElement(By.XPath("//div[@id='content']/my-paragraph[1]/span")).Text;
            Console.WriteLine(block1);

            var block2= driver.FindElement(By.XPath("//div[@id='content']/my-paragraph[2]/ul")).Text;
            Console.WriteLine(block2);
        }
        
        [Fact]
        public void testCorrectLogin() {
            BaseSetupBrowser();
            driver.Navigate().GoToUrl(@"http://the-internet.herokuapp.com/login");

            LoginPage loginPage = new LoginPage(driver);
            loginPage.loginValidUser("tomsmith", "SuperSecretPassword!");
            var successLoginText = driver.FindElement(By.XPath("//div[@class='flash success']")).Displayed;
            Assert.True(successLoginText);
            driver.Close();
        }

        [Fact]
        public void testIncorrectLogin() {
            BaseSetupBrowser();
            driver.Navigate().GoToUrl(@"http://the-internet.herokuapp.com/login");

            LoginPage loginPage = new LoginPage(driver);
            loginPage.loginValidUser("tomsmith", "IncorrectPassword");
            var failedLoginText = driver.FindElement(By.XPath("//div[@class='flash error']")).Displayed;
            Assert.True(failedLoginText);
            driver.Close();
        }

        // public class SignInPage {
        //     protected IWebDriver driver;
        //     private By usernameBy = By.Id("username");
        //     private By passwordBy = By.Id("password");
        //     private By signinBy = By.ClassName("radius");

        //     public SignInPage(IWebDriver driver){
        //         this.driver = driver;
        //     }

        //     public void loginValidUser(String userName, String password) {
        //         driver.FindElement(usernameBy).SendKeys(userName);
        //         driver.FindElement(passwordBy).SendKeys(password);
        //         driver.FindElement(signinBy).Click();
        //     }
        // }

        [Fact]
        public void DragAndDropUsingJS(){		
            // Act
            BaseSetupBrowser();
            driver.Navigate().GoToUrl(@"http://the-internet.herokuapp.com/drag_and_drop");

            IWebElement LocatorFrom = driver.FindElement(By.Id("column-a"));
            IWebElement LocatorTo = driver.FindElement(By.Id("column-b"));
            String xto = LocatorTo.Location.X.ToString();
            String yto = LocatorTo.Location.Y.ToString();
            ((IJavaScriptExecutor)driver).ExecuteScript("function simulate(f,c,d,e){var b,a=null;for(b in eventMatchers)if(eventMatchers[b].test(c)){a=b;break}if(!a)return!1;document.createEvent?(b=document.createEvent(a),a==\"HTMLEvents\"?b.initEvent(c,!0,!0):b.initMouseEvent(c,!0,!0,document.defaultView,0,d,e,d,e,!1,!1,!1,!1,0,null),f.dispatchEvent(b)):(a=document.createEventObject(),a.detail=0,a.screenX=d,a.screenY=e,a.clientX=d,a.clientY=e,a.ctrlKey=!1,a.altKey=!1,a.shiftKey=!1,a.metaKey=!1,a.button=1,f.fireEvent(\"on\"+c,a));return!0} var eventMatchers={HTMLEvents:/^(?:load|unload|abort|error|select|change|submit|reset|focus|blur|resize|scroll)$/,MouseEvents:/^(?:click|dblclick|mouse(?:down|up|over|move|out))$/}; " +
            "simulate(arguments[0],\"mousedown\",0,0); simulate(arguments[0],\"mousemove\",arguments[1],arguments[2]); simulate(arguments[0],\"mouseup\",arguments[1],arguments[2]); ", LocatorFrom,xto,yto);
        }
    }
}

