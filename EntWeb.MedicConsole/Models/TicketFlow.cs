using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EntWeb.MedicConsole.Models
{
    public class TicketFlow
    {
        public string RFlowNo { set; get; }
        public string RegistNo { set; get; }
        public string TicketNo { set; get; }
        public string RUserNo { set; get; }
        public string RUserName { set; get; }
        public string CounterNo { set; get; }
        public string CounterName { set; get; }
        public string DataFrom { set; get; }
        public string RecipeState { set; get; }
        public string ProcessState { set; get; }
        public DateTime EnqueueTime { set; get; }
        public DateTime ProcessedTime { set; get; } 
    }
}