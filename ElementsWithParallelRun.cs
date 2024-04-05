using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
namespace Sel1
{
    //to make parallel
    [Parallelizable(ParallelScope.All)]
    public class Tests
    {
        //to run test parallaly
        public ThreadLocal<IWebDriver> driver = new ThreadLocal<IWebDriver>();

        [SetUp]
        public void Setup()
        {
            driver.Value = new ChromeDriver();
            driver.Value.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
            driver.Value.Manage().Window.Maximize();
            driver.Value.Url = "https://demoqa.com/";
        }
        [TearDown]
        public void TearDown()
        {
            driver.Value.Quit();
        }

        [Test]
        [Order(1)]
        public void HomePage()
        {
            //To check the home page
            string ch = "Selenium Online Training";
            string ch1 = driver.Value.FindElement(By.XPath("//img[@alt='Selenium Online Training']")).GetAttribute("alt");
            Assert.AreEqual(ch, ch1);
        }

        private void NavigateToElements(string element)
        {
            //To access the element.
            Thread.Sleep(3000);
            var js = (IJavaScriptExecutor)driver.Value;
            js.ExecuteScript("arguments[0].scrollIntoView(true)", driver.Value.FindElement(By.XPath("//h5[contains(text(),'Elements')]")));
            driver.Value.FindElement(By.XPath("//h5[contains(text(),'Elements')]")).Click();

            var js1 = (IJavaScriptExecutor)driver.Value;
            js1.ExecuteScript("arguments[0].scrollIntoView(true)", driver.Value.FindElement(By.XPath("//span[@class='text' and contains(text(),'" + element + "')]")));
            driver.Value.FindElement(By.XPath("//span[@class='text' and contains(text(),'" + element + "')]")).Click();
            //driver.Value.FindElement(By.XPath("//span[@class='text' and contains(text(),'"+element+"')]")).Click();

        }

        [Test]
        [Order(2)]
        public void ElementTextBOX()
        {
            NavigateToElements("Text Box");
            //To check Text box 
            string TB = "Text Box";
            string TB1 = driver.Value.FindElement(By.XPath("//h1[contains(text(),'Text Box')]")).Text;
            Assert.AreEqual(TB, TB1);

            //To assign value
            driver.Value.FindElement(By.XPath("//input[@id='userName']")).SendKeys("Girish Gaikwad");
            driver.Value.FindElement(By.XPath("//input[@id='userEmail']")).SendKeys("glitch1997@gmail.com");
            driver.Value.FindElement(By.XPath("//textarea[@id='currentAddress']")).SendKeys("LTIMindtree");
            driver.Value.FindElement(By.XPath("//textarea[@id='permanentAddress']")).SendKeys("Pimpri-Chinchwad");
            driver.Value.FindElement(By.Id("submit")).Click();

            //To check values
            string name1 = "Girish Gaikwad";
            string name = driver.Value.FindElement(By.XPath("//p[@id='name']")).Text;
            Assert.IsTrue(name.Contains(name1));

        }
        [Test]
        [Order(3)]
        public void ElementCheckBox()
        {
            NavigateToElements("Check Box");
            //To check Check Box
            string CB = "Check Box";
            string CB1 = driver.Value.FindElement(By.XPath("//h1[contains(text(),'Check Box')]")).Text;
            Assert.AreEqual(CB, CB1);

            //To Expand home
            driver.Value.FindElement(By.XPath("//button[@title='Toggle']")).Click();
            driver.Value.FindElement(By.XPath("//span[@class='rct-title' and contains(text(),'Desktop')]")).Click();

            //To check categories.
            List<string> SelectedCategories = new List<string> { "desktop", "notes", "commands" };
            IList<IWebElement> categories = driver.Value.FindElements(By.XPath("//span[@class='text-success']"));
            foreach (IWebElement category in categories)
            {
                if (SelectedCategories.Contains(category.Text))
                {
                    Console.WriteLine("Categories Selected");
                }
                else
                {
                    Console.WriteLine("Categories not Selected");
                }
            }
        }

