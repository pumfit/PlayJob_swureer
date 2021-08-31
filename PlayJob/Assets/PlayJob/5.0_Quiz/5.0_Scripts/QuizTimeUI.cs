using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuizTimeUI : MonoBehaviour
{
    public Text timeText;
    public float limitTime = 30;

    private Text questionText;
    private Text answerText_1;
    private Text answerText_2;
    private Text answerText_3;
    private Text hintText;

    public string question;
    public string answer_1;
    public string answer_2;
    public string answer_3;
    public string hint;
    public int answerNum;

    public Image[] Timebars;
    public float timeInterval;
    private int index = 9;
    public Sprite unfilledBar;

    public AudioSource correctBtn;
    public AudioSource wrongBtn;
    public AudioSource clockSource;

    void Start()
    {
        Time.timeScale = 1;
        questionText = GameObject.Find("QuizText").GetComponent<Text>();
        answerText_1 = GameObject.Find("AnswerText_1").GetComponent<Text>();
        answerText_2 = GameObject.Find("AnswerText_2").GetComponent<Text>();
        answerText_3 = GameObject.Find("AnswerText_3").GetComponent<Text>();
        hintText = GameObject.Find("Canvas").transform.Find("FailImage").transform.Find("HintText").GetComponent<Text>();

        questionText.text = question;
        answerText_1.text = answer_1;
        answerText_2.text = answer_2;
        answerText_3.text = answer_3;
        hintText.text = hint;

        timeInterval = (limitTime / 10);
        StartCoroutine(CountTime(timeInterval));

    }

    IEnumerator CountTime(float delayTime)
    {
        if (index >= 0)
        {
            Timebars[index].sprite = unfilledBar;
            index--;
        }

        yield return new WaitForSeconds(delayTime);
        StartCoroutine(CountTime(timeInterval));
    }


    void Update()
    {

        limitTime -= Time.deltaTime;
        if (limitTime < 30)
        {
            timeText.text = ""+Mathf.Round(limitTime);

            if (limitTime < 0)
            {
                GameObject.Find("Canvas").transform.Find("EndPanel").gameObject.SetActive(true);
                GameObject.Find("Canvas").transform.Find("FailImage").gameObject.SetActive(true);
                clockSource.Pause();
                limitTime = 0;
                Time.timeScale = 0;
            }
        }
    }
    public void OnClicked(Button button)
    {
        if ((button.name.Equals("AnswerButton1") && answerNum == 1)||
            (button.name.Equals("AnswerButton2") && answerNum == 2)||
            (button.name.Equals("AnswerButton3") && answerNum == 3))
        {//맞춘경우
            GameObject.Find("Canvas").transform.Find("EndPanel").gameObject.SetActive(true);
            GameObject.Find("Canvas").transform.Find("SucessImage").gameObject.SetActive(true);
            Time.timeScale = 0;
            clockSource.Pause();
            correctBtn.Play();
            addStar();
        }//틀린경우
        else
        {
            GameObject.Find("Canvas").transform.Find("EndPanel").gameObject.SetActive(true);
            GameObject.Find("Canvas").transform.Find("FailImage").gameObject.SetActive(true);
            Time.timeScale = 0;
            clockSource.Pause();
            wrongBtn.Play();
        }
    }

    void addStar()
    {
        int curStar = PlayerPrefs.GetInt("PlayerStar", 0);
        if (SceneManager.GetActiveScene().name == "QuizScene")
        {
            curStar += 1;
        }
        else if (SceneManager.GetActiveScene().name == "QuizScene_1")
        {
            curStar += 2;
        }
        else if (SceneManager.GetActiveScene().name == "QuizScene_2")
        {
            curStar += 3;
            bool isClear = true;
            PlayerPrefs.SetString("isClear", isClear.ToString());
        }
        PlayerPrefs.SetInt("PlayerStar", curStar);
    }

    public void quitQuiz()
    {
        SceneManager.LoadScene("QuizMainScene", LoadSceneMode.Single);
    }

    public void LoadMain()
    {
        destroyBGM();
        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
    }

    public void LoadQuizScence_1()
    {
        SceneManager.LoadScene("QuizScene", LoadSceneMode.Single);
    }

    public void LoadQuizScence_2()
    {
        SceneManager.LoadScene("QuizScene_1", LoadSceneMode.Single);
    }

    public void LoadQuizScence_3()
    {
        SceneManager.LoadScene("QuizScene_2", LoadSceneMode.Single);
    }

    void destroyBGM()
    {
        if (null != GameObject.Find("BGMController"))
        {
            Destroy(GameObject.Find("BGMController"));
        }
    }
}
