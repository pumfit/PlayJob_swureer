using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;


[System.Serializable]
public class Quiz
{
    public string QuestionText, AnswerText_1, AnswerText_2, AnswerText_3, 
                    Level, AnswerNum, HintText; //integer

    public Quiz(string _Level, string _QuestionText, string _AnswerText_1, string _AnswerText_2, string _AnswerText_3,
                     string _AnswerNum,string _HintText)
    {
        QuestionText = _QuestionText;   AnswerText_1 = _AnswerText_1; AnswerText_2 = _AnswerText_2; AnswerText_3 = _AnswerText_3;
        Level = _Level; AnswerNum = _AnswerNum; HintText = _HintText;
    }
}

public class QuizTimeUI_5_1 : MonoBehaviour
{
    public TextAsset QuizDatabase;
    public List<Quiz> Allquiz;//private 로 변경할 것임
    public List<Quiz> tempQuiz;
    public Quiz curQuiz;

    public Text timeText;
    public float limitTime = 10;

    public Text questionText;
    public Text[] answerText = new Text[3];
    private Text hintText;

    private int answerNum;

    public Image[] Timebars;
    private float timeInterval;

    private int timebarIndex = 9;//타임바 인덱스
    public Sprite unfilledBar;
    public Sprite filledBar;

    private string filePath;


    void Start() //각 퀴즈 스테이지 마다 해당 스크립트가 들어가야함
    {
        string[] line = QuizDatabase.text.Substring(0, QuizDatabase.text.Length - 1).Split('\n');
        for (int i = 0; i < line.Length; i++)
        {
            string[] row = line[i].Split('\t');
            Allquiz.Add(new Quiz(row[0], row[1], row[2], row[3], row[4], row[5], row[6]));
        }

        filePath = Application.persistentDataPath + "/RobbotQuizDB_210209.txt";
        Save();
        Load();

        if (SceneManager.GetActiveScene().name == "QuizScene_5.1_Robot_1")
        {
            tempQuiz = Allquiz.FindAll(x => int.Parse(x.Level) == 1);
        }
        else if (SceneManager.GetActiveScene().name == "QuizScene_5.1_Robot_2")
        {
            tempQuiz = Allquiz.FindAll(x => int.Parse(x.Level) == 2);
        }
        else if (SceneManager.GetActiveScene().name == "QuizScene_5.1_Robot_3")
        {
            tempQuiz = Allquiz.FindAll(x => int.Parse(x.Level) == 3);
        }

        curQuiz = tempQuiz[Random.Range(0, 4)];//0,1,2,3 4개중 랜덤

        Time.timeScale = 1;
        hintText = GameObject.Find("Canvas").transform.Find("FailImage").transform.Find("HintText").GetComponent<Text>();

        questionText.text = curQuiz.QuestionText;
        answerText[0].text = curQuiz.AnswerText_1;
        answerText[1].text = curQuiz.AnswerText_2;
        answerText[2].text = curQuiz.AnswerText_3;
        hintText.text = curQuiz.HintText;

        answerNum = int.Parse(curQuiz.AnswerNum);

        timeInterval = (limitTime / 10);
        StartCoroutine(CountTime(timeInterval));

    }

    IEnumerator CountTime(float delayTime) 
    {
        if (timebarIndex >= 0)
        {
            timeText.text = "" + timebarIndex;
            Timebars[timebarIndex].sprite = unfilledBar;
            timebarIndex--;
            MainSoundManager.instance?.ClockPlay();
        }
        else
        {
            GameObject.Find("Canvas").transform.Find("EndPanel").gameObject.SetActive(true);
            GameObject.Find("Canvas").transform.Find("FailImage").gameObject.SetActive(true);
            Time.timeScale = 0;
        }

        yield return new WaitForSeconds(delayTime);
        StartCoroutine(CountTime(delayTime));
    }

