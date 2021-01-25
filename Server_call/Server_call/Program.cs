using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace Server_call
{
    class Program
    {
        static void Main(string[] args)
        {
            Server_Call_Data serverP = new Server_Call_Data();
            serverP.ServerLink();
        }

        struct Server_Call_Data
        {
            private const string ip = "127.0.0.1";
            private const int port = 8080;

            internal void ServerLink()
            {
                var tcpEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
                var tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                tcpSocket.Bind(tcpEndPoint);
                tcpSocket.Listen(3);

                while(true)
                {
                    var listener = tcpSocket.Accept();
                    var buff = new byte[256];
                    var size = 0;
                    var data = new StringBuilder();

                    do
                    {
                        size = listener.Receive(buff);
                        data.Append(Encoding.UTF8.GetString(buff, 0, size));
                    }
                    while (listener.Available > 0);

                    FileWrite(data.ToString() + "|");

                    listener.Send(Encoding.UTF8.GetBytes("+"));

                    listener.Shutdown(SocketShutdown.Both);
                    listener.Close();
                }
            }

            void FileWrite(string _data)
            {
                StreamWriter sw = new StreamWriter(File.Open("data.txt", FileMode.Append));
                sw.WriteLine(_data);
                sw.Close();
            }
        }
    }
}