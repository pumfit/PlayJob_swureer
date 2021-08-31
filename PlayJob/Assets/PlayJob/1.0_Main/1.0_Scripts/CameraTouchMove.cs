using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTouchMove : MonoBehaviour
{
    private float dist;
    private Vector3 MouseStart;

    float m_fOldToucDis = 0f;       // 터치 이전 거리를 저장합니다.
    float m_fFieldOfView = 60f;     // 카메라의 FieldOfView의 기본값을 60으로 정합니다.

    // Start is called before the first frame update
    void Start()
    {
        dist = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        int nTouch = Input.touchCount;
        float m_fToucDis = 0f;
        float fDis = 0f;
        if (nTouch == 1)
        {
            if (Input.GetMouseButtonDown(0))
            {
                MouseStart = new Vector3(Input.mousePosition.x, 1, dist);//처음 움직인 값
                MouseStart = Camera.main.ScreenToWorldPoint(MouseStart);
                MouseStart.z = transform.position.z;
            }
            else if (Input.GetMouseButton(0))
            {
                var MouseMove = new Vector3(Input.mousePosition.x, 1, dist);//이동한 값
                MouseMove = Camera.main.ScreenToWorldPoint(MouseMove);
                MouseMove.z = transform.position.z;
                transform.position = Vector3.Lerp(transform.position, transform.position - (MouseMove - MouseStart), Time.deltaTime * 2f);
            }
        }
        else
        {
            if (Input.touchCount == 2 && (Input.touches[0].phase == TouchPhase.Moved || Input.touches[1].phase == TouchPhase.Moved))
            {
                m_fToucDis = (Input.touches[0].position - Input.touches[1].position).sqrMagnitude;
                fDis = (m_fToucDis - m_fOldToucDis) * 0.01f;
                // 이전 두 터치의 거리와 지금 두 터치의 거리의 차이를 FleldOfView를 차감합니다.
                m_fFieldOfView -= fDis;
                // 최대는 80, 최소는 40으로 더이상 증가 혹은 감소가 되지 않도록 합니다.
                m_fFieldOfView = Mathf.Clamp(m_fFieldOfView, 40.0f, 80.0f);
                // 확대 축소가 갑자기 되지않도록 보간합니다.
                Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, m_fFieldOfView, Time.deltaTime * 2f);

                m_fOldToucDis = m_fToucDis;
            }
        }


    }
}
    
