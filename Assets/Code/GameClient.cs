using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nettention.Proud;
using System.Net;
using TMPro;
using UnityEngine.SceneManagement;


public class GameClient : Singleton<GameClient>
{
    //void printMap(List<int> enemies)
    //{
    //    int ix = 0;
    //    for (int i = 0; i < 3; ++i)
    //    {
    //        for (int j = 0; j < 3; ++j)
    //        {
    //            print(enemies[ix++] + " ");
    //        }
    //        print('\n');
    //    }
    //}
    //���� �ڵ� Stub �������� �۷ι����� �������� Ŭ���̾�Ʈ �ܰ迡���� ����
    //Stub�� ���� �ʿ�
    PowerupS2G.Stub stubS2G;
    PowerupS2C.Stub stubS2C;

    //�۽� �ڵ� Ŭ���̾�Ʈ���� ������ �� �ڷ� ����
    PowerupC2S.Proxy proxy;
    Console console;
    NetClient netClient;
    NetConnectionParam param;
    bool connected = false;
    HostID groupHostID = HostID.HostID_None;
    [SerializeField] bool SoloPlayer;
    [SerializeField] int PlayerNumber;
    [SerializeField] int Score;
    [SerializeField] int PlayerCount=0;
    public SortedDictionary<int, int> finalplayersRank;
    private void Start()
    {
        console = GameObject.Find("Content").GetComponent<Console>();
        netClient = new NetClient();

        proxy = new PowerupC2S.Proxy();
        stubS2C = new PowerupS2C.Stub();
        //������ ���� Ŭ���̾�Ʈ���� �˸��°͵�
        stubS2G = new PowerupS2G.Stub();
        //������ ��� Ŭ���̾�Ʈ���� �˸��°͵�

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
            print("player : "+PlayerNo+" Entered to " +(int)remote);
            SetPlayerNumber(PlayerNo);
            return true;
        };//Player�� ��������, �ڽ��� �÷��̾� ��� ��� ȣ��Ʈ�� ������ �˰Ե�

        stubS2C.PlayerExit = (HostID remote, RmiContext rmiContext, bool isExit) =>
        {
            
            print("player Exit to " + remote);
            return true;
        };//Player�� �������� ��� �������� ���
        stubS2G.GameStart = (HostID remote, RmiContext rmiContext) =>
        {
            SceneManager.LoadScene("GameScene");
            GameClient.Instance.SetPlayer(false);
            return true;
        };
        /*2���̻��� �÷��̾ Reay�����϶� ����
         * Main ������ �ҷ�����
        �� ���� Ŭ���̾�Ʈ���� �ַ��÷��̰� �ƴ��� �˸�*/
        stubS2G.GameEnd = (HostID remote, RmiContext rmiContext) =>
        {
            CallHasPoint((int)(GameManager.Instance.hud.getscore()));
            return true;
        };//���� ������� ���ھ�� �Է¹޾� ������ �������� score�� ����

        stubS2G.PlayerMove = (HostID remote, RmiContext rmiContext, int playerNo, int key, List<int> enemies) =>
        {
            return true;
        };
        // TODO: Player �� �����϶� ��� ó���Ұ��ΰ�.
        stubS2G.PlayersRank = (HostID remote, RmiContext rmiContext, SortedDictionary<int, int> playersRank) =>
        {
            finalplayersRank = playersRank;
            return true;
        };//HasPoint����� ����Ǹ� playerRank���� �÷��̾ ������ ��Ÿ����.

        stubS2G.PlayersReady = (HostID remote, RmiContext rmiContext, SortedDictionary<int, bool> playersReady) =>
        {
            PlayerCount = playersReady.Count;
            console.set(playersReady);
            return true;
        };//Ready ��ư�� ������ 
        stubS2G.TimeNow = (HostID remote, RmiContext rmiContext, long ticksReamain) =>
        {
            GameManager.Instance.hud.SetTime(ticksReamain);
            return true;
        };//HUD���� ��Ƽ������ �ð��� �����Ѵ�.

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
    public int GetPlayerCount()
    {
        return PlayerCount;
    }

}
