using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotServerTest
{
    public class Market_CurrTypeItem
    {
        public int lastReviewTime { get; set; }
        public int pricePerUnit { get; set; }
        public int quantity { get; set; }
        public string worldName { get; set; }
        public int worldID { get; set; }
        public string creatorName { get; set; }
        //public long creatorID { get; set; }
        public bool hq { get; set; }
        public bool isCrafted { get; set; }
        public string retainerName { get; set; }
        public string retainerID { get; set; }
        public string sellerID { get; set; }
        public int total { get; set; }
    }

    public class Market_CurrType
    {
        public int itemID { get; set; }
        public long lastUploadTime { get; set; }
        public List<Market_CurrTypeItem> listings { get; set; }
    }


    public class Market_HistoryTypeItem
    {
        //public int lastReviewTime { get; set; }
        public int pricePerUnit { get; set; }
        public int quantity { get; set; }
        public string worldName { get; set; }
        public int worldID { get; set; }
        //public string creatorName { get; set; }
        //public long creatorID { get; set; }
        public bool hq { get; set; }
        public int timestamp { get; set; }
        //public bool isCrafted { get; set; }
        //public string retainerName { get; set; }
        //public string retainerID { get; set; }
        //public string sellerID { get; set; }
        //public int total { get; set; }
    }

    public class Market_HistoryType
    {
        public int itemID { get; set; }
        public long lastUploadTime { get; set; }
        public List<Market_HistoryTypeItem> entries { get; set; }
    }
}
