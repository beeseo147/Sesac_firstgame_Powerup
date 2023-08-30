using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nettention.Proud;
using System.Net;
using TMPro;
using UnityEngine.tvOS;


public class GameClient : MonoBehaviour
{
    //수신 코드 Stub 서버에서 글로벌에게 서버에서 클라이언트 단계에서의 구현
    //Stub의 구현 필요
    PowerupS2G.Stub stubS2G;
    PowerupS2C.Stub stubS2C;

    //송신 코드 클라이언트에서 서버로 갈 자료 정리
    PowerupC2S.Proxy proxy;

    NetClient netClient;
    NetConnectionParam param;
    bool connected = false;
    HostID groupHostID = HostID.HostID_None;


    private void Awake()
    {
        netClient = new NetClient();

        proxy = new PowerupC2S.Proxy();
        stubS2C = new PowerupS2C.Stub();
        stubS2G = new PowerupS2G.Stub();

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

        stubS2G.PlayersReady = (Nettention.Proud.HostID remote, Nettention.Proud.RmiContext rmiContext, SortedDictionary<int, bool> playersReady) =>
        {
            return true;
        };
        netClient.AttachProxy(proxy);
        netClient.AttachStub(stubS2G);
        netClient.AttachStub(stubS2C);

        param = new NetConnectionParam();
        param.protocolVersion.Set(new System.Guid("{489fa1cc-5df3-4581-9656-4d71aec834f1}"));
        param.serverPort = 33334;
        param.serverIP = "127.0.0.1";

        if (netClient.Connect(param) == false) ;
            Debug.LogError(string.Format("Failed to connect to server."));


    }

    private void OnGUI()
    {

    }

    void Update()
    {
        netClient.FrameMove();

        
    }
}
