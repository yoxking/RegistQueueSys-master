using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EntWeb.HDeptConsole
{
    public class ItemData
    {
        private string itemName;
        private string itemValue;

        public string ItemName { get { return this.itemName; } set { this.itemName = value; } }
        public string ItemValue { get { return this.itemValue; } set { this.itemValue = value; } }

        public ItemData() { }
        public ItemData(string itemName,string itemValue)
        {
            this.itemName = itemName;
            this.itemValue = itemValue;
        }
    }
}