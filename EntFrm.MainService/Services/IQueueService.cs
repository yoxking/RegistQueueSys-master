using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;

namespace EntFrm.MainService.Services
{
    [ServiceContract]
    public interface IQueueService
    {
        [OperationContract]
        [WebGet(UriTemplate = "/OnExecuteCommand?methodName={methodName}&paramList={paramList}", ResponseFormat = WebMessageFormat.Json)]   //WebGet属性用来收集客户信息
        //[WebInvoke(Method = "PUT", UriTemplate = "/customer/{id}")]  //WebInvoke属性被用于那些修改数据的添加或者删除客户信息的操作
        string OnExecuteCommand(string methodName, string paramList);  
    }
}
