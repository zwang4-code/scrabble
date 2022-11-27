using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium.Windows;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;


namespace ScrabbleAppiumTest
{
    [TestClass]
    public class Test_2_DesktopSession : SessionSetup
    {
        private static WindowsDriver<WindowsElement> desktopSession = null;
        private WindowsElement logboard = null;
        private WindowsElement passbutton = null;
        private WindowsElement swapbutton = null;
        private WindowsElement finishbutton = null;
        private WindowsElement finishTurnButton = null;
        string firstWindow = "";
        string secondWindow = "";
        string windowTitle = "";
        string firstPlayer = "";
        string secondPlayer = "";
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

            // Swith to first window
            windowHandler = desktopSession.SwitchTo().Window(firstWindow);
            SaveScreenShotToDocuments(desktopSession);

            // Verify first window title and store player number 
            windowTitle = windowHandler.Title;
            Assert.IsTrue(windowTitle.Contains("ScrabbleDesktop"));
            firstPlayer = windowTitle.Split('-')[0];

            // Press "Pass" button
            passbutton = desktopSession.FindElementByAccessibilityId("PassButton");
            passbutton.Click();
            Thread.Sleep(1500);

            // Verify that first player's logboard has the following infor
            logboard = desktopSession.FindElementByAccessibilityId("LogBoard");
            Assert.IsTrue(logboard.Text.Contains(firstPlayer + "first!"));
            Assert.IsTrue(logboard.Text.Contains(firstPlayer + "passed!"));

            // Swith to second window (next player's turn now)
            windowHandler = desktopSession.SwitchTo().Window(secondWindow);

            // Verify second window title and store player number
            windowTitle = windowHandler.Title;
            Assert.IsTrue(windowTitle.Contains("ScrabbleDesktop"));
            secondPlayer = windowTitle.Split('-')[0];

            // Verity that second player's logboard is updated with the following info
            logboard = desktopSession.FindElementByAccessibilityId("LogBoard");
            Assert.IsTrue(logboard.Text.Contains("Your turn!"));
            Assert.IsTrue(logboard.Text.Contains(firstPlayer + "passed!"));
            SaveScreenShotToDocuments(desktopSession);

            // Press "Swap" button
            swapbutton = desktopSession.FindElementByAccessibilityId("SwapButton");
            swapbutton.Click();
            Thread.Sleep(1500);

            // Verify the player's logboard is updated with the following info
            logboard = desktopSession.FindElementByAccessibilityId("LogBoard");
            Assert.IsTrue(logboard.Text.Contains("Select the tiles you don't want...Then press the FINISH button."));
            SaveScreenShotToDocuments(desktopSession);

            // Move pointer away to see "Swap" button change color and change to "Finish"
            Actions action = new Actions(desktopSession);
            action.MoveByOffset(50, 50).Perform();
            SaveScreenShotToDocuments(desktopSession);

            // Press "Finish" button (ID is also SwapButton)
            finishbutton = desktopSession.FindElementByAccessibilityId("SwapButton");
            finishbutton.Click();
            Thread.Sleep(1500);

            // Verify the second player's logboard is updated with the following info
            logboard = desktopSession.FindElementByAccessibilityId("LogBoard");
            Assert.IsTrue(logboard.Text.Contains(secondPlayer + "swapped his tiles!"));
            Assert.IsTrue(logboard.Text.Contains(secondPlayer + "finished his turn!"));
            SaveScreenShotToDocuments(desktopSession);

            // Switch to first window (next player's turn now)
            windowHandler = desktopSession.SwitchTo().Window(firstWindow);

            // Verify the first player's logboard is updated with the following info
            logboard = desktopSession.FindElementByAccessibilityId("LogBoard");
            Assert.IsTrue(logboard.Text.Contains("Your turn!"));
            Assert.IsTrue(logboard.Text.Contains(secondPlayer + "swapped his tiles!"));
            SaveScreenShotToDocuments(desktopSession);

            // Press "Finish Turn" button
            finishTurnButton = desktopSession.FindElementByAccessibilityId("ValidateButton");
            finishTurnButton.Click();

            // Verify the first player's logboard is update with the following info
            Assert.IsTrue(logboard.Text.Contains("Game Judge: \"You didn't score. Please try again!\""));
            SaveScreenShotToDocuments(desktopSession);
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
