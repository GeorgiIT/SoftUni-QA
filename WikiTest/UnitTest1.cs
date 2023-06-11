namespace SeleniumBasicsWikiTests;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using static System.Net.WebRequestMethods;



public class Tests
{
    IWebDriver driver;


    [SetUp]
    public void Setup()
    {
        ChromeOptions options = new ChromeOptions();
        options.BinaryLocation = "/Users/georgi/Desktop/Google Chrome.app/Contents/MacOS/Google Chrome";
        driver = new ChromeDriver(options);

    }

    [Test]
    public void Test_SearchForQAinWikipedia()
    {
        driver.Url = "https://www.wikipedia.org/";
        driver.FindElement(By.CssSelector("#searchInput")).Click();
        driver.FindElement(By.CssSelector("#searchInput")).SendKeys("QA");
       

        driver.FindElement(By.CssSelector("#search-form > fieldset > button > i")).Click();

        Assert.AreEqual("https://en.wikipedia.org/wiki/QA", driver.Url);

    }
    [TearDown]
    public void TearDown()
    {
        driver.Quit();
    }
}
