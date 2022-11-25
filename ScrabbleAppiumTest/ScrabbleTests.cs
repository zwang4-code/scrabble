//******************************************************************************
//
// Copyright (c) 2017 Microsoft Corporation. All rights reserved.
//
// This code is licensed under the MIT License (MIT).
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//
//******************************************************************************

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
        private static IWebDriver newWindow = null;

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
            Thread.Sleep(3000);
        }

        [TestMethod]
        public void Scrabble_SelectMobileMode()
        {
            dropdown = session.FindElementByClassName(nameof(ComboBox));
            dropdown.Click();
            dropdown.SendKeys("Mobile");
            dropdown.SendKeys(Keys.Enter);
            Assert.AreEqual("Mobile", dropdown.Text);
            Thread.Sleep(3000);
        }

        [TestMethod]
        public void Scrabble_SelectTextMode()
        {
            dropdown = session.FindElementByClassName(nameof(ComboBox));
            dropdown.Click();
            dropdown.SendKeys("Text");
            dropdown.SendKeys(Keys.Enter);
            Assert.AreEqual("Text", dropdown.Text);
            Thread.Sleep(3000);
        }

        [TestMethod]
        public void Scrabble_SelectTwoDesktopPlayer()
        {
            dropdown = session.FindElementByAccessibilityId("CB1");
            dropdown.Click();
            dropdown.SendKeys("Desktop");
            dropdown.SendKeys(Keys.Enter);

            dropdown2 = session.FindElementByAccessibilityId("CB4");
            dropdown2.Click();
            dropdown2.SendKeys("Desktop");
            dropdown2.SendKeys(Keys.Enter);

            startbutton = session.FindElementByAccessibilityId("StartButton");
            startbutton.Click();

            newWindow = session.SwitchTo().Window(session.WindowHandles[0]);
            Console.WriteLine(newWindow.Title);
            finishbutton = session.FindElementByAccessibilityId("ValidateButton");
            finishbutton.Click();

            Thread.Sleep(3000);
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

            newWindow = session.SwitchTo().Window(session.WindowHandles[0]);
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
