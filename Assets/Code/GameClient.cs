using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nettention.Proud;
using System.Net;
using TMPro;
using UnityEngine.tvOS;
using System;
using UnityEngine.SceneManagement;


public class GameClient : Singleton<GameClient>
{
    void printMap(List<int> enemies)
    {
        int ix = 0;
        for (int i = 0; i < 3; ++i)
        {
            for (int j = 0; j < 3; ++j)
            {
                print(enemies[ix++] + " ");
            }
            print('\n');
        }
    }
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
    [SerializeField] bool SoloPlayer;
    [SerializeField] int PlayerNumber;
    [SerializeField] int Score;
    public SortedDictionary<int, int> finalplayersRank;
    private void Start()
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

        stubS2C.PlayerEnter = (HostID remote, RmiContext rmiContext,int PlayerNo) =>
        {
            Debug.Log(string.Format("PlayerEnter"));
            print("player : "+PlayerNo+" Entered to " +(int)remote);
            SetPlayerNumber(PlayerNo);
            return true;
        };//Player가 들어왔을때, 자신의 플레이어 명과 어디 호스트에 들어갔는지 알게됨

        stubS2C.PlayerExit = (HostID remote, RmiContext rmiContext, bool isExit) =>
        {
            Debug.Log(string.Format("PlayerExit"));
            print("player Exit to " + (int)remote);
            return true;
        };//Player가 나간순간 어디서 나갔는지 기록
        stubS2G.GameStart = (HostID remote, RmiContext rmiContext) =>
        {
            Debug.Log(string.Format("GameStart"));
            SceneManager.LoadScene("SampleScene");
            GameClient.Instance.SetPlayer(false);
            return true;
        };//2명이상의 플레이어가 Reay상태일때 실행
        stubS2G.GameEnd = (HostID remote, RmiContext rmiContext) =>
        {
            CallHasPoint((int)(GameManager.Instance.hud.getscore()));//
            return true;
        };//게임 종료즉시 스코어값을 입력받아 각각의 유저에게 score값 정리

        stubS2G.PlayerMove = (HostID remote, RmiContext rmiContext, int playerNo, int key, List<int> enemies) =>
        {
            return true;
        };
        // TODO: Player 가 움직일때 어떻게 처리할것인가.
        stubS2G.PlayersRank = (HostID remote, RmiContext rmiContext, SortedDictionary<int, int> playersRank) =>
        {
            finalplayersRank = playersRank;
            return true;
        };//HasPoint실행시 실행되며 playerRank에는 플레이어별 순위가 나타난다.
        //TODO : 플레이어별 순위 말고 현재 점수에 대한 이야기도 해야한다.
        stubS2G.PlayersReady = (HostID remote, RmiContext rmiContext, SortedDictionary<int, bool> playersReady) =>
        {
            foreach (var item in playersReady)
            {
                //print("?÷????" + item.Key + " ?? " + item.Value + "????????.");
            }
            return true;
        };//
        stubS2G.TimeNow = (HostID remote, RmiContext rmiContext, long ticksReamain) =>
        {
            GameManager.Instance.hud.SetTime(ticksReamain);
            return true;
        };//HUD에게 멀티게임의 시간을 전달한다.

        

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

    public bool GetPlayer()
    {
        return SoloPlayer;
    }
    public void SetPlayer(bool solo)
    {
        SoloPlayer = solo;
    }
    public int GetPlayerNumber()
    {
        return PlayerNumber;
    }
    public void SetPlayerNumber(int number)
    {
        PlayerNumber = number;
    }
    public void SetScore(int a)
    {
        Score = a;
    }
    public int GetScore()
    {
        return Score;
    }

    public NetClient GetClient()
    {
        return netClient;
    }
}
