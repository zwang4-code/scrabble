using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium.Windows;
using System.Threading;
using System;
using OpenQA.Selenium;
using System.Windows.Controls.Primitives;
using OpenQA.Selenium.Interactions;

namespace ScrabbleAppiumTest
{
    [TestClass]
    public class Test_2_DesktopSession : SessionSetup
    {
        private static WindowsDriver<WindowsElement> desktopSession = null;
        private WindowsElement dropdown = null;
        private WindowsElement dropdown2 = null;
        private WindowsElement startbutton = null;
        private WindowsElement passbutton = null;
        private WindowsElement swapbutton = null;
        private WindowsElement finishbutton = null;
        private WindowsElement finishTurnButton = null;
        private IWebDriver windowHandler = null;


        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            desktopSession = Setup(context);
            Thread.Sleep(3000);
        }

        [TestMethod]
        public void DesktopMode_With_Two_Players()
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

            // Swith to first window, press "Pass" button
            windowHandler = desktopSession.SwitchTo().Window(firstWindow);
            Assert.IsTrue(windowHandler.Title.Contains("ScrabbleDesktop"));
            passbutton = desktopSession.FindElementByAccessibilityId("PassButton");
            passbutton.Click();
            Thread.Sleep(1500);

            // Swith to second window (next player's turn now), press "Swap" button 
            windowHandler = desktopSession.SwitchTo().Window(secondWindow);
            Assert.IsTrue(windowHandler.Title.Contains("ScrabbleDesktop"));
            swapbutton = desktopSession.FindElementByAccessibilityId("SwapButton");
            swapbutton.Click();
            Thread.Sleep(1500);

            // Move pointer away to see "Swap" button change color
            Actions action = new Actions(desktopSession);
            action.MoveByOffset(50, 50).Perform();

            // Press "Finish" button (ID is also SwapButton)
            finishbutton = desktopSession.FindElementByAccessibilityId("SwapButton");
            finishbutton.Click();
            Thread.Sleep(1500);

            // Switch to first window (next player's turn now), press "Finish Turn" button
            windowHandler = desktopSession.SwitchTo().Window(firstWindow);
            finishTurnButton = desktopSession.FindElementByAccessibilityId("ValidateButton");
            Thread.Sleep(1000);
            finishTurnButton.Click();
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
