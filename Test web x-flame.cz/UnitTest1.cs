using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;

namespace Test_web_x_flame.cz
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            IWebDriver driver = new FirefoxDriver();
            driver.Navigate().GoToUrl("https://www.x-flame.cz/");
            Thread.Sleep(1000);
            IReadOnlyList<IWebElement> links = driver.FindElements(By.TagName("a"));
            int rootLinks = links.Count();
            string actualTestingDefaultPage = driver.Url;

            for (int a = 0; a < rootLinks; a++)
            {
                if (a > 0)
                {
                    driver.Navigate().GoToUrl("https://www.x-flame.cz/");
                    links = driver.FindElements(By.TagName("a"));
                    if ((!string.IsNullOrEmpty(links[a].GetAttribute("href"))) && (!links[a].GetAttribute("href").Contains("#")) && (links[a].Displayed))
                    {
                        links[a].Click();
                        actualTestingDefaultPage = driver.Url;
                    }
                    else
                        break;
                }
                for (int i = 0; i < links.Count; i++)
                {
                    links = driver.FindElements(By.TagName("a"));
                    if ((!string.IsNullOrEmpty(links[i].GetAttribute("href"))) && (!links[i].GetAttribute("href").Contains("#")) && /*(!links[i].GetAttribute("href").Contains("https://www.x-flame.cz/cz-texty-1.html")) && (!links[i].GetAttribute("href").Contains("https://www.x-flame.cz/cz-texty-2.html")) && */(links[i].Displayed))
                    {
                        links[i].Click();
                        Thread.Sleep(1000);
                        driver.Navigate().GoToUrl(actualTestingDefaultPage);
                        links = driver.FindElements(By.TagName("a"));
                    }
                    if (i == links.Count - 1)
                        driver.Navigate().GoToUrl("https://www.x-flame.cz/");
                }
            }
            Assert.Pass();
        }
    }
}