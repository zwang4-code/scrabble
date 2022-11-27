using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium.Windows;
using System.Threading;
using System;
using OpenQA.Selenium;

namespace ScrabbleAppiumTest
{
    [TestClass]
    public class DesktopSessionTest : SessionSetup
    {
        private static WindowsDriver<WindowsElement> desktopSession = null;
        private WindowsElement dropdown = null;
        private WindowsElement dropdown2 = null;
        private WindowsElement startbutton = null;
        private WindowsElement finishbutton = null;
        private IWebDriver windowHandler = null;


        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            desktopSession = Setup(context);
            Thread.Sleep(3000);
        }

        [TestMethod]
        public void Scrabble_SelectTwoDesktopPlayer()
        {
            // Choose Desktop mode for first player 
            dropdown = desktopSession.FindElementByAccessibilityId("CB1");
            dropdown.Click();
            dropdown.SendKeys("Desktop");
            dropdown.SendKeys(Keys.Enter);

            // Choose Desktop mode for second player
            dropdown2 = desktopSession.FindElementByAccessibilityId("CB4");
            dropdown2.Click();
            dropdown2.SendKeys("Desktop");
            dropdown2.SendKeys(Keys.Enter);

            // Start session
            startbutton = desktopSession.FindElementByAccessibilityId("StartButton");
            startbutton.Click();
            Thread.Sleep(1500);

            // Assert 2 windows will open
            Assert.AreEqual(2, desktopSession.WindowHandles.Count);

            // Get names of the window handlers
            string firstWindow = desktopSession.WindowHandles[0];
            string secondWindow = desktopSession.WindowHandles[1];

            // Switch to first window, press "Finish" button
            windowHandler = desktopSession.SwitchTo().Window(firstWindow);
            Console.WriteLine(windowHandler.Title + " window open");
            finishbutton = desktopSession.FindElementByAccessibilityId("ValidateButton");
            finishbutton.Click();
            Thread.Sleep(1500);

            // Switch to second window
            windowHandler = desktopSession.SwitchTo().Window(secondWindow);
            Console.WriteLine(windowHandler.Title + " window open");
            Thread.Sleep(1500);

            // Close all windows
            CloseWindows(desktopSession);

            // Assert no window open
            Assert.AreEqual(0, desktopSession.WindowHandles.Count);
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            desktopSession.Quit();
        }
    }
}
