using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nettention.Proud;
using System.Net;
using TMPro;
using UnityEngine.tvOS;
using System;


public class GameClient : Singleton<GameClient>
{
    //���� �ڵ� Stub �������� �۷ι����� �������� Ŭ���̾�Ʈ �ܰ迡���� ����
    //Stub�� ���� �ʿ�
    PowerupS2G.Stub stubS2G;
    PowerupS2C.Stub stubS2C;

    //�۽� �ڵ� Ŭ���̾�Ʈ���� ������ �� �ڷ� ����
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

        stubS2C.PlayerEnter = (HostID remote, RmiContext rmiContext,bool isEnter) =>
        {
            Debug.Log(string.Format("PlayerEnter"));
            print("player Entered to " +(int)remote);
            return true;
        };//Ui�ܰ迡�� BtnType�� Togetherâ���� ����
        stubS2C.PlayerExit = (HostID remote, RmiContext rmiContext, bool isExit) =>
        {
            Debug.Log(string.Format("PlayerExit"));
            print("player Exit to " + (int)remote);
            return true;
        };//�Ѿ�� ���濡�� ������ ���ӵ��� Ȱ��ȭ���� ���
        stubS2G.GameStart = (HostID remote, RmiContext rmiContext) =>
        {
            Debug.Log(string.Format("GameStart"));
            return true;
        };//�����̳� ���� �ܰ�
        stubS2G.GameEnd = (HostID remote, RmiContext rmiContext) =>
        {
            Debug.Log(string.Format("GameEnd"));
            GameManager.Instance.hud.GameOver();
            return true;
        };//GameEnd������ �˷����� HUD���� ����
        stubS2G.PlayerMove = (HostID remote, RmiContext rmiContext, int playerNo, int key, List<int> enemies) =>
        {
            print("PlayerMove : " + playerNo + " is moving to" + key);
            return true;
        };//Player ��ũ��Ʈ���� ����
        stubS2G.PlayersRank = (HostID remote, RmiContext rmiContext, SortedDictionary<int, int> playersRank) =>
        {
            return true;
        };//GameOver ��ũ��Ʈ���� ����
        stubS2G.PlayersReady = (HostID remote, RmiContext rmiContext, SortedDictionary<int, bool> playersReady) =>
        {

            return true;
        };//������ ����ų� ������ ȭ�鿡�� ��ư�� Ȱ��ȭ Ready ��ư ���� �� Ready���¸� ��������.
        stubS2G.TimeNow = (HostID remote, RmiContext rmiContext, long ticksReamain) =>
        {
            ticksReamain = (long)GameManager.Instance.hud.gettime(); // Explicitly cast to long
            return true;
        };//HUD�� TIME�� �޴´�.
        

        netClient.AttachProxy(proxy);
        netClient.AttachStub(stubS2G);
        netClient.AttachStub(stubS2C);

        param = new NetConnectionParam();
        param.protocolVersion.Set(new System.Guid("{489fa1cc-5df3-4581-9656-4d71aec834f1}"));
        param.serverPort = 33334;
        param.serverIP = "127.0.0.1";

        if (netClient.Connect(param) == false)
            Debug.LogError(string.Format("Failed to connect to server."));


    }
    void Update()
    {
        netClient.FrameMove();
    }
    public void CallEnterRoom()
    {
        proxy.EnterRoom(HostID.HostID_Server, RmiContext.ReliableSend);
    }
    public void CallPlayerExit()
    {
        proxy.ExitRoom(HostID.HostID_Server, RmiContext.ReliableSend);
    }
    public void CallGetReady(bool isready)
    {
        proxy.GetReady(HostID.HostID_Server, RmiContext.ReliableSend, isready);
    }
    public void CallMove(int key, List<int> enemies)
    {
        proxy.Move(HostID.HostID_Server, RmiContext.ReliableSend, key,enemies);
    }
    public void CallHasPoint(int point)
    {
        proxy.HasPoint(HostID.HostID_Server, RmiContext.ReliableSend,point);
    }
    public NetClient GetClient()
    {
        return netClient;
    }
}
