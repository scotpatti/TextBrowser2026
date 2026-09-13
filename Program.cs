using TextBrowser;
using System;
using System.Net.Sockets;
using System.Text;

namespace TextBrowser
{
    public static class Program {
        public static void Main()
        {
            string hostname = "httpforever.com";
            string request = "GET / HTTP/1.1\r\nHost:httpforever.com\r\n\r\n";
            //byte[] buffer = ASCIIEncoding.ASCII.GetBytes(request);

            TcpClient client = new TcpClient();
            client.Connect(hostname, 80);

            NetworkStream stream = client.GetStream();

            stream.WriteString(request);
            string data = stream.ReadString();

            stream.Close();
            client.Close();

            Console.WriteLine(data);
        }
    }
}