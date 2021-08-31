using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ItemEditMode : MonoBehaviour
{
    private Button OKBtn;
    private Button CancleBtn;
    private Button RotateBtn;

    private Renderer myRenderer;
    private Color oldColor;

    private void Start()
    {
        OKBtn = GameObject.Find("Canvas").transform.Find("OkButton").GetComponent<Button>();//버튼 설정
        CancleBtn = GameObject.Find("Canvas").transform.Find("CancleButton").GetComponent<Button>();
        RotateBtn = GameObject.Find("Canvas").transform.Find("RotateButton").GetComponent<Button>();

        OKBtn.gameObject.SetActive(false);
        CancleBtn.gameObject.SetActive(false);
        RotateBtn.gameObject.SetActive(false);

        myRenderer = gameObject.GetComponent<Renderer>();
        oldColor = myRenderer.material.color;

    }

    void OnMouseDrag()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag == "Ground")
            {
                Vector3 mousepos = new Vector3((int)hit.point.x, transform.position.y, (int)hit.point.z);
                transform.position = mousepos;
            }
        }
    }

    private void OnMouseUp()
    {
        OKBtn.gameObject.SetActive(true);
        CancleBtn.gameObject.SetActive(true);
        RotateBtn.gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)//다른 건물들과 겹치게 설치 불가
    {
        if (other.CompareTag("Obstacle") || other.CompareTag("GameController"))//Obstacle은 기존 건물 GameController는  설치된 건물
        {
            foreach (Material mat in myRenderer.materials)
            {
                mat.color = new Color(0.8f, 0f, 0f, 0.5f);
            }

            OKBtn.interactable = false;//버튼 선택불가능하게
        }
    }

    private void OnTriggerExit(Collider other)
    {

        foreach (Material mat in myRenderer.materials)
        {
            mat.color = oldColor;
        }

        OKBtn.interactable = true;
    }
}
