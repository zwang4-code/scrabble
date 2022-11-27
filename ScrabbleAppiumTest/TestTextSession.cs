using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium.Windows;
using System.Threading;
using System;
using OpenQA.Selenium;

namespace ScrabbleAppiumTest
{
    [TestClass]
    public class TestTextSession : SessionSetup
    {
        private static WindowsDriver<WindowsElement> textSession = null;
        private WindowsElement dropdown = null;
        private WindowsElement dropdown2 = null;
        private WindowsElement startbutton = null;
        private WindowsElement textbox = null;
        private IWebDriver windowHandler = null;


        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            textSession = Setup(context);
            Thread.Sleep(3000);
        }

        [TestMethod]
        public void Scrabble_Select_TextMode_TwoPlayers()
        {
            // Choose Text mode for first player
            dropdown = textSession.FindElementByAccessibilityId("CB1");
            dropdown.Click();
            dropdown.SendKeys("Text");
            dropdown.SendKeys(Keys.Enter);

            // Choose Text mode for second player
            dropdown2 = textSession.FindElementByAccessibilityId("CB4");
            dropdown2.Click();
            dropdown2.SendKeys("Text");
            dropdown2.SendKeys(Keys.Enter);

            // Start session
            startbutton = textSession.FindElementByAccessibilityId("StartButton");
            startbutton.Click();

            // Assert 2 windows will open
            Assert.AreEqual(2, textSession.WindowHandles.Count);

            // Get names of the window handlers
            string firstWindow = textSession.WindowHandles[0];
            string secondWindow = textSession.WindowHandles[1];

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
