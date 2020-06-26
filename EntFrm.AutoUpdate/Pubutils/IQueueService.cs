namespace EntFrm.AutoUpdate
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName = "IQueueService")]
    public interface IQueueService
    {
        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IQueueService/OnExecuteCommand", ReplyAction = "http://tempuri.org/IQueueService/OnExecuteCommandResponse")]
        string OnExecuteCommand(string methodName, string paramList);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IQueueService/OnExecuteCommand", ReplyAction = "http://tempuri.org/IQueueService/OnExecuteCommandResponse")]
        System.Threading.Tasks.Task<string> OnExecuteCommandAsync(string methodName, string paramList);

    }
}
