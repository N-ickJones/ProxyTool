using System;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Generic;
using System.Threading;
using System.Text.RegularExpressions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumExtras.WaitHelpers;


using System.Drawing;
using System.Drawing.Imaging;
using System.Windows;


namespace TcpClient
{
    public class TestRunner
    {
        private static List<TestCase> TestCaseQueue;
        private static List<string> WebAppHosts;
        private static readonly bool Local = false;
        private static readonly string Host_React = Local ? "react.localhost" : "react.myvenv.club";
        private static readonly string Host_Razor = Local ? "razor.localhost" : "razor.myvenv.club";
        private static readonly string Host_Django = Local ? "django.localhost" : "django.myvenv.club";
        private static readonly string Host_Flask = Local ? "flask.localhost" : "flask.myvenv.club";
        private static readonly string Host_Ruby = Local ? "ruby.localhost" : "ruby.myvenv.club";
        private static readonly string Host_Laravel = Local ? "laravel.localhost" : "laravel.myvenv.club";
        private static readonly string Host_Spring = Local ? "spring.localhost" : "spring.myvenv.club";
        private static readonly string Host_Phoenix = Local ? "phoenix.localhost" : "phoenix.myvenv.club";
        private static readonly string Host_Express = Local ? "express.localhost" : "express.myvenv.club";

        public TestRunner(List<TestCase> testCases, List<string> webAppHosts = null)
        {
            TestCaseQueue = testCases;

            if (webAppHosts != null)
            {
                WebAppHosts = webAppHosts;
            }
            else
            {
                WebAppHosts = new List<string>
                {
                    Host_React,
                    Host_Razor,
                    Host_Django,
                    Host_Flask,
                    Host_Ruby,
                    Host_Laravel,
                    Host_Spring,
                    Host_Phoenix,
                    Host_Express
                };
            }
        }

        public async Task StartTest(int cacheTimer = 60)
        {
            var options = new ChromeOptions();
            options.AddArgument("--log-level=3");
            var driver = new ChromeDriver(options);
            NavigateToUrl(driver, "https://research.myvenv.club");
            Thread.Sleep(2000);
            foreach (TestCase testcase in TestCaseQueue)
            {
                // Invalid file character windows /\ : | * ? " < > .. \ \0 invalid linux
                using StreamWriter file = new StreamWriter($"test\\output\\TestCase_{DateTime.Now.ToString("yyyy-MM-dd__hh-mm-ss")}_{testcase.Name}.html", true);
                Console.Write($"Running Testcase: {testcase.Name}_{DateTime.Now.ToString("yyyy-MM-dd__hh-mm-ss")}\n");
                await FormatTest(file, testcase);
                await RunTest(driver, file, testcase);
                //CacheTimeout(cacheTimer);
            }
            driver.Close();
        }

        private static async Task FormatTest(StreamWriter file, TestCase testcase)
        {
            await file.WriteAsync($"<!DOCTYPE html><html>");
            await file.WriteAsync($"<head>" +
                "<link rel=\"stylesheet\" href=\"https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/css/bootstrap.min.css\" integrity=\"sha384-Vkoo8x4CGsO3+Hhxv8T/Q5PaXtkKtu6ug5TOeNV6gBiFeWPGFN9MuhOf23Q9Ifjh\" crossorigin=\"anonymous\">" +
                $"</head>");
            await file.WriteAsync($"<body class=\"text-center mb-3\">");
            await file.WriteAsync($"<h2>Test: {testcase.Name}</h2>");
            await file.WriteAsync($"<p>{testcase.Description}</p>");
        }

