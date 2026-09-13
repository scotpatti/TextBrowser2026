using System.Net.Sockets;
using System.Text;

namespace TextBrowser
{
    public static class Extensions
    {
        public static bool WriteString(this NetworkStream stream, string data)
        {
            bool rval = true;
            try
            {
                byte[] buffer = Encoding.ASCII.GetBytes(data);
                stream.Write(buffer, 0, buffer.Length);
                stream.Flush();
            }
            catch
            {
                rval = false;
            }
            return rval;
        }

        public static string ReadString(this NetworkStream stream)
        {
            stream.ReadTimeout = 10000;
            StringBuilder sb = new StringBuilder();
            byte[] buffer = new byte[4096];
            int totalBytesRead = 0;
            try
            {
                int bytesRead;
                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    sb.Append(Encoding.ASCII.GetString(buffer, 0, bytesRead));
                    totalBytesRead += bytesRead;
                    if (bytesRead == 0)
                    {
                        break;
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Connection Closed because of timeout!");
            }

            Console.WriteLine($"Read {totalBytesRead} bytes total.");
            return sb.ToString();
        }
    }
}