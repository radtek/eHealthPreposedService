﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace eHPS.BackgroundService.WFService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://tempurl.org", ConfigurationName="WFService.n_webserviceSoap")]
    public interface n_webserviceSoap {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempurl.org/f_get_data", ReplyAction="*")]
        eHPS.BackgroundService.WFService.f_get_dataResponse f_get_data(eHPS.BackgroundService.WFService.f_get_dataRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempurl.org/f_get_data", ReplyAction="*")]
        System.Threading.Tasks.Task<eHPS.BackgroundService.WFService.f_get_dataResponse> f_get_dataAsync(eHPS.BackgroundService.WFService.f_get_dataRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class f_get_dataRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="f_get_data", Namespace="http://tempurl.org", Order=0)]
        public eHPS.BackgroundService.WFService.f_get_dataRequestBody Body;
        
        public f_get_dataRequest() {
        }
        
        public f_get_dataRequest(eHPS.BackgroundService.WFService.f_get_dataRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempurl.org")]
    public partial class f_get_dataRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string as_code;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string as_data;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string as_return;
        
        public f_get_dataRequestBody() {
        }
        
        public f_get_dataRequestBody(string as_code, string as_data, string as_return) {
            this.as_code = as_code;
            this.as_data = as_data;
            this.as_return = as_return;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class f_get_dataResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="f_get_dataResponse", Namespace="http://tempurl.org", Order=0)]
        public eHPS.BackgroundService.WFService.f_get_dataResponseBody Body;
        
        public f_get_dataResponse() {
        }
        
        public f_get_dataResponse(eHPS.BackgroundService.WFService.f_get_dataResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempurl.org")]
    public partial class f_get_dataResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=0)]
        public int f_get_dataResult;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string as_data;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string as_return;
        
        public f_get_dataResponseBody() {
        }
        
        public f_get_dataResponseBody(int f_get_dataResult, string as_data, string as_return) {
            this.f_get_dataResult = f_get_dataResult;
            this.as_data = as_data;
            this.as_return = as_return;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface n_webserviceSoapChannel : eHPS.BackgroundService.WFService.n_webserviceSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class n_webserviceSoapClient : System.ServiceModel.ClientBase<eHPS.BackgroundService.WFService.n_webserviceSoap>, eHPS.BackgroundService.WFService.n_webserviceSoap {
        
        public n_webserviceSoapClient() {
        }
        
        public n_webserviceSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public n_webserviceSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public n_webserviceSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public n_webserviceSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        eHPS.BackgroundService.WFService.f_get_dataResponse eHPS.BackgroundService.WFService.n_webserviceSoap.f_get_data(eHPS.BackgroundService.WFService.f_get_dataRequest request) {
            return base.Channel.f_get_data(request);
        }
        
        public int f_get_data(string as_code, ref string as_data, ref string as_return) {
            eHPS.BackgroundService.WFService.f_get_dataRequest inValue = new eHPS.BackgroundService.WFService.f_get_dataRequest();
            inValue.Body = new eHPS.BackgroundService.WFService.f_get_dataRequestBody();
            inValue.Body.as_code = as_code;
            inValue.Body.as_data = as_data;
            inValue.Body.as_return = as_return;
            eHPS.BackgroundService.WFService.f_get_dataResponse retVal = ((eHPS.BackgroundService.WFService.n_webserviceSoap)(this)).f_get_data(inValue);
            as_data = retVal.Body.as_data;
            as_return = retVal.Body.as_return;
            return retVal.Body.f_get_dataResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<eHPS.BackgroundService.WFService.f_get_dataResponse> eHPS.BackgroundService.WFService.n_webserviceSoap.f_get_dataAsync(eHPS.BackgroundService.WFService.f_get_dataRequest request) {
            return base.Channel.f_get_dataAsync(request);
        }
        
        public System.Threading.Tasks.Task<eHPS.BackgroundService.WFService.f_get_dataResponse> f_get_dataAsync(string as_code, string as_data, string as_return) {
            eHPS.BackgroundService.WFService.f_get_dataRequest inValue = new eHPS.BackgroundService.WFService.f_get_dataRequest();
            inValue.Body = new eHPS.BackgroundService.WFService.f_get_dataRequestBody();
            inValue.Body.as_code = as_code;
            inValue.Body.as_data = as_data;
            inValue.Body.as_return = as_return;
            return ((eHPS.BackgroundService.WFService.n_webserviceSoap)(this)).f_get_dataAsync(inValue);
        }
    }
}
