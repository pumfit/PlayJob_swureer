using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSetting : MonoBehaviour
{
    public Slider bgmSlider;
    public Slider effectSlider;

    public AudioSource audioBGM;

    private float playerVol = 1f;
    private float playerVol_2 = 1f;

    bool isBGMOn = true;
    bool isEffectOn = true;

    public Button[] btnOnOff;
    public Sprite[] btnSprites;

    // Start is called before the first frame update
    void Start()
    {
        audioBGM = MainSoundManager.instance.MainAudio;

        playerVol = PlayerPrefs.GetFloat("BGMvol", 0.5f);
        bgmSlider.value = playerVol;
        audioBGM.volume = bgmSlider.value;

        playerVol_2 = PlayerPrefs.GetFloat("Effectvol", 0.5f);
        effectSlider.value = playerVol_2;

    }

    public void BGMSlider()
    {
        audioBGM.volume = bgmSlider.value;

        playerVol = bgmSlider.value;
        PlayerPrefs.SetFloat("BGMvol", playerVol);
        if (bgmSlider.value == 0)
        {
            isBGMOn = false;
            btnOnOff[0].image.sprite = btnSprites[0];
        }
        else
        {
            isBGMOn = true;
            btnOnOff[0].image.sprite = btnSprites[1];
        }
    }

    public void EffectSlider()
    {
        

        playerVol_2 = effectSlider.value;
        PlayerPrefs.SetFloat("Effectvol", playerVol_2);
        if (effectSlider.value == 0)
        {
            isEffectOn = false;
            btnOnOff[1].image.sprite = btnSprites[0];
        }
        else
        {
            isEffectOn = true;
            btnOnOff[1].image.sprite = btnSprites[1];
        }
    }

    public void OnOffBGM()
    {
        if (isBGMOn)
        {
            audioBGM.volume = 0;

            playerVol = 0;
            bgmSlider.value = 0;
            PlayerPrefs.SetFloat("BGMvol", 0);
            isBGMOn = false;
            btnOnOff[0].image.sprite = btnSprites[0];
        }
        else
        {
            audioBGM.volume = 0.5f;

            playerVol = 0.5f;
            bgmSlider.value = 0.5f;
            PlayerPrefs.SetFloat("BGMvol", 0.5f);
            isBGMOn = true;
            btnOnOff[0].image.sprite = btnSprites[1];
        }
    }


    public void OnOffEffect()
    {
        if (isEffectOn)
        {
            playerVol_2 = 0;
            effectSlider.value = 0;
            PlayerPrefs.SetFloat("Effectvol", 0f);
            isEffectOn = false;
            btnOnOff[1].image.sprite = btnSprites[0];
        }
        else
        {
            playerVol_2 = 0.5f;
            effectSlider.value = 0.5f;
            PlayerPrefs.SetFloat("Effectvol", 0.5f);
            isEffectOn = true;
            btnOnOff[1].image.sprite = btnSprites[1];
        }
    }


}
