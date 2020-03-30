using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;
using System.Text;
using System.Threading;

namespace DealerOnTestV4
{
    [TestClass]
    public class UnitTest3
    {
        private const string expected = "584-681-5189";
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private bool acceptNextAlert = true;

        [TestInitialize]
        public void SetupTest()
        {
            driver = new ChromeDriver();                //Assigns Chrome Driver to driver
            driver.Manage().Window.Maximize();          //Maximizes the driver window
            verificationErrors = new StringBuilder();   //Error verification
        }

        [TestMethod]
        public void TestMethod3()
        {
            driver.Navigate().GoToUrl("https://www.rothbardchevy.com/");                                                            //Takes the driver to open rothbardchevy.com
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));                                               //Wait declared on the test method to wait for elements to load
            try
            {
                driver.SwitchTo().Window(driver.WindowHandles.Last());                                                              //Switches the driver to the PopUp window 
                driver.FindElement(By.XPath("//img[@src='/resources/components/coupon/eas/images/coupon_no_0_1.jpg']")).Click();    //Locates and click the No Thanks button
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("No Coupon Found");                                                                               //If the coupon is not displayed, write it to console
            }
            driver.SwitchTo().Window(driver.WindowHandles.Last());                                                                  //Switches the driver to the correct page in the browser
            Actions action = new Actions(driver);                                                                                   //Initialize Hover Over Action
            IWebElement staff = driver.FindElement(By.Id("parent_6"));                                                              //Locate the target element
            action.MoveToElement(staff).Perform();                                                                                  //Action to move the mouse over the element
            driver.FindElement(By.Id("7_child_3")).Click();                                                                         //Find and click the meet our staff section   
            driver.FindElement(By.XPath("//*[@id='content']/section/div/div[2]/ul/li[3]/a")).Click();                               //Find and click the executive team tab
            string phoneNo = driver.FindElement(By.XPath("//*[@id='tab-pane-4417']/div[1]/div[2]/ul/li[3]/ul/li[2]/a")).Text;       //Find the phone number element, retrieve text and assign to variable
            try
            {
                Assert.AreEqual(expected, phoneNo);                                                                                 //Verify that the expected phone number is correct
                Console.WriteLine("The expected phone number is displayed");                                                        //Write in console if the correct number is found
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("The expected phone number is not displayed or is incorrect");                                    //Write in console if the incorrect number is found
            }

            driver.SwitchTo().Window(driver.WindowHandles.Last());                                                                  //Forces the browser to stay on the current window
            Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();                                                             //Takes a screenshot and saves the file on the Desktop
            ss.SaveAsFile("C:\\Users\\Admin\\Desktop\\EvidenceScreenshot.png");
        }
        private bool IsElementPresent(By by)                                                                                        //Check for Element exception
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
        private bool IsAlertPresent()                                                                                               //Check for Alerts Exception
        {
            try
            {
                driver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }
        private string CloseAlertAndGetItsText()                                                                                    //If there are alerts, get the text
        {
            try
            {
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert)
                {
                    alert.Accept();
                }
                else
                {
                    alert.Dismiss();
                }
                return alertText;
            }
            finally
            {
                acceptNextAlert = true;

            }
        }
        [TestCleanup]
        public void CleanUpTest()                                                                                                   //Clean test
        {
            try
            {
                Thread.Sleep(TimeSpan.FromSeconds(3));                                                                              //Close browser
                driver.Quit();
            }
            catch (Exception)
            {
                Console.WriteLine("Errors have been generated please check logs");
            }
        }
    }
}

