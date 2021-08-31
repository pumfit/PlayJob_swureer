using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

[System.Serializable]
public class Serialization<T>
{
    public Serialization(List<T> _target) => target = _target;
    public List<T> target;
}

public class DBManager : MonoBehaviour
{
    public GameObject[] Slot;
    public GameObject infoPanel;

    [Header("몬스터 관련 sprite 배열")]
    public Sprite[] panelImage;
    public Sprite[] characterImage;


    [Header("버튼 관련 sprite")]
    public Sprite panelLockImage;
    public Sprite panelActiveSpr;
    public Sprite panelUnactiveSpr;
    private GameObject tempSelectedBtn; 

    [Header("UI Text")]
    public Text LockNumText;
    public Text UnlockNumText;

    void Start()
    {
        CountLock();
        SetSlotImage();
    }

    public void SlotClick(int slotNum)
    {

        if (PlayerPrefs.HasKey("Character_" + slotNum + "_AR_Clear"))
        {
            if (infoPanel.gameObject.activeSelf == true)
            {
                infoPanel.GetComponent<Image>().sprite = panelImage[slotNum+1];               
            }
            else
            {
                infoPanel.GetComponent<Image>().sprite = panelImage[0];
            }

            if (tempSelectedBtn != null)
            {
                tempSelectedBtn.GetComponent<Image>().sprite = panelUnactiveSpr;
            }
            Slot[slotNum].GetComponent<Image>().sprite = panelActiveSpr;
            tempSelectedBtn = Slot[slotNum];

        }
        else
        {
            if (tempSelectedBtn != null)
            {
                tempSelectedBtn.GetComponent<Image>().sprite = panelUnactiveSpr;
            }

            infoPanel.GetComponent<Image>().sprite = panelImage[0];
        }

    }


    void CountLock()
    {
        int lockedNum = 0;
        int unLokedNum = 0;
        for (int i = 0; i < 15; i++)
        {
            if (PlayerPrefs.HasKey("Character_" + i + "_AR_Clear"))
            {
                unLokedNum++;
            }
            else
            {
                lockedNum++;
            }
        }
        LockNumText.text = lockedNum.ToString();
        UnlockNumText.text = unLokedNum.ToString();

    }

    void SetSlotImage()
    {
        for (int i = 0; i < 15; i++)
        {
            if (PlayerPrefs.HasKey("Character_" + i + "_AR_Clear"))
            {
                Slot[i].transform.GetChild(0).GetComponent<Image>().gameObject.SetActive(true);
                Slot[i].transform.GetChild(0).GetComponent<Image>().sprite = characterImage[i];
            }
            else
            {
                Slot[i].GetComponent<Image>().sprite = panelLockImage;
                Slot[i].transform.GetChild(0).GetComponent<Image>().gameObject.SetActive(false);
            }
        }

    }
}
