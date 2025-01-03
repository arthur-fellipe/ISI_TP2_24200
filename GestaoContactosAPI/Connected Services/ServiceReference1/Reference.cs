﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ServiceReference1
{
    using System.Runtime.Serialization;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.2.0-preview1.23462.5")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Contact", Namespace="http://schemas.datacontract.org/2004/07/GestaoContactosSOAP.Models")]
    public partial class Contact : object
    {
        
        private string EmailField;
        
        private int IDField;
        
        private string NomeField;
        
        private string TelefoneField;
        
        private int UserIDField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Email
        {
            get
            {
                return this.EmailField;
            }
            set
            {
                this.EmailField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ID
        {
            get
            {
                return this.IDField;
            }
            set
            {
                this.IDField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Nome
        {
            get
            {
                return this.NomeField;
            }
            set
            {
                this.NomeField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Telefone
        {
            get
            {
                return this.TelefoneField;
            }
            set
            {
                this.TelefoneField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int UserID
        {
            get
            {
                return this.UserIDField;
            }
            set
            {
                this.UserIDField = value;
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.2.0-preview1.23462.5")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.IContactService")]
    public interface IContactService
    {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IContactService/GetAllContacts", ReplyAction="http://tempuri.org/IContactService/GetAllContactsResponse")]
        System.Threading.Tasks.Task<ServiceReference1.Contact[]> GetAllContactsAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IContactService/GetContactById", ReplyAction="http://tempuri.org/IContactService/GetContactByIdResponse")]
        System.Threading.Tasks.Task<ServiceReference1.Contact> GetContactByIdAsync(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IContactService/AddContact", ReplyAction="http://tempuri.org/IContactService/AddContactResponse")]
        System.Threading.Tasks.Task<int> AddContactAsync(ServiceReference1.Contact contact);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IContactService/UpdateContact", ReplyAction="http://tempuri.org/IContactService/UpdateContactResponse")]
        System.Threading.Tasks.Task<int> UpdateContactAsync(ServiceReference1.Contact contact);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IContactService/DeleteContact", ReplyAction="http://tempuri.org/IContactService/DeleteContactResponse")]
        System.Threading.Tasks.Task<int> DeleteContactAsync(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IContactService/AddUser", ReplyAction="http://tempuri.org/IContactService/AddUserResponse")]
        System.Threading.Tasks.Task AddUserAsync(string username, string passwordHash);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IContactService/ValidateUser", ReplyAction="http://tempuri.org/IContactService/ValidateUserResponse")]
        System.Threading.Tasks.Task<bool> ValidateUserAsync(string username, string passwordHash);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.2.0-preview1.23462.5")]
    public interface IContactServiceChannel : ServiceReference1.IContactService, System.ServiceModel.IClientChannel
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.2.0-preview1.23462.5")]
    public partial class ContactServiceClient : System.ServiceModel.ClientBase<ServiceReference1.IContactService>, ServiceReference1.IContactService
    {
        
        /// <summary>
        /// Implement this partial method to configure the service endpoint.
        /// </summary>
        /// <param name="serviceEndpoint">The endpoint to configure</param>
        /// <param name="clientCredentials">The client credentials</param>
        static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);
        
        public ContactServiceClient() : 
                base(ContactServiceClient.GetDefaultBinding(), ContactServiceClient.GetDefaultEndpointAddress())
        {
            this.Endpoint.Name = EndpointConfiguration.BasicHttpBinding_IContactService.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public ContactServiceClient(EndpointConfiguration endpointConfiguration) : 
                base(ContactServiceClient.GetBindingForEndpoint(endpointConfiguration), ContactServiceClient.GetEndpointAddress(endpointConfiguration))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public ContactServiceClient(EndpointConfiguration endpointConfiguration, string remoteAddress) : 
                base(ContactServiceClient.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public ContactServiceClient(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(ContactServiceClient.GetBindingForEndpoint(endpointConfiguration), remoteAddress)
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public ContactServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress)
        {
        }
        
        public System.Threading.Tasks.Task<ServiceReference1.Contact[]> GetAllContactsAsync()
        {
            return base.Channel.GetAllContactsAsync();
        }
        
        public System.Threading.Tasks.Task<ServiceReference1.Contact> GetContactByIdAsync(int id)
        {
            return base.Channel.GetContactByIdAsync(id);
        }
        
        public System.Threading.Tasks.Task<int> AddContactAsync(ServiceReference1.Contact contact)
        {
            return base.Channel.AddContactAsync(contact);
        }
        
        public System.Threading.Tasks.Task<int> UpdateContactAsync(ServiceReference1.Contact contact)
        {
            return base.Channel.UpdateContactAsync(contact);
        }
        
        public System.Threading.Tasks.Task<int> DeleteContactAsync(int id)
        {
            return base.Channel.DeleteContactAsync(id);
        }
        
        public System.Threading.Tasks.Task AddUserAsync(string username, string passwordHash)
        {
            return base.Channel.AddUserAsync(username, passwordHash);
        }
        
        public System.Threading.Tasks.Task<bool> ValidateUserAsync(string username, string passwordHash)
        {
            return base.Channel.ValidateUserAsync(username, passwordHash);
        }
        
        public virtual System.Threading.Tasks.Task OpenAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndOpen));
        }
        
        private static System.ServiceModel.Channels.Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.BasicHttpBinding_IContactService))
            {
                System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
                result.MaxReceivedMessageSize = int.MaxValue;
                result.AllowCookies = true;
                return result;
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.BasicHttpBinding_IContactService))
            {
                return new System.ServiceModel.EndpointAddress("http://localhost:55211/ContactService.svc");
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.Channels.Binding GetDefaultBinding()
        {
            return ContactServiceClient.GetBindingForEndpoint(EndpointConfiguration.BasicHttpBinding_IContactService);
        }
        
        private static System.ServiceModel.EndpointAddress GetDefaultEndpointAddress()
        {
            return ContactServiceClient.GetEndpointAddress(EndpointConfiguration.BasicHttpBinding_IContactService);
        }
        
        public enum EndpointConfiguration
        {
            
            BasicHttpBinding_IContactService,
        }
    }
}
