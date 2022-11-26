
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Documents;

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

        // TODO: TRIED TO WRITE CLOSE WINDOWS INSTRUCTIONS HERE BUT DID NOT WORK.. 
        public static void CloseWindows(WindowsDriver<WindowsElement> session)
        {
            // Close all the windows
            if (session != null)
            {
                IReadOnlyCollection<String> allWindows = session.WindowHandles;

                List<IWebDriver> windowHandlers = null;

                foreach (String oneWindow in allWindows)
                {
                    windowHandlers.Add(session.SwitchTo().Window(oneWindow));

                }

                foreach (IWebDriver windowHandler in windowHandlers)
                {
                    windowHandler.Close();
                    Console.WriteLine(windowHandler.Title, " window closed");
                }

            }
            // Assert all windows are closed
            Assert.AreEqual(0, session.WindowHandles.Count);

            // Close the session
            session.Quit();
            Console.WriteLine("Session ended");
            Thread.Sleep(1500);
        }
    }
}
