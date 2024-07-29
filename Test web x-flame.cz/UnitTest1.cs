using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;
using System.Diagnostics;
using System;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using System.Security.Cryptography.X509Certificates;

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
            IWebDriver driver = new FirefoxDriver(); // Adding Firefox driver
            driver.Navigate().GoToUrl("https://www.x-flame.cz/"); // Go to root url
            Thread.Sleep(1000); // Preventive pause before click
            IReadOnlyList<IWebElement> links = driver.FindElements(By.TagName("a")); // Saving all <a> elements
            int rootLinks = links.Count(); // Saving count of all <a> elements
            string actualTestingDefaultPage = driver.Url; // Saving actual driver url address
            string currentDirectory = Directory.GetCurrentDirectory(); // Saving actual directory address
            string parentDirectory = Path.Combine(currentDirectory, "..", "..", "..", ".."); // saving specific parent folder
            string filePath = Path.Combine(parentDirectory, "output.txt"); // saving file path with name of text document which will be created
            string absolutePath = Path.GetFullPath(filePath); // Get absolute path of variable filePath

            if (!File.Exists(absolutePath)) // Check if file exists in absolutePath
            {
                using (FileStream fs = File.Create(absolutePath)) // Creating file output.txt
                {
                    fs.Close(); // Closing file stream
                }
            }
            else // Else code sector
            {
                using (FileStream fs = new FileStream(absolutePath, FileMode.Truncate)) // Removing content of file
                {
                    fs.Close(); // Closing file stream
                }
            }
            for (int a = 0; a < rootLinks; a++) // Cyclus for testing all <a> elements on all web pages
            {
                if (a > 0) // After 2. cycle in this cycle end then run this codition
                {
                    driver.Navigate().GoToUrl("https://www.x-flame.cz/"); // Go to this root url
                    links = driver.FindElements(By.TagName("a")); // Save all elements <a> links
                    if ((!string.IsNullOrEmpty(links[a].GetAttribute("href"))) && (!links[a].GetAttribute("href").Contains("#")) && (links[a].Displayed) && (links[a].Enabled) && (IsElementClickable(driver, links[a]))) // Condition if element is displaying and clickable
                    {
                        links[a].Click(); // Click on element
                        actualTestingDefaultPage = driver.Url; // Save actual url address
                    }
                    else // Else code sector
                    {
                        using (StreamWriter writer = new StreamWriter(absolutePath, true)) // Start using streamwriting
                        {
                            string elementHref = links[a].GetAttribute("href"); // Save all <a> elements
                            if (elementHref == "#") // Condition if elementHref contains # and if yes the write text of probably error to file
                            {
                                writer.WriteLine("Na adrese: " + driver.Url + " Hodnota atributu \"href\" obsahuje pouze '#'"); // Save text data to file
                            }
                            else if (elementHref == null) // Condition if elementHref is null and if yes the write text of probably error to file
                            {
                                writer.WriteLine("Na adrese: " + driver.Url + " Obsah elementu není k dispozici protože neobsahuje atribut \"href\""); // Save text data to file
                            }
                            else if (string.IsNullOrEmpty(elementHref)) // Condition if elementHref is null or empty and if yes the write text of probably error to file
                            {
                                writer.WriteLine("Na adrese: " + driver.Url + " Obsah elementu není k dispozici protože Atribut \"href\" je prázdný"); // Save text data to file
                            }
                            else if (elementHref.StartsWith("javascript")) // Condition if elementHref start with javascript and if yes the write text of probably error to file
                            {
                                writer.WriteLine("Na adrese: " + driver.Url + " Obsah elementu není k dispozici protože Atribut \"href\" obsahuje JavaScript"); // Save text data to file
                            }
                            else // Else code sector
                                writer.WriteLine("Na adrese: " + driver.Url + " Nelze kliknout, mùže se jednat o chybu. Element: " + links[a].Text + " Atribut elementu: " + elementHref); // Save text data to file
                        }
                    }
                }
                int linksCount = links.Count;
                for (int i = 0; i < linksCount; i++)
                {
                    links = driver.FindElements(By.TagName("a"));

                    if ((!string.IsNullOrEmpty(links[i].GetAttribute("href"))) && (!links[i].GetAttribute("href").Contains("#")) && /*(!links[i].GetAttribute("href").Contains("https://www.x-flame.cz/cz-texty-1.html")) && (!links[i].GetAttribute("href").Contains("https://www.x-flame.cz/cz-texty-2.html")) && */(links[i].Displayed) && (links[i].Enabled)/* && IsElementClickable(driver, currentLink[i])*/ && (IsElementClickable(driver, links[i])))
                    {
                        links = driver.FindElements(By.TagName("a")); // Save all <a> tag links
                        links[i].Click(); // Click on element
                        Thread.Sleep(1000); // Pause before next click
                        driver.Navigate().GoToUrl(actualTestingDefaultPage); // Go to actual testing page url address
                    }
                    else // Else code sector
                    {
                        string elementHref = links[i].GetAttribute("href"); // save attributes of current <a> element
                        using (StreamWriter writer = new StreamWriter(absolutePath, true)) // Start using streamwriting
                        {
                            if (elementHref == "#") // Condition if elementHref contains # and if yes the write text of probably error to file
                            {
                                writer.WriteLine("Na adrese: " + driver.Url + " Hodnota atributu \"href\" obsahuje pouze '#'"); // Save text data to file
                            }
                            else if (elementHref == null) // Condition if elementHref is null and if yes the write text of probably error to file
                            {
                                writer.WriteLine("Na adrese: " + driver.Url + " Obsah elementu není k dispozici protože neobsahuje atribut \"href\""); // Save text data to file
                            }
                            else if (string.IsNullOrEmpty(elementHref)) // Condition if elementHref is null or empty and if yes the write text of probably error to file
                            {
                                writer.WriteLine("Na adrese: " + driver.Url + " Obsah elementu není k dispozici protože Atribut \"href\" je prázdný"); // Save text data to file
                            }
                            else if (elementHref.StartsWith("javascript")) // Condition if elementHref start with javascript and if yes the write text of probably error to file
                            {
                                writer.WriteLine("Na adrese: " + driver.Url + " Obsah elementu není k dispozici protože Atribut \"href\" obsahuje JavaScript"); // Save text data to file
                            }
                            else // Else code sector
                                writer.WriteLine("Na adrese: " + driver.Url + " Nelze kliknout, mùže se jednat o chybu. Element: " + links[i].Text + " Atribut elementu: " + elementHref); // Save text data to file
                        }
                    }
                    links = driver.FindElements(By.TagName("a")); // Save all <a> elements
                    linksCount = links.Count; // Updating count of links
                }
            }

            Assert.Pass();
        }
        public static bool IsElementClickable(IWebDriver driver, IWebElement element) // Method for check if is element visible on page and clickable on page
        {
            try // Try for check if element is visible and clickable if yes then return true
            {

                IJavaScriptExecutor js = (IJavaScriptExecutor)driver; // Save JavaScript driver executor

                int elementPosition = element.Location.Y; // Save actual position of element

                js.ExecuteScript($"window.scrollTo(0, {elementPosition});"); // Scrolling element into view

                System.Threading.Thread.Sleep(500); // Waiting for end of scrolling

                // Checking element visibility after scrolling
                bool isElementInView = (bool)js.ExecuteScript(
                    "var elem = arguments[0],                 " +
                    "  box = elem.getBoundingClientRect(),    " +
                    "  cx = box.left + box.width / 2,         " +
                    "  cy = box.top + box.height / 2,         " +
                    "  e = document.elementFromPoint(cx, cy); " +
                    "for (; e; e = e.parentElement) {         " +
                    "  if (e === elem)                        " +
                    "    return true;                         " +
                    "}                                        " +
                    "return false;                            ",
                    element);
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5)); // Wait before check if element is clickable (SeleniumExtras must be installed)
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(element)); // Check if element is clickable


                bool isElementInViewPort; // Contains true if element is in view port or false if not
                var location = element.Location; // Save element location
                var size = element.Size; // Save element size

                // Získá velikost okna prohlížeèe
                var windowSize = driver.Manage().Window.Size; // Save Size of Window

                // Zkontroluje, zda je element ve viditelné èásti okna prohlížeèe
                isElementInViewPort = location.Y >= 0 && location.Y + size.Height <= windowSize.Height && location.X >= 0 && location.X + size.Width <= windowSize.Width; // Check if element is in view port


                if (isElementInView && isElementInViewPort) // Condition if element is visible and if is element in view port
                    return true;
                else // Else code sector
                    return false;
            }
            catch (WebDriverTimeoutException) // Catch if try code can't be run
            {
                return false; // Return false because element can't be clickable
            }
        }
    }
}