using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System;

namespace Cchat
{
    public static class CchatConnectionInfo
    {
        public static string GetServerIP()
        {
            using (WebClient webClient = new WebClient())
            {
                string extIPAddress = webClient.DownloadString("http://icanhazip.com").Trim();
                IPAddress extIPv4Address;
                if (IPAddress.TryParse(extIPAddress, out extIPv4Address))
                {
                    if (extIPv4Address.AddressFamily == AddressFamily.InterNetwork)
                        return extIPAddress;
                }
            }
            return null;
        }

        public static string GetClientIP(TcpClient client)
        {
            if (client != null)
            {
                string localEnd = client.Client.LocalEndPoint.ToString();

                int length = localEnd.IndexOf(":", StringComparison.Ordinal);
                if (length > 0)
                {
                    return localEnd.Substring(0, length);
                }
            }
            return null;
        }

        public static string GetPort()
        {
            return "5000";
        }

        public static int LatencyResult(string host)
        {
            using (Ping ping = new Ping())
            {
                PingReply reply = ping.Send(host);
                return (int)reply.RoundtripTime;
            }
        }
    }
}