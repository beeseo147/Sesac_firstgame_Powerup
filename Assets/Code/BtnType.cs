using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Unity.Collections.AllocatorManager;

public class BtnType : MonoBehaviour
{
    bool isSound;
    public BTNType Key;
    public CanvasGroup mainGroup;
    public CanvasGroup optionGroup;
    public CanvasGroup PlayGroup;
    public AudioSource Click;
    public void OnBtnClick()
    {
        Debug.Log(Key);
        switch (Key)
        {
            case BTNType.Play:
                Click.Play();
                CanvasGroupOn(PlayGroup);
                CanvasGroupOff(mainGroup);
                CanvasGroupOff(optionGroup);
                break;
            case BTNType.Option:
                Click.Play();
                CanvasGroupOn(optionGroup);
                CanvasGroupOff(mainGroup);
                CanvasGroupOff(PlayGroup);
                break;
            case BTNType.Quit:
                Click.Play();
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
                break;

            case BTNType.Sound:
                Click.Play();
                if (isSound)
                {
                    Debug.Log("Sound On");
                }
                else
                    isSound = true;

                isSound = !isSound;
                break;
            case BTNType.Back:
                Click.Play();
                CanvasGroupOn(mainGroup);
                CanvasGroupOff(optionGroup);
                CanvasGroupOff(PlayGroup);
                break;

            case BTNType.Solo:
                Click.Play();
                SceneManager.LoadScene("SampleScene");
                Debug.Log("게임실행");
                break;
            case BTNType.Togeter:
                Click.Play();
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

}
