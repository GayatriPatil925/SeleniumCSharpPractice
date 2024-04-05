using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace DemoQA
{
    public class Widgets
    {
        public IWebDriver driver;
        public IJavaScriptExecutor js;
        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);
            driver.Manage().Window.Maximize();
            driver.Url = "https://demoqa.com/";
            Thread.Sleep(3000);
            js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoView(true)", driver.FindElement(By.XPath("//h5[contains(text(),'Widgets')]")));
            driver.FindElement(By.XPath("//h5[contains(text(),'Widgets')]")).Click();
        }
        private void NavigateToWidgets(string element)
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
        public void Accodian()
        {
            NavigateToWidgets("Accordian");
            //To verify Accordian 
            string AC = "Accordian";
            string AC1 = driver.FindElement(By.XPath("//h1[contains(text(),'Accordian')]")).Text;
            Assert.AreEqual(AC, AC1);

            //To create list of cards
            IList<IWebElement> Cards = driver.FindElements(By.XPath("//div[@class='card']"));
            //To create list title
            List<string> title = new List<string> { "What is Lorem Ipsum?", "Where does it come from?", "Why do we use it?" };
            //To create list of Card_body
            List<string> Card_Body = new List<string> { "Lorem Ipsum is simply", "Contrary to popular belief", "It is a long established" };
            //Loop on list
            foreach (var ele in Cards)
            {
                for (int i = 0; i < title.Count; i++)
                {
                    //to check title is present or not
                    if (ele.Text.Contains(title[i]))
                    {
                        //if card body is available or not
                        if (!ele.Text.Contains(Card_Body[i]))
                        {
                            js.ExecuteScript("arguments[0].scrollIntoView(true)", ele);
                            //if cardbody is not available it will click on title
                            ele.Click();
                            //find element on list by using class name.
                            string para = ele.FindElement(By.ClassName("card-body")).Text;
                            Console.WriteLine(para);
                        }
                        else
                        {
                            Console.WriteLine(ele.Text);
                        }
                    }
                }
            }
        }
        [Test]
        [Order(1)]
        public void DatePicker()
        {
            NavigateToWidgets("Date Picker");
            //To verify Date Picker 
            string DP = "Date Picker";
            string DP1 = driver.FindElement(By.XPath("//h1[contains(text(),'Date Picker')]")).Text;
            Assert.AreEqual(DP, DP1);

            Data("July", "1997", "4th");
            DataOfDate_Time("July", "1897", "4th", "00:00");
        }

        public void Data(string month, string year, string date)
        {
            //For Date only
            driver.FindElement(By.Id("datePickerMonthYearInput")).Click();
            //To select month   
            IWebElement month_Ele = driver.FindElement(By.ClassName("react-datepicker__month-select"));
            //Select class used for dropdown
            SelectElement MH = new SelectElement(month_Ele);
            MH.SelectByText(month);

            //To select year dropdown
            IWebElement year_Ele = driver.FindElement(By.ClassName("react-datepicker__year-select"));
            //Select class used foe dropdown
            SelectElement ye = new SelectElement(year_Ele);
            ye.SelectByText(year);

            driver.FindElement(By.XPath($"//div[contains(@aria-label,'{month} {date}, {year}')]")).Click();
        }

        public void DataOfDate_Time(string month, string year, string date, string time)
        {
            //For Date and Time
            driver.FindElement(By.Id("dateAndTimePickerInput")).Click();
            //to click on month dropdown
            driver.FindElement(By.ClassName("react-datepicker__month-read-view--selected-month")).Click();
            //to select month in dropdown
            driver.FindElement(By.XPath($"//div[@class='react-datepicker__month-option' and text()='{month}']")).Click();
            //to click on year dropdown
            driver.FindElement(By.ClassName("react-datepicker__year-read-view--selected-year")).Click();
            //to select year in dropdown


            bool year_check = false;
            while (!year_check)
            {
                try
                {
                    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(2));
                    IWebElement select_year = driver.FindElement(By.XPath($"//div[@class='react-datepicker__year-option' and text()='{year}']"));
                    select_year.Click();
                    year_check = true;

                }
                catch
                {
                    if (Convert.ToInt32(year) > 2024)
                    {
                        driver.FindElement(By.XPath("//a[contains(@class,'react-datepicker__navigation--years-upcoming')]")).Click();
                    }
                    else
                    {
                        js.ExecuteScript("arguments[0].scrollIntoView(true)", driver.FindElement(By.XPath("//a[contains(@class,'react-datepicker__navigation--years-previous')]")));
                        driver.FindElement(By.XPath("//a[contains(@class,'react-datepicker__navigation--years-previous')]")).Click();
                    }
                }
            }
            driver.FindElement(By.XPath($"//div[contains(@aria-label,'{month} {date}, {year}')]")).Click();
            driver.FindElement(By.XPath($"//li[@class='react-datepicker__time-list-item ' and text()='{time}']")).Click();
        }
        [Test]
        [Order(2)]
        public void ProgressBar()
        {
            NavigateToWidgets("Progress Bar");
            //To verify Progress Bar 
            string PB = "Progress Bar";
            string PB1 = driver.FindElement(By.XPath("//h1[contains(text(),'Progress Bar')]")).Text;
            Assert.AreEqual(PB, PB1);

            IWebElement startOrStop = driver.FindElement(By.Id("startStopButton"));
            startOrStop.Click();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromMinutes(20))
            {
                PollingInterval = TimeSpan.FromMilliseconds(10),
                Message = "No such element found, continuing to try...."
            };
            wait.Until(driver =>
            {
                IWebElement progressBar = driver.FindElement(By.XPath("//div[@role='progressbar']"));
                string progressValue = progressBar.GetAttribute("aria-valuenow");
                return progressValue == "21"; // Change "70" to your desired value
            });
            startOrStop.Click();
        }
        [Test]
        [Order(3)]
        public void Tool_Tips()
        {
            NavigateToWidgets("Tool Tips");
            //To verify Tabs 
            string TT = "Tool Tips";
            string TT1 = driver.FindElement(By.XPath("//h1[contains(text(),'Tool Tips')]")).Text;
            Assert.AreEqual(TT, TT1);

            Actions ac = new Actions(driver);
            IWebElement Perf = driver.FindElement(By.Id("toolTipButton"));
            ac.MoveToElement(Perf).Perform();
            string Hover_text = driver.FindElement(By.XPath("//div[contains(@class,'bs-tooltip-right')]")).Text;
            Console.WriteLine(Hover_text);
        }
        [Test]
        [Order(4)]
        public void Slider()
        {
            NavigateToWidgets("Slider");
            //To verify Slider 
            string Sd = "Slider";
            string Sd1 = driver.FindElement(By.XPath("//h1[contains(text(),'Slider')]")).Text;
            Assert.AreEqual(Sd, Sd1);

            IWebElement slider = driver.FindElement(By.XPath("//input[@class=\"range-slider range-slider--primary\"]"));
            int SliderValue = 5;
            bool con = false;
            while (!con)
            {
                if (SliderValue > 25)
                {
                    slider.SendKeys(Keys.ArrowRight);
                }
                else
                {
                    slider.SendKeys(Keys.ArrowLeft);
                }
                if (slider.GetAttribute("value") == Convert.ToString(SliderValue))
                {
                    con = true;
                }
            }

            string veriflySlider = driver.FindElement(By.Id("sliderValue")).GetAttribute("value");
            Assert.AreEqual(SliderValue.ToString(), veriflySlider);

        }
        [Test]
        [Order(5)]
        public void Menu()
        {
            NavigateToWidgets("Menu");
            //To verify Menu 
            string Tb = "Menu";
            string tb1 = driver.FindElement(By.XPath("//h1[contains(text(),'Menu')]")).Text;
            Assert.AreEqual(Tb, tb1);

            Actions menu = new Actions(driver);
            menu.MoveToElement(MenuHelper("Main Item 2")).Perform();
            js.ExecuteScript("arguments[0].scrollIntoView(true)", MenuHelper("SUB SUB LIST"));
            menu.MoveToElement(MenuHelper("SUB SUB LIST")).Perform();
            js.ExecuteScript("arguments[0].scrollIntoView(true)", MenuHelper("Sub Sub Item 2"));
            menu.MoveToElement(MenuHelper("Sub Sub Item 2")).Perform();
        }

        public IWebElement MenuHelper(string ele)
        {
            IWebElement perf = driver.FindElement(By.XPath("//a[contains(text(),'" + ele + "')]"));
            return perf;
        }

        [Test]
        [Order(6)]
        public void Select_Menu()
        {
            NavigateToWidgets("Select Menu");
            //To verify Select Menu 
            string Tb = "Select Menu";
            string tb1 = driver.FindElement(By.XPath("//h1[contains(text(),'Select Menu')]")).Text;
            Assert.AreEqual(Tb, tb1);

            js.ExecuteScript("arguments[0].scrollIntoView(true)", driver.FindElement(By.XPath("//div[@class=' css-1wa3eu0-placeholder' and text()='Select Title']")));
            driver.FindElement(By.XPath("//div[@class=' css-1wa3eu0-placeholder' and text()='Select Title']")).Click();
            Thread.Sleep(1000);
            IWebElement title = driver.FindElement(By.Id("react-select-3-input"));
            title.SendKeys("Ms.");
            title.SendKeys(Keys.Enter);

           
           //To select color   
           IWebElement  color= driver.FindElement(By.Id("oldSelectMenu"));
            js.ExecuteScript("arguments[0].scrollIntoView(true)",color);
           //Select class used for dropdown
           SelectElement MH = new SelectElement(color);
            MH.SelectByText("Black");

        }
    }
}

