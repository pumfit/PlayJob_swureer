using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{

    public void onQuitGame()
    {
        MainSoundManager.instance?.ClickPlay();

        Application.Quit();
    }
    public void onARButton()
    {
        MainSoundManager.instance?.ClickPlay();

        SceneManager.LoadScene("ARStudyScene",LoadSceneMode.Single);
    }
    public void onQuizButton()
    {
        MainSoundManager.instance?.ClickPlay();

        SceneManager.LoadScene("QuizMainScene", LoadSceneMode.Single);
    }
    public void onMainScene()
    {
        MainSoundManager.instance?.ClickPlay();
        if (MainSoundManager.instance != null && !MainSoundManager.instance.MainAudio.isPlaying)
        {
            MainSoundManager.instance?.MainThemePlay();
        }
        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
    }
    public void onQuizRobotFirstScene()
    {
        MainSoundManager.instance?.ClickPlay();

        SceneManager.LoadScene("QuizScene_5.1_Robot_1", LoadSceneMode.Single);
    }
    public void onQuizRobotSecondScene()
    {
        MainSoundManager.instance?.ClickPlay();

        SceneManager.LoadScene("QuizScene_5.1_Robot_2", LoadSceneMode.Single);
    }
    public void onQuizRobotThirdScene()
    {
        MainSoundManager.instance?.ClickPlay();

        SceneManager.LoadScene("QuizScene_5.1_Robot_3", LoadSceneMode.Single);
    }
    public void onQuizDesignFirstScene()
    {
        MainSoundManager.instance?.ClickPlay();

        SceneManager.LoadScene("QuizScene_5.2_Design_1", LoadSceneMode.Single);
    }
    public void onQuizDesignSecondScene()
    {
        MainSoundManager.instance?.ClickPlay();

        SceneManager.LoadScene("QuizScene_5.2_Design_2", LoadSceneMode.Single);
    }
    public void onQuizDesignThirdScene()
    {
        MainSoundManager.instance?.ClickPlay();

        SceneManager.LoadScene("QuizScene_5.2_Design_3", LoadSceneMode.Single);
    }
    public void onQuizConnectFirstScene()
    {
        MainSoundManager.instance?.ClickPlay();

        SceneManager.LoadScene("QuizScene_5.3_Connect_1", LoadSceneMode.Single);
    }
    public void onQuizConnectSecondScene()
    {
        MainSoundManager.instance?.ClickPlay();

        SceneManager.LoadScene("QuizScene_5.3_Connect_2", LoadSceneMode.Single);
    }
    public void onQuizConnectThirdScene()
    {
        MainSoundManager.instance?.ClickPlay();

        SceneManager.LoadScene("QuizScene_5.3_Connect_3", LoadSceneMode.Single);
    }
    public void onQuizSafeFirstScene()
    {
        MainSoundManager.instance?.ClickPlay();

        SceneManager.LoadScene("QuizScene_5.4_Safe_1", LoadSceneMode.Single);
    }
    public void onQuizSafeSecondScene()
    {
        MainSoundManager.instance?.ClickPlay();

        SceneManager.LoadScene("QuizScene_5.4_Safe_2", LoadSceneMode.Single);
    }
    public void onQuizSafeThirdScene()
    {
        MainSoundManager.instance?.ClickPlay();

        SceneManager.LoadScene("QuizScene_5.4_Safe_3", LoadSceneMode.Single);
    }
    public void onMonsterBookScene()
    {
        MainSoundManager.instance?.ClickPlay();

        SceneManager.LoadScene("MonsterBookScene", LoadSceneMode.Single);
    }
    public void onShopScene()
    {
        MainSoundManager.instance?.ClickPlay();

        SceneManager.LoadScene("ShopScene", LoadSceneMode.Single);
    }
}

