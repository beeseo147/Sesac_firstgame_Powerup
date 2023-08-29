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
                break;

            case BTNType.Solo:
                SceneManager.LoadScene("SampleScene");
                Debug.Log("게임실행");
                break;
            case BTNType.Togeter:
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
