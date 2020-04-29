using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
//using System.Net.Http.Headers;

namespace HttpCacheHelper
{
    class Program
    {
        public static async Task Main()
        {
            TestTool test = new TestTool();
            test.GenerateTestCases();
            await test.Run();
            Console.WriteLine("The cache has expired and another test can be executed.");
        }
    }
}
