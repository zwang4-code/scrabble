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
        private WindowsElement passbutton = null;
        private WindowsElement swapbutton = null;
        private WindowsElement finishbutton = null;
        private WindowsElement finishTurnButton = null;
        string firstWindow = "";
        string secondWindow = "";
        private IWebDriver windowHandler = null;


        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            desktopSession = Setup(context);
            Thread.Sleep(3000);
        }

        [TestInitialize]
        public void DesktopMode_Start_For_Two_Players()
        {
            // Choose Desktop mode for the first 2 players
            StartGameWithTwoPlayers(desktopSession, "Desktop");

            // Get names of the 2 window handlers
            firstWindow = desktopSession.WindowHandles[0];
            secondWindow = desktopSession.WindowHandles[1];
        }

        [TestMethod]
        public void DesktopMode_Test_Buttons()
        {
            // Assert 2 windows will open
            Assert.AreEqual(2, desktopSession.WindowHandles.Count);

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
        }

        [TestCleanup]
        public void CloseWindows()
        {
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
