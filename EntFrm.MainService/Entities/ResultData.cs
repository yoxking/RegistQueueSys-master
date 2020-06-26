using System.Runtime.Serialization;

namespace EntFrm.MainService.Entities
{
    //数据契约
    [DataContract]
    public class ResultData
    {
        [DataMember]
        public string Code { set; get; }

        [DataMember]
        public string Message { set; get; }

        [DataMember]
        public string Data { set; get; }
    }
}
