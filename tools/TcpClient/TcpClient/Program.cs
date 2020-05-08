using System.IO;
using System.Collections.Generic;
using Nito.AsyncEx;

namespace TcpClient
{
    public class Program
    {
        public static async void MainAsync(string[] args)
        {
            bool https = true;
            List<TestCase> TestCaseQueue = new List<TestCase>
            {
                /*
                new TestCase(
                    name: "Non-ASCII Characters",
                    description: "Testing if non-ascii have effects on the web applications when sent through the HTTP Headers",
                    httpRequest: File.ReadAllText(@"test\input\NonAsciiInjection.txt"),
                    queryParams: "",
                    encoding: RequestEncoding.UTF8,
                    https: https
                ),
                */
                /*
                new TestCase(
                    name: "Duplicate Connection",
                    description: "Test if Duplicate Connection Headers accepts last value",
                    httpRequest: File.ReadAllText(@"test\input\DuplicteConnection.txt"),
                    queryParams: "",
                    encoding: RequestEncoding.UTF8,
                    https: https
                ),
                */

                new TestCase(
                    name: "Duplicate Header with List",
                    description: "Test what happens when duplicate headers are present that use a list",
                    httpRequest: File.ReadAllText(@"test\input\DuplicateListHeader.txt"),
                    queryParams: "",
                    encoding: RequestEncoding.ASCII,
                    https: https
                ),

                /*
                new TestCase(
                    name: "Test",
                    description: "Teste",
                    httpRequest: File.ReadAllText(@"test\input\test.txt"),
                    queryParams: "",
                    encoding: RequestEncoding.UTF8,
                    https: https
                ),
                */

                /*
                new TestCase(
                    name: "JavaScript Injection",
                    description: "A test to see if a basic JavaScript can be injected through HTTP Headers",
                    httpRequest: File.ReadAllText(@"test\input\JavaScriptInjection.txt"),
                    queryParams: "",
                    encoding: RequestEncoding.ASCII,
                    https: https
                ),
                */

            };
            TestRunner testRunner = new TestRunner(TestCaseQueue);
            await testRunner.StartTest();
        }

        public static void Main(string[] args)
        {
            AsyncContext.Run(() => MainAsync(args));
        }

    }
}
