namespace SumNumbersSiteTests;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

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
    public void Test_WithInvalidInput()
    {
        driver.Url = "https://sum-numbers.nakov.repl.co/";

        driver.FindElement(By.CssSelector("#number1")).Click();
        driver.FindElement(By.CssSelector("#number1")).SendKeys("a");

        driver.FindElement(By.CssSelector("#number2")).Click();
        driver.FindElement(By.CssSelector("#number2")).SendKeys("b");

        driver.FindElement(By.CssSelector("#calcButton")).Click();

        string actualResult = driver.FindElement(By.CssSelector("#result > i")).Text;
        Assert.AreEqual("invalid input", actualResult);
    }

    [Test]
    public void TestWithTwoIntegers()
    {
        driver.Url = "https://sum-numbers.nakov.repl.co/";

        driver.FindElement(By.CssSelector("#number1")).Click();
        driver.FindElement(By.CssSelector("#number1")).SendKeys("5");

        driver.FindElement(By.CssSelector("#number2")).Click();
        driver.FindElement(By.CssSelector("#number2")).SendKeys("10");

        driver.FindElement(By.CssSelector("#calcButton")).Click();

        string actualResult = driver.FindElement(By.CssSelector("#result > pre")).Text;

        Assert.AreEqual("15", actualResult);
    }

    [Test]
    public void TestResetButton()
    {
        driver.Url = "https://sum-numbers.nakov.repl.co/";

        driver.FindElement(By.CssSelector("#number1")).Click();
        driver.FindElement(By.CssSelector("#number1")).SendKeys("a");

        driver.FindElement(By.CssSelector("#number2")).Click();
        driver.FindElement(By.CssSelector("#number2")).SendKeys("b");

        
        driver.FindElement(By.CssSelector("#resetButton")).Click();

        string numberOne = driver.FindElement(By.CssSelector("#number1")).Text;

        Assert.IsEmpty(numberOne);

        string numberTwo = driver.FindElement(By.CssSelector("#number2")).Text;

        Assert.IsEmpty(numberTwo);

       

    }


        [TearDown]
    public void TearDown()
    {
        driver.Quit();
    }
}
