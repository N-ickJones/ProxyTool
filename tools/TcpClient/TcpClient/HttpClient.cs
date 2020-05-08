using System;
using System.IO;
using System.Net.Security;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TcpClient
{
    public class HttpClient
    {
        private readonly StreamWriter outputFile;

        public HttpClient(StreamWriter file)
        {
            outputFile = file;
        }

        public async Task<string> HttpRequestAsync(string httpRequest, string host, bool https = true, RequestEncoding encoding = RequestEncoding.ASCII)
        {
            using var tcpClient = new System.Net.Sockets.TcpClient(host, https ? 443 : 80);

            if (https)
            {
                using SslStream stream = new SslStream(tcpClient.GetStream());
                tcpClient.SendTimeout = 500;
                tcpClient.ReceiveTimeout = 1000;
                await stream.AuthenticateAsClientAsync(host);
                await SendAsync(stream, httpRequest, encoding);
                var result = await ResponseAsync(stream, host);
                return result;
            }
            else
            {
                using NetworkStream stream = tcpClient.GetStream();
                tcpClient.SendTimeout = 500;
                tcpClient.ReceiveTimeout = 1000;
                await SendAsync(stream, httpRequest);
                var result = await ResponseAsync(stream, host);
                return result;
            }
        }

        public async Task SendAsync(Stream stream, string httpRequest, RequestEncoding encoding = RequestEncoding.ASCII)
        {
            var headerBytes = BuildSegment(httpRequest, encoding);
            await stream.WriteAsync(headerBytes, 0, headerBytes.Length);
        }

        public async void SendAsync(Stream stream, string httpRequest, string requestBody, RequestEncoding encoding = RequestEncoding.ASCII)
        {
            var headerBytes = BuildSegment(httpRequest, encoding);
            await stream.WriteAsync(headerBytes, 0, headerBytes.Length);

            var bodyBytes = BuildSegment(requestBody);
            await stream.WriteAsync(bodyBytes, 0, bodyBytes.Length);
        }
        
        public byte[] BuildSegment(string httpRequest, RequestEncoding encoding = RequestEncoding.ASCII)
        {
            return encoding switch
            {
                RequestEncoding.ASCII => Encoding.ASCII.GetBytes(httpRequest),
                RequestEncoding.Unicode => Encoding.Unicode.GetBytes(httpRequest),
                RequestEncoding.UTF7 => Encoding.UTF7.GetBytes(httpRequest),
                RequestEncoding.UTF8 => Encoding.UTF8.GetBytes(httpRequest),
                RequestEncoding.UTF32 => Encoding.UTF32.GetBytes(httpRequest),
                RequestEncoding.BigEndianUnicode => Encoding.BigEndianUnicode.GetBytes(httpRequest),
                _ => Encoding.ASCII.GetBytes(httpRequest),
            };
        }

        public async Task<string> ResponseAsync(SslStream sslStream, string appHost)
        {
            using var memory = new MemoryStream();
            await sslStream.CopyToAsync(memory);
            return await ProcessResponse(memory, appHost);
        }
        public async Task<string> ResponseAsync(NetworkStream stream, string appHost)
        {
            using var memory = new MemoryStream();

           
            return await ProcessResponse(memory, appHost);     //;await ProcessResponse(memory, appHost);
        }

        public async Task<string> ProcessResponse(MemoryStream memory, string appHost)
        {
            memory.Position = 0;
            var data = memory.ToArray();

            //Gets the Headers Position
            var index = BinaryMatch(data, Encoding.ASCII.GetBytes("\r\n\r\n")) + 4;

            var headers = Encoding.ASCII.GetString(data, 0, index);
            return headers; 

            //await stream.CopyToAsync(memory);
            //Console.WriteLine($"Received Response from Host: {appHost}");
            //await outputFile.WriteAsync($"<h5>Received Response from Host: {appHost}</h5>");
            //return Encoding.UTF8.GetString(memory.ToArray());
        }

        public int BinaryMatch(byte[] input, byte[] pattern)
        {
            int sLen = input.Length - pattern.Length + 1;
            for (int i = 0; i < sLen; ++i)
            {
                bool match = true;
                for (int j = 0; j < pattern.Length; ++j)
                {
                    if (input[i + j] != pattern[j])
                    {
                        match = false;
                        break;
                    }
                }
                if (match)
                {
                    return i;
                }
            }
            return -1;
        }

    }

    public enum RequestEncoding
    {
        ASCII,
        Unicode,
        UTF7,
        UTF8,
        UTF32,
        BigEndianUnicode
    }
}

/* // Sourced from https://stackoverflow.com/questions/19523088/create-http-request-using-tcpclient

//using System.IO.Compression;
public async Task<string> ProcessResponse(MemoryStream memory, string appHost)
{
    memory.Position = 0;
    var data = memory.ToArray();

    //Gets the Headers Position
    var index = BinaryMatch(data, Encoding.ASCII.GetBytes("\r\n\r\n")) + 4;
    var headers = Encoding.ASCII.GetString(data, 0, index);

    Console.WriteLine($"Received Response from Host: {appHost}");
    await outputFile.WriteAsync($"<h5>Received Response from Host: {appHost}</h5>");
    Console.WriteLine(headers);
    await outputFile.WriteAsync($"<h6>Response Headers</h6>{headers.Replace("\n", "<br/>")}"); ;

    memory.Position = index;
    string result;

    //Handles Gzip Compression if needed
    if (headers.IndexOf("Content-Encoding: gzip") > 0)
    {
        using GZipStream decompressionStream = new GZipStream(memory, CompressionMode.Decompress);
        using var decompressedMemory = new MemoryStream();
        decompressionStream.CopyTo(decompressedMemory);
        decompressedMemory.Position = 0;
        result = Encoding.UTF8.GetString(decompressedMemory.ToArray());
    }
    else
    {
        result = Encoding.UTF8.GetString(data, index, data.Length - index);

        //Chinese Character set handling
        //result = Encoding.GetEncoding("gbk").GetString(data, index, data.Length - index);
    }

    return result;
}

public int BinaryMatch(byte[] input, byte[] pattern)
{
    int sLen = input.Length - pattern.Length + 1;
    for (int i = 0; i < sLen; ++i)
    {
        bool match = true;
        for (int j = 0; j < pattern.Length; ++j)
        {
            if (input[i + j] != pattern[j])
            {
                match = false;
                break;
            }
        }
        if (match)
        {
            return i;
        }
    }
    return -1;
}

*/
