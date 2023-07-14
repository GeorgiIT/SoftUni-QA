using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.DevTools.V112.DOMDebugger;
using System.Xml.Linq;

namespace ContactBookAutomatedUI_Tests
{
    public class Tests
    {
        IWebDriver driver;
        [OneTimeSetUp]
        public void Setup()
        {
            ChromeOptions options = new ChromeOptions();
            options.BinaryLocation = "C:\\Program Files\\Google\\Chrome\\Application\\chrome.exe";
            driver = new ChromeDriver(options);
        }

        [Test]
        public void AssertThatTheFirstContactIsSteveJobs()
        {
            driver.Url = "https://contactbook.nakov.repl.co/";

            driver.FindElement(By.CssSelector("body > aside > ul > li:nth-child(2) > a")).Click();
            Thread.Sleep(1500);

            driver.FindElement(By.Id("contact1")).Click();

            Assert.AreEqual(driver.Url, "https://contactbook.nakov.repl.co/contacts/1");

            string firstName = driver.FindElement(By.CssSelector("#contact1 > tbody > tr.fname > td")).Text;
            string lastName = driver.FindElement(By.CssSelector("#contact1 > tbody > tr.lname > td")).Text;

            Assert.That(firstName == "Steve" && lastName == "Jobs");




        }

        [Test]
        public void SearchForAlbert_AndAssertThatTheFirstResultIsAlbertEinstein()
        {
            driver.Url = "https://contactbook.nakov.repl.co/";
            driver.FindElement(By.CssSelector("body > aside > ul > li:nth-child(4) > a")).Click();
            Thread.Sleep(1000);

            driver.FindElement(By.Id("keyword")).SendKeys("albert");
            driver.FindElement(By.Id("search")).Click();
            Thread.Sleep(1000);

            string firstName = driver.FindElement(By.CssSelector("#contact3 > tbody > tr.fname > td")).Text;
            string lastName = driver.FindElement(By.CssSelector("#contact3 > tbody > tr.lname > td")).Text;

            Assert.That(firstName == "Albert" && lastName == "Einstein");
        }

        [Test]
        public void InvalidSearch_ForEmptyResults()
        {
            driver.Url = "https://contactbook.nakov.repl.co/";
            driver.FindElement(By.CssSelector("body > aside > ul > li:nth-child(4) > a")).Click();
            Thread.Sleep(1000);

            driver.FindElement(By.Id("keyword")).SendKeys("invalid2635");
            driver.FindElement(By.Id("search")).Click();
            Thread.Sleep(1000);

            string result = driver.FindElement(By.Id("searchResult")).Text;

            Assert.AreEqual(result, "No contacts found.");
        }
 
        [Test]
        public void CreatingNewContactWithInvalidEmail_ExpectedForError()
        {
            driver.Url = "https://contactbook.nakov.repl.co/";
            driver.FindElement(By.CssSelector("body > aside > ul > li:nth-child(3) > a")).Click();
            Thread.Sleep(1000);

            driver.FindElement(By.Id("firstName")).SendKeys("Georgi");
            driver.FindElement(By.Id("lastName")).SendKeys("Todorov");
            driver.FindElement(By.Id("email")).SendKeys("invalidmail.com");
            driver.FindElement(By.Id("phone")).SendKeys("1234567890");
            driver.FindElement(By.Id("comments")).SendKeys("Progress need more consistent work");
            driver.FindElement(By.Id("create")).Click();
            Thread.Sleep(1000);

            string errMsg = driver.FindElement(By.CssSelector("body > main > div")).Text;

            Assert.AreEqual(errMsg, "Error: Invalid email!");

        }

        [Test]
        public void NewContact_AndProperlyAddedCheck()
        {
            driver.Url = "https://contactbook.nakov.repl.co/";
            string contactsNum = driver.FindElement(By.CssSelector("body > main > section > b")).Text;
            driver.FindElement(By.CssSelector("body > aside > ul > li:nth-child(3) > a")).Click();
            Thread.Sleep(1000);

            int number = int.Parse(contactsNum);

            driver.FindElement(By.Id("firstName")).SendKeys("Georgi");
            driver.FindElement(By.Id("lastName")).SendKeys("Todorov");
            driver.FindElement(By.Id("email")).SendKeys("valid@mail.com");
            driver.FindElement(By.Id("phone")).SendKeys("1234567890");
            driver.FindElement(By.Id("comments")).SendKeys("Progress need more consistent work");
            driver.FindElement(By.Id("create")).Click();
            Thread.Sleep(1000);

            

            string firstName = driver.FindElement(By.CssSelector($"#contact{number+1} > tbody > tr.fname > td")).Text;
            string lastName = driver.FindElement(By.CssSelector($"#contact{number+1} > tbody > tr.lname > td")).Text;

            driver.Url = $"https://contactbook.nakov.repl.co/contacts/{number + 1}";
            Assert.That(firstName == "Georgi" && lastName == "Todorov");

        }


        [OneTimeTearDown]
        public void TurnDown()
        {
            driver.Quit();
        }
    }
}