using ClassLib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client.Lib
{
    internal class SendResponse
    {
        public Status SendStatus { get; set; }
        public string ReceiveData { get; set; }
    }

    public class SendBase
    {
        internal SendResponse Send<T>(T source, bool isCallback)
        {
            var response = new SendResponse();

            try
            {
                var point = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 55788);
                var client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                client.Connect(point);
                if (!client.Connected)
                {
                    response.SendStatus = Status.ConnectFail;
                    response.ReceiveData = "未連線";
                    return response;
                }
                client.SendTimeout = 5000;
                client.ReceiveTimeout = 5000;

                var json = JsonConvert.SerializeObject(source);
                var msg = Encoding.Default.GetBytes(json);
                client.Send(msg);

                if (isCallback)
                {
                    var buf = new byte[8];
                    var length = 0;
                    do
                    {
                        length = client.Receive(buf);
                        response.ReceiveData += Encoding.Default.GetString(buf, 0, length);
                    }
                    while (length > 0);
                }

                client.Shutdown(SocketShutdown.Both);
                client.Close();

                response.SendStatus = Status.Success;
            }
            catch (SocketException sktex)
            {
                response.SendStatus = Status.Fail;
                response.ReceiveData = sktex.Message;
            }
            catch (Exception ex)
            {
                response.SendStatus = Status.Exception;
                response.ReceiveData = ex.Message;
            }

            return response;
        }
    }
}
