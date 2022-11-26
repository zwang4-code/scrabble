using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium.Windows;
using System.Threading;
using System;
using OpenQA.Selenium;
using System.Windows.Controls;

namespace ScrabbleAppiumTest
{
    [TestClass]
    public class ScrabbleTests : ScrabbleSession
    {
        private static WindowsElement dropdown = null;
        private static WindowsElement dropdown2 = null;
        private static WindowsElement startbutton = null;
        private static WindowsElement finishbutton = null;
        private static WindowsElement textbox = null;
        private static IWebDriver windowHandler = null;


        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            Setup(context);
            Thread.Sleep(3000);
        }


        [TestMethod]
        public void Scrabble_SelectDesktopMode()
        {
            dropdown = session.FindElementByClassName(nameof(ComboBox));
            dropdown.Click();
            dropdown.SendKeys("Desktop");
            dropdown.SendKeys(Keys.Enter);
            Assert.AreEqual("Desktop", dropdown.Text);
            Thread.Sleep(1500);
        }

        [TestMethod]
        public void Scrabble_SelectMobileMode()
        {
            dropdown = session.FindElementByClassName(nameof(ComboBox));
            dropdown.Click();
            dropdown.SendKeys("Mobile");
            dropdown.SendKeys(Keys.Enter);
            Assert.AreEqual("Mobile", dropdown.Text);
            Thread.Sleep(1500);
        }

        [TestMethod]
        public void Scrabble_SelectTextMode()
        {
            dropdown = session.FindElementByClassName(nameof(ComboBox));
            dropdown.Click();
            dropdown.SendKeys("Text");
            dropdown.SendKeys(Keys.Enter);
            Assert.AreEqual("Text", dropdown.Text);
            Thread.Sleep(1500);
        }

        [TestMethod]
        public void Scrabble_SelectTwoDesktopPlayer()
        {
            // Choose Desktop mode for first player 
            dropdown = session.FindElementByAccessibilityId("CB1");
            dropdown.Click();
            dropdown.SendKeys("Desktop");
            dropdown.SendKeys(Keys.Enter);

            // Choose Desktop mode for second player

            dropdown2 = session.FindElementByAccessibilityId("CB4");
            dropdown2.Click();
            dropdown2.SendKeys("Desktop");
            dropdown2.SendKeys(Keys.Enter);

            // Start session
            startbutton = session.FindElementByAccessibilityId("StartButton");
            startbutton.Click();
            Thread.Sleep(1500);

            // Assert 2 windows will open
            Assert.AreEqual(2, session.WindowHandles.Count);

            // Get names of the window handlers
            string firstWindow = session.WindowHandles[0];
            string secondWindow = session.WindowHandles[1];

            // Switch to first window, press "Finish" button, then close window
            windowHandler = session.SwitchTo().Window(firstWindow);
            Console.WriteLine(windowHandler.Title, " window open");
            finishbutton = session.FindElementByAccessibilityId("ValidateButton");
            finishbutton.Click();
            windowHandler.Close();
            Thread.Sleep(1500);

            // Switch to second window, then close window
            windowHandler = session.SwitchTo().Window(secondWindow);
            Console.WriteLine(windowHandler.Title, " window open");
            windowHandler.Close();

            // Assert no window open
            Assert.AreEqual(0, session.WindowHandles.Count);
            Thread.Sleep(1500);
        }

        [TestMethod]
        public void Scrabble_SelectTwoTextPlayer()
        {
            dropdown = session.FindElementByAccessibilityId("CB1");
            dropdown.Click();
            dropdown.SendKeys("Text");
            dropdown.SendKeys(Keys.Enter);

            dropdown2 = session.FindElementByAccessibilityId("CB4");
            dropdown2.Click();
            dropdown2.SendKeys("Text");
            dropdown2.SendKeys(Keys.Enter);

            startbutton = session.FindElementByAccessibilityId("StartButton");
            startbutton.Click();

            windowHandler = session.SwitchTo().Window(session.WindowHandles[0]);
            textbox = session.FindElementByAccessibilityId("UserInputBox");
            textbox.Click();
            textbox.SendKeys("PASS");
            Assert.AreEqual("PASS", textbox.Text);
            Thread.Sleep(2000);
            session.FindElementByAccessibilityId("SubmitButton").Click();
            Thread.Sleep(3000);

            session.SwitchTo().Window(session.WindowHandles[1]);
            textbox = session.FindElementByAccessibilityId("UserInputBox");
            textbox.Click();
            textbox.SendKeys("RANK");
            Assert.AreEqual("RANK", textbox.Text);
            Thread.Sleep(2000);
            session.FindElementByAccessibilityId("SubmitButton").Click();
            Thread.Sleep(3000);
        }


        [ClassCleanup]
        public static void ClassCleanup()
        {
            TearDown();
        }

    }
}
