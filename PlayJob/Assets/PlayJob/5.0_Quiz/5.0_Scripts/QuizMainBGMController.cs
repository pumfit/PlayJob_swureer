using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizMainBGMController : MonoBehaviour
{

    public AudioSource button;
    public AudioSource nextBtn;

    public void PlayButton()
    {
        button.Play();
    }

    public void PlayNextButton()
    {
        nextBtn.Play();
    }
}
