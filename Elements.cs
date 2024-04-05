using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DemoQA
{
    public class Elements
    {
        public IWebDriver driver;
        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
            driver.Manage().Window.Maximize();
            driver.Url = "https://demoqa.com/";
        }
        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
        [Test]
        public void NevigateToForms()
        {
            Thread.Sleep(3000);
            var js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoView(true)", driver.FindElement(By.XPath("//h5[contains(text(),'Forms')]")));
            driver.FindElement(By.XPath("//h5[contains(text(),'Forms')]")).Click();
        }
        private void NavigateToElements(string element)
        {
            //To access the element.
            Thread.Sleep(3000);
            var js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoView(true)", driver.FindElement(By.XPath("//h5[contains(text(),'Elements')]")));
            driver.FindElement(By.XPath("//h5[contains(text(),'Elements')]")).Click();

            var js1 = (IJavaScriptExecutor)driver;
            js1.ExecuteScript("arguments[0].scrollIntoView(true)", driver.FindElement(By.XPath("//span[@class='text' and contains(text(),'" + element + "')]")));
            driver.FindElement(By.XPath("//span[@class='text' and contains(text(),'" + element + "')]")).Click();
            //driver.Value.FindElement(By.XPath("//span[@class='text' and contains(text(),'"+element+"')]")).Click();

        }
        [Test]
        public void TestNew()
        {
            NavigateToElements("Broken Links - Images");
            string BL = "Broken Links - Images";
            string BL1 = driver.FindElement(By.XPath("//h1[contains(text(),'Broken Links - Images')]")).Text;
            Assert.AreEqual(BL, BL1);

            string hrefs = driver.FindElement(By.XPath("//a[contains(text(),'Broken Link')]")).GetAttribute("href");
            Console.WriteLine(hrefs);
            try
            {
                // Create a new HttpWebRequest
                var request = System.Net.WebRequest.Create(hrefs) as System.Net.HttpWebRequest;
                // Set the request method
                request.Method = "HEAD";
                // Get the response
                var response = request.GetResponse() as System.Net.HttpWebResponse;
                // Check if the response status code indicates success
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Console.WriteLine($"{hrefs} is working fine.");
                }
                // Close the response
                response.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{hrefs} is broken: {ex.Message}");
            }

            string imageUrl = "https://demoqa.com/images/Toolsqa_1.jpg";
            try
            {
                // Create a web request to the image URL
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(imageUrl);
                request.Method = "GET";

                // Get the response
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                // Check if the response status is successful
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    // Check if the content type is an image
                    string contentType = response.ContentType;
                    if (contentType.StartsWith("image"))
                    {
                        Console.WriteLine("Image loaded successfully");
                    }
                    else
                    {
                        Console.WriteLine("Response is not an image");
                    }
                }
                else
                {
                    Console.WriteLine("Failed to load image");
                }

                // Close the response
                response.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

    }
}
