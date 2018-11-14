using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server.Lib
{

    public class SocketServer
    {
        public SocketServer()
        {
            new Thread(
                new ThreadStart(StartSocket)
                ).Start();
        }

        private void StartSocket()
        {
            var point = new IPEndPoint(IPAddress.Any, 55788);
            var server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            server.Bind(point);
            server.Listen(10);

            while (true)
            {
                Thread.Sleep(1000);
                var client = server.Accept();
                var listen = new Listen(client);
                var thread = new Thread(new ThreadStart(listen.ToListen));
                thread.Start();
            }
        }
    }

    class Listen
    {
        Socket _socket;
        public Listen(Socket client)
        {
            Console.WriteLine("server do - Listen("+DateTime.Now+")"); 
            _socket = client;
        }

        internal void ToListen()
        {
            var buffer = new byte[2];
            var receiveData = "";
            var isFirstReceive = true;

            while (true)
            {
				try {
                    var length = _socket.Receive(buffer);
                    if (!isFirstReceive) Console.WriteLine("server do - length:"+ length.ToString()); 

                    if (length == 0) break;
					if (isFirstReceive)
					{
                        var totalLength = BitConverter.ToInt16(buffer, 0);
						Array.Resize(ref buffer, totalLength);
						isFirstReceive = false;
						continue;
					}
                    receiveData = Encoding.Default.GetString(buffer, 0, length);
                    var result = $"GET: {receiveData},\"DateTime\":{DateTime.Now},\" Length\": {buffer.Length}".PadLeft(8 + 4 + 57);

                    Console.WriteLine( DateTime.Today + " : server Receive = " + receiveData.ToString());
                    Console.WriteLine( DateTime.Today + " : server Send = " + result.ToString());

                    var resultBuf = Encoding.Default.GetBytes(result);
                    var codeBuf = BitConverter.GetBytes((short)0x3030);
                    var sendData = BitConverter.GetBytes((short)(resultBuf.Length + codeBuf.Length));

                    Array.Resize(ref sendData, sendData.Length + resultBuf.Length + codeBuf.Length);
                    Array.ConstrainedCopy(resultBuf, 0, sendData, 2, resultBuf.Length);
                    Array.ConstrainedCopy(codeBuf, 0, sendData, sendData.Length - 2, codeBuf.Length);
                    try
                    {
                        _socket.Send(sendData);
                    }
                    catch (Exception SocketSendException)
                    {
                        Console.WriteLine("server send data Exception:"+SocketSendException.ToString());  
                    }
				}
				catch (Exception exx)
                {

					Console.WriteLine("Exception : "+ exx.ToString());
				}
                break;
            }
            _socket.Shutdown(SocketShutdown.Both);
            _socket.Close();
        }
    }
}
