using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARUIManager : MonoBehaviour
{
    [Header("AR화면 도움말 UI")]
    public GameObject HelpPanel;

    public void onClickHelp()
    {
        HelpPanel.gameObject.SetActive(true);
    }

    public void onClickHelpExit()
    {
        HelpPanel.gameObject.SetActive(false);
    }
}
