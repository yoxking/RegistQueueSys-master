using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntFrm.Framework.Utility
{
    public class RegKeyModel
    {
        private string encDogId;
        private string organizName;
        private string updateDate;
        private string activeDate;
        private string activeCount;
        private string activeValCode;

        public string EncDogId { get => encDogId; set => encDogId = value; }
        public string OrganizName { get => organizName; set => organizName = value; }
        public string UpdateDate { get => updateDate; set => updateDate = value; }
        public string ActiveDate { get => activeDate; set => activeDate = value; }
        public string ActiveCount { get => activeCount; set => activeCount = value; }
        public string ActiveValCode { get => activeValCode; set => activeValCode = value; }

        public RegKeyModel(string encDogId, string organizName, string updateDate, string activeDate, string activeCount, string activeValCode)
        {
            this.encDogId = encDogId;
            this.organizName = organizName;
            this.updateDate = updateDate;
            this.activeDate = activeDate;
            this.activeCount = activeCount;
            this.activeValCode = activeValCode;
        }
    }
}

