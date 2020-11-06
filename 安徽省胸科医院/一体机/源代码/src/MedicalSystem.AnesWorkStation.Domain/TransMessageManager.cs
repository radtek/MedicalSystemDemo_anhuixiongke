﻿using MedicalSystem.AnesWorkStationCoordination.Log;
using MedicalSystem.AnesWorkStationCoordination.Model;
using MedicalSystem.Coordination.Interface;
using MedicalSystem.Message.Common;
using MedicalSystem.Message.Lib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;

namespace MedicalSystem.AnesWorkStation.Domain
{
    /// <summary>
    /// 跨平台通信的管理类
    /// </summary>
    public class TransMessageManager
    {
        private Connection connection = null;                                                          // 和服务端连接的对象
        private object locker = new object();                                                          // 数据锁
        private LoginModel curLoginModel = null;                                                       // 登陆者客户端
        private LoginModel childLoginModel = null;                                                     // 一体机的小麦助手
        private bool childHasLogin = false;                                                        // 判断一体机的小麦助手是否登录
        private List<ClientModel> allClientModelList = new List<ClientModel>();                        // 所有在线用户列表
        private List<TransMessageModel> allMsgList = new List<TransMessageModel>();                    // 所有的消息列表
        private string MedicalSystemMessageServerURL = AppSettings.MedicalSystemMessageServerURL;            //消息平台ip地址

