using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizMainUI : MonoBehaviour
{
    public Image questionImage;
    public Text star;

    public List<GameObject> stageImages = new List<GameObject>(new GameObject[4]);

    public Sprite starFilled;

    public Sprite[] GuiedSprites;
    public GameObject GuiedPanel;
    public GameObject[] GuiedPanelButtons;

    // Start is called before the first frame update
    void Start()
    {
        questionImage = GameObject.Find("Canvas").transform.Find("QuestionImage").GetComponent<Image>();
        star.text = PlayerPrefs.GetInt("PlayerStar", 0).ToString();

        CheckStageUnLock();

        CheckGuiedPanel();
        CheckStageImageUnLock();
        CheckStageStar();
    }

    private void CheckStageImageUnLock()//i는 순서대로 로봇 디자인 연결 안전
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (PlayerPrefs.HasKey("Stage_" + i + "_" + j+ "_UnLocked"))//"Stage_0_0_UnLocked"를 해제했다면
                {
                    stageImages[i].transform.GetChild(j + 3).gameObject.SetActive(false);//이미지 해제
                }
            }
        }       
    }

    private void CheckStageStar()//i는 순서대로 로봇 디자인 연결 안전
    {
        for (int i = 0; i < 4; i++)//0,1,2,3
        {
            for (int j = 0; j < 3; j++)
            {
                if (PlayerPrefs.HasKey("Stage_" + i + "_" + j + "_Clear"))//"Stage_0_0_Clear"을 클리어했다면
                {
                    for (int starNum = 0; starNum <= j; starNum++)
                    {
                        stageImages[i].transform.GetChild(j).transform.GetChild(starNum).GetComponent<Image>().sprite = starFilled;//별 채우기
                    }
                   
                }
            }
        }
    }

    private void CheckStageUnLock()
    {
        if (!PlayerPrefs.HasKey("Stage_0_0_UnLocked"))//로봇마을
        {
            if (PlayerPrefs.HasKey("Character_0_AR_Clear") && PlayerPrefs.HasKey("Character_1_AR_Clear")
                && PlayerPrefs.HasKey("Character_2_AR_Clear") && PlayerPrefs.HasKey("Character_3_AR_Clear"))
            {
                PlayerPrefs.SetString("Stage_0_0_UnLocked", "Clear");
            }
        }
        if (!PlayerPrefs.HasKey("Stage_1_0_UnLocked"))//디자인마을
        {
            if (PlayerPrefs.HasKey("Character_4_AR_Clear") && PlayerPrefs.HasKey("Character_5_AR_Clear")
                && PlayerPrefs.HasKey("Character_6_AR_Clear") && PlayerPrefs.HasKey("Character_7_AR_Clear"))
            {
                PlayerPrefs.SetString("Stage_1_0_UnLocked", "Clear");
            }
        }
        if (!PlayerPrefs.HasKey("Stage_2_0_UnLocked"))//로봇마을
        {
            if (PlayerPrefs.HasKey("Character_8_AR_Clear") && PlayerPrefs.HasKey("Character_9_AR_Clear")
                && PlayerPrefs.HasKey("Character_10_AR_Clear"))
            {
                PlayerPrefs.SetString("Stage_2_0_UnLocked", "Clear");
            }
        }
        if (!PlayerPrefs.HasKey("Stage_3_0_UnLocked"))//로봇마을
        {
            if (PlayerPrefs.HasKey("Character_11_AR_Clear") && PlayerPrefs.HasKey("Character_12_AR_Clear")
                && PlayerPrefs.HasKey("Character_13_AR_Clear") && PlayerPrefs.HasKey("Character_14_AR_Clear"))
            {
                PlayerPrefs.SetString("Stage_3_0_UnLocked", "Clear");
            }
        }

    }

    private void CheckGuiedPanel()
    {
        if (PlayerPrefs.HasKey("Stage_0_0_UnLocked"))
        {
            GuiedPanelButtons[0].gameObject.SetActive(false);
        }
        if (PlayerPrefs.HasKey("Stage_1_0_UnLocked"))
        {
            GuiedPanelButtons[1].gameObject.SetActive(false);
        }
        if (PlayerPrefs.HasKey("Stage_2_0_UnLocked"))
        {
            GuiedPanelButtons[2].gameObject.SetActive(false);
        }
        if (PlayerPrefs.HasKey("Stage_3_0_UnLocked"))
        {
            GuiedPanelButtons[3].gameObject.SetActive(false);
        }
    }

    public void onClickedQBtn()
    {
        MainSoundManager.instance?.ClickPlay();

        questionImage.gameObject.SetActive(true);
    }

    public void onClickedQExit()
    {
        MainSoundManager.instance?.ClickPlay();

        questionImage.gameObject.SetActive(false);
    }

    public void onClickedRobotLocked()
    {
        MainSoundManager.instance?.ClickPlay();

        GuiedPanel.gameObject.GetComponent<Button>().gameObject.GetComponent<Image>().sprite = GuiedSprites[0];
        GuiedPanel.gameObject.SetActive(true);
    }

    public void onClickedDesignLocked()
    {
        MainSoundManager.instance?.ClickPlay();

        GuiedPanel.gameObject.GetComponent<Button>().gameObject.GetComponent<Image>().sprite = GuiedSprites[1];
        GuiedPanel.gameObject.SetActive(true);
    }

    public void onClickedConnectLocked()
    {
        MainSoundManager.instance?.ClickPlay();

        GuiedPanel.gameObject.GetComponent<Button>().gameObject.GetComponent<Image>().sprite = GuiedSprites[2];
        GuiedPanel.gameObject.SetActive(true);
    }

    public void onClickedSafeLocked()
    {
        MainSoundManager.instance?.ClickPlay();

        GuiedPanel.gameObject.GetComponent<Button>().gameObject.GetComponent<Image>().sprite = GuiedSprites[3];
        GuiedPanel.gameObject.SetActive(true);
    }

    public void onClickedGuide()
    {
        MainSoundManager.instance?.ClickPlay();

        GuiedPanel.gameObject.SetActive(false);
    }
}
