using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Net.Sockets;
using LitJson;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace BotServerTest
{
    class Program
    {
        static void Main(string[] args)
        {

            InitJsonLoader();

            //HttpWebRequest curr_request = (HttpWebRequest)WebRequest.Create("https://universalis.app/api/%E7%8C%AB%E5%B0%8F%E8%83%96/34706");
            //HttpWebResponse curr_response = (HttpWebResponse)curr_request.GetResponse();

            //string curr_t = "";

            //using (StreamReader readStream = new StreamReader(curr_response.GetResponseStream(), Encoding.UTF8))
            //{
            //    curr_t = readStream.ReadToEnd();
            //}


            //HttpWebRequest hist_request = (HttpWebRequest)WebRequest.Create("https://universalis.app/api/history/%E7%8C%AB%E5%B0%8F%E8%83%96/34706");
            //HttpWebResponse hist_response = (HttpWebResponse)hist_request.GetResponse();

            //string hist_t = "";

            //using (StreamReader readStream = new StreamReader(hist_response.GetResponseStream(), Encoding.UTF8))
            //{
            //    hist_t = readStream.ReadToEnd();
            //}

            RunTataProcess();

            //Utlity.CreateMarketImageWithText(curr_t, hist_t, Color.Black, "D:\\tata\\BotServerTest_CSharp\\BotServerTest\\BotServerTest\\res\\texture\\MatkerBasetest.jpg");

            MyHttpServer server = new MyHttpServer();
            Thread httpthread = new Thread(new ThreadStart(server.OnInit));
            httpthread.Start();

            TickChecker tickChecker = new TickChecker();
            Thread tickthread = new Thread(new ThreadStart(tickChecker.OnInit));
            tickthread.Start();
            //MsgSender sender = new MsgSender();
            //sender.SendMsg("asd");

            //HttpServerTest server = new HttpServerTest();
            //server.Init();

            //sender.SendHttpMsg("send_group_msg?group_id=822612889&message=test");

            //sender.SendHttpMsg("send_private_msg?user_id=1270672343&message=test");

            //MsgLinstener lis = new MsgLinstener();
            //lis.Listen();

            Console.Read();
        }

        public static void InitJsonLoader()
        {
            string outstring_QianCaoSi = "";

            Utlity.LoadStringFile(Const.JsonInPutPath_QianCaoSi, ref outstring_QianCaoSi);
            ////Console.WriteLine(outstring);
            Const.QianCaoSi_Json = JsonMapper.ToObject(outstring_QianCaoSi);

            outstring_QianCaoSi = "";

            string outstring_ItemList = "";

            Utlity.LoadStringFile(Const.JsonInPutPath_AllItem, ref outstring_ItemList);

            Const.ItemList = JsonMapper.ToObject<List<Const.ItemClass>>(outstring_ItemList);

            outstring_ItemList = "";

        }

        public static void RunTataProcess()
        {
            ProcessStartInfo start = new ProcessStartInfo(Const.TataPath);

            Process.Start(start);
        }
    }



    //public class FinalHttpServerTest
    //{

    //}

    //public class HttpServerTest
    //{
    //    public Socket serverSocket;
    //    public bool isRunning = false;
    //    public void Init()
    //    {
    //        serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    //        serverSocket.Bind(new IPEndPoint(IPAddress.Parse(Const.host), Const.listenport));
    //        serverSocket.Listen(10);
    //        isRunning = true;

    //        while (isRunning)
    //        {
    //            Socket clientSocket = serverSocket.Accept();
    //            Thread requestThread = new Thread(() => {  });
    //            requestThread.Start();
    //        }

    //    }
    //}


    //public class MsgLinstener
    //{
    //    public void Listen()
    //    {
    //        IPAddress ip = IPAddress.Parse(Const.host);
    //        IPEndPoint ipe = new IPEndPoint(ip, Const.listenport);
    //        Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

    //        socket.Bind(ipe);
    //        //socket.Connect(ipe);
    //        socket.Listen(10);

    //        while(true)
    //        {
    //            Socket client = socket.Accept();
    //            Console.WriteLine("get client");

                

    //            byte[] recv = new byte[1024];
    //            int length = client.Receive(recv);

    //            string recmsg = Encoding.UTF8.GetString(recv, 0, length);

    //            Console.WriteLine(recmsg);
    //        }

    //        //string msg = "send_private_msg?user_id=1270672343&message=test";
    //        //byte[] data = Encoding.UTF8.GetBytes(msg);
    //        //client.Send(data);



    //    }
    //}

    //public class MsgSender
    //{
    //    private const string url = "http://127.0.0.1:8000/";

    //    public void SendMsg(string msg)
    //    {

    //        IPAddress ip = IPAddress.Parse(Const.host);
    //        IPEndPoint ipe = new IPEndPoint(ip, Const.listenport);
    //        Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

    //        socket.Connect(ipe);

    //        string sendStr = msg;
    //        byte[] bs = Encoding.ASCII.GetBytes(sendStr);
    //        Console.WriteLine("发送消息");
    //        socket.Send(bs, bs.Length, 0);

    //    }

    //    public string SendHttpMsg(string msg)
    //    {
    //        HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url + msg);
    //        req.Method = "Get";
    //        req.ContentType = "text/html;charset=UTF-8";

    //        HttpWebResponse res = (HttpWebResponse)req.GetResponse();
    //        Stream resStream = res.GetResponseStream();
    //        StreamReader rs = new StreamReader(resStream, Encoding.GetEncoding("utf-8"));
    //        string retstring = rs.ReadToEnd();
    //        rs.Close();
    //        resStream.Close();
    //        return retstring;
    //    }
    //}

    public class MyHttpServer
    {
        TalkWorker talkWorker = new TalkWorker();

        public void OnInit()
        {
            HttpListener httplistener = new HttpListener();

            httplistener.AuthenticationSchemes = AuthenticationSchemes.Anonymous;
            httplistener.Prefixes.Add("http://127.0.0.1:8000/");
            httplistener.Start();
            while (true)
            {
                HttpListenerContext httpListenerContext = httplistener.GetContext();
                httpListenerContext.Response.StatusCode = 200;

                using (StreamWriter writer = new StreamWriter(httpListenerContext.Response.OutputStream))
                {
                    //Console.WriteLine(httpListenerContext.Request.QueryString["interval"]);
                    //Console.WriteLine(httpListenerContext.Request.Url);
                    var stream = httpListenerContext.Request.InputStream;
                    StreamReader reader = new StreamReader(stream);

                    string str = reader.ReadToEnd();
                    try
                    {
                        writer.WriteLine("");
                        writer.Close();
                        reader.Close();
                    }
                    catch
                    {

                    }
                    //talkWorker.OnGetMsg(str);
                    try
                    {
                        talkWorker.OnGetMsg(str);
                    }
                    catch
                    {
                        Console.WriteLine("Error");
                    }


                    //Console.WriteLine(str);
                }
            }
        }
    }

    public class TickChecker
    {
        private int lastTick_NewsCount = 0;
        public void OnInit()
        {
            while(true)
            {
                try
                {
                    HttpWebRequest request_curr = (HttpWebRequest)WebRequest.Create(Const.FFXIVNewsUrl);
                    HttpWebResponse response_curr = (HttpWebResponse)request_curr.GetResponse();

                    string msg = "";

                    using (StreamReader readStream = new StreamReader(response_curr.GetResponseStream(), Encoding.UTF8))
                    {
                        msg = readStream.ReadToEnd();
                        readStream.Close();
                    }
                    OnCheckedNewNews(msg);
                    Thread.Sleep(Const.FFXIVNewsTickR);
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }

        public void OnCheckedNewNews(string msg)
        {
            JsonData jd = JsonMapper.ToObject(msg);
            int count = Int32.Parse(jd["TotalCount"].ToString());
            if(lastTick_NewsCount == 0)
            {
                lastTick_NewsCount = count;
            }
            if(count != lastTick_NewsCount)
            {
                int targetcount = Math.Abs(count - lastTick_NewsCount);
                OnNewsHappen(targetcount);
                lastTick_NewsCount = count;
            }
        }

        public void OnNewsHappen(int change_number)
        {
            for (int i = 0; i < change_number; i++)
            {
                string targeturl = Utlity.CombatFFXIVTargetNewsUrl(i);

                HttpWebRequest request_curr = (HttpWebRequest)WebRequest.Create(targeturl);
                HttpWebResponse response_curr = (HttpWebResponse)request_curr.GetResponse();

                string msg = "";

                using (StreamReader readStream = new StreamReader(response_curr.GetResponseStream(), Encoding.UTF8))
                {
                    msg = readStream.ReadToEnd();
                    readStream.Close();
                }
                DealFFXIVNewsMsg(msg);
            }
        }

        public void DealFFXIVNewsMsg(string msg)
        {
            JsonData jd = JsonMapper.ToObject(msg);
            JsonData newinfo = jd["Data"][0];

            string Title = newinfo["Title"].ToString();
            string Summary = newinfo["Summary"].ToString();
            string Author = newinfo["Author"].ToString();
            string PublishDate = newinfo["PublishDate"].ToString();
            string imageurl = newinfo["HomeImagePath"].ToString();

            Image image = Image.FromStream(WebRequest.Create(imageurl).GetResponse().GetResponseStream());
            image.Save(Const.imageOutPutPath_FFXIVNews);

            StringBuilder sb = new StringBuilder();

            sb.AppendLine(@"来自 最终幻想14国服新闻中心 ：  日期 ：" + PublishDate);
            sb.AppendLine(Title);
            sb.AppendLine(Summary);
            sb.AppendLine(Utlity.CombatImageMsg("News.jpg"));
            sb.AppendLine(Author);

            for (int i = 0; i < Const.newsRegisterGroup.Length; i++)
            {
                string targetgroup = Const.newsRegisterGroup[i];
                TalkWorker.SendGroupMsg(targetgroup, sb.ToString());
            }
            
        }
    }

    public class TalkWorker
    {

        public void OnGetMsg(string msg)
        {
            JsonData jsondata = JsonMapper.ToObject(msg);
            //Console.WriteLine(jsondata["post_type"]);

            string post_type = jsondata["post_type"].ToString();

            switch(post_type)
            {
                case Const.posttype_message:
                    {
                        string msgtype = jsondata["message_type"].ToString();
                        string user_id = jsondata["user_id"].ToString();
                        string message = jsondata["message"].ToString();
                        switch (msgtype)
                        {
                            case Const.messagetype_group:
                                {
                                    string groupid = jsondata["group_id"].ToString();
                                    //Console.WriteLine("Group : " + message);
                                    OnGetGroupMsg(message, user_id, groupid);
                                }
                                break;
                            case Const.messagetype_private:
                                {
                                    //Console.WriteLine("Private : " + message);
                                    OnGetPrivateMsg(message,user_id);
                                }
                                break;
                        }
                    }
                    break;
                case Const.posttype_notice:
                    {

                    }
                    break;
            }

        }


        private void OnGetPrivateMsg(string msg,string userid)
        {
            SendGroupMsg("822612889", msg);
        }

        public void OnGetGroupMsg(string msg, string userid,string groupid)
        {
            if(msg.Contains("/占卜"))
            {


                string luckreturn = Utlity.CombatLuckMsg(userid);

                //Console.WriteLine(luckreturn);
                SendGroupMsg(groupid, luckreturn.ToString());
            }
            else if (msg.Contains("/luck"))
            {
                Utlity.CombatZhanBuMsg(userid);
                SendGroupMsg(groupid, Utlity.CombatZhanBuMsg(userid));
            }
            else if (msg.Contains("/market"))
            {
                SendGroupMsg(groupid, Utlity.CombatMatkerMsg(msg, userid));

            }
        }

        public static void SendGroupMsg(string groupid,string message)
        {
            string msg = Const.posturl + "send_group_msg?" + "group_id=" + groupid + "&message=" + message;
            SendMsg(msg);
        }

        public static void SendMsg(string msg)
        {
            //IPAddress ip = IPAddress.Parse(Const.host);
            //IPEndPoint ipe = new IPEndPoint(ip, Const.portend);
            //Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //socket.Connect(ipe);

            //string sendStr = msg;
            //byte[] bs = Encoding.ASCII.GetBytes(sendStr);
            //Console.WriteLine("发送消息");
            //socket.Send(bs, bs.Length, 0);

            //Console.WriteLine("sendmsg msg : " + msg);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(msg);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            using (StreamReader readStream = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
            {
                //Console.WriteLine("SendMsgSuccessful msg : " + msg);
                //Console.WriteLine("read");
                readStream.Close();
            }


        }
    }
}
