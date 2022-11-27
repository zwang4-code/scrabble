using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium.Windows;
using System.Threading;
using OpenQA.Selenium;
using System.Windows.Controls;

namespace ScrabbleAppiumTest
{
    [TestClass]
    public class TestMenuSession : SessionSetup
    {
        private static WindowsDriver<WindowsElement> menuSession = null;
        private WindowsElement dropdown = null;
        private IWebDriver windowHandler = null;


        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            menuSession = Setup(context);
            Thread.Sleep(3000);
        }


        [TestMethod]
        public void Scrabble_SelectDesktopMode()
        {
            dropdown = menuSession.FindElementByClassName(nameof(ComboBox));
            dropdown.Click();
            dropdown.SendKeys("Desktop");
            dropdown.SendKeys(Keys.Enter);
            Assert.AreEqual("Desktop", dropdown.Text);
            Thread.Sleep(1500);
        }

        [TestMethod]
        public void Scrabble_SelectMobileMode()
        {
            dropdown = menuSession.FindElementByClassName(nameof(ComboBox));
            dropdown.Click();
            dropdown.SendKeys("Mobile");
            dropdown.SendKeys(Keys.Enter);
            Assert.AreEqual("Mobile", dropdown.Text);
            Thread.Sleep(1500);
        }

        [TestMethod]
        public void Scrabble_SelectTextMode()
        {
            dropdown = menuSession.FindElementByClassName(nameof(ComboBox));
            dropdown.Click();
            dropdown.SendKeys("Text");
            dropdown.SendKeys(Keys.Enter);
            Assert.AreEqual("Text", dropdown.Text);
            Thread.Sleep(1500);

            // Close window
            CloseWindows(menuSession);

            // Assert no window open
            Assert.AreEqual(0, menuSession.WindowHandles.Count);
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            menuSession.Quit();
        }
    }
}
