using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace DealerOnTestV4
{
    [TestClass]
    public class UnitTest1
    {
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
        public void TestMethod1()
        {
            driver.Navigate().GoToUrl("https://www.rothbardchevy.com/");                                                            //Takes the driver to open rothbardchevy.com
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));                                               //wait declared on the test method to wait for elements to load
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
            driver.FindElement(By.Id("parent_2")).Click();                                                                          //Clicks on UsedCars tab

            IList<IWebElement> list = driver.FindElements(By.XPath("//*[contains(text(),'Mileage:')]"));                            //Finds the collection of elements related to Used Cars based on the mileage text
            Assert.IsNotNull(list.Count(), "No Used Cars Found!");                                                                  //Make sure the list is not null if it is, throws and exception no Used cars found
            int UsedCarsCount = list.Count;                                                                                         //Assigns the list to an int variable
            for (int i = 0; i < UsedCarsCount; i++)                                                                                 //For Loop to count how many used cars are available
            {
                driver.FindElements(By.TagName("li[mileageDisplay]"));                                                              //This For Loop could be used to click over a collection of check boxes as well
            }
            Console.WriteLine(UsedCarsCount);                                                                                       //Write the count of used cars to console
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
        public void CleanUpTest()
        {
            try
            {
                //Wait for 3 seconds to see the card populate then close the browser
                Thread.Sleep(TimeSpan.FromSeconds(3));
                driver.Quit();
            }
            catch (Exception)
            {
                Console.WriteLine("Errors have been generated please check logs");
            }
        }

    }
}

