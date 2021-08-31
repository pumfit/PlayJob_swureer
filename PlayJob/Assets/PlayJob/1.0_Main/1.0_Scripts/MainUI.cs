using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MainUI : MonoBehaviour
{
    [Header("메인UI내 패널")]
    public GameObject settingPanel;
    public GameObject shopPanel;
    public GameObject editPanel;
    public GameObject editExitBtn;

    public GameObject[] hideObjects;

    [Header("메인UI내 star개수 텍스트")]
    public Text starText;

    [Header("메인 건물 생성 UI")]
    public GameObject SuccessPanel;
    public Sprite[] townPanel;
    public GameObject[] buildObjects;


    [Header("스킵 건물 이후 삭제예정")]
    public GameObject AfterObject;

    // Start is called before the first frame update
    private void Awake()
    {
        settingPanel.gameObject.SetActive(false);
        shopPanel.gameObject.SetActive(false);
        editPanel.gameObject.SetActive(false);
    }

    private void Update()
    {
        starText.text = PlayerPrefs.GetInt("PlayerStar", 0).ToString();

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                Debug.DrawRay(gameObject.transform.position, ray.direction, Color.green);
            }
        }
    }

    void Start()
    {
        starText.text = PlayerPrefs.GetInt("PlayerStar", 0).ToString();

        if (PlayerPrefs.HasKey("BuildObject"))//이후 삭제
        {
            AfterObject.gameObject.SetActive(true);
        }
        checkClearQuiz();
    }

    public void onClickSetting()
    {
        MainSoundManager.instance?.ClickPlay();

        settingPanel.gameObject.SetActive(true);
    }

    public void onClickSettingExit()
    {
        MainSoundManager.instance?.ClickPlay();

        settingPanel.gameObject.SetActive(false);
    }

    public void onClickShop()
    {
        MainSoundManager.instance?.ClickPlay();

        shopPanel.gameObject.SetActive(true);
    }

    public void onClickShopExit()
    {
        MainSoundManager.instance?.ClickPlay();

        shopPanel.gameObject.SetActive(false);
    }

    public void onClickEdit()
    {
        MainSoundManager.instance?.ClickPlay();

        editExitBtn.gameObject.SetActive(true);
        editPanel.gameObject.SetActive(true);

        Camera.main.orthographicSize = 70;
        Camera.main.transform.Rotate(new Vector3(2, 0, 0));

        foreach (GameObject obj in hideObjects)
        {
            obj.gameObject.SetActive(false);
        }
    }

    public void onClickEditExit()
    {
        MainSoundManager.instance?.ClickPlay();

        editExitBtn.gameObject.SetActive(false);
        editPanel.gameObject.SetActive(false);

        Camera.main.orthographicSize = 60;
        Camera.main.transform.Rotate(new Vector3(-2, 0, 0));

        foreach (GameObject obj in hideObjects)
        {
            obj.gameObject.SetActive(true);
        }
    }

    public void resetStar()
    {
        PlayerPrefs.SetString("BuildObject", "Clear");
       // AfterObject.gameObject.SetActive(true);

        PlayerPrefs.SetString("Stage_0_0_Clear", "Clear");
        PlayerPrefs.SetString("Stage_0_1_UnLocked", "Clear");
        PlayerPrefs.SetString("Stage_0_1_Clear", "Clear");
        PlayerPrefs.SetString("Stage_0_2_UnLocked", "Clear");
        PlayerPrefs.SetString("Stage_0_2_Clear", "Clear");
        PlayerPrefs.SetString("Stage_0_3_UnLocked", "Clear");
        PlayerPrefs.SetString("Stage_0_3_Clear", "Clear");

        PlayerPrefs.SetString("Build_Robbot_Clear", "Clear");

        PlayerPrefs.SetString("Stage_1_0_Clear", "Clear");
        PlayerPrefs.SetString("Stage_1_1_UnLocked", "Clear");
        PlayerPrefs.SetString("Stage_1_1_Clear", "Clear");
        PlayerPrefs.SetString("Stage_1_2_UnLocked", "Clear");
        PlayerPrefs.SetString("Stage_1_2_Clear", "Clear");
        PlayerPrefs.SetString("Stage_1_3_UnLocked", "Clear");
        PlayerPrefs.SetString("Stage_1_3_Clear", "Clear");

        PlayerPrefs.SetString("Build_Connect_Clear", "Clear");

        PlayerPrefs.SetString("Stage_2_0_Clear", "Clear");
        PlayerPrefs.SetString("Stage_2_1_UnLocked", "Clear");
        PlayerPrefs.SetString("Stage_2_1_Clear", "Clear");
        PlayerPrefs.SetString("Stage_2_2_UnLocked", "Clear");
        PlayerPrefs.SetString("Stage_2_2_Clear", "Clear");
        PlayerPrefs.SetString("Stage_2_3_UnLocked", "Clear");
        PlayerPrefs.SetString("Stage_2_3_Clear", "Clear");

        PlayerPrefs.SetString("Character_" + 0 + "_AR_Clear", "Clear");
        PlayerPrefs.SetString("Character_" + 1 + "_AR_Clear", "Clear");
        PlayerPrefs.SetString("Character_" + 2 + "_AR_Clear", "Clear");
        PlayerPrefs.SetString("Character_" + 3 + "_AR_Clear", "Clear");
        PlayerPrefs.SetString("Character_" + 4 + "_AR_Clear", "Clear");
        PlayerPrefs.SetString("Character_" + 5 + "_AR_Clear", "Clear");
        PlayerPrefs.SetString("Character_" + 6 + "_AR_Clear", "Clear");
        PlayerPrefs.SetString("Character_" + 7 + "_AR_Clear", "Clear");
        PlayerPrefs.SetString("Character_" + 8 + "_AR_Clear", "Clear");
        PlayerPrefs.SetString("Character_" + 9 + "_AR_Clear", "Clear");
        PlayerPrefs.SetString("Character_" + 10 + "_AR_Clear", "Clear");

    }

    public void resetNextStar()
    {
        PlayerPrefs.SetInt("PlayerStar", 100);

        PlayerPrefs.SetString("Character_" + 11 + "_AR_Clear", "Clear");
        PlayerPrefs.SetString("Character_" + 12 + "_AR_Clear", "Clear");
        PlayerPrefs.SetString("Character_" + 13 + "_AR_Clear", "Clear");
        PlayerPrefs.SetString("Character_" + 14 + "_AR_Clear", "Clear");

    }

    public void resetStart()
    {
        PlayerPrefs.DeleteAll();
    }

    public void ExitTownNoticePanel()
    {
        SuccessPanel.gameObject.SetActive(false);
    }

    void checkClearQuiz()//변화된 번호만 받아올수있는가?
    {
        if (PlayerPrefs.HasKey("Stage_0_2_Clear"))//로봇마을
        {
            if (!(PlayerPrefs.HasKey("Build_Robbot_Clear")))
            {
                SuccessPanel.transform.GetChild(0).GetComponent<Image>().sprite = townPanel[0];
                SuccessPanel.gameObject.SetActive(true);
            }

            buildObjects[0].SetActive(true);
            buildObjects[4].SetActive(false);
            PlayerPrefs.SetString("Build_Robbot_Clear", "Clear");
        }

        if (PlayerPrefs.HasKey("Stage_1_2_Clear"))
        {
            if (!(PlayerPrefs.HasKey("Build_Design_Clear")))
            {
                SuccessPanel.transform.GetChild(0).GetComponent<Image>().sprite = townPanel[1];
                SuccessPanel.gameObject.SetActive(true);
            }

            buildObjects[1].SetActive(true);
            buildObjects[5].SetActive(false);
            PlayerPrefs.SetString("Build_Design_Clear", "Clear");
        }

        if (PlayerPrefs.HasKey("Stage_2_2_Clear"))
        {
            if (!(PlayerPrefs.HasKey("Build_Connect_Clear")))
            {
                SuccessPanel.transform.GetChild(0).GetComponent<Image>().sprite = townPanel[2];
                SuccessPanel.gameObject.SetActive(true);
            }

            buildObjects[2].SetActive(true);
            buildObjects[6].SetActive(false);
            PlayerPrefs.SetString("Build_Connect_Clear", "Clear");
        }

        if (PlayerPrefs.HasKey("Stage_3_2_Clear"))
        {
            AfterObject.gameObject.SetActive(true);

            if (!(PlayerPrefs.HasKey("Build_Safe_Clear")))
            {
                SuccessPanel.transform.GetChild(0).GetComponent<Image>().sprite = townPanel[3];
                SuccessPanel.gameObject.SetActive(true);
            }

            buildObjects[3].SetActive(true);
            buildObjects[7].SetActive(false);
            PlayerPrefs.SetString("Build_Safe_Clear", "Clear");
        }


    }

}
