using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;
using System.Threading;

namespace DealerOnTestV4
{
    [TestClass]
    public class UnitTest2
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
        public void TestMethod2()
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
            driver.FindElement(By.Id("isUsed")).Click();                                                                            //Find and click on the Used radio button

            driver.FindElement(By.XPath("//*[@id='isVehicleInfo']/div[1]/div/select")).Click();                                     //Find and click on the car year dropdown
            var year = driver.FindElement(By.XPath("//*[@id='isVehicleInfo']/div[1]/div/select"));                                  //Set the variable for the car year
            var selectYear = new SelectElement(year);
            System.Threading.Thread.Sleep(2000);                                                                                    //Wait to allow elements to populate if needed
            selectYear.SelectByValue("2017");                                                                                       //Select 2017 year option

            driver.FindElement(By.XPath("//*[@id='isVehicleInfo']/div[2]/div/select")).Click();                                     //Find and click on the car make dropdown
            var brand = driver.FindElement(By.XPath("//*[@id='isVehicleInfo']/div[2]/div/select"));                                 //Set the variable for the car make
            var brandOption = new SelectElement(brand);
            System.Threading.Thread.Sleep(2000);                                                                                    //Wait to allow the elements to populate if needed
            brandOption.SelectByValue("Chevrolet");                                                                                 //Select Chevrolet option

            driver.FindElement(By.XPath("//*[@id='isVehicleInfo']/div[3]/div/select")).Click();                                     //Find and click on the car model dropdown
            var model = driver.FindElement(By.XPath("//*[@id='isVehicleInfo']/div[3]/div/select"));                                 //Set the variable for the car model
            var modelOption = new SelectElement(model);
            System.Threading.Thread.Sleep(2000);                                                                                    //Wait to allow the elements to populate if needed
            modelOption.SelectByValue("Corvette");                                                                                  //Select the Covertte option

            driver.FindElement(By.Id("isSubmit")).Click();

            IList<IWebElement> list = driver.FindElements(By.ClassName("notranslate"));                                             //Finds the collection of elements related to Used Cars based on the mileage text
            Assert.IsNotNull(list.Count(), "No Used Corvettes Found!");                                                             //Make sure the list is not null if it is, throws and exception no Used cars found
            int UsedVettesCount = list.Count;                                                                                       //Assigns the list to an int variable
            for (int i = 0; i < UsedVettesCount; i++)                                                                               //For Loop to count how many used cars are available
            {
                driver.FindElements(By.ClassName("notranslate"));                                                                   //This For Loop could be used to click over a collection of check boxes as well
            }
            Console.WriteLine(value: UsedVettesCount);                                                                              //Write the count of used cars to console
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
        [TestCleanup]                                                                                                       //Clean test
        public void CleanUpTest()
        {
            try
            {
                Thread.Sleep(TimeSpan.FromSeconds(3));
                driver.Quit();                                                                                              //Close Browser
            }
            catch (Exception)
            {
                Console.WriteLine("Errors have been generated please check logs");
            }
        }
    }
}

