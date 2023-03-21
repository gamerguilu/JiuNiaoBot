using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using System.Drawing;
using System.Net.Sockets;
using LitJson;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.Diagnostics;
using static BotServerTest.Const;

namespace BotServerTest
{
    public class ConfigLoader
    {
        public void Initilize()
        {
            if (Const.DataMiningPath != "")
            {
                LoadItemConfig();
            }
        }

        private bool LoadItemConfig()
        {
            string itemconfigpath = Const.DataMiningPath + @"\Item.csv";
            if (File.Exists(itemconfigpath))
            {

                System.IO.FileStream fs = new System.IO.FileStream(itemconfigpath, System.IO.FileMode.Open);
                System.IO.StreamReader sr = new System.IO.StreamReader(fs, Encoding.GetEncoding("gb2312"));

                string tempText = "";
                int lineindex = 0;
                List<ItemClass> datalist = new List<ItemClass>();
                List<string> keylist = new List<string>();
                List<string> keytype = new List<string>();
                while ((tempText = sr.ReadLine()) != null)
                {
                    string[] arr = tempText.Split(new char[] { ',' }, StringSplitOptions.None);
                    lineindex++;
                    //一般第一行为标题，所以取出来作为标头
                    if (lineindex == 1)
                    {

                    }
                    else if (lineindex == 2)
                    {
                        foreach (string str in arr)
                        {
                            if (str == @"#")
                            {
                                keylist.Add("ID");
                            }
                            else if (str == @"Singular")
                            {
                                keylist.Add("Name");
                            }
                            else if (str == "")
                            {
                                keylist.Add("none");
                            }
                            else
                            {
                                keylist.Add(str);
                            }
                        }
                    }
                    else if (lineindex == 3 )
                    {
                        foreach(string str in arr)
                        {
                            keytype.Add(str);
                        }
                    }
                    else
                    {
                        ItemClass jd = new ItemClass();
                        for (int i = 0; i < arr.Length; i++)
                        {
                            if (keylist.Count > i)
                            {
                                string key = keylist[i];
                                if (key != null && key != "" && key != "none")
                                {
                                    if (key == "ID")
                                    {
                                        jd.ID = arr[i];
                                    }
                                    if (key == "Name")
                                    {
                                        jd.Name = arr[i];
                                    }
                                }
                            }
                        }

                        if (jd.Name != null && jd.Name != "")
                        {
                            datalist.Add(jd);
                        }
                    }
                }

                string jsonstring = JsonMapper.ToJson(datalist);
                //Console.WriteLine(jsonstring);
                Utlity.SaveStringFile(Const.JsonInPutPath_AllItem,jsonstring );
                //关闭流
                sr.Close(); fs.Close();
            }

            return false;
        }

    }
}
