using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium.Windows;
using System.Threading;
using OpenQA.Selenium;
using System.Windows.Controls;

namespace ScrabbleAppiumTest
{
    [TestClass]
    public class Test_1_MenuSession : SessionSetup
    {
        private static WindowsDriver<WindowsElement> menuSession = null;
        private WindowsElement dropdown = null;
        private WindowsElement aboutbutton = null;
        private WindowsElement authorElement = null;
        private WindowsElement startbutton = null;
        private WindowsElement errorMessage = null;
        private IWebDriver windowHandler = null;


        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            menuSession = Setup(context);
            Thread.Sleep(3000);
        }


        [TestMethod]
        public void MenuTest_1_Select_DesktopMode()
        {
            dropdown = menuSession.FindElementByClassName(nameof(ComboBox));
            dropdown.Click();
            dropdown.SendKeys("Desktop");
            dropdown.SendKeys(Keys.Enter);
            Assert.AreEqual("Desktop", dropdown.Text);
            Thread.Sleep(1500);
        }

        [TestMethod]
        public void MenuTest_2_Select_TextMode()
        {
            dropdown = menuSession.FindElementByClassName(nameof(ComboBox));
            dropdown.Click();
            dropdown.SendKeys("Text");
            dropdown.SendKeys(Keys.Enter);
            Assert.AreEqual("Text", dropdown.Text);
            Thread.Sleep(1500);
        }

        [TestMethod]
        public void MenuTest_3_Select_MobileMode()
        {
            dropdown = menuSession.FindElementByClassName(nameof(ComboBox));
            dropdown.Click();
            dropdown.SendKeys("Mobile");
            dropdown.SendKeys(Keys.Enter);
            Assert.AreEqual("Mobile", dropdown.Text);
            Thread.Sleep(1500);
        }

        [TestMethod]
        public void MenuTest_4_Click_About()
        {
            aboutbutton = menuSession.FindElementByAccessibilityId("AboutButton");
            aboutbutton.Click();
            Thread.Sleep(2000);

            // Assert 2 windows are open
            Assert.AreEqual(2, menuSession.WindowHandles.Count);

            // Get name of the about window handler
            string aboutWindow = menuSession.WindowHandles[0];

            // Swith to about window
            windowHandler = menuSession.SwitchTo().Window(aboutWindow);

            // Assert "Author" appears on the author window
            authorElement = menuSession.FindElementByAccessibilityId("About");
            Assert.IsTrue(authorElement.Text.Contains("Author"));

            // Close all windows
            CloseWindows(menuSession);

            // Assert no window open
            Assert.AreEqual(0, menuSession.WindowHandles.Count);
            Thread.Sleep(1500);
        }


        [ClassCleanup]
        public static void ClassCleanup()
        {
            menuSession.Quit();
        }
    }
}
