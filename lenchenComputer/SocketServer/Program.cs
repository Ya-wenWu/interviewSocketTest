using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Server;

namespace SocketServer
{
    public class Program
    {
        static void Main(string[] args)
        {
            new Server.Lib.SocketServer();

            Console.WriteLine("Listening...");
            Console.Read();
        }
    }
}
