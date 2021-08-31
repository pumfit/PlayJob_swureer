using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSoundManager : MonoBehaviour
{
    public static MainSoundManager instance; //정적 변수

    public AudioSource MainAudio;
    public AudioSource EffectAudio;

    public AudioClip MainLobbySound;
    public AudioClip ClearSoundEffect;

    public AudioClip ClickSoundEffect;
    public AudioClip CorrectSoundEffect;
    public AudioClip WrongSoundEffect;
    public AudioClip ClockSoundEffect;
    public AudioClip BuySoundEffect;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else if (instance != this)
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("BGMvol"))
        {
            PlayerPrefs.SetFloat("BGMvol", 0.5f);
        }

        MainAudio = GetComponent<AudioSource>();
        EffectAudio = transform.Find("EffectSoundSorce").GetComponent<AudioSource>();

        MainAudio.clip = MainLobbySound;
        MainAudio.Play();

        MainAudio.volume = PlayerPrefs.GetFloat("BGMvol");
        EffectAudio.volume = PlayerPrefs.GetFloat("Effectvol");
    }

    // Update is called once per frame
    void Update()
    {
        MainAudio.volume = PlayerPrefs.GetFloat("BGMvol");
        EffectAudio.volume = PlayerPrefs.GetFloat("Effectvol");
    }

    public void MainThemePlay()
    {
        MainAudio.Stop();

        MainAudio.clip = MainLobbySound;
        MainAudio.loop = true;
        MainAudio.Play();

    }

    public void MainThemePause()
    {
        MainAudio.Pause();
    }

    public void ClearPlay()
    {
        MainAudio.Stop();

        MainAudio.clip = ClearSoundEffect;
        MainAudio.loop = false;
        MainAudio.Play();

    }

    public void ClickPlay()
    {
        EffectAudio.PlayOneShot(ClickSoundEffect);
    }

    public void CorrectPlay()
    {
        EffectAudio.PlayOneShot(CorrectSoundEffect);
    }

    public void WrongPlay()
    {
        EffectAudio.PlayOneShot(WrongSoundEffect);
    }

    public void ClockPlay()
    {
        EffectAudio.PlayOneShot(ClockSoundEffect);
    }

    public void BuyPlay()
    {
        EffectAudio.PlayOneShot(BuySoundEffect);
    }


    /*
public void ClickBuyPlay()
{
EffectAudio.PlayOneShot(ClickBuySoundEffect);
}

public void ClickCanclePlay()
{
EffectAudio.PlayOneShot(ClickCancleSoundEffect);
}

public void GetMobPlay()
{
EffectAudio.PlayOneShot(MobGetSoundEffect);
}

public void FailGetMobPlay()
{
EffectAudio.PlayOneShot(MobFailGetSoundEffect);
}

public void ClickErrorPlay()
{
EffectAudio.PlayOneShot(ErrorSoundEffect);
}

public void ClickSpecialEffectPlay()
{
EffectAudio.PlayOneShot(ClickSpecialEffect);
}

public void OpenEffectPlay()
{
EffectAudio.PlayOneShot(OpenSpecialEffect);
}

public void Mute()
{
MainAudio.Stop();
//MainAudio.mute = true;
}

public void UnMute()
{
MainAudio.Play();

//MainAudio.mute = false;
}


public void PlayGamePlay()
{
if (MainAudio.isPlaying)
{
    MainAudio.Stop();

    MainAudio.clip = sound_play;
    MainAudio.loop = true;
    MainAudio.Play();
}
}

public void CodingPlay()
{
if (MainAudio.isPlaying)
{
    MainAudio.Stop();

    MainAudio.clip = sound_coding;
    MainAudio.loop = true;
    MainAudio.Play();
}
}

public void ClearPlay()
{
if (MainAudio.isPlaying)
{
    MainAudio.Stop();

    MainAudio.clip = sound_clear;
    MainAudio.loop = false;
    MainAudio.Play();
}
}

public void FailPlay()
{
if (MainAudio.isPlaying)
{
    MainAudio.Stop();

    MainAudio.clip = sound_fail;
    MainAudio.loop = false;
    MainAudio.Play();
}
}

public void PlayBtn()
{
MainAudio.PlayOneShot(sound_btn);
}*/
}