        [Test]
        [Order(4)]
        public void ElementRadioButton()
        {
            NavigateToElements("Radio Button");
            //To check Check Box
            string RB = "Radio Button";
            string RB1 = driver.Value.FindElement(By.XPath("//h1[contains(text(),'Radio Button')]")).Text;
            Assert.AreEqual(RB, RB1);

            //To click radio button

            IWebElement element = driver.Value.FindElement(By.XPath("//label[@class='custom-control-label' and contains(text(),'Yes')]"));
            element.Click();
            string LikeSite = element.Text;

            Assert.IsTrue(driver.Value.FindElement(By.XPath("//span[@class='text-success']")).Text == LikeSite);
        }

        [Test]
        [Order(5)]
        public void ElementWebTables()
        {
            NavigateToElements("Web Tables");
            string WB = "Web Tables";
            string WB1 = driver.Value.FindElement(By.XPath("//h1[contains(text(),'Web Tables')]")).Text;
            Assert.AreEqual(WB, WB1);
            //To clieck on Add
            driver.Value.FindElement(By.XPath("//button[@id='addNewRecordButton']")).Click();
            //To switch to dialog Box
            driver.Value.SwitchTo();
            driver.Value.FindElement(By.XPath("//input[@id='firstName']")).SendKeys("Glitch");
            driver.Value.FindElement(By.Id("lastName")).SendKeys("Gaikwad");
            driver.Value.FindElement(By.Id("userEmail")).SendKeys("glitch@gmail.com");
            driver.Value.FindElement(By.Id("age")).SendKeys("49");
            driver.Value.FindElement(By.Id("salary")).SendKeys("2000000");
            driver.Value.FindElement(By.Id("department")).SendKeys("Automation Testing");
            driver.Value.FindElement(By.XPath("//button[@id='submit']")).Click();
            driver.Value.SwitchTo().DefaultContent();

            string FN = "Glitch";
            for (int i = 1; i < 10; i++)
            {
                Console.WriteLine(driver.Value.FindElement(By.XPath("//div[@class='rt-tr-group'][" + i + "]/div/div[1]")).Text);
                if (FN == driver.Value.FindElement(By.XPath("//div[@class='rt-tr-group'][" + i + "]/div/div[1]")).Text)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Entry is not present");
                }
            }
        }

        [Test]
        [Order(6)]
        public void ElementButtons()
        {
            NavigateToElements("Buttons");
            string B = "Buttons";
            string B1 = driver.Value.FindElement(By.XPath("//h1[contains(text(),'Buttons')]")).Text;
            Assert.AreEqual(B, B1);

            Actions act = new Actions(driver.Value);

            //Double click on element
            IWebElement doubleClick = driver.Value.FindElement(By.Id("doubleClickBtn"));
            act.DoubleClick(doubleClick).Perform();

            //To verify 
            string DCV1 = "You have done a double click";
            string DCV = driver.Value.FindElement(By.XPath("//p[@id='doubleClickMessage']")).Text;
            Assert.AreEqual(DCV, DCV1);

            //Right Click on Element
            IWebElement rightClick = driver.Value.FindElement(By.Id("rightClickBtn"));
            act.ContextClick(rightClick).Perform();

            //To Verify
            String RCv = "You have done a right click";
            string RCv1 = driver.Value.FindElement(By.Id("rightClickMessage")).Text;
            Assert.AreEqual(RCv, RCv1);
        }

        [Test]
        [Order(7)]
        public void ElementLink()
        {
            NavigateToElements("Links");
            string L = "Links";
            string L1 = driver.Value.FindElement(By.XPath("//h1[contains(text(),'Links')]")).Text;
            Assert.AreEqual(L, L1);

            //to verify 
            driver.Value.FindElement(By.Id("simpleLink")).Click();
            //to switch tab
            driver.Value.SwitchTo().Window(driver.Value.WindowHandles[1]);
            //to verify tab
            string ch = "Selenium Online Training";
            string ch1 = driver.Value.FindElement(By.XPath("//img[@alt='Selenium Online Training']")).GetAttribute("alt");
            Assert.AreEqual(ch, ch1);
            driver.Value.Close();

            driver.Value.SwitchTo().Window(driver.Value.WindowHandles[0]);

            Thread.Sleep(3000);
            var js = (IJavaScriptExecutor)driver.Value;
            js.ExecuteScript("arguments[0].scrollIntoView(true)", driver.Value.FindElement(By.Id("created")));
            driver.Value.FindElement(By.Id("created")).Click();

            string created = "Link has responded with staus 201 and status text Created";
            string created1 = driver.Value.FindElement(By.XPath("//p[@id='linkResponse']")).Text;
            Assert.AreEqual(created, created1);
        }