        public TransMessageManager(MED_USERS user)
        {
            curLoginModel = GetCurLoginModel(user);
            string name = this.SetClientName();
            this.connection = new Connection(name, curLoginModel.MessageServerURL);
            this.connection.DelegateSendMessage += this.DelegateSendMessage;
            this.connection.DelegateConnectionClosed += this.DelegateConnectionClosed;
            this.connection.DelegateOnLine += this.DelegateOnLine;
            this.connection.DelegateUnLine += this.DelegateUnLine;
            this.connection.DelegateGetUserList += this.DelegateGetUserDict;
        }
        /// <summary>
        /// 手动连接服务端
        /// </summary>
        public bool OpenConnection()
        {
            bool result = true;
            lock (this.locker)
            {
                try
                {
                    if (null != this.connection && !this.connection.ConnectionStatus)
                    {
                        this.connection.Connect();
                        int i = 0;
                        while(!this.connection.ConnectionStatus && i < 25)
                        {
                            System.Threading.Thread.Sleep(200);
                            i++;
                        }
                    }
                }
                catch
                {
                    result = false;
                }
            }

            return result;
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        public void SendMsg(TransMessageModel transMsgModel)
        {
            if (null != this.connection && this.connection.ConnectionStatus && null != transMsgModel)
            {
                transMsgModel.SendDateTime = DateTime.Now;
                transMsgModel.SourceClientName = JsonConvert.SerializeObject(this.curLoginModel);

                if (transMsgModel.CurEnumMessageType == EnumMessageType.Single)
                {
                    this.connection.SendMessage(transMsgModel.TargetClientName, JsonConvert.SerializeObject(transMsgModel));
                }
                else
                {
                    this.connection.SendALLMessage(JsonConvert.SerializeObject(transMsgModel));
                }
            }
        }

        /// <summary>
        /// 转发消息
        /// </summary>
        /// <param name="transMsgModel">消息体</param>
        /// <param name="targetName">转发消息的接收者</param>
        public void ForwardMsg(TransMessageModel transMsgModel, string targetName)
        {
            if (null != this.connection && this.connection.ConnectionStatus && null != transMsgModel)
            {
                if (transMsgModel.ReceiveDateTime == DateTime.MinValue)
                {
                    transMsgModel.ReceiveDateTime = DateTime.Now;
                }
                this.connection.SendMessage(targetName, JsonConvert.SerializeObject(transMsgModel));
            }
        }

        /// <summary>
        /// 获取客户端名称
        /// </summary>
        private string SetClientName()
        {
            LoginModel tempModel = this.curLoginModel;
            string result = string.Empty;
            result = JsonConvert.SerializeObject(tempModel);
            return result;
        }

        /// <summary>
        /// 获取服务端主动推送消息，同时弹出消息对话框
        /// </summary>
        public void DelegateSendMessage(string sendConnectionId, string sendName, string message)
        {
            try
            {
                LoginModel tar = JsonConvert.DeserializeObject<LoginModel>(sendName);
                if (null == tar || (tar.SessionID.Equals(this.curLoginModel.SessionID)))
                {
                    return;
                }

                // 反序列化消息体
                TransMessageModel curTransMsgModel = JsonConvert.DeserializeObject<TransMessageModel>(message);
                if (null != curTransMsgModel)
                {
                    switch (curTransMsgModel.CurEnumFunctionType)
                    {
                        // 聊天记录
                        case EnumFunctionType.MessageCommunication:
                            this.MessageCommunicatioin(curTransMsgModel);
                            break;

                        // 请求视频通讯
                        case EnumFunctionType.VideoRequest:
                            // Messenger.Default.Send<object>(this, EnumMessageKey.ResponseVideoComm);
                            break;

                        case EnumFunctionType.HasNewVersion:
                            System.Windows.Forms.MessageBox.Show("有新版信息：" + curTransMsgModel.MessageContent);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error("接收消息异常", ex);
            }
        }

        /// <summary>
        /// 聊天记录处理
        /// </summary>
        private void MessageCommunicatioin(TransMessageModel curTransMsgModel)
        {
            try
            {
                if (DateTime.MinValue == curTransMsgModel.ReceiveDateTime)
                {
                    curTransMsgModel.ReceiveDateTime = DateTime.Now;
                }

                // 把消息记录分派到对应的客户
                LoginModel source = JsonConvert.DeserializeObject<LoginModel>(curTransMsgModel.SourceClientName);
                LoginModel target = JsonConvert.DeserializeObject<LoginModel>(curTransMsgModel.TargetClientName);
                // 自身是发送方，则接收方为主
                if (source.SessionID.Equals(this.curLoginModel.SessionID))
                {
                    foreach (ClientModel client in this.allClientModelList)
                    {
                        if (client.CurCoordinationLoginModel.SessionID.Equals(target.SessionID))
                        {
                            curTransMsgModel.CurFlowDirection = FlowDirection.RightToLeft;//聊天图形方向
                            curTransMsgModel.HasRead = true;
                            client.CurMsgRecordList.Add(curTransMsgModel);
                            break;
                        }
                    }
                }
                else
                {
                    if (this.childHasLogin)
                    {
                        // 接收到其他客户端消息
                        foreach (ClientModel client in this.allClientModelList)
                        {
                            if (client.CurCoordinationLoginModel.SessionID.Equals(source.SessionID))
                            {
                                curTransMsgModel.CurFlowDirection = FlowDirection.LeftToRight;//聊天图形方向
                                client.CurMsgRecordList.Add(curTransMsgModel);
                                // 转发给子一体机
                                // curTransMsgModel.HasRead = true;
                                this.ForwardMsg(curTransMsgModel, JsonConvert.SerializeObject(this.ChildLoginModel));
                                break;
                            }
                        }
                    }

                    // Messenger.Default.Send<bool>(true, EnumMessageKey.NewUnreadMessage);
                }

                curTransMsgModel.HasRead = this.childHasLogin;
                this.allMsgList.Add(curTransMsgModel);
            }
            catch (Exception ex)
            {
                Logger.Error("聊天记录处理异常", ex);
            }
        }

        /// <summary>
        /// 手动关闭跨平台的消息连接
        /// </summary>
        public void CloseConnection()
        {
            try
            {
                if (null != this.connection && this.connection.ConnectionStatus)
                {
                    this.connection.Close();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("关闭跨平台消息失败！", ex);
            }
        }

        /// <summary>
        /// 连接失败OR和服务器断开连接
        /// </summary>
        private void DelegateConnectionClosed()
        {
        }

        /// <summary>
        /// 成功连接服务器
        /// </summary>
        private void DelegateOnLine(string connectionId, string name)
        {
            this.connection.GetUserList();
        }

        /// <summary>
        /// 和服务器断开连接
        /// </summary>
        private void DelegateUnLine(string connectionId, string name)
        {
            // 更新列表
            try
            {
                LoginModel unLineModel = JsonConvert.DeserializeObject<LoginModel>(name);
                if (null != unLineModel)
                {
                    ClientModel unLineClient = this.allClientModelList.FirstOrDefault(x => x.CurCoordinationLoginModel.SessionID.Equals(unLineModel.SessionID));
                    if (null != unLineClient)
                    {
                        this.allClientModelList.Remove(unLineClient);
                        // unLineClient.IsOnLine = false;
                    }

                    if (unLineModel.SessionID.Equals(this.ChildLoginModel.SessionID))
                    {
                        this.childHasLogin = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error("消息平台断开服务器异常", ex);
            }
        }

        /// <summary>
        /// 服务端主动推送所有在线的用户列表
        /// </summary>
        public void DelegateGetUserDict(List<MessageUser> curMessageUserList)
        {
            if (null != curMessageUserList)
            {
                int tag = 0;

                foreach (MessageUser item in curMessageUserList)
                {
                    LoginModel tempLoginModel = null;
                    try
                    {
                        tempLoginModel = JsonConvert.DeserializeObject<LoginModel>(item.UserName);
                        if (null == tempLoginModel)
                        {
                            continue;
                        }

                        ClientModel findLogin = this.allClientModelList.FirstOrDefault(x => x.CurCoordinationLoginModel.SessionID.Equals(tempLoginModel.SessionID));
                        if (null == findLogin)
                        {
                            ClientModel cm = new ClientModel()
                            {
                                CurMessageUser = item,
                                CurCoordinationLoginModel = tempLoginModel,
                                CurCallListData = new CallListData(CallStatus.Free,
                                                                   tempLoginModel.Information,
                                                                   tempLoginModel.UserName,
                                                                   tempLoginModel.UserID)
                            };

                            // 标记登陆者
                            if (tempLoginModel.SessionID.Equals(this.curLoginModel.SessionID))
                            {
                                cm.IsLoginClient = true;
                            }
                            else if (!this.childHasLogin && tempLoginModel.SessionID.Equals(this.ChildLoginModel.SessionID))
                            {
                                tag = 1;
                            }

                            this.allClientModelList.Add(cm);
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Error("获取在线用户列表出错!", ex);
                    }
                }

                // 当小麦助手首次登录时将消息记录转发给小麦
                if (tag > 0)
                {
                    // 必须等待小麦助手初始化完成后才能转发消息，不然可能会导致转发的消息丢失
                    for (int i = 0; i < 50; i++)
                    {
                        System.Threading.Thread.Sleep(100);
                        // ExtendAppContext.Current.DoEvents();
                    }

                    this.childHasLogin = true;
                    foreach (TransMessageModel msg in this.allMsgList)
                    {
                        this.ForwardMsg(msg, JsonConvert.SerializeObject(this.ChildLoginModel));
                        msg.HasRead = true;
                    }
                }
            }
        }

        /// <summary>
        /// 主动获取用户列表
        /// </summary>
        public void GetUserDict()
        {
            while (!this.connection.ConnectionStatus)
            {
                System.Threading.Thread.Sleep(100);
            }

            // 获取所有在线用户
            this.connection.GetUserList();
        }





        /// <summary>
        /// 登录消息平台实体类
        /// </summary>
        private LoginModel GetCurLoginModel(MED_USERS user)
        {
            // 自身的ID
            string sessionID = Guid.NewGuid().ToString();
            // 对应的小麦助手的ID
            string childSessionID = Guid.NewGuid().ToString();
            // 一体机的信息
            string information = "平台";
            return new LoginModel(sessionID,
                "Ane",
                EnumAppType.Platform,
                user.LOGIN_NAME,
                user.USER_NAME,
                information,
                MedicalSystemMessageServerURL,
                childSessionID);
        }

        /// <summary>
        /// 小麦助手的登录实体
        /// </summary>
        public LoginModel ChildLoginModel
        {
            get
            {
                if (null == this.childLoginModel)
                {
                    this.childLoginModel = new LoginModel(curLoginModel.ChildSessionID, "Ane",
                                                          EnumAppType.AnesWorkStationAssistant,
                                                          curLoginModel.UserID,
                                                          curLoginModel.UserName,
                                                          curLoginModel.Information,
                                                          MedicalSystemMessageServerURL);
                }

                return this.childLoginModel;
            }
        }

        /// <summary>
        /// 心跳监测
        /// </summary>
        public void SendHeartBeatMessage()
        {
            connection.SendHeartBeatMessage();
        }
        /// <summary>
        /// 获取是否存在未读消息
        /// </summary>
        /// <returns></returns>
        public Boolean GetHasReadMsg()
        {
            return allMsgList.Count(x => x.HasRead == false) > 0 ? true : false;
        }
    }
}
