using LitJson;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BotServerTest
{
    public static class Utlity
    {

        public static string GetRandomInStringList(int randomSeed,string[] stringArray)
        {
            if(stringArray != null)
            {
                Random r = new Random(randomSeed);
                int index = r.Next(0, stringArray.Length);
                return stringArray[index];
            }

            return "";
        }

        public static List<Const.ItemClass> GetItemsByName(string name)
        {
            List<Const.ItemClass> itemlist = new List<Const.ItemClass>();

            List<Const.ItemClass> list = Const.ItemList;

            if (list != null)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if(list[i].Name.Contains(name))
                    {
                        itemlist.Add(list[i]);
                    }
                }
            }

            return itemlist;
        }

        public static string CheckServer(string msg)
        {
            string server = "";

            if(msg == "猫" || msg == "猫区" || msg == "猫小胖")
            {
                server = "猫小胖";
            }
            else if(msg == "鸟" || msg == "鸟区" || msg == "陆行鸟")
            {
                server = "陆行鸟";
            }
            else if(msg == "猪" || msg == "猪区" || msg == "莫古力")
            {
                server = "莫古力";
            }
            else if (msg == "狗" || msg == "狗区" || msg == "豆豆柴")
            {
                server = "豆豆柴";
            }
            else
            {
                if(Const.Server_List.Contains(msg))
                {
                    server = msg;
                }
            }


            return server;
        }

        public static string CombatMatkerMsg(string recvmsg,string userid)
        {
            string msg = "";

            string[] strlist = recvmsg.Split(' ');

            Regex rex = new Regex(@"[0-9]*[\u4e00-\u9fa5]*[0-9]*[\u4e00-\u9fa5]*[0-9]*");

            string targetserver = "";

            string targetitemid = "";

            string targetitemname = "";

            string targeturl_curr = "";

            string targeturl_history = "";

            for (int i = 0; i < strlist.Length; i++)
            {
                string target = strlist[i];
                targetserver = (CheckServer(target) != "" ? CheckServer(target): targetserver);
                if (rex.IsMatch(target) && !target.Contains("/market"))
                {
                    List<Const.ItemClass> list = Utlity.GetItemsByName(target);
                    if (list.Count == 1)
                    {
                        Const.ItemClass item = list[0];
                        targetitemid = item.ID;
                        targetitemname = item.Name;
                    }
                    else if (list.Count < 1)
                    {
                        msg = Utlity.CombatAtMsg(userid) + " \n" + " 俺没找到你要的物品，看看名字有没有输对 ";
                    }
                    else
                    {
                        if(targetitemid == "")
                        {
                            msg = Utlity.CombatAtMsg(userid) + " \n" + " 找到了名称包含 " + strlist[i] + "  的物品 " + list.Count + "  个";
                        }
                    }

                }
            }

            if(targetserver == "")
            {

            }
            else
            {
                if(targetitemid == "")
                {

                }
                else
                {
                    targeturl_curr = Const.universalisurl + targetserver + "/" + targetitemid;
                    targeturl_history = Const.universalisurl_history + targetserver + "/" + targetitemid;
                }
            }

            string text_curr = "";
            string text_history = "";

            if(targeturl_curr != "" && targetitemid != "")
            {
                HttpWebRequest request_curr = (HttpWebRequest)WebRequest.Create(targeturl_curr);
                HttpWebResponse response_curr = (HttpWebResponse)request_curr.GetResponse();


                using (StreamReader readStream = new StreamReader(response_curr.GetResponseStream(), Encoding.UTF8))
                {
                    text_curr = readStream.ReadToEnd();
                    readStream.Close();
                }

                HttpWebRequest request_hist = (HttpWebRequest)WebRequest.Create(targeturl_history);
                HttpWebResponse response_hist = (HttpWebResponse)request_hist.GetResponse();


                using (StreamReader readStream = new StreamReader(response_hist.GetResponseStream(), Encoding.UTF8))
                {
                    text_history = readStream.ReadToEnd();
                    readStream.Close();
                }


                CreateMarketImageWithText(text_curr, text_history, Color.Black, Const.imageOutPutPath_Market, targetserver, targetitemname);
                msg = " " + CombatAtMsg(userid) + "\n" + CombatImageMsg(@"MarkerBasetest.jpg");
            }



            return msg;
        }

        public static bool LoadStringFile(string filepath, ref string fileContent)
        {
            FileStream fs = new FileStream(filepath, FileMode.Open);
            if(fs.CanRead)
            {
                StreamReader sr = new StreamReader(fs);
                fileContent = sr.ReadToEnd();
                return true; 
            }
            return false;
        }

        public static int GetRandomIntByLimit(int min,int max,int randomSeed)
        {
            if(min <= max)
            {
                Random r = new Random(randomSeed);
                return r.Next(min, max);
            }

            return 0;
        }

        public static string CombatAtMsg(string userid)
        {
            string msg = "[CQ:at,qq=" + userid + "]";
            return msg;
        }

        public static string CombatImageMsg(string filepath)
        {
            string msg = "[CQ:image,file=" + filepath + "]";
            return msg;
        }

        public static int StrLength(string str)
        {
            if(str != "")
            {
                return System.Text.Encoding.Default.GetBytes(str).Length;
            }
            return 0;
        }

        public static string CombatZhanBuMsg(string userid)
        {
            string msg = "";

            CreateZhanBuImageWithText(CombatZhanBuInfo(userid), Color.Black,Const.imageOutPutPath_ZhanBu);

            msg = Utlity.CombatAtMsg(userid) + " \n " + Utlity.CombatImageMsg(@"ZhanBu.jpg");


            return msg;
        }

        public static string CombatZhanBuInfo(string userid)
        {
            string msg = "";

            StringBuilder sb = new StringBuilder();
            int lucknumber = GetLuckNumber(userid);

            JsonData data = Const.QianCaoSi_Json[lucknumber];
            if(data != null)
            {
                string Title = data["Title"].ToString();
                string Song = data["Song"].ToString();
                string DecSong = data["DecSong"].ToString();
                string Desc = data["Desc"].ToString();

                sb.AppendLine(Title);
                sb.AppendLine();

                string[] songs = Song.Split('；');
                sb.AppendLine(songs[0] + '；');
                sb.AppendLine(songs[1]);
                sb.AppendLine();

                string[] decsongs = ClipString(DecSong,20);
                for (int i = 0; i < decsongs.Length; i++)
                {
                    sb.AppendLine(decsongs[i]);
                }

                sb.AppendLine();

                string[] descs = Desc.Split('。');
                for (int i = 0; i < descs.Length - 1; i++)
                {
                    sb.AppendLine(descs[i] + '。');
                }

                msg = sb.ToString();
            }

            return msg;
        }

        public static string[] ClipString(string content,int warpnumber)
        {
            List<string> arr = new List<string>();
            string warp = "";
            for (int i = 0; i < content.Length; i++)
            {
                warp += content[i];
                if (warp.Length >= warpnumber)
                {
                    arr.Add(warp);
                    warp = "";
                }
                else if (i == content.Length -1)
                {
                    arr.Add(warp);
                }
            }
            return arr.ToArray();
        }

        public static string CombatMarketInfoMsg(string itemname)
        {
            string msg = "";





            return msg;
        }

        public static string CreateZhanBuImageWithText(string showtext,Color FontColor,string targetfilepath)
        {
            string path = "";

            //Bitmap image = new Bitmap(512, 512);
            Image img = Image.FromFile(Const.imageInPutPath_ZhanBu);
            Graphics g = Graphics.FromImage(img);

            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;

            //g.Clear(BGcolor);

            Font f = new Font("Arial", 65, FontStyle.Bold);

            Brush b = new SolidBrush(FontColor);

            //Brush r = new SolidBrush(BGcolor);

            //StringFormat sf = new StringFormat();

            //sf.Alignment = StringAlignment.Center;

            //sf.LineAlignment = StringAlignment.Center;

            g.DrawString(showtext, f, b, 320, 500);

            img.Save(targetfilepath, System.Drawing.Imaging.ImageFormat.Jpeg);

            return path;
        }

        public static string CreateMarketImageWithText(string currtext,string historytext,Color FontColor,string targetfilepath,string targetServer,string targetitem)
        {
            string path = "";



            Image img = Image.FromFile(Const.imageInPutPath_Market);


            Graphics g = Graphics.FromImage(img);

            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;

            Font f = new Font("Courier New", 25, FontStyle.Bold);

            Brush b = new SolidBrush(FontColor);

            DateTime dtime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));

            Market_CurrType curr_jd = JsonMapper.ToObject<Market_CurrType>(currtext);

            if(curr_jd == null)
            {
                return "";
            }

            StringBuilder curr_sb = new StringBuilder();

            curr_sb.AppendLine("当前在售：");
            curr_sb.AppendLine("单价\t\t数量\t服务器\t\thq\t雇员\t\t  刷新时间");

            List<Market_CurrTypeItem> curr_list = curr_jd.listings;
            for (int i = 0; i < curr_list.Count; i++)
            {
                if (i >= 42)
                    continue;
                Market_CurrTypeItem item = curr_list[i];
                long lTime = long.Parse(item.lastReviewTime + "0000000");
                TimeSpan toNow = new TimeSpan(lTime);
                DateTime dtimee = dtime.Add(toNow);
                if(item.worldName == null)
                {
                    item.worldName = targetServer;
                }
                curr_sb.AppendLine(item.pricePerUnit + (item.pricePerUnit < 1000000 ? "\t" : "") + "\t" + item.quantity + "\t" + item.worldName +(StrLength(item.worldName) < 7 ? "\t" : "") +
                    "\t" + item.hq + "\t" + item.retainerName + (StrLength(item.retainerName) > 13 ? "" : "\t") + (StrLength(item.retainerName) < 7 ? "\t" : "") + dtimee.ToShortDateString() + " " + dtimee.ToShortTimeString());
            }

            g.DrawString(curr_sb.ToString(), f, b, 50, 90);


            Market_HistoryType hist_jd = JsonMapper.ToObject<Market_HistoryType>(historytext);

            if(hist_jd == null)
            {
                return "";
            }

            StringBuilder history_sb = new StringBuilder();

            history_sb.AppendLine("历史销售：");
            history_sb.AppendLine("单价\t\t数量\t服务器\t\tHQ\t  购买时间\t");

            List<Market_HistoryTypeItem> history_list = hist_jd.entries;
            for (int i = 0; i < history_list.Count; i++)
            {
                if (i >= 42)
                    continue;
                Market_HistoryTypeItem item = history_list[i];
                long lTime = long.Parse(item.timestamp + "0000000");
                TimeSpan toNow = new TimeSpan(lTime);
                DateTime dtimee = dtime.Add(toNow);

                if (item.worldName == null)
                {
                    item.worldName = targetServer;
                }

                history_sb.AppendLine(item.pricePerUnit + (item.pricePerUnit <= 9999999 ? "\t" : "") + "\t" + item.quantity + "\t" + item.worldName + (StrLength(item.worldName) < 7 ? "\t" : "") +
                    "\t" + item.hq + "\t" + dtimee.ToShortDateString() + " " + dtimee.ToShortTimeString() + "\t");
                
            }

            g.DrawString(history_sb.ToString(), f, b, 1588, 90);

            g.DrawString("Server : " + targetServer, f, b, 700, 1788);

            g.DrawString("Item : " + targetitem, f, b, 2120, 0);

            img.Save(targetfilepath, System.Drawing.Imaging.ImageFormat.Jpeg);

            //JsonData history_jd = JsonMapper.ToObject(historytext);



            return path;
        }

        public static int GetLuckNumber(string userid)
        {
            int id = Convert.ToInt32(userid.Substring((userid.Length - 9)>0 ? (userid.Length - 9):0));

            int idrandom = new Random(id).Next(5000);

            int day = DateTime.Now.DayOfYear;

            int year = DateTime.Now.Year;

            int dayrandom = new Random(day + id).Next(5000);

            int delid = (int)Math.Floor((idrandom * dayrandom + day * 320 + year * 3650) / dayrandom * 1d);

            Random r = new Random(delid);

            int randomvalue = r.Next(5000);

            int luck_number = Utlity.GetRandomIntByLimit(1, 101, randomvalue);

            return luck_number;
        }

        public static string CombatLuckMsg(string userid)
        {
            int id = Convert.ToInt32(userid.Substring((userid.Length - 9) > 0 ? (userid.Length - 9) : 0));

            int idrandom = new Random(id).Next(5000);

            int day = DateTime.Now.DayOfYear;

            int year = DateTime.Now.Year;

            int dayrandom = new Random(day + id).Next(4000);

            int delid = (int)Math.Floor((idrandom * dayrandom + day * 320 + year * 3650) / dayrandom * 1d);

            Random r = new Random(delid);
            int randomvalue = r.Next(5000);

            int luck_number = Utlity.GetRandomIntByLimit(1, 101, randomvalue);

            string luck_job = Utlity.GetRandomInStringList(randomvalue + new Random(id + 2).Next(), Const.Job_List);

            string luck_dye = Utlity.GetRandomInStringList(randomvalue + new Random(id + 3).Next(), Const.Dye_List);

            string luck_asay = Utlity.GetRandomInStringList(randomvalue + new Random(id + 4).Next(), Const.ASay_List);

            string[] luck_asaylist = luck_asay.Split('|');

            string goodat = luck_asaylist[0];

            string asay = luck_number > 50 ? luck_asaylist[1] : luck_asaylist[2];

            string unluckevent = luck_asay;
            int unluckindex = 1;

            while(unluckevent == luck_asay)
            {
                unluckevent = Utlity.GetRandomInStringList(randomvalue + unluckindex, Const.ASay_List);
                unluckindex++;
            }

            string unluck = unluckevent.Split('|')[0];

            string msg = " " + Utlity.CombatAtMsg(userid) + "\n 运势：" + luck_number + "% 幸运职业：  " + luck_job + " \n 宜：" + goodat + "  忌: " + unluck + " 幸运染剂：" + luck_dye + " \n " + asay;

            return msg;
        }

    }
}
