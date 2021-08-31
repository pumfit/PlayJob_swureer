using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainShopManager : MonoBehaviour
{
    [Header("상점UI내 star개수 텍스트")]
    public Text starText;
    [Header("아이템 눌렀을때 정보창 모음")]
    public GameObject ItemInfolist;
    public GameObject Itemlist;

    public Sprite buyItemSpr;
    [Header("스타 부족시 등장하는 UI패널")]
    public GameObject BuyNoticePanel;

    private void Awake()
    {
        ItemInfolist.gameObject.SetActive(false);
    }

    void Start()
    {
        starText.text = PlayerPrefs.GetInt("PlayerStar", 0).ToString();

        CheckLockedItemList();
    }

    public void CheckStarNum() { starText.text = PlayerPrefs.GetInt("PlayerStar", 0).ToString();  }

    public void SelectItemList(int i)
    {
        MainSoundManager.instance?.ClickPlay();

        ItemInfolist.gameObject.SetActive(true);

        for (int j = 0;j < ItemInfolist.transform.childCount; j++)
        {
            ItemInfolist.transform.GetChild(j).gameObject.SetActive(false);
        }
        ItemInfolist.transform.GetChild(0).gameObject.SetActive(true);
        ItemInfolist.transform.GetChild(i).gameObject.SetActive(true);
    }

    public void ExitItemInfoList()
    {
        MainSoundManager.instance?.ClickPlay();

        ItemInfolist.gameObject.SetActive(false);
    }

    public void ExitBuyNotice()
    {
        MainSoundManager.instance?.ClickPlay();

        BuyNoticePanel.gameObject.SetActive(false);
    }

    public void ClickedBuyItem(int i)
    {
        int Playerstar = PlayerPrefs.GetInt("PlayerStar", 0);
        int price = int.Parse(ItemInfolist.transform.GetChild(i).transform.GetChild(2).GetComponent<Text>().text);

        if (Playerstar - price >= 0)
        {
            MainSoundManager.instance?.BuyPlay();

            int curItemNum = PlayerPrefs.GetInt("Item_" + i, 0);
            curItemNum++;
            PlayerPrefs.SetInt("Item_" + i, curItemNum);

            PlayerPrefs.SetInt("PlayerStar", Playerstar - price);
            starText.text = PlayerPrefs.GetInt("PlayerStar", 0).ToString();

            ItemInfolist.gameObject.SetActive(false);
        }
        else//잔액부족시
        {
            MainSoundManager.instance?.WrongPlay();

            BuyNoticePanel.gameObject.SetActive(true);
        }
    }

    void CheckLockedItemList()
    {
        if (PlayerPrefs.HasKey("Stage_0_2_Clear"))
        {
            for (int i= 0; i < 5; i++)
            {
                Itemlist.transform.GetChild(i).transform.GetChild(4).gameObject.SetActive(false);//잠금풀이
                ItemInfolist.transform.GetChild(i+1).transform.GetChild(3).GetComponent<Button>().interactable = true;
                ItemInfolist.transform.GetChild(i + 1).transform.GetChild(3).GetComponent<Image>().sprite = buyItemSpr;
                //ItemInfolist.transform.GetChild(i).transform.GetChild(3).gameObject. 구매하기로 바꿈
            }
        }

        if (PlayerPrefs.HasKey("Stage_1_2_Clear"))
        {
            for (int i = 5; i < 10; i++)
            {
                Itemlist.transform.GetChild(i).transform.GetChild(4).gameObject.SetActive(false);//잠금풀이
                ItemInfolist.transform.GetChild(i+1).transform.GetChild(3).GetComponent<Button>().interactable = true;
                ItemInfolist.transform.GetChild(i + 1).transform.GetChild(3).GetComponent<Image>().sprite = buyItemSpr;
            }
        }

        if (PlayerPrefs.HasKey("Stage_2_2_Clear"))
        {
            for (int i = 10; i < 15; i++)
            {
                Itemlist.transform.GetChild(i).transform.GetChild(4).gameObject.SetActive(false);//잠금풀이
                ItemInfolist.transform.GetChild(i+1).transform.GetChild(3).GetComponent<Button>().interactable = true;
                ItemInfolist.transform.GetChild(i + 1).transform.GetChild(3).GetComponent<Image>().sprite = buyItemSpr;
            }
        }

        if (PlayerPrefs.HasKey("Stage_3_2_Clear"))
        {
            for (int i = 15; i < 20; i++)
            {
                Itemlist.transform.GetChild(i).transform.GetChild(4).gameObject.SetActive(false);//잠금풀이
                ItemInfolist.transform.GetChild(i+1).transform.GetChild(3).GetComponent<Button>().interactable = true;
                ItemInfolist.transform.GetChild(i + 1).transform.GetChild(3).GetComponent<Image>().sprite = buyItemSpr;
            }
        }
    }
}
