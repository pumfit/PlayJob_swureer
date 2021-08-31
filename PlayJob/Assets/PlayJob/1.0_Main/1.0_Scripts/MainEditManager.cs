using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

[System.Serializable]
public class ItemInfo
{
    public ItemInfo(int _Item, float _posX, float _posZ,float _rotY)
    {
        ItemIndex = _Item; posX = _posX; posZ = _posZ; rotY = _rotY;
    }
    public int ItemIndex; 
    public float posX,posZ,rotY;
}


public class MainEditManager : MonoBehaviour
{
    public GameObject ItemContent;

    [Header("아이템 내부 데이터 베이스")]
    public List<ItemInfo> AllItemList;
    private string FilePath;

    [Header("아이템 프리팹 리스트")]
    public GameObject[] prefabItem;
    public GameObject[] prefabItemAlpha;

    public GameObject CreateAlpha;
    public GameObject CreateItem;

    [Header("Edit 모드 UI")]
    public Button RotateBtn;
    public Button OKBtn;
    public Button CancleBtn;
    public Button CancleDeleteBtn;
    public Button DeleteBtn;
    public GameObject Middle;

    private bool Isclicked = false;
    private bool IsEditModeOn = false;

    private GameObject selectedTarget;

    public Button[] ItemBtn;
    private Button SelectedBtn;
    private int ItemIndex;

    void Start()
    {
        FilePath = Application.persistentDataPath + "/ItemSetInfo.txt";
        Load();

        foreach (ItemInfo info in AllItemList)
        {
            GameObject saveItem = Instantiate(prefabItem[info.ItemIndex - 1]);
            saveItem.transform.position = new Vector3(info.posX, 0, info.posZ);
            saveItem.transform.Rotate(0,info.rotY-45,0);
        }

        loadItemNum();

    }

    public void onClickedEditAndReset() { loadItemNum(); IsEditModeOn = true; }

