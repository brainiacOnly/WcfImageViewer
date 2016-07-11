﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WcfImageViewer.Services.Tests.PictureClient {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Picture", Namespace="http://schemas.datacontract.org/2004/07/WcfImageViewer.Contracts")]
    [System.SerializableAttribute()]
    public partial class Picture : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime CreationDateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private byte[] ImageField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
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
        public System.DateTime CreationDate {
            get {
                return this.CreationDateField;
            }
            set {
                if ((this.CreationDateField.Equals(value) != true)) {
                    this.CreationDateField = value;
                    this.RaisePropertyChanged("CreationDate");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public byte[] Image {
            get {
                return this.ImageField;
            }
            set {
                if ((object.ReferenceEquals(this.ImageField, value) != true)) {
                    this.ImageField = value;
                    this.RaisePropertyChanged("Image");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
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
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="PictureClient.IPictureManager")]
    public interface IPictureManager {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPictureManager/GetAll", ReplyAction="http://tempuri.org/IPictureManager/GetAllResponse")]
        WcfImageViewer.Services.Tests.PictureClient.Picture[] GetAll();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPictureManager/GetAll", ReplyAction="http://tempuri.org/IPictureManager/GetAllResponse")]
        System.Threading.Tasks.Task<WcfImageViewer.Services.Tests.PictureClient.Picture[]> GetAllAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPictureManager/Get", ReplyAction="http://tempuri.org/IPictureManager/GetResponse")]
        byte[] Get(string name);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPictureManager/Get", ReplyAction="http://tempuri.org/IPictureManager/GetResponse")]
        System.Threading.Tasks.Task<byte[]> GetAsync(string name);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPictureManager/Upload", ReplyAction="http://tempuri.org/IPictureManager/UploadResponse")]
        void Upload(WcfImageViewer.Services.Tests.PictureClient.Picture picture);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPictureManager/Upload", ReplyAction="http://tempuri.org/IPictureManager/UploadResponse")]
        System.Threading.Tasks.Task UploadAsync(WcfImageViewer.Services.Tests.PictureClient.Picture picture);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IPictureManagerChannel : WcfImageViewer.Services.Tests.PictureClient.IPictureManager, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class PictureManagerClient : System.ServiceModel.ClientBase<WcfImageViewer.Services.Tests.PictureClient.IPictureManager>, WcfImageViewer.Services.Tests.PictureClient.IPictureManager {
        
        public PictureManagerClient() {
        }
        
        public PictureManagerClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public PictureManagerClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public PictureManagerClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public PictureManagerClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public WcfImageViewer.Services.Tests.PictureClient.Picture[] GetAll() {
            return base.Channel.GetAll();
        }
        
        public System.Threading.Tasks.Task<WcfImageViewer.Services.Tests.PictureClient.Picture[]> GetAllAsync() {
            return base.Channel.GetAllAsync();
        }
        
        public byte[] Get(string name) {
            return base.Channel.Get(name);
        }
        
        public System.Threading.Tasks.Task<byte[]> GetAsync(string name) {
            return base.Channel.GetAsync(name);
        }
        
        public void Upload(WcfImageViewer.Services.Tests.PictureClient.Picture picture) {
            base.Channel.Upload(picture);
        }
        
        public System.Threading.Tasks.Task UploadAsync(WcfImageViewer.Services.Tests.PictureClient.Picture picture) {
            return base.Channel.UploadAsync(picture);
        }
    }
}
