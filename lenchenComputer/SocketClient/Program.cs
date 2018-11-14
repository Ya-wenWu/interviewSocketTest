using ClassLib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SocketClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Connecting...");

            var socket = new Client.Lib.SocketClient();

            Console.WriteLine("Please input your message:");
            string inputData = Console.ReadLine();
            Console.WriteLine("your message is :" + inputData.ToString());

            var dto = new ForSocket()
            {
                Function = FunctionName.FunctionOne,
                IsCallback = true,
                ThisInpuMsg = inputData,
            };

            var result = socket.GetData(dto);

            Console.WriteLine(JsonConvert.SerializeObject(result));
            Console.WriteLine("Socket Output data1:" + result.Status);

            Thread.Sleep(2000);

            Console.WriteLine("Disconnected...");
            Console.ReadKey();
        }
    }
}
