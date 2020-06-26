using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.DataAdapter.Entities;
using EntFrm.Framework.Utility;
using Newtonsoft.Json;
using System;
using System.Net;

namespace EntFrm.DataAdapter.Services
{
    public class NettyHostHandler : SimpleChannelInboundHandler<string>
    { 

        //重写基类方法，当链接上服务器后，马上发送Hello World消息到服务端
        public override void ChannelActive(IChannelHandlerContext context)
        {
            base.ChannelActive(context);
        }

        public override void ChannelInactive(IChannelHandlerContext context)
        {
            base.ChannelInactive(context);
            //删除
            NettyChannelMap.delChannel((ISocketChannel)context.Channel);
        }

        public override void ExceptionCaught(IChannelHandlerContext context, Exception exception)
        {
            Console.WriteLine("Exception: " + exception);
            //context.CloseAsync();
        }

        protected override void ChannelRead0(IChannelHandlerContext context, string message)
        {
            try
            {
                string ipAddress = ((IPEndPoint)context.Channel.RemoteAddress).Address.ToString().Substring(7);

                NettyData nettyData = JsonConvert.DeserializeObject<NettyData>(message);
                if (nettyData != null)
                {
                    switch (nettyData.type)
                    {
                        case NettyType.HRTBEAT:
                            AddNewHeartbeat(nettyData.data);
                            break;
                        case NettyType.REGISTER:
                            NettyChannelMap.addChannel(nettyData.devCode, (ISocketChannel)context.Channel);
                            AddNewRegister(nettyData.data);
                            break;
                        case NettyType.RESULT:
                            AddNewCmdresult(ipAddress,nettyData.data);
                            break;
                        case NettyType.COMMAND:
                            ExecuteCommand(nettyData.devCode,nettyData.data);
                            break;
                        default: break;
                    }
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void AddNewRegister(string registInfo)
        {
            try
            {
                int count = 0;
                HeartBeat heartBeat = JsonConvert.DeserializeObject<HeartBeat>(registInfo);
                DsPlayerInfo player = null;

                DsPlayerInfoBLL playerBLL = new DsPlayerInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());
                DsPlayerInfoCollections playerColl = playerBLL.GetRecordsByPaging(ref count, 1, 1, " PlayerCode='" + heartBeat.PlayerCode + "' ");
                if (playerColl != null && playerColl.Count > 0)
                {
                    //player = playerColl.GetFirstOne();
                    //player.sIpAddress = heartBeat.IpAddress;
                    //player.iLocalPort = int.Parse(heartBeat.LocalPort);
                    //player.sMacAddress = heartBeat.MacAddress;
                    //player.sResolution = heartBeat.Resolution;
                    //player.sApVersion = heartBeat.ApVersion;
                    //player.sOSVersion = heartBeat.OSVersion;

                    //player.dModDate = DateTime.Now;

                    //playerBLL.UpdateRecord(player);
                }
                else
                {
                    player = new DsPlayerInfo();
                    player.sPlayerNo = CommonHelper.Get_New12ByteGuid();
                    player.sPlayerName = heartBeat.PlayerCode;
                    player.sPlayerCode = heartBeat.PlayerCode;
                    player.sPClassNo = "";
                    player.sIpAddress = heartBeat.IpAddress;
                    player.iLocalPort = int.Parse(heartBeat.LocalPort);
                    player.sMacAddress = heartBeat.MacAddress;
                    player.sResolution = heartBeat.Resolution;
                    player.sParamsFmt = "";
                    player.iOnlineState = 0;
                    player.dOnDuration = 0;
                    player.sApVersion = heartBeat.ApVersion; 
                    player.sOSVersion = heartBeat.OSVersion;
                    player.sStartupTime = "1970-01-01 06:00:00";
                    player.sShutdownTime = "1970-01-01 22:00:00";
                    player.sMachineCode = "";
                    player.iIsAuthorize = 0;
                    player.iCheckState = 0;
                    player.sBranchNo = "";

                    player.sAddOptor = "";
                    player.dAddDate = DateTime.Now;
                    player.sModOptor = "";
                    player.dModDate = DateTime.Now;
                    player.iValidityState = 1;
                    player.sComments = "";
                    player.sAppCode = IUserContext.GetAppCode() + ";";

                     playerBLL.AddNewRecord(player); 
                }
            }
            catch (Exception ex)
            {
                MainFrame.PrintMessage(ex.Message);
            }
        }

        private void AddNewHeartbeat(string hrtbeatInfo)
        {
            try
            {
                HeartBeat heartBeat = JsonConvert.DeserializeObject<HeartBeat>(hrtbeatInfo);

                if (heartBeat != null)
                {
                    DsHrtbeatFlowsBLL flowBLL = new DsHrtbeatFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());
                    DsPlayerInfoBLL playerBLL = new DsPlayerInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());

                    int count = playerBLL.GetCountByCondition("PlayerCode='"+heartBeat.PlayerCode+"' And CheckState=1 ");

                    if (count > 0)
                    {
                        DsHrtbeatFlows flow = new DsHrtbeatFlows();
                        flow.sBFlowNo = CommonHelper.Get_New12ByteGuid();
                        flow.sPlayerNo = IPublicHelper.getPlayerNoByCode(heartBeat.PlayerCode);
                        flow.sPlayerCode = heartBeat.PlayerCode;
                        flow.sIpAddress = heartBeat.IpAddress;
                        flow.iLocalPort = int.Parse(heartBeat.LocalPort);
                        flow.dRegistDate = DateTime.Now;
                        flow.iCheckState = 0;

                        flow.sAddOptor = "";
                        flow.dAddDate = DateTime.Now;
                        flow.sModOptor = "";
                        flow.dModDate = DateTime.Now;
                        flow.iValidityState = 1;
                        flow.sComments = hrtbeatInfo;
                        flow.sAppCode = IUserContext.GetAppCode() + ";";

                        flowBLL.AddNewRecord(flow);
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private void AddNewCmdresult(string ipAddress, string cmdresultInfo)
        {
            try
            {
                ResultData resultData = JsonConvert.DeserializeObject<ResultData>(cmdresultInfo);

                if (resultData != null)
                {
                    DsResultFlowsBLL infoBLL = new DsResultFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());
                    DsResultFlows info = new DsResultFlows();
                    info.sRFlowNo = CommonHelper.Get_New12ByteGuid();
                    info.sClassNo = "1";
                    info.sRetCode = resultData.code.ToString();
                    info.sRetError = resultData.error;
                    info.sIpAddress = ipAddress;
                    if (!string.IsNullOrEmpty(resultData.result))
                    {
                        string[] retArr = resultData.result.Split('|');

                        info.sCmdCode = retArr[0];
                        if (retArr[0].Equals("Snapshot"))
                        {
                            info.sCmdResult = retArr[1].Replace("//n", "");
                        }
                        //更新下载状态更新
                        else if (retArr[0].Equals("DloadStatus"))
                        {
                            info.sCmdResult = retArr[1];
                            UpdateDloadStatus(retArr[1]);
                        }
                        //更新下载进度
                        else if (retArr[0].Equals("DloadProgress"))
                        {
                            info.sCmdResult = retArr[1];
                            UpdateDloadProgress(retArr[1]);
                        }
                        else
                        {
                            info.sCmdResult = retArr[1];
                        }
                    }
                    else
                    {
                        info.sCmdCode = "";
                        info.sCmdResult = "";
                    }

                    info.sAddOptor = "";
                    info.dAddDate = DateTime.Now;
                    info.sModOptor = "";
                    info.dModDate = DateTime.Now;
                    info.iValidityState = 1;
                    info.sComments = "";
                    info.sAppCode = IUserContext.GetAppCode() + ";";

                    infoBLL.AddNewRecord(info);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ExecuteCommand(string devCode, string commandInfo)
        {
            try
            {
                CmmdData cmdData = JsonConvert.DeserializeObject<CmmdData>(commandInfo);

                if (cmdData != null && cmdData.cmmdType.Equals("MAdapter"))
                {
                    NettyHostService.CreateInstance().SendCommandData(devCode, commandInfo);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //更新下载状态
        private void UpdateDloadStatus(String result)
        {
            try
            {
                string[] args = result.Split(',');
                string status = args[0];
                string devCode = args[1];
                string programId = args[2];

                int i = 0;
                string playerNo = IPublicHelper.getPlayerNoByCode(devCode);
                DsDwloadFlowsBLL infoBLL = new DsDwloadFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());
                DsDwloadFlowsCollections infoColl = infoBLL.GetRecordsByPaging(ref i,1,1, "PlayerNo='"+ playerNo + "' And ProgmNo='"+programId+"' ");

                if(infoColl!=null&& infoColl.Count > 0)
                {
                    DsDwloadFlows info = infoColl.GetFirstOne();

                    info.sDloadStatus = status;
                    info.sDloadProgress = "";
                    info.dModDate = DateTime.Now;

                    infoBLL.UpdateRecord(info);
                }

            }
            catch (Exception e)
            { 
            }
        }

        //更新下载进度
        private void UpdateDloadProgress(String result)
        {
            try
            {

                String[] args = result.Split(',');
                String progress = args[0];
                String devCode = args[1];
                String programId = args[2];

                int i = 0;
                string playerNo = IPublicHelper.getPlayerNoByCode(devCode);
                DsDwloadFlowsBLL infoBLL = new DsDwloadFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());
                DsDwloadFlowsCollections infoColl = infoBLL.GetRecordsByPaging(ref i, 1, 1, "PlayerNo='" + playerNo + "' And ProgmNo='" + programId + "' ");

                if (infoColl != null && infoColl.Count > 0)
                {
                    DsDwloadFlows info = infoColl.GetFirstOne();

                    info.sDloadProgress = progress+"%";
                    info.dModDate = DateTime.Now;

                    infoBLL.UpdateRecord(info);
                }
            }
            catch (Exception e)
            { 
            }
        }
    }
}