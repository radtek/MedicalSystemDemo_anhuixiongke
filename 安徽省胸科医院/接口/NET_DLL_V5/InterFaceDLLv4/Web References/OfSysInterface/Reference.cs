﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

// 
// 此源代码是由 Microsoft.VSDesigner 4.0.30319.42000 版自动生成。
// 
#pragma warning disable 1591

namespace InterFaceV5.OfSysInterface {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="WebDocareSoap", Namespace="http://tempuri.org/")]
    public partial class WebDocare : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback HelloWorldOperationCompleted;
        
        private System.Threading.SendOrPostCallback OfsysteminterfaceOperationCompleted;
        
        private System.Threading.SendOrPostCallback DocareSysInterfaceOperationCompleted;
        
        private System.Threading.SendOrPostCallback TestOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public WebDocare() {
            this.Url = global::InterFaceV5.Properties.Settings.Default.InterFaceV5_OfSysInterface_WebDocare;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event HelloWorldCompletedEventHandler HelloWorldCompleted;
        
        /// <remarks/>
        public event OfsysteminterfaceCompletedEventHandler OfsysteminterfaceCompleted;
        
        /// <remarks/>
        public event DocareSysInterfaceCompletedEventHandler DocareSysInterfaceCompleted;
        
        /// <remarks/>
        public event TestCompletedEventHandler TestCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/HelloWorld", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string HelloWorld() {
            object[] results = this.Invoke("HelloWorld", new object[0]);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void HelloWorldAsync() {
            this.HelloWorldAsync(null);
        }
        
        /// <remarks/>
        public void HelloWorldAsync(object userState) {
            if ((this.HelloWorldOperationCompleted == null)) {
                this.HelloWorldOperationCompleted = new System.Threading.SendOrPostCallback(this.OnHelloWorldOperationCompleted);
            }
            this.InvokeAsync("HelloWorld", new object[0], this.HelloWorldOperationCompleted, userState);
        }
        
        private void OnHelloWorldOperationCompleted(object arg) {
            if ((this.HelloWorldCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.HelloWorldCompleted(this, new HelloWorldCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/Ofsysteminterface", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string Ofsysteminterface(string paraIn) {
            object[] results = this.Invoke("Ofsysteminterface", new object[] {
                        paraIn});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void OfsysteminterfaceAsync(string paraIn) {
            this.OfsysteminterfaceAsync(paraIn, null);
        }
        
        /// <remarks/>
        public void OfsysteminterfaceAsync(string paraIn, object userState) {
            if ((this.OfsysteminterfaceOperationCompleted == null)) {
                this.OfsysteminterfaceOperationCompleted = new System.Threading.SendOrPostCallback(this.OnOfsysteminterfaceOperationCompleted);
            }
            this.InvokeAsync("Ofsysteminterface", new object[] {
                        paraIn}, this.OfsysteminterfaceOperationCompleted, userState);
        }
        
        private void OnOfsysteminterfaceOperationCompleted(object arg) {
            if ((this.OfsysteminterfaceCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.OfsysteminterfaceCompleted(this, new OfsysteminterfaceCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/DocareSysInterface", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string DocareSysInterface(ParamInputDomain domain) {
            object[] results = this.Invoke("DocareSysInterface", new object[] {
                        domain});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void DocareSysInterfaceAsync(ParamInputDomain domain) {
            this.DocareSysInterfaceAsync(domain, null);
        }
        
        /// <remarks/>
        public void DocareSysInterfaceAsync(ParamInputDomain domain, object userState) {
            if ((this.DocareSysInterfaceOperationCompleted == null)) {
                this.DocareSysInterfaceOperationCompleted = new System.Threading.SendOrPostCallback(this.OnDocareSysInterfaceOperationCompleted);
            }
            this.InvokeAsync("DocareSysInterface", new object[] {
                        domain}, this.DocareSysInterfaceOperationCompleted, userState);
        }
        
        private void OnDocareSysInterfaceOperationCompleted(object arg) {
            if ((this.DocareSysInterfaceCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.DocareSysInterfaceCompleted(this, new DocareSysInterfaceCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/Test", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string Test() {
            object[] results = this.Invoke("Test", new object[0]);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void TestAsync() {
            this.TestAsync(null);
        }
        
        /// <remarks/>
        public void TestAsync(object userState) {
            if ((this.TestOperationCompleted == null)) {
                this.TestOperationCompleted = new System.Threading.SendOrPostCallback(this.OnTestOperationCompleted);
            }
            this.InvokeAsync("Test", new object[0], this.TestOperationCompleted, userState);
        }
        
        private void OnTestOperationCompleted(object arg) {
            if ((this.TestCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.TestCompleted(this, new TestCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1586.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class ParamInputDomain {
        
        private Patient patientField;
        
        private OperatorBase operatorBaseField;
        
        private Operation operationField;
        
        private Bar barField;
        
        private EmrDocUpLoad emrDocUpLoadField;
        
        private ErrInfo errInfoField;
        
        private LabTest labTestField;
        
        private string coltdField;
        
        private string appField;
        
        private string codeField;
        
        private string msgTypeField;
        
        private string routeField;
        
        private string hospitalBranchField;
        
        private string reserved1Field;
        
        private string reserved2Field;
        
        private string reserved3Field;
        
        private string reserved4Field;
        
        private string reserved5Field;
        
        private string reserved6Field;
        
        private string reserved7Field;
        
        private string reserved8Field;
        
        private string reserved9Field;
        
        private string reserved10Field;
        
        private string sendMessageField;
        
        private string receiveMessageField;
        
        private string resultField;
        
        private bool openClientField;
        
        /// <remarks/>
        public Patient Patient {
            get {
                return this.patientField;
            }
            set {
                this.patientField = value;
            }
        }
        
        /// <remarks/>
        public OperatorBase OperatorBase {
            get {
                return this.operatorBaseField;
            }
            set {
                this.operatorBaseField = value;
            }
        }
        
        /// <remarks/>
        public Operation Operation {
            get {
                return this.operationField;
            }
            set {
                this.operationField = value;
            }
        }
        
        /// <remarks/>
        public Bar Bar {
            get {
                return this.barField;
            }
            set {
                this.barField = value;
            }
        }
        
        /// <remarks/>
        public EmrDocUpLoad EmrDocUpLoad {
            get {
                return this.emrDocUpLoadField;
            }
            set {
                this.emrDocUpLoadField = value;
            }
        }
        
        /// <remarks/>
        public ErrInfo ErrInfo {
            get {
                return this.errInfoField;
            }
            set {
                this.errInfoField = value;
            }
        }
        
        /// <remarks/>
        public LabTest LabTest {
            get {
                return this.labTestField;
            }
            set {
                this.labTestField = value;
            }
        }
        
        /// <remarks/>
        public string Coltd {
            get {
                return this.coltdField;
            }
            set {
                this.coltdField = value;
            }
        }
        
        /// <remarks/>
        public string App {
            get {
                return this.appField;
            }
            set {
                this.appField = value;
            }
        }
        
        /// <remarks/>
        public string Code {
            get {
                return this.codeField;
            }
            set {
                this.codeField = value;
            }
        }
        
        /// <remarks/>
        public string MsgType {
            get {
                return this.msgTypeField;
            }
            set {
                this.msgTypeField = value;
            }
        }
        
        /// <remarks/>
        public string Route {
            get {
                return this.routeField;
            }
            set {
                this.routeField = value;
            }
        }
        
        /// <remarks/>
        public string HospitalBranch {
            get {
                return this.hospitalBranchField;
            }
            set {
                this.hospitalBranchField = value;
            }
        }
        
        /// <remarks/>
        public string Reserved1 {
            get {
                return this.reserved1Field;
            }
            set {
                this.reserved1Field = value;
            }
        }
        
        /// <remarks/>
        public string Reserved2 {
            get {
                return this.reserved2Field;
            }
            set {
                this.reserved2Field = value;
            }
        }
        
        /// <remarks/>
        public string Reserved3 {
            get {
                return this.reserved3Field;
            }
            set {
                this.reserved3Field = value;
            }
        }
        
        /// <remarks/>
        public string Reserved4 {
            get {
                return this.reserved4Field;
            }
            set {
                this.reserved4Field = value;
            }
        }
        
        /// <remarks/>
        public string Reserved5 {
            get {
                return this.reserved5Field;
            }
            set {
                this.reserved5Field = value;
            }
        }
        
        /// <remarks/>
        public string Reserved6 {
            get {
                return this.reserved6Field;
            }
            set {
                this.reserved6Field = value;
            }
        }
        
        /// <remarks/>
        public string Reserved7 {
            get {
                return this.reserved7Field;
            }
            set {
                this.reserved7Field = value;
            }
        }
        
        /// <remarks/>
        public string Reserved8 {
            get {
                return this.reserved8Field;
            }
            set {
                this.reserved8Field = value;
            }
        }
        
        /// <remarks/>
        public string Reserved9 {
            get {
                return this.reserved9Field;
            }
            set {
                this.reserved9Field = value;
            }
        }
        
        /// <remarks/>
        public string Reserved10 {
            get {
                return this.reserved10Field;
            }
            set {
                this.reserved10Field = value;
            }
        }
        
        /// <remarks/>
        public string SendMessage {
            get {
                return this.sendMessageField;
            }
            set {
                this.sendMessageField = value;
            }
        }
        
        /// <remarks/>
        public string ReceiveMessage {
            get {
                return this.receiveMessageField;
            }
            set {
                this.receiveMessageField = value;
            }
        }
        
        /// <remarks/>
        public string Result {
            get {
                return this.resultField;
            }
            set {
                this.resultField = value;
            }
        }
        
        /// <remarks/>
        public bool OpenClient {
            get {
                return this.openClientField;
            }
            set {
                this.openClientField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1586.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class Patient {
        
        private string patientIdField;
        
        private System.Nullable<decimal> visitIdField;
        
        private string inpNoField;
        
        private string wardCodeField;
        
        private string deptCodeField;
        
        private System.DateTime startDateField;
        
        private System.DateTime stopDateField;
        
        /// <remarks/>
        public string PatientId {
            get {
                return this.patientIdField;
            }
            set {
                this.patientIdField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Nullable<decimal> VisitId {
            get {
                return this.visitIdField;
            }
            set {
                this.visitIdField = value;
            }
        }
        
        /// <remarks/>
        public string InpNo {
            get {
                return this.inpNoField;
            }
            set {
                this.inpNoField = value;
            }
        }
        
        /// <remarks/>
        public string WardCode {
            get {
                return this.wardCodeField;
            }
            set {
                this.wardCodeField = value;
            }
        }
        
        /// <remarks/>
        public string DeptCode {
            get {
                return this.deptCodeField;
            }
            set {
                this.deptCodeField = value;
            }
        }
        
        /// <remarks/>
        public System.DateTime StartDate {
            get {
                return this.startDateField;
            }
            set {
                this.startDateField = value;
            }
        }
        
        /// <remarks/>
        public System.DateTime StopDate {
            get {
                return this.stopDateField;
            }
            set {
                this.stopDateField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1586.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class LabTest {
        
        private string testNoField;
        
        /// <remarks/>
        public string TestNo {
            get {
                return this.testNoField;
            }
            set {
                this.testNoField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1586.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class ErrInfo {
        
        private bool flagField;
        
        private string errMsgField;
        
        /// <remarks/>
        public bool Flag {
            get {
                return this.flagField;
            }
            set {
                this.flagField = value;
            }
        }
        
        /// <remarks/>
        public string ErrMsg {
            get {
                return this.errMsgField;
            }
            set {
                this.errMsgField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1586.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class EmrDocUpLoad {
        
        private string mrClassField;
        
        private string mrSubClassField;
        
        private System.Nullable<decimal> archiveKeyField;
        
        private System.Nullable<decimal> archiveTimesField;
        
        private System.Nullable<decimal> emrFileIndexField;
        
        private string emrFileNameField;
        
        /// <remarks/>
        public string MrClass {
            get {
                return this.mrClassField;
            }
            set {
                this.mrClassField = value;
            }
        }
        
        /// <remarks/>
        public string MrSubClass {
            get {
                return this.mrSubClassField;
            }
            set {
                this.mrSubClassField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Nullable<decimal> ArchiveKey {
            get {
                return this.archiveKeyField;
            }
            set {
                this.archiveKeyField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Nullable<decimal> ArchiveTimes {
            get {
                return this.archiveTimesField;
            }
            set {
                this.archiveTimesField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Nullable<decimal> EmrFileIndex {
            get {
                return this.emrFileIndexField;
            }
            set {
                this.emrFileIndexField = value;
            }
        }
        
        /// <remarks/>
        public string EmrFileName {
            get {
                return this.emrFileNameField;
            }
            set {
                this.emrFileNameField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1586.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class Bar {
        
        private string barCodeField;
        
        private string barParmField;
        
        /// <remarks/>
        public string BarCode {
            get {
                return this.barCodeField;
            }
            set {
                this.barCodeField = value;
            }
        }
        
        /// <remarks/>
        public string BarParm {
            get {
                return this.barParmField;
            }
            set {
                this.barParmField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1586.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class Operation {
        
        private System.Nullable<decimal> scheduleIdField;
        
        private System.Nullable<decimal> operIdField;
        
        private System.Nullable<System.DateTime> startDataTimeField;
        
        private System.Nullable<System.DateTime> stopDataTimeField;
        
        private System.Nullable<decimal> billAtrField;
        
        private string operStepField;
        
        private System.Nullable<decimal> operStatusField;
        
        private string performedcodeField;
        
        private string hisVisitIdField;
        
        private string hisScheduleIdField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Nullable<decimal> ScheduleId {
            get {
                return this.scheduleIdField;
            }
            set {
                this.scheduleIdField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Nullable<decimal> OperId {
            get {
                return this.operIdField;
            }
            set {
                this.operIdField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Nullable<System.DateTime> StartDataTime {
            get {
                return this.startDataTimeField;
            }
            set {
                this.startDataTimeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Nullable<System.DateTime> StopDataTime {
            get {
                return this.stopDataTimeField;
            }
            set {
                this.stopDataTimeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Nullable<decimal> BillAtr {
            get {
                return this.billAtrField;
            }
            set {
                this.billAtrField = value;
            }
        }
        
        /// <remarks/>
        public string OperStep {
            get {
                return this.operStepField;
            }
            set {
                this.operStepField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Nullable<decimal> OperStatus {
            get {
                return this.operStatusField;
            }
            set {
                this.operStatusField = value;
            }
        }
        
        /// <remarks/>
        public string Performedcode {
            get {
                return this.performedcodeField;
            }
            set {
                this.performedcodeField = value;
            }
        }
        
        /// <remarks/>
        public string HisVisitId {
            get {
                return this.hisVisitIdField;
            }
            set {
                this.hisVisitIdField = value;
            }
        }
        
        /// <remarks/>
        public string HisScheduleId {
            get {
                return this.hisScheduleIdField;
            }
            set {
                this.hisScheduleIdField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1586.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class OperatorBase {
        
        private string operatorField;
        
        private string operatorDeptField;
        
        private System.Nullable<System.DateTime> operateTimeField;
        
        private string userIDField;
        
        private string pWDField;
        
        /// <remarks/>
        public string Operator {
            get {
                return this.operatorField;
            }
            set {
                this.operatorField = value;
            }
        }
        
        /// <remarks/>
        public string OperatorDept {
            get {
                return this.operatorDeptField;
            }
            set {
                this.operatorDeptField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Nullable<System.DateTime> OperateTime {
            get {
                return this.operateTimeField;
            }
            set {
                this.operateTimeField = value;
            }
        }
        
        /// <remarks/>
        public string UserID {
            get {
                return this.userIDField;
            }
            set {
                this.userIDField = value;
            }
        }
        
        /// <remarks/>
        public string PWD {
            get {
                return this.pWDField;
            }
            set {
                this.pWDField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    public delegate void HelloWorldCompletedEventHandler(object sender, HelloWorldCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class HelloWorldCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal HelloWorldCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    public delegate void OfsysteminterfaceCompletedEventHandler(object sender, OfsysteminterfaceCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class OfsysteminterfaceCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal OfsysteminterfaceCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    public delegate void DocareSysInterfaceCompletedEventHandler(object sender, DocareSysInterfaceCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class DocareSysInterfaceCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal DocareSysInterfaceCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    public delegate void TestCompletedEventHandler(object sender, TestCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class TestCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal TestCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591