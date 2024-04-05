using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace DemoQA
{
    public class Alert
    {
        public IWebDriver driver;
        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
            driver.Manage().Window.Maximize();
            driver.Url = "https://demoqa.com/";
            Thread.Sleep(3000);
            var js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoView(true)", driver.FindElement(By.XPath("//h5[contains(text(),'Alerts, Frame & Windows')]")));
            driver.FindElement(By.XPath("//h5[contains(text(),'Alerts, Frame & Windows')]")).Click();
        }
        private void NavigateToAlert_Frame(string element)
        {
            var js1 = (IJavaScriptExecutor)driver;
            js1.ExecuteScript("arguments[0].scrollIntoView(true)", driver.FindElement(By.XPath("//span[@class='text' and contains(text(),'" + element + "')]")));
            driver.FindElement(By.XPath("//span[@class='text' and contains(text(),'" + element + "')]")).Click();
            //driver.Value.FindElement(By.XPath("//span[@class='text' and contains(text(),'"+element+"')]")).Click();

        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
        [Test]
        [Order(0)]
        public void Alert1()
        {
            NavigateToAlert_Frame("Alerts");
            //To verify Alert 
            string AT = "Alerts";
            string AT1 = driver.FindElement(By.XPath("//h1[contains(text(),'Alerts')]")).Text;
            Assert.AreEqual(AT, AT1);

            //To handle Alert
            driver.FindElement(By.XPath("//button[@id='alertButton']")).Click();
            driver.SwitchTo().Alert().Accept();

            //To handle timerAlert
            driver.FindElement(By.XPath("//button[@id='timerAlertButton']")).Click();
            Thread.Sleep(5000);
            driver.SwitchTo().Alert().Accept();

            //To handle confirmButton Alert
            driver.FindElement(By.XPath("//button[@id='confirmButton']")).Click();
            driver.SwitchTo().Alert().Dismiss();
            //To verify cancel
            string CB = "Cancel";
            string CB1 = driver.FindElement(By.XPath("//span[@id='confirmResult']")).Text;
            Assert.IsTrue(CB1.Contains(CB));

            //To handle promtButton
            driver.FindElement(By.XPath("//button[@id='promtButton']")).Click();
            driver.SwitchTo().Alert().SendKeys("Gayatri");
            driver.SwitchTo().Alert().Accept();
            //to verify ProtButton
            string PB = "Gayatri";
            string PB1 = driver.FindElement(By.XPath("//span[@id='promptResult']")).Text;
            Assert.IsTrue(PB1.Contains(PB));
        }

        [Test]
        [Order(1)]
        public void Frames()
        {
            NavigateToAlert_Frame("Frames");
            //To verify frames
            string FR = "Frames";
            string FR1 = driver.FindElement(By.XPath("//h1[contains(text(),'Frames')]")).Text;
            Assert.AreEqual(FR, FR1);

            //To get into the frame 1
            driver.SwitchTo().Frame("frame1");
            string frameText = driver.FindElement(By.Id("sampleHeading")).Text;
            Console.WriteLine(frameText);

            //to get out of frame
            driver.SwitchTo().DefaultContent();
            string VerifyFR = driver.FindElement(By.XPath("//h1[contains(text(),'Frames')]")).Text;
            Console.WriteLine(VerifyFR);
        }

        [Test]
        [Order(2)]
        public void NestedFrame()
        {
            NavigateToAlert_Frame("Nested Frames");
            //To verify Nested frames
            string NF = "Nested Frames";
            string NF1 = driver.FindElement(By.XPath("//h1[contains(text(),'Nested Frames')]")).Text;
            Assert.AreEqual(NF, NF1);

            //To get into parent frame
            driver.SwitchTo().Frame("frame1");
            string VerifyPF = driver.FindElement(By.XPath("//*[text()='Parent frame']")).Text;
            Console.WriteLine(VerifyPF);

            //To get into child frame
            //Store the web element
            IWebElement iframe = driver.FindElement(By.XPath(" //iframe[@srcdoc='<p>Child Iframe</p>']"));
            //Switch to the frame
            driver.SwitchTo().Frame(iframe);
            string VerifyCF = driver.FindElement(By.XPath("//*[text()='Child Iframe']")).Text;
            Console.WriteLine(VerifyCF);

            //to get out of frame
            driver.SwitchTo().DefaultContent();
            string VerifyNF1 = driver.FindElement(By.XPath("//h1[contains(text(),'Nested Frames')]")).Text;
            Console.WriteLine(VerifyNF1);
        }

        [Test]
        [Order(3)]
        public void ModalDialog()
        {
            NavigateToAlert_Frame("Modal Dialogs");
            //To verify Nested frames
            string MD = "Modal Dialogs";
            string MD1 = driver.FindElement(By.XPath("//h1[contains(text(),'Modal Dialogs')]")).Text;
            Assert.AreEqual(MD, MD1);

            driver.FindElement(By.Id("showSmallModal")).Click();
            driver.FindElement(By.Id("closeSmallModal")).Click();
        }
    }
}