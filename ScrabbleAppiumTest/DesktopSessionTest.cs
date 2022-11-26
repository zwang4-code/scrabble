using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium.Windows;
using System.Threading;
using System;
using OpenQA.Selenium;
using System.Windows.Controls;

namespace ScrabbleAppiumTest
{
    [TestClass]
    public class DesktopSessionTest : SessionSetup
    {
        private static WindowsDriver<WindowsElement> desktopSession = null;
        private static WindowsElement dropdown = null;
        private static WindowsElement dropdown2 = null;
        private static WindowsElement startbutton = null;
        private static WindowsElement finishbutton = null;
        private static IWebDriver windowHandler = null;


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

            // Switch to first window, press "Finish" button, then close window
            windowHandler = desktopSession.SwitchTo().Window(firstWindow);
            Console.WriteLine(windowHandler.Title, " window open");
            finishbutton = desktopSession.FindElementByAccessibilityId("ValidateButton");
            finishbutton.Click();
            windowHandler.Close();
            Thread.Sleep(1500);

            // Switch to second window, then close window
            windowHandler = desktopSession.SwitchTo().Window(secondWindow);
            Console.WriteLine(windowHandler.Title, " window open");
            windowHandler.Close();

            // Assert no window open
            Assert.AreEqual(0, desktopSession.WindowHandles.Count);
            Thread.Sleep(1500);
        }

    }
}
