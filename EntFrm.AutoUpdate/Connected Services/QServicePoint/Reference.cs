﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace EntFrm.AutoUpdate.QServicePoint {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="QServicePoint.IQueueService")]
    public interface IQueueService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IQueueService/OnExecuteCommand", ReplyAction="http://tempuri.org/IQueueService/OnExecuteCommandResponse")]
        string OnExecuteCommand(string methodName, string paramList);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://tempuri.org/IQueueService/OnExecuteCommand", ReplyAction="http://tempuri.org/IQueueService/OnExecuteCommandResponse")]
        System.IAsyncResult BeginOnExecuteCommand(string methodName, string paramList, System.AsyncCallback callback, object asyncState);
        
        string EndOnExecuteCommand(System.IAsyncResult result);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IQueueServiceChannel : EntFrm.AutoUpdate.QServicePoint.IQueueService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class OnExecuteCommandCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public OnExecuteCommandCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public string Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class QueueServiceClient : System.ServiceModel.ClientBase<EntFrm.AutoUpdate.QServicePoint.IQueueService>, EntFrm.AutoUpdate.QServicePoint.IQueueService {
        
        private BeginOperationDelegate onBeginOnExecuteCommandDelegate;
        
        private EndOperationDelegate onEndOnExecuteCommandDelegate;
        
        private System.Threading.SendOrPostCallback onOnExecuteCommandCompletedDelegate;
        
        public QueueServiceClient() {
        }
        
        public QueueServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public QueueServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public QueueServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public QueueServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public event System.EventHandler<OnExecuteCommandCompletedEventArgs> OnExecuteCommandCompleted;
        
        public string OnExecuteCommand(string methodName, string paramList) {
            return base.Channel.OnExecuteCommand(methodName, paramList);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.IAsyncResult BeginOnExecuteCommand(string methodName, string paramList, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginOnExecuteCommand(methodName, paramList, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public string EndOnExecuteCommand(System.IAsyncResult result) {
            return base.Channel.EndOnExecuteCommand(result);
        }
        
        private System.IAsyncResult OnBeginOnExecuteCommand(object[] inValues, System.AsyncCallback callback, object asyncState) {
            string methodName = ((string)(inValues[0]));
            string paramList = ((string)(inValues[1]));
            return this.BeginOnExecuteCommand(methodName, paramList, callback, asyncState);
        }
        
        private object[] OnEndOnExecuteCommand(System.IAsyncResult result) {
            string retVal = this.EndOnExecuteCommand(result);
            return new object[] {
                    retVal};
        }
        
        private void OnOnExecuteCommandCompleted(object state) {
            if ((this.OnExecuteCommandCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.OnExecuteCommandCompleted(this, new OnExecuteCommandCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void OnExecuteCommandAsync(string methodName, string paramList) {
            this.OnExecuteCommandAsync(methodName, paramList, null);
        }
        
        public void OnExecuteCommandAsync(string methodName, string paramList, object userState) {
            if ((this.onBeginOnExecuteCommandDelegate == null)) {
                this.onBeginOnExecuteCommandDelegate = new BeginOperationDelegate(this.OnBeginOnExecuteCommand);
            }
            if ((this.onEndOnExecuteCommandDelegate == null)) {
                this.onEndOnExecuteCommandDelegate = new EndOperationDelegate(this.OnEndOnExecuteCommand);
            }
            if ((this.onOnExecuteCommandCompletedDelegate == null)) {
                this.onOnExecuteCommandCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnOnExecuteCommandCompleted);
            }
            base.InvokeAsync(this.onBeginOnExecuteCommandDelegate, new object[] {
                        methodName,
                        paramList}, this.onEndOnExecuteCommandDelegate, this.onOnExecuteCommandCompletedDelegate, userState);
        }
    }
}
