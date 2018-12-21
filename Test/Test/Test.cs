using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using System.Threading;

namespace Test
{
    [TestClass]
    public class Test
    {
        //Zmiana składu pizzy
        [TestMethod]
        public void Test1()
        {
            bool result = true;

            try
            {
                IWebDriver driver = new ChromeDriver();

                driver.Navigate().GoToUrl("http://localhost:56503/Default");

                driver.FindElement(By.Id("MainContent_LvListOffersPizza_ctrl0_BtnOfferDetails_0")).Click();
                driver.FindElement(By.Id("MainContent_GvListIngredients_CbStatus_7")).Click();
                driver.FindElement(By.Id("MainContent_GvListIngredients_CbStatus_8")).Click();
                driver.FindElement(By.Id("MainContent_GvListIngredients_CbStatus_0")).Click();
                driver.FindElement(By.Id("MainContent_GvListIngredients_CbStatus_1")).Click();

                driver.Quit();
            }
            catch (Exception)
            {
                result = false;
            }

            Assert.AreEqual(result, true);
        }

        //Składanie zamówienia
        [TestMethod]
        public void Test2()
        {
            bool result = true;

            try
            {
                IWebDriver driver = new ChromeDriver();

                driver.Navigate().GoToUrl("http://localhost:56503/Default");

                driver.FindElement(By.Id("MainContent_LvListOffersPizza_ctrl0_BtnOfferDetails_0")).Click();
                driver.FindElement(By.Id("MainContent_BtnOrder")).Click();
                driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='PLN'])[4]/following::button[1]")).Click();
                Thread.Sleep(2000);
                driver.FindElement(By.Id("MainContent_Name")).Click();
                driver.FindElement(By.Id("MainContent_Name")).Clear();
                driver.FindElement(By.Id("MainContent_Name")).SendKeys("Jan");
                driver.FindElement(By.Id("MainContent_Surname")).Clear();
                driver.FindElement(By.Id("MainContent_Surname")).SendKeys("Kowalski");
                driver.FindElement(By.Id("MainContent_Street")).Clear();
                driver.FindElement(By.Id("MainContent_Street")).SendKeys("Leszna");
                driver.FindElement(By.Id("MainContent_HouseNumber")).Clear();
                driver.FindElement(By.Id("MainContent_HouseNumber")).SendKeys("109");
                driver.FindElement(By.Id("MainContent_PostCode")).Clear();
                driver.FindElement(By.Id("MainContent_PostCode")).SendKeys("94-325");
                driver.FindElement(By.Id("MainContent_City")).Clear();
                driver.FindElement(By.Id("MainContent_City")).SendKeys("Warszawa");
                Thread.Sleep(2000);
                driver.FindElement(By.Id("MainContent_AcceptOrder")).Click();

                driver.Quit();
            }
            catch (Exception)
            {
                result = false;
            }

            Assert.AreEqual(result, true);
        }

        //Anulowanie zamówienia
        [TestMethod]
        public void Test3()
        {
            bool result = true;

            try
            {
                IWebDriver driver = new ChromeDriver();

                driver.Navigate().GoToUrl("http://localhost:56503/Default");

                driver.FindElement(By.Id("MainContent_LvListOffersPizza_ctrl0_BtnOfferDetails_0")).Click();
                driver.FindElement(By.Id("MainContent_BtnOrder")).Click();
                driver.FindElement(By.Id("MainContent_LvListOrdersPizza_ctrl0_cancelOrder_0")).Click();

                driver.Quit();
            }
            catch (Exception)
            {
                result = false;
            }

            Assert.AreEqual(result, true);
        }

        [TestMethod]
        public void Test4()
        {
            Assert.AreEqual(WebService.Data.GetCustomerById(4022).Id_Customer, 4022);
        }

        [TestMethod]
        public void Test5()
        {
            Assert.AreEqual(WebService.Data.GetListOffersPizza().Count, 8);
        }

        [TestMethod]
        public void Test6()
        {
            Assert.AreEqual(WebService.Data.SetCustomer("Test", "Test", "Test", "Test", 0, "00-000").Id_Customer, 6023);
        }
    }
}
