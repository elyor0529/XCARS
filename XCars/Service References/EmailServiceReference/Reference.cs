﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace XCars.EmailServiceReference {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="CompositeType", Namespace="http://schemas.datacontract.org/2004/07/EmailService")]
    [System.SerializableAttribute()]
    public partial class CompositeType : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool BoolValueField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string StringValueField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool BoolValue {
            get {
                return this.BoolValueField;
            }
            set {
                if ((this.BoolValueField.Equals(value) != true)) {
                    this.BoolValueField = value;
                    this.RaisePropertyChanged("BoolValue");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string StringValue {
            get {
                return this.StringValueField;
            }
            set {
                if ((object.ReferenceEquals(this.StringValueField, value) != true)) {
                    this.StringValueField = value;
                    this.RaisePropertyChanged("StringValue");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="EmailServiceReference.ISMTPService")]
    public interface ISMTPService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISMTPService/Send", ReplyAction="http://tempuri.org/ISMTPService/SendResponse")]
        void Send(string fromAddress, string toAddress, string messageSubject, string body);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISMTPService/Send", ReplyAction="http://tempuri.org/ISMTPService/SendResponse")]
        System.Threading.Tasks.Task SendAsync(string fromAddress, string toAddress, string messageSubject, string body);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISMTPService/GetDataUsingDataContract", ReplyAction="http://tempuri.org/ISMTPService/GetDataUsingDataContractResponse")]
        XCars.EmailServiceReference.CompositeType GetDataUsingDataContract(XCars.EmailServiceReference.CompositeType composite);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISMTPService/GetDataUsingDataContract", ReplyAction="http://tempuri.org/ISMTPService/GetDataUsingDataContractResponse")]
        System.Threading.Tasks.Task<XCars.EmailServiceReference.CompositeType> GetDataUsingDataContractAsync(XCars.EmailServiceReference.CompositeType composite);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ISMTPServiceChannel : XCars.EmailServiceReference.ISMTPService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class SMTPServiceClient : System.ServiceModel.ClientBase<XCars.EmailServiceReference.ISMTPService>, XCars.EmailServiceReference.ISMTPService {
        
        public SMTPServiceClient() {
        }
        
        public SMTPServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public SMTPServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SMTPServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SMTPServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void Send(string fromAddress, string toAddress, string messageSubject, string body) {
            base.Channel.Send(fromAddress, toAddress, messageSubject, body);
        }
        
        public System.Threading.Tasks.Task SendAsync(string fromAddress, string toAddress, string messageSubject, string body) {
            return base.Channel.SendAsync(fromAddress, toAddress, messageSubject, body);
        }
        
        public XCars.EmailServiceReference.CompositeType GetDataUsingDataContract(XCars.EmailServiceReference.CompositeType composite) {
            return base.Channel.GetDataUsingDataContract(composite);
        }
        
        public System.Threading.Tasks.Task<XCars.EmailServiceReference.CompositeType> GetDataUsingDataContractAsync(XCars.EmailServiceReference.CompositeType composite) {
            return base.Channel.GetDataUsingDataContractAsync(composite);
        }
    }
}
