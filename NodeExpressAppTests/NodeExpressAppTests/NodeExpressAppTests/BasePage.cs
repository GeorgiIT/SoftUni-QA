using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using OpenQA.Selenium.Support.UI;

namespace NodeExpressAppTests
{

    public class Tests
    {
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            ChromeOptions options = new ChromeOptions();
            options.BinaryLocation = "C:\\Program Files\\Google\\Chrome\\Application\\chrome.exe";
            driver = new ChromeDriver(options);
        }

        [Test]
        public void BasePageWorkingTest()
        {
            driver.Url = "https://mvc-app-node-express-1.georgiit.repl.co/";

            Assert.AreEqual(driver.Url, "https://mvc-app-node-express-1.georgiit.repl.co/");

        }

        [Test]
        public void HeadingParagraphTest()
        {
            driver.Url = "https://mvc-app-node-express-1.georgiit.repl.co/";
            string heading = driver.FindElement(By.CssSelector("body > h1")).Text;
            Assert.AreEqual(heading, "Students Registry");
        }

        [Test]
        public void LinksCheckOnNavBar()
        {
            driver.Url = "https://mvc-app-node-express-1.georgiit.repl.co/";

            driver.FindElement(By.CssSelector("body > a:nth-child(3)")).Click();
            string StudentViewHeading = driver.FindElement(By.CssSelector("body > h1")).Text;

            Assert.AreEqual(StudentViewHeading, "Registered Students");

            driver.FindElement(By.CssSelector("body > a:nth-child(1)")).Click();

            Thread.Sleep(2000);

            string homeHeading = driver.FindElement(By.CssSelector("body > h1")).Text;
            Assert.AreEqual(homeHeading, "Students Registry");


            driver.FindElement(By.CssSelector("body > a:nth-child(5)")).Click();
            string AddStudentsHeading = driver.FindElement(By.CssSelector("body > h1")).Text;

            Thread.Sleep(2000);
            Assert.AreEqual(AddStudentsHeading, "Register New Student");



        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}