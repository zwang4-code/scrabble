
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Threading;

namespace ScrabbleAppiumTest
{
    public class SessionSetup
    {
        // Note: append /wd/hub to the URL if you're directing the test at Appium
        private const string WindowsApplicationDriverUrl = "http://127.0.0.1:4723/wd/hub";
        private const string ScrabblePath = @"C:\Users\hh\Desktop\5210-Scrabble-GitHub\Scrabble\bin\Debug\Scrabble2018.exe";
        //@"C:\Users\vdeepak\Downloads\Preedhi\Team2_Scrabble\Scrabble\bin\Debug\Scrabble2018.exe";
        //@"C:\Users\hh\Desktop\5210-Scrabble-GitHub\Scrabble\bin\Debug\Scrabble2018.exe";

        public static WindowsDriver<WindowsElement> Setup(TestContext context)
        {
            // Set up desired capabilities 
            DesiredCapabilities appCapabilities = new DesiredCapabilities();
            appCapabilities.SetCapability("app", ScrabblePath);
            appCapabilities.SetCapability("deviceName", "WindowsPC");

            // Create a new session to bring up an instance of the Scrabble application
            WindowsDriver<WindowsElement> session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);
            Assert.IsNotNull(session);

            // Set implicit timeout to 1.5 seconds to make element search to retry every 500 ms for at most three times
            session.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1.5);

            return session;
        }

        public void StartGameWithTwoPlayers(WindowsDriver<WindowsElement> session, string mode)
        {
            WindowsElement dropdown = null;
            WindowsElement dropdown2 = null;
            WindowsElement startbutton = null;

            // Choose Desktop mode for first player 
            dropdown = session.FindElementByAccessibilityId("CB1");
            dropdown.Click();
            dropdown.SendKeys(mode);
            dropdown.SendKeys(Keys.Enter);

            // Choose Desktop mode for second player
            dropdown2 = session.FindElementByAccessibilityId("CB4");
            dropdown2.Click();
            dropdown2.SendKeys(mode);
            dropdown2.SendKeys(Keys.Enter);

            SaveScreenShotToDocuments(session);

            // Start session
            startbutton = session.FindElementByAccessibilityId("StartButton");
            startbutton.Click();
            Thread.Sleep(1500);
        }

        public void SaveScreenShotToDocuments(WindowsDriver<WindowsElement> session)
        { 
            Screenshot screenshot = session.GetScreenshot();
            string fileName = string.Format(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) +
                      @"\Screenshot" + "_" + DateTime.Now.ToString("(dd_MMMM_hh_mm_ss_tt)") + ".png");
            screenshot.SaveAsFile(fileName);
        }

        public void CloseWindows(WindowsDriver<WindowsElement> session)
        {
            // Close all the windows
            if (session != null)
            {
                IReadOnlyCollection<String> allWindows = session.WindowHandles;

                foreach (String oneWindow in allWindows)
                {
                    IWebDriver windowHandler = session.SwitchTo().Window(oneWindow);
                    windowHandler.Close();
                    Thread.Sleep(1500);
                }
            }
        }
    }
}