        [Test]
        [Order(8)]
        public void BrokenLink()
        {
            //Broken Links - Images
            NavigateToElements("Broken Links - Images");
            string BL = "Broken Links - Images";
            string BL1 = driver.Value.FindElement(By.XPath("//h1[contains(text(),'Broken Links - Images')]")).Text;
            Assert.AreEqual(BL, BL1);

            //to verify broken img
            string img = "https://demoqa.com/images/Toolsqa_1.jpg";
            string img1 = driver.Value.FindElement(By.XPath("//img[@src='/images/Toolsqa_1.jpg']")).GetAttribute("src");
            Assert.AreEqual(img1, img);

            //to verify broken link
            Thread.Sleep(3000);
            var js = (IJavaScriptExecutor)driver.Value;
            js.ExecuteScript("arguments[0].scrollIntoView(true)", driver.Value.FindElement(By.XPath("//a[@href='http://the-internet.herokuapp.com/status_codes/500']")));
            driver.Value.FindElement(By.XPath("//a[@href='http://the-internet.herokuapp.com/status_codes/500']")).Click();
            Thread.Sleep(3000);
            string BroL = "Status Codes";
            string Brol1 = driver.Value.FindElement(By.XPath("//h3[contains(text(),'Status Codes')]")).Text;
            Assert.AreEqual(BroL, Brol1);
        }

        [Test]
        [Order(9)]
        public void UploadDownload()
        {
            //Upload and Download
            NavigateToElements("Upload and Download");
            string UD = "Upload and Download";
            string UD1 = driver.Value.FindElement(By.XPath("//h1[contains(text(),'Upload and Download')]")).Text;
            Assert.AreEqual(UD, UD1);

            //To Download
            driver.Value.FindElement(By.Id("downloadButton")).Click();
            Thread.Sleep(5000);

            //to verify file on local 
            string path = "JPGFilePath";
            if (File.Exists(path))
            {
                //File.Delete(path);
                Console.WriteLine("File Exists");
            }
            else
            {
                Console.WriteLine("File does not exist");
            }

            //To Upload
            driver.Value.FindElement(By.Id("uploadFile")).SendKeys(path);
            //To Verify upload
            string UV = "C:\\fakepath\\sampleFile.jpeg";
            string UV1 = driver.Value.FindElement(By.Id("uploadedFilePath")).Text;
            Assert.AreEqual(UV, UV1);
            File.Delete(path);
        }

        [Test]
        [Order(10)]
        public void DynamicProperties()
        {
            //Dynamic Properties
            NavigateToElements("Dynamic Properties");
            string DP = "Dynamic Properties";
            string DP1 = driver.Value.FindElement(By.XPath("//h1[contains(text(),'Dynamic Properties')]")).Text;
            Assert.AreEqual(DP, DP1);

            //ExpectedConditions.ElementExists(By.Id("enableAfter"))
            //To verify enable button
            WebDriverWait wait = new WebDriverWait(driver.Value, TimeSpan.FromSeconds(10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("enableAfter")));
            driver.Value.FindElement(By.Id("enableAfter")).Click();

            //to verify color change
            var js1 = (IJavaScriptExecutor)driver.Value;
            js1.ExecuteScript("arguments[0].scrollIntoView(true)", driver.Value.FindElement(By.Id("colorChange")));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath("//button[@class='mt-4 text-danger btn btn-primary']")));

            //to verify visible
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("visibleAfter")));
            driver.Value.FindElement(By.Id("visibleAfter")).Click();
        }
    }
}