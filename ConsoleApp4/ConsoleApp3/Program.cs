using System;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;

namespace ConsoleApp3
{
    class Program
    {
        static Socket handler;
        static string message;
        static byte[] bytes = new byte[1024];
        static string ipAddress;
        static string resText;
        static void Main(string[] args)
        {
            //Task Ht = Http();
            //Ht.Wait();
            // IPアドレスやポートの設定
            // 受信データバッファ
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

            // ソケットの作成
            Socket listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            // 通信の受け入れ準備
            listener.Bind(localEndPoint);
            listener.Listen(1000);
            // 通信の確立
            //handler = listener.Accept();
            do
            {
                socket(listener);
                //message = Console.ReadLine();
                //text(message);
            }
            while (message != "end");
            end();


        }
        static void socket(Socket listener)
        {
            handler = listener.Accept();
            // 任意の処理
            // データの受取をReceiveで行う。
            int bytesRec = handler.Receive(bytes);
            string data = Encoding.UTF8.GetString(bytes, 0, bytesRec);
            Console.WriteLine(data);
            text(data);
        }
        static void text(String ko)
        {
            // クライアントにSendで返す。
            byte[] message = Encoding.UTF8.GetBytes(ko);
            // Sendで送信
            handler.Send(message);

        }
        static void end()
        {
            // ソケットの終了
            handler.Shutdown(SocketShutdown.Both);
            handler.Close();
        }
        static async Task Test()
        {
            byte[] bytes = new byte[1024];
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[1];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

            // ソケットの作成
            Socket listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            // 通信の受け入れ準備
            listener.Bind(localEndPoint);
            listener.Listen(10);

            // 通信の確立
            handler = listener.Accept();
            // 任意の処理
            // データの受取をReceiveで行う。
            int bytesRec = handler.Receive(bytes);
            string data = Encoding.UTF8.GetString(bytes, 0, bytesRec);
            Console.WriteLine(data);

        }
        /// <summary>
        /// サーバーを構築してメッセージを受信する
        /// </summary>
        /// 
        //public static void SetServer()
        //{
        //    // IPアドレスやポートの設定
        //    // 受信データバッファ
        //    byte[] bytes = new byte[1024];
        //    IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
        //    IPAddress ipAddress = ipHostInfo.AddressList[0];
        //    IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

        //    // ソケットの作成
        //    Socket listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        //    // 通信の受け入れ準備
        //    listener.Bind(localEndPoint);
        //    listener.Listen(10);

        //    // 通信の確立
        //    Socket handler = listener.Accept();


        //    // 任意の処理
        //    // データの受取をReceiveで行う。
        //    int bytesRec = handler.Receive(bytes);
        //    string data = Encoding.UTF8.GetString(bytes, 0, bytesRec);
        //    Console.WriteLine(data);

        //    // 大文字に変更
        //    data = data.ToUpper();

        //    // クライアントにSendで返す。
        //    byte[] message = Encoding.UTF8.GetBytes(data);
        //    handler.Send(message);

        //    // ソケットの終了
        //    handler.Shutdown(SocketShutdown.Both);
        //    handler.Close();
        //}
        static async Task Http()
        {
            foreach (var ni in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (ni.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 || ni.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                {
                    foreach (var ip in ni.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            ipAddress = ip.Address.ToString();
                        }
                    }
                }
            }
            string url = "http://localhost/info.php";

            WebClient wc = new WebClient();
            //NameValueCollectionの作成
            NameValueCollection ps =
                new NameValueCollection();
            //送信するデータ（フィールド名と値の組み合わせ）を追加
            //ps.Add("word", "インターネット");
            ps.Add("ip", ipAddress);
            //データを送信し、また受信する
            byte[] resData = wc.UploadValues(url, ps);
            wc.Dispose();

            //受信したデータを表示する
            resText = Encoding.UTF8.GetString(resData);
            Console.WriteLine(resText);
        }
    }
}
