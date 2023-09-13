using PowerupC2S;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Unity.Collections.AllocatorManager;

public class BtnType : MonoBehaviour
{
    bool isSound =true;
    public BTNType Key;
    [Header("#CanvasGroup")]
    [SerializeField] CanvasGroup mainGroup;
    [SerializeField] CanvasGroup optionGroup;
    [SerializeField] CanvasGroup PlayGroup;
    [SerializeField] CanvasGroup MultiGroup;
    private void Awake()
    {
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
        //MainMenu
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
        //OptionMenu
            case BTNType.Sound:
                if (isSound)
                {
                    AudioManager.Instance.musicSource.Stop();
                }
                else
                {
                    AudioManager.Instance.musicSource.Play();
                }
                isSound = !isSound;
                break;
            case BTNType.Back:
                CanvasGroupOn(mainGroup);
                CanvasGroupOff(optionGroup);
                CanvasGroupOff(PlayGroup);
                CanvasGroupOff(MultiGroup);
                PlayerExit();
                break;
        //PlayGruop
            case BTNType.Solo:
                print("씬 전환전");
                SceneManager.LoadScene("SampleScene");
                Debug.Log("게임실행");
                GameClient.Instance.SetPlayer(true);
                GameClient.Instance.SetPlayerNumber(1);
                break;
            case BTNType.Togeter:
                Debug.Log("함께하기");
                CanvasGroupOff(PlayGroup);
                CanvasGroupOff(mainGroup);
                CanvasGroupOff(optionGroup);
                CanvasGroupOn(MultiGroup);
                PlayerEnter();
                break;
        //MultiGruop
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
                SceneManager.LoadScene("SampleScene");
                Debug.Log("멀티게임실행");
                GameClient.Instance.SetPlayer(false);
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
        GameClient.Instance.CallEnterRoom();
    }
    public void PlayerExit()
    {
        print("player Exit");
        GameClient.Instance.CallPlayerExit();
    }
    private void PlayerGetReady(bool isready)
    {
        print("player Ready");
        GameClient.Instance.CallGetReady(isready);
    }
}
