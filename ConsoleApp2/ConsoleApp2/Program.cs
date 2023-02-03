using System;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;


namespace ConsoleApp2
{
    class Program
    {
        static Socket socket;
        static string message;
        static string ipAddress;
        static string resText;
        static void Main(string[] args)
        {
            //Task task = Http();
            //task.Wait();
            //// IPアドレスやポートの設定
            //IPHostEntry ipHostInfo = Dns.GetHostEntry("172.16.1.254");
            //IPAddress ipAddress = ipHostInfo.AddressList[0];
            //IPEndPoint remoteEP = new IPEndPoint(ipAddress, 11000);
            //do
            //{
            //    message = Console.ReadLine();
            //    SetClient(message, ipAddress, remoteEP);
            //} while (message != "end");
            //socket.Shutdown(SocketShutdown.Both);
            //socket.Close();
            //00000000000000000000000000000000000000000000000000000000000000000000000000000000000000000

            //データを送信するリモートホストとポート番号
            string remoteHost = "172.16.1.254";
            int remotePort = 11000;

            //UdpClientオブジェクトを作成する
            UdpClient udp =
                new UdpClient();

            for (; ; )
            {
                //送信するデータを作成する
                Console.WriteLine("送信する文字列を入力してください。");
                string sendMsg = Console.ReadLine();
                byte[] sendBytes = Encoding.UTF8.GetBytes(sendMsg);

                //リモートホストを指定してデータを送信する
                udp.Send(sendBytes, sendBytes.Length, remoteHost, remotePort);

                //または、
                //udp = new UdpClient(remoteHost, remotePort);
                //として、
                //udp.Send(sendBytes, sendBytes.Length);

                //"exit"と入力されたら終了
                if (sendMsg.Equals("exit"))
                {
                    break;
                }
            }

            //UdpClientを閉じる
            udp.Close();

            Console.WriteLine("終了しました。");
            Console.ReadLine();




        }
        //static void Text(string sendMessage)
        //{
        //    // IPアドレスやポートの設定
        //    IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
        //    Console.WriteLine(ipHostInfo.AddressList[1]);
        //    IPAddress ipAddress = ipHostInfo.AddressList[1];
        //    IPEndPoint remoteEP = new IPEndPoint(ipAddress, 11000);
        //    // ソケットを作成
        //    Socket socket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

        //    // 接続する。失敗するとエラーで落ちる。
        //    socket.Connect(remoteEP);
        //    // Sendで送信
        //    byte[] message = Encoding.UTF8.GetBytes(sendMessage);
        //    socket.Send(message);

        //}
        //static async Task Test()
        //{
        //    // Receiveで受信
        //    byte[] bytes = new byte[1024];
        //    int bytesRec = socket.Receive(bytes);
        //    string data = Encoding.UTF8.GetString(bytes, 0, bytesRec);
        //    Console.WriteLine(data);

        //}
        //static void end()
        //{
        //    // ソケットを終了
        //    socket.Shutdown(SocketShutdown.Both);
        //    socket.Close();
        //}
        /// <summary>
        /// クライアントを構築してメッセージを送信する
        /// </summary>
        /// <param name="sendMessage">送信するメッセージ</param>
        public static void SetClient(string sendMessage, IPAddress ipAddress, IPEndPoint remoteEP)
        {


            // ソケットを作成
            Socket socket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            // 接続する。失敗するとエラーで落ちる。
            socket.Connect(remoteEP);

            // Sendで送信
            byte[] message = Encoding.UTF8.GetBytes(sendMessage);
            socket.Send(message);

            // Receiveで受信
            byte[] bytes = new byte[1024];
            int bytesRec = socket.Receive(bytes);
            string data = Encoding.UTF8.GetString(bytes, 0, bytesRec);
            Console.WriteLine(data);
        }
        static async Task Test()
        {
            // Receiveで受信
            byte[] bytes = new byte[1024];
            int bytesRec = socket.Receive(bytes);
            string data = Encoding.UTF8.GetString(bytes, 0, bytesRec);
        }
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
            Console.WriteLine(ipAddress);
            //string url = "http://localhost/info.php";

            //WebClient wc = new WebClient();
            ////NameValueCollectionの作成
            //NameValueCollection ps =
            //    new NameValueCollection();
            ////送信するデータ（フィールド名と値の組み合わせ）を追加
            ////ps.Add("word", "インターネット");
            //ps.Add("ip2", ipAddress);
            ////データを送信し、また受信する
            //byte[] resData = wc.UploadValues(url, ps);
            //wc.Dispose();

            ////受信したデータを表示する
            //resText = Encoding.UTF8.GetString(resData);
            //Console.WriteLine(resText);
        }
    }
}