    public void OnClicked(Button button)
    {
        if ((button.name.Equals("AnswerButton1") && answerNum == 1) ||
            (button.name.Equals("AnswerButton2") && answerNum == 2) ||
            (button.name.Equals("AnswerButton3") && answerNum == 3))
        {//맞춘경우
            GameObject.Find("Canvas").transform.Find("EndPanel").gameObject.SetActive(true);
            if (PlayerPrefs.HasKey("Stage_0_2_Clear") || !(SceneManager.GetActiveScene().name == "QuizScene_5.1_Robot_3"))
            {
                GameObject.Find("Canvas").transform.Find("SucessImage").gameObject.SetActive(true);
            } else
            {
                GameObject.Find("Canvas").transform.Find("FirstSucessImage").gameObject.SetActive(true);
            }

            Time.timeScale = 0;
            addStar();
            MainSoundManager.instance?.CorrectPlay();

        }//틀린경우
        else
        {
            GameObject.Find("Canvas").transform.Find("EndPanel").gameObject.SetActive(true);
            GameObject.Find("Canvas").transform.Find("FailImage").gameObject.SetActive(true);
            Time.timeScale = 0;

            MainSoundManager.instance?.WrongPlay();
        }
    }

    void addStar() //클리어시 별 획득량
    {
        int curStar = PlayerPrefs.GetInt("PlayerStar", 0);
        if (SceneManager.GetActiveScene().name == "QuizScene_5.1_Robot_1")
        {
            curStar += 1;
            PlayerPrefs.SetString("Stage_0_0_Clear", "Clear");
            PlayerPrefs.SetString("Stage_0_1_UnLocked", "Clear");
        }
        else if (SceneManager.GetActiveScene().name == "QuizScene_5.1_Robot_2")
        {
            curStar += 2;
            PlayerPrefs.SetString("Stage_0_1_Clear", "Clear");
            PlayerPrefs.SetString("Stage_0_2_UnLocked", "Clear");
        }
        else if (SceneManager.GetActiveScene().name == "QuizScene_5.1_Robot_3")
        {
            curStar += 3;
            bool isClear = true;
            PlayerPrefs.SetString("isClear", isClear.ToString());
            PlayerPrefs.SetString("Stage_0_2_Clear", "Clear");
        }
        PlayerPrefs.SetInt("PlayerStar", curStar);
    }

    public void quitQuiz()
    {
        MainSoundManager.instance?.ClickPlay();

        SceneManager.LoadScene("QuizMainScene", LoadSceneMode.Single);
    }

    public void LoadMain()
    {
        MainSoundManager.instance?.ClickPlay();

        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
    }

    public void LoadQuizScence_1()
    {
        MainSoundManager.instance?.ClickPlay();

        SceneManager.LoadScene("QuizScene_5.1_Robot_1", LoadSceneMode.Single);
    }

    public void LoadQuizScence_2()
    {
        MainSoundManager.instance?.ClickPlay();

        SceneManager.LoadScene("QuizScene_5.1_Robot_2", LoadSceneMode.Single);
    }

    public void LoadQuizScence_3()
    {
        MainSoundManager.instance?.ClickPlay();

        SceneManager.LoadScene("QuizScene_5.1_Robot_3", LoadSceneMode.Single);
    }

    public void ReLoadScene()
    {
        for (int i = 0; i < Timebars.Length; i++)
        {
            Timebars[i].sprite = filledBar;
        }

        GameObject.Find("Canvas").transform.Find("EndPanel").gameObject.SetActive(false);
        GameObject.Find("Canvas").transform.Find("FailImage").gameObject.SetActive(false);

        timebarIndex = 9;
        Time.timeScale = 1.0f;
        StopAllCoroutines();
        StartCoroutine(CountTime(1.0f));
    }


    void Save()
    {
        string jdata = JsonUtility.ToJson(new Serialization<Quiz>(Allquiz));
        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(jdata);
        string code = System.Convert.ToBase64String(bytes);

        File.WriteAllText(filePath, code);
    }

    void Load()
    {
        if (!File.Exists(filePath))
        {
            ResetItem();
            return;
        }
        string code = File.ReadAllText(filePath);

        byte[] bytes = System.Convert.FromBase64String(code);
        string jdata = System.Text.Encoding.UTF8.GetString(bytes);
        Allquiz = JsonUtility.FromJson<Serialization<Quiz>>(jdata).target;
    }

    void ResetItem()
    {
        Save();
        Load();
    }
    /*
        void destroyBGM()
    {
        if (null != GameObject.Find("BGMController"))
        {
            Destroy(GameObject.Find("BGMController"));
        }
    } 
     */

}
