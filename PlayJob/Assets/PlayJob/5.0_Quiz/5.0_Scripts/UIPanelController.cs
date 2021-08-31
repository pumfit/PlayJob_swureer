using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPanelController : MonoBehaviour
{
    [Header("퀴즈 메인내 Text UI")]
    public Text themaText;
    public Text titleText;
    public Text infoText;

    public List<Image> mainPanels;
    public Image MainTitle;

    public GameObject ScrollViewContent;

    public Image[] leftMainIcons;
    public Image[] rightMainIcons;

    public Sprite leftFilled;
    public Sprite rightFilled;

    private Sprite leftUnFilled;
    private Sprite rightUnFilled;

    public Sprite[] titlePanels;

    private float rate;
    // Start is called before the first frame update
    void Start()
    {
        leftUnFilled = leftMainIcons[0].sprite;
        rightUnFilled = rightMainIcons[0].sprite;

        rate = -ScrollViewContent.GetComponent<RectTransform>().rect.width;//0~3600
    }

    private void Update()
    {
        float contentX = ScrollViewContent.GetComponent<RectTransform>().localPosition.x;

        if ( rate * 0.25 < contentX )
        {
            mainPanels[0].transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            mainPanels[1].transform.localScale = new Vector3(0.7f, 0.7f, 1.0f);
            mainPanels[2].transform.localScale = new Vector3(0.7f, 0.7f, 1.0f);
            mainPanels[3].transform.localScale = new Vector3(0.7f, 0.7f, 1.0f);
            SetTitleText(0);
            SetImage(0);
            SetPanelIcon(0);
        }
        else if (rate * 0.5 < contentX)
        {
            mainPanels[0].transform.localScale = new Vector3(0.7f, 0.7f, 1.0f);
            mainPanels[1].transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            mainPanels[2].transform.localScale = new Vector3(0.7f, 0.7f, 1.0f);
            mainPanels[3].transform.localScale = new Vector3(0.7f, 0.7f, 1.0f);
            SetTitleText(1);
            SetImage(1);
            SetPanelIcon(1);
        }
        else if (rate * 0.75 < contentX)
        {
            mainPanels[0].transform.localScale = new Vector3(0.7f, 0.7f, 1.0f);
            mainPanels[1].transform.localScale = new Vector3(0.7f, 0.7f, 1.0f);
            mainPanels[2].transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            mainPanels[3].transform.localScale = new Vector3(0.7f, 0.7f, 1.0f);
            SetTitleText(2);
            SetImage(2);
            SetPanelIcon(2);
        }
        else if (rate < contentX)
        {
            mainPanels[0].transform.localScale = new Vector3(0.7f, 0.7f, 1.0f);
            mainPanels[1].transform.localScale = new Vector3(0.7f, 0.7f, 1.0f);
            mainPanels[2].transform.localScale = new Vector3(0.7f, 0.7f, 1.0f);
            mainPanels[3].transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            SetTitleText(3);
            SetImage(3);
            SetPanelIcon(3);
        }

    }

    void SetLayerOrder(int i)
    {
        switch (i)
        {
            case 0:
                ScrollViewContent.transform.Find("MainDesign").transform.SetAsFirstSibling();
                break;
            case 1:
                ScrollViewContent.transform.Find("MainRobot").transform.SetAsFirstSibling();
                break;
            case 2:
                ScrollViewContent.transform.Find("MainConnect").transform.SetAsFirstSibling();
                break;
            case 3:
                ScrollViewContent.transform.Find("MainSafe").transform.SetAsFirstSibling();
                break;
        }
    }

    void SetImage(int i)
    {
        for (int j = 0; j < 4; j++)
        {
            if (i == j)
            {
                for (int t = 0; t < 3; t++)
                {
                    mainPanels[j].transform.GetChild(t).GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                }
            }
            else
            {
                for (int t = 0; t < 3; t++)
                {
                    mainPanels[j].transform.GetChild(t).GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
                }
            }

        }

    }

    void SetTitleText(int i)
    {
        switch (i)
        {
            case 0:
                themaText.text = "로봇 마을";
                titleText.text = "로봇 마을";
                break;
            case 1:
                themaText.text = "디자인 마을";
                titleText.text = "디자인 마을";
                break;
            case 2:
                themaText.text = "연결 마을";
                titleText.text = "연결 마을";
                break;
            case 3:
                themaText.text = "안전 마을";
                titleText.text = "안전 마을";
                break;                
        }
    }

    void SetPanelIcon(int i)
    {
        for (int j = 0; j < 3; j++)
        {
            if (PlayerPrefs.HasKey("Stage_" + i + "_" + j + "_UnLocked"))//"Stage_0_0_UnLocked"를 해제했다면
            {
                leftMainIcons[j].sprite = leftFilled;
                rightMainIcons[j].sprite = rightFilled;

                if (j == 1)
                {
                    infoText.text = "직업과 관련된 개념들에 대해 학습해볼까?";
                }
                else if (j == 2)
                {
                    infoText.text = "개념들을 좀 더 자세하게 학습해볼까?";
                }

            }
            else
            {
                leftMainIcons[j].sprite = leftUnFilled;
                rightMainIcons[j].sprite = rightUnFilled;
            }
        }

        if (PlayerPrefs.HasKey("Stage_" + i + "_2_Clear"))//"Stage_0_2_Clear"를 해제했다면
        {
            MainTitle.sprite = titlePanels[i];
        }
        else
        {
            MainTitle.sprite = titlePanels[i+4];
        }
    }

    /*
     public void RButton(Button btn)
{
    float contentX = ScrollViewContent.GetComponent<RectTransform>().position.x;
    float RcontentX = contentX - 800;

    if (RcontentX < -3700)
    {
        Vector3 curPos = ScrollViewContent.GetComponent<RectTransform>().position;
        ScrollViewContent.GetComponent<RectTransform>().position = new Vector3(RcontentX, curPos.y, curPos.z);
    }
}

public void LButton(Button btn)
{
    Debug.Log("ddd");
    float contentX = ScrollViewContent.GetComponent<RectTransform>().position.x;
    float LcontentX = contentX + 800;

    if (LcontentX < 100)
    {
        Vector3 curPos = ScrollViewContent.GetComponent<RectTransform>().position;
        ScrollViewContent.GetComponent<RectTransform>().position = new Vector3(LcontentX, curPos.y,curPos.z);
    }

}
     */

}
