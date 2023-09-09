using PowerupC2S;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Unity.Collections.AllocatorManager;

public class BtnType : MonoBehaviour
{
    bool isSound;
    public BTNType Key;
    [SerializeField] CanvasGroup mainGroup;
    [SerializeField] CanvasGroup optionGroup;
    [SerializeField] CanvasGroup PlayGroup;
    [SerializeField] CanvasGroup MultiGroup;
    public GameClient gameClient;
    private void Awake()
    {
        gameClient = GameObject.Find("Game Client").GetComponent<GameClient>();
        mainGroup = GameObject.Find("MainMenu").GetComponent<CanvasGroup>();
        optionGroup = GameObject.Find("OptionMenu").GetComponent<CanvasGroup>();

        PlayGroup = GameObject.Find("PlayMenu").GetComponent<CanvasGroup>();
        MultiGroup = GameObject.Find("MultiMenu").GetComponent<CanvasGroup>();

    }
    public void OnBtnClick()
    {
        Debug.Log(Key);
        AudioManager.Instance.PlaySFX("ClikKey");
        switch (Key)
        {
            case BTNType.Play:
                CanvasGroupOn(PlayGroup);
                CanvasGroupOff(mainGroup);
                CanvasGroupOff(optionGroup);
                break;
            case BTNType.Option:
                CanvasGroupOn(optionGroup);
                CanvasGroupOff(mainGroup);
                CanvasGroupOff(PlayGroup);
                break;
            case BTNType.Quit:
#if UNITY_EDITOR

                UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
                break;

            case BTNType.Sound:
                if (isSound)
                {
                    AudioManager.Instance.musicSource.Stop();
                }
                else
                    AudioManager.Instance.musicSource.Play();
                isSound = !isSound;
                break;
            case BTNType.Back:
                CanvasGroupOn(mainGroup);
                CanvasGroupOff(optionGroup);
                CanvasGroupOff(PlayGroup);
                CanvasGroupOff(MultiGroup);
                PlayerExit();
                break;

            case BTNType.Solo:
                SceneManager.LoadScene("SampleScene");
                GameManager.Instance.SetPlayer(true);
                Debug.Log("게임실행");
                break;
            case BTNType.Togeter:
                Debug.Log("함께하기");
                CanvasGroupOff(PlayGroup);
                CanvasGroupOff(mainGroup);
                CanvasGroupOff(optionGroup);
                CanvasGroupOn(MultiGroup);
                PlayerEnter();
                break;
            case BTNType.Ready:
                PlayerGetReady(true);
                break;
            case BTNType.Exit:
                PlayerGetReady(false);
                CanvasGroupOn(PlayGroup);
                CanvasGroupOff(mainGroup);
                CanvasGroupOff(optionGroup);
                CanvasGroupOff(MultiGroup);
                break;
            case BTNType.Start:

                break;

        }
    }
    public void CanvasGroupOn(CanvasGroup cg)
    {
        cg.alpha = 1.0f;
        cg.interactable = true;
        cg.blocksRaycasts = true;
    }
    public void CanvasGroupOff(CanvasGroup cg)
    {
        cg.alpha = 0.0f;
        cg.interactable = false;
        cg.blocksRaycasts = false;
    }

    private void PlayerEnter()
    {
        print("player enter");
        gameClient.CallEnterRoom();
    }
    public void PlayerExit()
    {
        print("player Exit");
        gameClient.CallPlayerExit();
    }
    private void PlayerGetReady(bool isready)
    {
        print("player Ready");
        gameClient.CallGetReady(isready);
    }
}
