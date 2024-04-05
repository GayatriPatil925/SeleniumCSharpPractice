using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Interactions;
using System.Xml.Linq;

namespace DemoQA
{
    public class Interaction
    {
        public IWebDriver driver;
        public IJavaScriptExecutor js;
        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
            driver.Manage().Window.Maximize();
            driver.Url = "https://demoqa.com/";
            Thread.Sleep(3000);
            js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoView(true)", driver.FindElement(By.XPath("//h5[contains(text(),'Interactions')]")));
            driver.FindElement(By.XPath("//h5[contains(text(),'Interactions')]")).Click();
        }
        private void NavigateToAlert_Frame(string element)
        {
            js.ExecuteScript("arguments[0].scrollIntoView(true)", driver.FindElement(By.XPath("//span[@class='text' and contains(text(),'" + element + "')]")));
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
        public void Droppable()
        {
            NavigateToAlert_Frame("Droppable");
            //To verify Alert 
            string AT = "Droppable";
            string AT1 = driver.FindElement(By.XPath("//h1[contains(text(),'Droppable')]")).Text;
            Assert.AreEqual(AT, AT1);

            IWebElement dragMe = driver.FindElement(By.Id("draggable"));
            IWebElement Drop = driver.FindElement(By.Id("droppable"));

            Actions actions = new Actions(driver);
            js.ExecuteScript("arguments[0].scrollIntoView(true)", dragMe);
            actions.DragAndDrop(dragMe, Drop).Perform();

            string Drop1 = driver.FindElement(By.XPath("//div[@id='droppable']/p")).Text;
            Console.WriteLine(Drop1);
        }
        [Test]
        [Order(1)]
        public void DroppableAccept()
        {
            NavigateToAlert_Frame("Droppable");
            //To verify Alert 
            string AT = "Droppable";
            string AT1 = driver.FindElement(By.XPath("//h1[contains(text(),'Droppable')]")).Text;
            Assert.AreEqual(AT, AT1);

            driver.FindElement(By.Id("droppableExample-tab-accept")).Click();
            IWebElement element = driver.FindElement(By.Id("acceptable"));
            Actions actions = new Actions(driver);
            js.ExecuteScript("arguments[0].scrollIntoView(true)", element);
            //actions.ClickAndHold(element).MoveByOffset(10,20).Perform();

            IWebElement element1 = driver.FindElement(By.Id("droppable"));
            //String s = element1.GetCssValue("background-color");
            //Console.WriteLine(s);

            actions.ClickAndHold(element).MoveToElement(element1).Release().Perform();
            IWebElement element3 = driver.FindElement(By.Id("droppable"));
            String color = element3.GetCssValue("background-color");
            Console.WriteLine(color);
        }
        [Test]
        [Order(2)]
        public void Sortable()
        {
            NavigateToAlert_Frame("Sortable");
            //To verify Alert 
            string AT = "Sortable";
            string AT1 = driver.FindElement(By.XPath("//h1[contains(text(),'Sortable')]")).Text;
            Assert.AreEqual(AT, AT1);

            /*
            IList<IWebElement> elements = driver.FindElements(By.XPath("//div[@id='demo-tabpane-list']/descendant::div[@class='list-group-item list-group-item-action']"));

            js.ExecuteScript("arguments[0].scrollIntoView(true)", elements[0]);
            var sortedele= elements.Reverse().ToList();

            Actions actions = new Actions(driver);
            var topele = sortedele[0];
            foreach(var ele  in sortedele.Skip(1))
            {
                js.ExecuteScript("arguments[0].scrollIntoView(true)", ele);
                actions.ClickAndHold(ele).MoveToElement(topele).Release().Perform();
            }
            */

            IList<IWebElement> elements = driver.FindElements(By.XPath("//div[@id='demo-tabpane-list']/descendant::div[@class='list-group-item list-group-item-action']"));
            js.ExecuteScript("arguments[0].scrollIntoView(true)", elements[0]);

            //var sortedElements = elements.OrderByDescending(element => mapping[element.Text]).ToList();

            var sortedElements = elements.Reverse().ToList();

            Actions actions = new Actions(driver);
            var topElement = sortedElements[0];
            foreach (var element in sortedElements.Skip(1))
            {
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true)", element);
                actions.ClickAndHold(element).MoveToElement(topElement).Release().Perform();
            }

        }
    }
}
