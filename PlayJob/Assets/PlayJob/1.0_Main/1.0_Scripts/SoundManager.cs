using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance = null;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null) // 정적으로 자신을 체크
            instance = this; //정적으로 자신을 저장
        else if (instance != this)
            Destroy(gameObject);
    }

    public void PlayMainSound()
    {
        MainSoundManager.instance.MainThemePlay();
    }

    public void PlayClearEffect()
    {
        MainSoundManager.instance.ClearPlay();
    }
}