        private static async Task RunTest(IWebDriver driver, StreamWriter file, TestCase testCase, int iterationTimer = 2)
        {
            HttpClient httpClient = new HttpClient(file);
            await file.WriteAsync($"<hr /><div>{testCase.HttpRequest.Replace("\n", "<br/>")}</div><hr />");

            foreach (string appHost in WebAppHosts)
            {
                //Swap Host in the request & Write
                testCase.HttpRequest = Regex.Replace(testCase.HttpRequest, "Host: (.*)", $"Host: {appHost}");

                Console.WriteLine($"\nSending Request to Host: {appHost}");
                await file.WriteAsync($"<h5>Sending Request to Host: {appHost}</h5>");

                Console.Write(testCase.HttpRequest);


                // Send the Tcp Request
                string response = await httpClient.HttpRequestAsync(testCase.HttpRequest, appHost, true, testCase.Encoding);

                var screenShotFileName = GenerateScreenshot(driver, testCase, appHost);


                await file.WriteAsync($"<h6>Response Headers</h6><xmp>{response}</xmp>");

                await file.WriteAsync($"<p>The following was displayed:</p>");

                Thread.Sleep(TimeSpan.FromSeconds(iterationTimer));
                await file.WriteAsync($"<img src=\"{ screenShotFileName }\" class=\"img-fluid border border-info\" alt=\"{appHost} screenshot\"");
                await file.WriteAsync($"<hr />");
            }

            // Closing Tags and Bootstrap javascript
            await file.WriteAsync($"<script src=\"https://code.jquery.com/jquery-3.4.1.slim.min.js\" integrity=\"sha384-J6qa4849blE2+poT4WnyKhv5vZF5SrPo0iEjwBvKU7imGFAV0wwj1yYfoRSJoZ+n\" crossorigin=\"anonymous\"></script>" +
                    "<script src=\"https://cdn.jsdelivr.net/npm/popper.js@1.16.0/dist/umd/popper.min.js\" integrity=\"sha384-Q6E9RHvbIyZFJoft+2mJbHaEWldlvI9IOYy5n3zV9zzTtmI3UksdQRVvoxMfooAo\" crossorigin=\"anonymous\"></script>" +
                    "<script src=\"https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/js/bootstrap.min.js\" integrity=\"sha384-wfSDF2E50Y2D1uUdj0O3uMBJnjuUD4Ih7YwaYd1iqfktj0Uod8GCExl3Og8ifwB6\" crossorigin=\"anonymous\"></script>" +
                    "</body></html>");

        }

        private static string GenerateScreenshot(IWebDriver driver, TestCase testCase, string appHost)
        {
            var url = $"{appHost}{testCase.QueryParams}";
            NavigateToUrl(driver, testCase.Https ? $"https://{url}" : $"http://{url}");
            Thread.Sleep(3000);
            string fileName;
            try
            {
                //Screenshot screenShot = ((ITakesScreenshot)driver).GetScreenshot();
                //fileName = $"images\\save_{DateTime.Now.ToString("yyyy-MM-dd__hh-mm-ss")}.png";
                //screenShot.SaveAsFile($"test\\output\\{fileName}", ScreenshotImageFormat.Png);
                fileName = TakeScreenshot(driver, testCase, appHost);
            }
            catch
            {
                fileName = TakeScreenshot(driver, testCase, appHost);
            }

            return fileName;
        }

        private static string TakeScreenshot(IWebDriver driver, TestCase testCase, string appHost)
        {
            Rectangle bounds = new Rectangle(
                driver.Manage().Window.Position,
                driver.Manage().Window.Size
            );
            using Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height);
            using (Graphics graphic = Graphics.FromImage(bitmap))
            {
                graphic.CopyFromScreen(driver.Manage().Window.Position, Point.Empty, bounds.Size);
            }
            string fileName = $"images\\save_{DateTime.Now.ToString("yyyy-MM-dd__hh-mm-ss")}.png";
            bitmap.Save($"test\\output\\{fileName}", ImageFormat.Png);
            return fileName;
        }

        private static void CacheTimeout(int cacheTimer)
        {
            int ticks = 5;
            int interval = cacheTimer / ticks;
            for (int i = 0; i < ticks; i++)
            {
                Console.WriteLine($"Waiting for cache to reset... {cacheTimer - (i * interval)} seconds left");
                Thread.Sleep(TimeSpan.FromSeconds(interval));
            }
            Console.WriteLine("Cache Has Reset");
        }

        private static void NavigateToUrl(IWebDriver driver, string url)
        {
            try
            {
                AcceptAlerts(driver);
                driver.Navigate().GoToUrl(url);
            }
            catch
            {
            }
        }

        private static void AcceptAlerts(IWebDriver driver)
        {
            try
            {
                driver.SwitchTo().Alert().Accept();
            }
            catch
            {
            }
        }

        /*
        private static string ConvertHtmlToReadable(string data)
        {
            return $"<xmp>{data}</xmp>";
        }
        */

    }

    public class TestCase
    {
        public TestCase(string name, string description, string httpRequest, string queryParams = "", RequestEncoding encoding = RequestEncoding.ASCII, bool https = true)
        {
            Name = name;
            Description = description;
            HttpRequest = httpRequest;
            QueryParams = queryParams;
            Encoding = encoding;
            Https = https;
        }
        public string Name { get; set; }
        public string Description { get; set; }
        public string HttpRequest { get; set; }
        public string QueryParams { get; set; }
        public RequestEncoding Encoding { get; set; }
        public bool Https { get; set; }
    }
}
