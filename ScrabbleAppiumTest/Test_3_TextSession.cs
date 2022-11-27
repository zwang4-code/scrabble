using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium.Windows;
using System.Threading;
using System;
using OpenQA.Selenium;

namespace ScrabbleAppiumTest
{
    [TestClass]
    public class Test_3_TextSession : SessionSetup
    {
        private static WindowsDriver<WindowsElement> textSession = null;
        private WindowsElement textbox = null;
        string firstWindow = "";
        string secondWindow = "";
        private IWebDriver windowHandler = null;


        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            textSession = Setup(context);
            Thread.Sleep(3000);
        }

        [TestInitialize]
        public void TextMode_Start_For_Two_Players()
        {
            // Choose Text mode for the first two players
            StartGameWithTwoPlayers(textSession, "Text");

            // Get names of the window handlers
            firstWindow = textSession.WindowHandles[0];
            secondWindow = textSession.WindowHandles[1];
        }

        [TestMethod]
        public void TextMode_Test_InputBoxes()
        {
            // Assert 2 windows will open
            Assert.AreEqual(2, textSession.WindowHandles.Count);

            // Enter "PASS" in textbox of first window
            windowHandler = textSession.SwitchTo().Window(firstWindow);
            Console.WriteLine(windowHandler.Title + " window open");
            textbox = textSession.FindElementByAccessibilityId("UserInputBox");
            textbox.Click();
            textbox.SendKeys("PASS");
            Assert.AreEqual("PASS", textbox.Text);
            textSession.FindElementByAccessibilityId("SubmitButton").Click();
            Thread.Sleep(1500);

            // Enter "RANK" in textbox of second window
            textSession.SwitchTo().Window(secondWindow);
            Console.WriteLine(windowHandler.Title + " window open");
            textbox = textSession.FindElementByAccessibilityId("UserInputBox");
            textbox.Click();
            textbox.SendKeys("RANK");
            Assert.AreEqual("RANK", textbox.Text);
            textSession.FindElementByAccessibilityId("SubmitButton").Click();
            Thread.Sleep(1500);
        }

        [TestCleanup]
        public void CloseWindows()
        {
            // Close all windows
            CloseWindows(textSession);

            // Assert no window open
            Assert.AreEqual(0, textSession.WindowHandles.Count);
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            textSession.Quit();
        }
    }
}