    void loadItemNum()//아이템 01부터 시작
    {
        for (int i = 0; i < ItemContent.transform.childCount; i++)
        {
            ItemContent.transform.GetChild(i).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text 
                = (PlayerPrefs.GetInt("Item_" + (i + 1), 0)).ToString();
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && Isclicked == false && IsEditModeOn== true)//클릭시에
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "GameController")
                {
                    if (!(selectedTarget == null))
                    {
                        selectedTarget.GetComponent<Outline>().OutlineWidth = 0;//이전 선택된 오브젝트는 선택 해제
                    }

                    selectedTarget = hit.collider.gameObject;
                    selectedTarget.GetComponent<Outline>().OutlineWidth = 6;

                    DeleteBtn.gameObject.SetActive(true);
                    CancleDeleteBtn.gameObject.SetActive(true);

                    OffBtnInterection();
                }
            }
        }
    }

    public void selectedItem(int num)//0부터 시작
    {
        SelectedBtn = ItemContent.transform.GetChild(num).GetComponent<Button>();

        ItemIndex = num;
        int currentItemNum = PlayerPrefs.GetInt("Item_" + (num + 1), 0);

        if (!Isclicked && currentItemNum > 0)
        {
            OffBtnInterection();
            //건물 선택이 이미 되어있는 상태라면
            if (!(selectedTarget == null))
            {
                selectedTarget = null;
                DeleteBtn.gameObject.SetActive(false);
                CancleDeleteBtn.gameObject.SetActive(false);
            }
            Isclicked = true;

            SelectedBtn.transform.GetChild(0).GetComponent<Text>().text = currentItemNum - 1 + "";//갯수 하나 제외
            CreateAlpha = Instantiate(prefabItemAlpha[ItemIndex], Middle.transform);

        }
        else
        {
            Isclicked = false;

            OKBtn.gameObject.SetActive(false);
            CancleBtn.gameObject.SetActive(false);
            RotateBtn.gameObject.SetActive(false);

            SelectedBtn.transform.GetChild(0).GetComponent<Text>().text = currentItemNum + "";//갯수 하나 제외 취소
            SelectedBtn = null;
            if (!(CreateAlpha == null))
            {
                Destroy(CreateAlpha);
            }
        }

    }

    public void SetItem()//createalpha.transform.position 에서 실제 생성할 아이템으로 변경
    {
        CreateItem = Instantiate(prefabItem[ItemIndex], CreateAlpha.transform.position, Quaternion.identity);
        CreateItem.transform.Rotate(0f, CreateAlpha.transform.rotation.eulerAngles.y, 0f);
        Destroy(CreateAlpha);
        int currentItemNum = int.Parse(SelectedBtn.transform.GetChild(0).GetComponent<Text>().text);

        PlayerPrefs.SetInt("Item_"+ (ItemIndex+1), currentItemNum);

        ItemInfo newItem = new ItemInfo((ItemIndex + 1), CreateItem.transform.position.x, CreateItem.transform.position.z, CreateAlpha.transform.rotation.eulerAngles.y);
        AllItemList.Add(newItem);
        Save();

        OnBtnInterection();

        OKBtn.gameObject.SetActive(false);
        CancleBtn.gameObject.SetActive(false);
        RotateBtn.gameObject.SetActive(false);

        Isclicked = false;
    }

    public void RotateItem()
    {
        CreateAlpha.transform.Rotate(Vector3.up, 90);
    }

    public void CancleSetItem()
    {
        Destroy(CreateAlpha);
        SelectedBtn.transform.GetChild(0).GetComponent<Text>().text = PlayerPrefs.GetInt("Item_"+ (ItemIndex + 1), 0) + "";//갯수 하나 제외 취소

        OnBtnInterection();

        OKBtn.gameObject.SetActive(false);
        CancleBtn.gameObject.SetActive(false);
        RotateBtn.gameObject.SetActive(false);

        Isclicked = false;
    }

    public void CancleDeleteItem()
    {
        selectedTarget.GetComponent<Outline>().OutlineWidth = 0;
        selectedTarget = null;

        OnBtnInterection();

        DeleteBtn.gameObject.SetActive(false);
        CancleDeleteBtn.gameObject.SetActive(false);

        Isclicked = false;
    }

    public void DeleteItem()
    {
        foreach (ItemInfo info in AllItemList)
        {
            if (selectedTarget.transform.position.x == info.posX)
            {
                AllItemList.Remove(info);
                break;
            }
        }
        Save();

        Destroy(selectedTarget);
        string[] targetName = selectedTarget.name.Split(new char[] { '_' ,'('});

        int itemNum = int.Parse(targetName[1]);
        int currentItemNum = PlayerPrefs.GetInt("Item_"+ itemNum, 0) + 1;
        PlayerPrefs.SetInt("Item_" + itemNum, currentItemNum);

        loadItemNum();
        OnBtnInterection();

        DeleteBtn.gameObject.SetActive(false);
        CancleDeleteBtn.gameObject.SetActive(false);
    }

    public void onClickEditExit()
    {
        IsEditModeOn = false;

        OKBtn.gameObject.SetActive(false);
        CancleBtn.gameObject.SetActive(false);
        RotateBtn.gameObject.SetActive(false);

        if (!(CreateAlpha == null))
        {
            Destroy(CreateAlpha);
            Isclicked = false;
            OnBtnInterection();
        }

        if (!(selectedTarget==null))
        {
            selectedTarget.GetComponent<Outline>().OutlineWidth = 0;
            selectedTarget = null;
        }
    }

    public void OffBtnInterection()
    {
        foreach (Button btn in ItemBtn)
        {
            btn.interactable = false;
        }
    }

    public void OnBtnInterection()
    {
        foreach (Button btn in ItemBtn)
        {
            btn.interactable = true;
        }
    }

    void Save()
    {
        string jdata = JsonUtility.ToJson(new Serialization<ItemInfo>(AllItemList));
        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(jdata);
        string code = System.Convert.ToBase64String(bytes);

        File.WriteAllText(FilePath, code);
    }

    void Load()
    {
        if (!File.Exists(FilePath))
        {
            ResetItem();
            return;
        }
        string code = File.ReadAllText(FilePath);

        byte[] bytes = System.Convert.FromBase64String(code);
        string jdata = System.Text.Encoding.UTF8.GetString(bytes);
        AllItemList = JsonUtility.FromJson<Serialization<ItemInfo>>(jdata).target;
    }

    void ResetItem()
    {
        Save();
        Load();
    }

}
