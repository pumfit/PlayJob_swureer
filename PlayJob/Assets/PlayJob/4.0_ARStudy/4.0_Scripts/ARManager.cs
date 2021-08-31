using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class ARManager : MonoBehaviour
{
    [Header("AR화면 기본 설정")]
    public ARTrackedImageManager m_trackedImageManager;
    public ARSession session;

    [Header("AR화면 속 Canvas UI")]
    public GameObject CardNoticeImage;
    public GameObject MainDialog;
    public GameObject[] TagImage;

    [Header("AR화면 몹 프리팹")]
    [SerializeField]
    private TrackedPrefab[] prefabToInstantiate;

    [Header("AR화면 오디오 설정")]
    public AudioSource mainAudioSource;

    private Dictionary<string, GameObject> instanciatePrefab;
    private bool IsFirst = true;
    private bool isTagOn = false;

    private void Awake()
    {
        //MainDialog.gameObject.SetActive(false);
        mainAudioSource.Stop();
        instanciatePrefab = new Dictionary<string, GameObject>();
    }

    private void OnEnable()
    {
        m_trackedImageManager.trackedImagesChanged += OnTrackedImageChanged;
    }

    private void OnDisable() //게임오브젝트가 비활성화 될 때마다 호출되는 함수입니다.
    {
        m_trackedImageManager.trackedImagesChanged -= OnTrackedImageChanged;
    }

    private void OnTrackedImageChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {

        foreach (ARTrackedImage addedImage in eventArgs.added)
        {
            InstantiateGameObject(addedImage);
        }

        foreach (ARTrackedImage updatedImage in eventArgs.updated)
        {
            if (updatedImage.trackingState == UnityEngine.XR.ARSubsystems.TrackingState.Tracking)
            {
                UpdateTrackingGameObject(updatedImage);
            }
            else if (updatedImage.trackingState == UnityEngine.XR.ARSubsystems.TrackingState.Limited)
            {
                UpdateLimitedGameObject(updatedImage);

            }
            else //대부분의 경우 호출되지않음
            {
                UpdateNoneGameObject(updatedImage);
            }
        }

        foreach (ARTrackedImage removedImage in eventArgs.removed)//호출되지않음! 관련 문서 참고
        {
            DestroyGameObject(removedImage);
        }
    }

    private void InstantiateGameObject(ARTrackedImage addedImage)//이미지 추가 상태 (프리팹 생성)
    {
        if (IsFirst)
        {
            int monsterIndex = int.Parse(addedImage.referenceImage.name);
            GameObject.Find("DialogueManager").GetComponent<DialogueManager>().SetDialogue(monsterIndex);

            for (int i = 0; i < prefabToInstantiate.Length; i++)
            {
                if (addedImage.referenceImage.name == prefabToInstantiate[i].name)
                {
                    GameObject prefab = Instantiate<GameObject>(prefabToInstantiate[i].prefab, transform.parent);
                    prefab.transform.position = addedImage.transform.position;
                    prefab.transform.rotation = addedImage.transform.rotation;
                    prefab.transform.Rotate(new Vector3(-15f, 180f, 0f), Space.Self);

                    instanciatePrefab.Add(addedImage.referenceImage.name, prefab);//프리팹 리스트에 추가

                    CardNoticeImage.gameObject.SetActive(false);

                    mainAudioSource.Play();
                }
            }

            IsFirst = false;

        }

    }

    private void UpdateTrackingGameObject(ARTrackedImage updatedImage)//이미지 트래킹 상태
    {

        for (int i = 0; i < instanciatePrefab.Count; i++)
        {
            if (instanciatePrefab.TryGetValue(updatedImage.referenceImage.name, out GameObject prefab))
            {
                prefab.transform.position = updatedImage.transform.position;
                prefab.transform.rotation = updatedImage.transform.rotation;
                prefab.transform.Rotate(new Vector3(-15f, 180f, 0f), Space.Self);
                prefab.SetActive(true);

                MainDialog.gameObject.SetActive(true);
                CardNoticeImage.gameObject.SetActive(false);

            }
        }
    }

    private void UpdateLimitedGameObject(ARTrackedImage updatedImage) //updatedImage된 이미지가 리미티드 상태
    {
        for (int i = 0; i < instanciatePrefab.Count; i++)//Limited상태의 카드 프리팹을 찾아 비활성화
        {
            if (instanciatePrefab.TryGetValue(updatedImage.referenceImage.name, out GameObject prefab))
            {
                prefab.SetActive(false);

                MainDialog.gameObject.SetActive(false);
                CardNoticeImage.gameObject.SetActive(true);

            }
        }
    }

    private void UpdateNoneGameObject(ARTrackedImage updateImage) //None된 이미지가 리미티드 상태
    {
        for (int i = 0; i < instanciatePrefab.Count; i++)
        {
            if (instanciatePrefab.TryGetValue(updateImage.referenceImage.name, out GameObject prefab))
            {
                prefab.SetActive(false);
            }

        }
    }

    private void DestroyGameObject(ARTrackedImage removedImage) //Destroy된 이미지가 리미티드 상태라 아마 호출 x
    {
        for (int i = 0; i < instanciatePrefab.Count; i++)
        {
            if (instanciatePrefab.TryGetValue(removedImage.referenceImage.name, out GameObject prefab))
            {
                instanciatePrefab.Remove(removedImage.referenceImage.name);
                Destroy(prefab);
            }
        }
    }

}

[System.Serializable]
public struct TrackedPrefab
{
    public string name;
    public GameObject prefab;
}
