using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium.Windows;
using System.Threading;
using System;
using OpenQA.Selenium;
using System.Windows.Controls;

namespace ScrabbleAppiumTest
{
    [TestClass]
    public class TextSessionTest : SessionSetup
    {
        private static WindowsDriver<WindowsElement> textSession = null;
        private static WindowsElement dropdown = null;
        private static WindowsElement dropdown2 = null;
        private static WindowsElement startbutton = null;
        private static WindowsElement finishbutton = null;
        private static WindowsElement textbox = null;
        private static IWebDriver windowHandler = null;


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

            // 
            windowHandler = textSession.SwitchTo().Window(textSession.WindowHandles[0]);
            textbox = textSession.FindElementByAccessibilityId("UserInputBox");
            textbox.Click();
            textbox.SendKeys("PASS");
            Assert.AreEqual("PASS", textbox.Text);
            Thread.Sleep(2000);
            textSession.FindElementByAccessibilityId("SubmitButton").Click();
            Thread.Sleep(3000);

            textSession.SwitchTo().Window(textSession.WindowHandles[1]);
            textbox = textSession.FindElementByAccessibilityId("UserInputBox");
            textbox.Click();
            textbox.SendKeys("RANK");
            Assert.AreEqual("RANK", textbox.Text);
            Thread.Sleep(2000);
            textSession.FindElementByAccessibilityId("SubmitButton").Click();
            Thread.Sleep(3000);
        }


        //[ClassCleanup]
        //public static void SessionCleanup()
        //{
        //    CloseWindows();
        //}

    }
}
