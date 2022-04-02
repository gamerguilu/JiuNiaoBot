using LitJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotServerTest
{

    public static class Const
    {
        public const int listenport = 8000;
        public const int endport = 5700;
        public const string host = "127.0.0.1";
        public const string posturl = "http://127.0.0.1:5700/";

        public const string eheartbeat = "heartbeat";

        public const string universalisurl = "https://universalis.app/api/";
        public const string universalisurl_history = "https://universalis.app/api/history/";

        public const string posttype_message = "message";
        public const string posttype_notice = "notice";

        public const string messagetype_group = "group";
        public const string messagetype_private = "private";

        public static string TataPath = AppDomain.CurrentDomain.BaseDirectory + @"go-cqhttp.bat";

        public static JsonData QianCaoSi_Json;

        public static string FFXIVNewsUrl = @"https://ff.web.sdo.com/inc/newdata.ashx?url=List?gameCode=ff&category=5309,5310,5311,5312,5313&pageIndex=0&pageSize=1";
        public static int FFXIVNewsTickR = 5000;

        public static string JsonInPutPath_QianCaoSi = AppDomain.CurrentDomain.BaseDirectory + @"res\json\浅草寺.json";
        public static string JsonInPutPath_AllItem = AppDomain.CurrentDomain.BaseDirectory + @"res\json\AllItem.json";

        public static string imageInPutPath_Market = AppDomain.CurrentDomain.BaseDirectory + @"res\texture\MatkerBase.jpg";
        public static string imageOutPutPath_Market = AppDomain.CurrentDomain.BaseDirectory + @"data\images\MarkerBasetest.jpg";

        public static string imageInPutPath_ZhanBu = AppDomain.CurrentDomain.BaseDirectory + @"res\texture\ZhanBuBase.jpg";
        public static string imageOutPutPath_ZhanBu = AppDomain.CurrentDomain.BaseDirectory + @"data\images\ZhanBu.jpg";

        public static string imageOutPutPath_FFXIVNews = AppDomain.CurrentDomain.BaseDirectory + @"data\images\News.jpg";

        public static string[] newsRegisterGroup = new string[] { "675683939", "822612889" };


        public class ItemClass
        {
            public string ID { get; set; }
            public string Name { get; set; }
        }
        public static List<ItemClass> ItemList;

        public static readonly string[] Server_List = new string[] {"水晶塔","银泪湖", "太阳海岸", "伊修加德", "红茶川", "紫水栈桥", "延夏", "静语庄园", "摩杜纳", "海猫茶屋", "柔风海湾", "琥珀原"
            ,"白银乡", "白金幻象", "神拳痕", "潮风亭", "旅人栈桥", "拂晓之间", "龙巢神殿", "梦羽宝境", "红玉海", "神意之地", "拉诺西亚", "幻影群岛", "萌芽池", "宇宙和音", "沃仙曦染", "晨曦王座"
            ,"Aether","Adamantoise","Cactuar","Faerie","Gilgamesh","Jenova","Midgardsormr","Sargatanas","Siren"
            ,"Primal","Behemoth","Excalibur","Exodus","Famfrit","Hyperion","Lamia","Leviathan","Ultros","Crystal"
            ,"Balmung","Brynhildr","Coeurl","Diabolos","Goblin","Malboro","Mateus","Zalera","Chaos","Cerberus","Louisoix"
            ,"Moogle","Omega","Ragnarok","Spriggan","Light","Lich","Odin","Phoenix","Shiva","Twintania","Zodiark","Materia"
            ,"Bismarck","Ravana","Sephirot","Sophia","Zurvan","Elemental","Aegis","Atomos","Carbuncle","Garuda","Gungnir"
            ,"Kujata","Ramuh","Tonberry","Typhon","Unicorn","Gaia","Alexander","Bahamut","Durandal","Fenrir","Ifrit","Ridill"
            ,"Tiamat","Ultima","Valefor","Yojimbo","Zeromus","Mana","Anima","Asura","Belias","Chocobo","Hades","Ixion","Mandragora"
            ,"Masamune","Pandaemonium","Shinryu","Titan"};

        public static readonly string[] Job_List = new string[]{ "骑士", "战士", "黑暗骑士", "绝枪战士", "武僧", "龙骑士", "忍者", "武士", "钐镰客", "吟游诗人", "机工士", "舞者", "黑魔法师",
            "召唤师", "赤魔法师", "青魔法师", "白魔法师", "学者", "占星术士", "贤者", "能工巧匠", "大地使者" };

        public static readonly string[] Dye_List = new string[] { "红宝石染剂","樱桃粉染剂","丝雀黄染剂", "香草黄染剂", "龙骑蓝染剂", "松石蓝染剂", "泡桐黑染剂", "珍珠白染剂", "金属铜染剂",
            "金属橙染剂", "金属黄染剂", "无暇白染剂", "煤玉黑染剂", "闪耀银染剂", "闪耀银染剂", "闪耀金染剂", "柔彩粉染剂", "黑暗红染剂", "黑暗棕染剂", "柔彩绿染剂", "黑暗绿染剂", "柔彩蓝染剂",
            "黑暗蓝染剂","柔彩紫染剂","黑暗紫染剂","金属红染剂","金属绿染剂","金属蓝染剂","金属靛染剂","金属紫染剂","素雪白染剂","罗兰莓染剂","卫月红染剂","果酒红染剂","日落橙染剂","钴铁棕染剂",
            "南瓜橙染剂","沃土棕染剂","蜂蜜黄染剂","玉米黄染剂","猎人绿染剂","口花绿染剂","深林绿染剂","天上蓝染剂","魔花绿染剂","靛青蓝染剂","东洲蓝染剂","风暴蓝染剂","虚空蓝染剂","皇室蓝染剂",
            "深渊蓝染剂","莲花粉染剂","蜂鸟粉染剂","仙子梅染剂","帝王紫染剂","黑白灰染剂","古菩灰染剂","石板灰染剂","木炭灰染剂","玫瑰粉染剂","丁香紫染剂","铁锈红染剂","珊瑚粉染剂","鲜血红染剂",
            "鲑鱼粉染剂","台地红染剂","树皮棕染剂","巧克力染剂","铁锈棕染剂","软木棕染剂","卢恩棕染剂","奥猴棕染剂","山羊棕染剂","橡果棕染剂","山栗棕染剂","哥布林染剂","页岩棕染剂","鼹鼠棕染剂",
            "骸骨白染剂","黄沙棕染剂","沙漠黄染剂","猛豹黄染剂","奶油黄染剂","日影黄染剂","萄干棕染剂","泥沼绿染剂","妖精绿染剂","青柠绿染剂","苔藓绿染剂","牧草绿染剂","橄榄绿染剂","沼泽绿染剂",
            "苹果绿染剂","仙人掌染剂","金龟绿染剂","地神绿染剂","绿松蓝染剂","寒冰蓝染剂","天空蓝染剂","海雾蓝染剂","孔雀蓝染剂","罗海蓝染剂","腐尸蓝染剂","青磷蓝染剂","油墨蓝染剂","盗龙蓝染剂",
            "午夜蓝染剂","阴影蓝染剂","薰衣草染剂","忧郁紫染剂","醋栗紫染剂","虹膜紫染剂","葡萄紫染剂"};

        public static readonly string[] ASay_List = new string[] {
            "诸事|萨纳兰今天也是艳阳高照啊|看样子今天还是摸了吧",
            "优雷卡|zai？新岛，来刷触发|杀死了光战，幸福兔心满意足地离去了。",
            "刷外观|打低roll高，一步到位|为什么我刷了十多把还是没出啊",
            "找CP|我CP呢,我放在这这么大一个CP呢|孤苦0仃，无1无靠",
            "偷情|我听见雨滴落在青青草地~|偷情要被发现啦，嘻嘻！",
            "抓奸|我觉得你肯定能抓到谁在泼绿漆|同心戒指，使用！",
            "开荒|光之战士冲鸭！|希望地板不太凉🙏",
            "挂机|对面路过的猫娘幻化好好看啊|【游戏管理员】机器人>>在吗？",
            "挖宝|今天你自带未来观测|吉田概率学.jpg",
            "采集|是一位快乐的尼哥呢|【游戏管理员】机器人>>请在10秒内计算⑧+⑥-③并回复答案。",
            "海钓|真厉害，这可是爆钓的好机会啊！|莫莫拉莫拉！莫莫拉莫拉！",
            "钓鱼王|达成了“▶愿者上钩16”的成就！|上钩的鱼逃走了。",
            "PVP|您就是人头收割机？|你们不要追我啊啊啊",
            "逛RP店|RP店的小哥哥（小姐姐）私下加你好友了|今日RP店没有营业",
            "日随|今天会不会遇到可爱的豆芽呢？|不好，遇到的是毒豆芽！",
            "装修|今天灵感爆发，调整一下房间布局好了|我的浮空怎么总被吸附下去啊QAQ",
            "蹲房|我感觉你要蹲到L房了！|还是洗洗睡吧，房子不值得你蹲",
            "练级|冲呀，阿马罗骑士|好累啊，不想再努力了",
            "拍照|近朱者赤，近你者甜|Direct出现致命错误",
            "炒股|橙了橙了，logs橙了|蓝绿徘徊带点灰，我的输出怎么上不去",
            "渡劫|有dalao抬你一手|+1,+1,+1,+1,怎么一晚上过去了还在二阶段",
            "刷坐骑|打低roll高，一步到位|我还是慢慢吃低保吧",
            "幻卡|今天的流行规则真好玩|卡怎么总被吃啊，哭了",
            "喷风|怎么其他人都掉下去了|要 死 一 起 死",
            "赛鸟|CP冲冲冲呀|加 重",
            "天宫死宫|这就去单人爬百层|宝箱变成了拟态怪",
            "无|摸鱼才是王道|嘻嘻！",
        };


    }
}
