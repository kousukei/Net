using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Net.Http;

namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {

            SetServer();
        }

        public void http()
        {
            var hc = new HttpClient();

        }
        /// <summary>
        /// サーバーを構築してメッセージを受信する
        /// </summary>
        public static void SetServer()
        {
            byte[] bytes = new byte[1024];
            string hostName = ""; // 空文字にする
            IPHostEntry ip = Dns.GetHostEntry(hostName);
            IPAddress iPAddress = ip.AddressList[1];

                Console.WriteLine(iPAddress);
                IPEndPoint localEndPoint = new IPEndPoint(iPAddress, 11000);
                // ソケットの作成
                Socket listener = new Socket(iPAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                // 通信の受け入れ準備
                listener.Bind(localEndPoint);
                listener.Listen(10);
                Socket handler = listener.Accept();
                int bytesRec = handler.Receive(bytes);
                string data = Encoding.UTF8.GetString(bytes, 0, bytesRec);
                Console.WriteLine(data);
                data = data.ToUpper();
                // クライアントにSendで返す。
                byte[] message = Encoding.UTF8.GetBytes(data);
                handler.Send(message);

                // ソケットの終了
                handler.Shutdown(SocketShutdown.Both);
                handler.Close();
            
        }
    }
}
