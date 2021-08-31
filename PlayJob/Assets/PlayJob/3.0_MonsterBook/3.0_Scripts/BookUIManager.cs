using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookUIManager : MonoBehaviour
{
    [Header("탭 버튼 용 UI")]
    public Button[] TabBtn;

    public GameObject MonsterTab;
    public GameObject BadgeTab;

    public Sprite[] BtnOnOff;//0,1 Monster 2,3 Badge

    [Header("뱃지 클리어 확인용 UI")]
    public Image[] Badges;
    public Sprite[] clearBadges;

    private void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            if (PlayerPrefs.HasKey("Stage_" + i + "_2_Clear"))//"Stage_i_2_Clear"를 클리어했다면
            {
                Badges[i].sprite = clearBadges[i];
            }
        }
    }
    public void clickedMonsterTab()
    {
        MonsterTab.gameObject.SetActive(true);
        BadgeTab.gameObject.SetActive(false);

        TabBtn[0].GetComponent<Image>().sprite = BtnOnOff[0];
        TabBtn[1].GetComponent<Image>().sprite = BtnOnOff[3];
    }

    public void clickedBabgeTab()
    {
        MonsterTab.gameObject.SetActive(false);
        BadgeTab.gameObject.SetActive(true);

        TabBtn[0].GetComponent<Image>().sprite = BtnOnOff[1];
        TabBtn[1].GetComponent<Image>().sprite = BtnOnOff[2];
    }
}
