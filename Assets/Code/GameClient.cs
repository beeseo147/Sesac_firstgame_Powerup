using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nettention.Proud;
using System.Net;
using TMPro;
using UnityEngine.tvOS;

public class GameClient : MonoBehaviour
{
    PowerupS2G.Stub stubS2G;
    PowerupS2C.Stub stubS2C;
    PowerupC2S.Proxy proxy;
    NetClient netClient;
    NetConnectionParam param;
    bool connected = false;
    HostID groupHostID = HostID.HostID_None;

    private void Awake()
    {

        netClient = new NetClient();
        param = new NetConnectionParam();
        param.protocolVersion.Set(new System.Guid("{0x489fa1cc,0x5df3,0x4581,{0x96,0x56,0x4d,0x71,0xae,0xc8,0x34,0xf1}}"));
        param.serverPort = 33334;
        param.serverIP = "127.0.0.1";
        proxy = new PowerupC2S.Proxy();
        stubS2C = new PowerupS2C.Stub();
        stubS2G = new PowerupS2G.Stub();
        netClient.AttachProxy(proxy);
        netClient.AttachStub(stubS2G);
        netClient.AttachStub(stubS2C);
        netClient.Connect(param);
        if (netClient.Connect(param) == false) 
        Debug.LogError(string.Format("Failed to connect to server."));


        netClient.JoinServerCompleteHandler = (ErrorInfo info, ByteArray replyFromServer) =>
        {
            if (info.errorType == ErrorType.Ok)
            {
                connected = true;
            }
            proxy.EnterRoom(HostID.HostID_Server, RmiContext.FastEncryptedReliableSend);
        };

        netClient.LeaveServerHandler = (ErrorInfo info) =>
        {
            connected = false;
            proxy.ExitRoom(HostID.HostID_Server, RmiContext.FastEncryptedReliableSend);
        };

        netClient.P2PMemberJoinHandler = (HostID memberHostID, HostID groupHostID, int memberCount, ByteArray customField) =>
        {
            this.groupHostID = groupHostID;
            Debug.Log(string.Format("join P2P group {0}.", this.groupHostID));
        };

        netClient.P2PMemberLeaveHandler = (HostID memberHostID, HostID groupHostID, int memberCount) =>
        {

            Debug.Log(string.Format("leave P2P group {0}.", this.groupHostID));
            this.groupHostID = HostID.HostID_None;
        };
        netClient.ReceivedUserMessageHandler = (sender, rmiContext, payload) =>
        {

        };

        //proxy.Move = (HostID remote, RmiContext ReliableSend, int Key, List<int> enemies) =>
        //{

        //};
        //stubS2G.GameStart = (Message remote, RmiContext ReliableSend, Object hostTag, HostID remote) =>
        //{

        //}




    }

    private void OnGUI()
    {

    }

    void Update()
    {
        netClient.FrameMove();

        
    }
}
