using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class Dialogue
{
    [TextArea(3, 10)]
    public List<string> sentences;
}

[System.Serializable]
public class Sentence
{
    public Sentence(string _mobNum, string _dialogueIndex, string _sentence)
    {
        mobNum = _mobNum; dialogueIndex = _dialogueIndex;  sentence = _sentence;
    }
    public string mobNum, dialogueIndex;
    public string sentence;
}

[System.Serializable]
public class DialogAudio
{
    public AudioClip[] startAudioClips;
    public AudioClip[] TagOneAudioClips;//태그 갯수 3개
    public AudioClip[] TagTwoAudioClips;
    public AudioClip[] TagThreeAudioClips;
    public AudioClip[] endAudioClips;
}

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue 기본 설정")]
    public Text dialogueText;
    public GameObject AllDialoguObject;

    [Header("Dialogue 데이터베이스")]
    public TextAsset DialogueDatabase;
    public List<Sentence> AllSentenceList;

    [Header("학습 종료 패널 UI")]
    public GameObject EndPanel;
    public Sprite[] EndPanelSprites;

    [Header("받아올 다이얼로그들 및 UI")] 
    public GameObject Dialogue;
    public Sprite[] DialogueMobSprites;
    public Dialogue startDialogue;
    public Dialogue[] TagDialogue;
    public Dialogue endDialogue;

    [Header("해시태그 버튼UI")] //배열로 바꿔도 되지만 쉽게 알아보기위해 각각 나눠서 사용
    public Button[] HashTagBtns;
    public Sprite[] HashTagClearSprs;
    public Sprite[] AllHashTagUnClearSprs;
    public Sprite[] AllHashTagClearSprs;

    [Header("출력 오디오")]
    public AudioSource mainAudioSource;
    public DialogAudio[] audios;

    private Queue<string> sentences = new Queue<string>();
    private int MobIndex = 0;
    private int DialogueIndex = 0;
    private int audioIndex = 0;
    private int MaxIndex = 0;

    //public int presentMapNumber;
    //public int presentStageNumber;
    //private object talkcharImg;

    void Awake()
    {
        MainSoundManager.instance?.MainThemePause();

        mainAudioSource = gameObject.GetComponent<AudioSource>();//오디오

        if (!(PlayerPrefs.GetFloat("Effectvol") == 0))
        {
            //mainAudioSource.volume = PlayerPrefs.GetFloat("Effectvol");//기존 오디오 소스 크기가 작아 자체적으로 소리 키움
            mainAudioSource.volume = 1;
        }
        else
        {
            mainAudioSource.volume = 0;
        }

    }
    private void Start()
    {
        string[] line = DialogueDatabase.text.Substring(0, DialogueDatabase.text.Length - 1).Split('\n');
        for (int i = 0; i < line.Length; i++)
        {
            string[] row = line[i].Split('\t');
            AllSentenceList.Add(new Sentence(row[0], row[1], row[2]));
        }

        //sprite resources 들 가져오기
        EndPanelSprites = Resources.LoadAll<Sprite>("4.0_Panel_GetMonster");
        DialogueMobSprites = Resources.LoadAll<Sprite>("4.0_Panel_Scripts");
        AllHashTagUnClearSprs = Resources.LoadAll<Sprite>("4.0_Tag_unclear");
        AllHashTagClearSprs = Resources.LoadAll<Sprite>("4.0_Tag_clear");


    }
    public void SetDialogue(int monsterIndex)
    {
        //인덱스 받아오기
        MobIndex = monsterIndex;

        //UI 변경하기
        EndPanel.transform.GetChild(0).GetComponent<Image>().sprite = EndPanelSprites[MobIndex];
        Dialogue.GetComponent<Image>().sprite = DialogueMobSprites[MobIndex];

        for (int i = 0; i<3; i++)
        {
            HashTagBtns[i].GetComponent<Image>().sprite = AllHashTagUnClearSprs[(3  * monsterIndex )+ i];//0일때 0,1,2  1일때 3,4,5  2일때 6,7,8 3일때 9,10,11
            //HashTagBtns[i].GetComponent<Image>().rectTransform.sizeDelta = new Vector2(AllHashTagUnClearSprs[i].rect.width, AllHashTagUnClearSprs[i].rect.height);

            HashTagClearSprs[i] = AllHashTagClearSprs[(3 * monsterIndex) + i];
        }

        //대사 가져오기
        List<Sentence> mobSentence = AllSentenceList.FindAll(x => int.Parse(x.mobNum) == monsterIndex);

        startDialogue.sentences.Clear();
        TagDialogue[0].sentences.Clear();
        TagDialogue[1].sentences.Clear();
        TagDialogue[2].sentences.Clear();
        endDialogue.sentences.Clear();

        List<Sentence> tempSentence = mobSentence.FindAll(x => int.Parse(x.dialogueIndex) == 0);
        foreach (Sentence st in tempSentence)
        {
            startDialogue.sentences.Add(st.sentence);
        }
        tempSentence = mobSentence.FindAll(x => int.Parse(x.dialogueIndex) == 1);
        foreach (Sentence st in tempSentence)
        {
            TagDialogue[0].sentences.Add(st.sentence);
        }
        tempSentence = mobSentence.FindAll(x => int.Parse(x.dialogueIndex) == 2);
        foreach (Sentence st in tempSentence)
        {
            TagDialogue[1].sentences.Add(st.sentence);
        }
        tempSentence = mobSentence.FindAll(x => int.Parse(x.dialogueIndex) == 3);
        foreach (Sentence st in tempSentence)
        {
            TagDialogue[2].sentences.Add(st.sentence);
        }
        tempSentence = mobSentence.FindAll(x => int.Parse(x.dialogueIndex) == 4);
        foreach (Sentence st in tempSentence)
        {
            endDialogue.sentences.Add(st.sentence);
        }

        Dialogue.gameObject.SetActive(true);//dialogue 켜기

        MaxIndex = startDialogue.sentences.Count;
        StartDialogue(startDialogue);
    }

    public void ResetCurrentDialogueKey()//210127 추가
    {
        AllDialoguObject.SetActive(true);

        StartDialogue(startDialogue);
    }

    public void StartDialogue(Dialogue dialogue)
    {
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        MainSoundManager.instance?.ClickPlay();

        if (sentences.Count == 0)//대화창의 대사가 모두 끝난경우
        {
            audioIndex = 0;//오디오 초기화

            switch (DialogueIndex)
            {
                case 0:
                    foreach (Button btn in HashTagBtns)
                    {
                        btn.gameObject.SetActive(true);
                    }
                    AllDialoguObject.SetActive(false);
                    break;
                case 1:
                    HashTagBtns[0].image.sprite = HashTagClearSprs[0];
                    PlayerPrefs.SetString("Character_" + MobIndex + "_1_Clear", "Clear");
                    AllDialoguObject.SetActive(false);
                    break;
                case 2:
                    HashTagBtns[1].image.sprite = HashTagClearSprs[1];
                    PlayerPrefs.SetString("Character_" + MobIndex + "_2_Clear", "Clear");
                    AllDialoguObject.SetActive(false);
                    break;
                case 3:
                    HashTagBtns[2].image.sprite = HashTagClearSprs[2];
                    PlayerPrefs.SetString("Character_" + MobIndex + "_3_Clear", "Clear");
                    AllDialoguObject.SetActive(false);
                    break;
                case 4:
                    mainAudioSource.clip = audios[0].endAudioClips[audioIndex];
                    MainSoundManager.instance?.ClearPlay();
                    EndPanel.gameObject.SetActive(true);
                    PlayerPrefs.SetString("Character_"+ MobIndex + "_AR_Clear", "Clear");
                    Debug.Log("클리어 몬스터 : " + "Character_" + MobIndex + "_AR_Clear");

                    PlayerPrefs.DeleteKey("Character_"+ MobIndex + "_1_Clear");
                    PlayerPrefs.DeleteKey("Character_" + MobIndex + "_2_Clear");
                    PlayerPrefs.DeleteKey("Character_" + MobIndex + "_3_Clear");

                    AllDialoguObject.SetActive(false);
                    mainAudioSource.Pause();
                    return;
            }

            if (PlayerPrefs.HasKey("Character_" + MobIndex + "_1_Clear") &&
                PlayerPrefs.HasKey("Character_" + MobIndex + "_2_Clear") &&
                PlayerPrefs.HasKey("Character_" + MobIndex + "_3_Clear"))
            {
                DialogueIndex = 4;
                AllDialoguObject.SetActive(true);
                MaxIndex = endDialogue.sentences.Count;
                StartDialogue(endDialogue);
            }

            return;

        }
        else
        {
            switch (DialogueIndex)
            {
                case 0:
                    mainAudioSource.clip = audios[MobIndex]?.startAudioClips[audioIndex];//0은 캐릭터번호
                 break;
                case 1:
                    mainAudioSource.clip = audios[MobIndex]?.TagOneAudioClips[audioIndex];
                 break;
                case 2:
                    mainAudioSource.clip = audios[MobIndex]?.TagTwoAudioClips[audioIndex];
                 break;
                case 3:
                    mainAudioSource.clip = audios[MobIndex]?.TagThreeAudioClips[audioIndex];
                 break;
                case 4:
                    mainAudioSource.clip = audios[MobIndex]?.endAudioClips[audioIndex];
                 break;
            }  
            mainAudioSource?.Play();
            audioIndex++;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));

    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        dialogueText.text  =  sentence;
        yield return null;
    }

    public void OnClickedHashTag(int i)//0은 인삿말 1,2,3은 태그를 의미
    {
        audioIndex = 0;
        DialogueIndex = i+1;

        MaxIndex = TagDialogue[i].sentences.Count;
        StartDialogue(TagDialogue[i]);

        AllDialoguObject.SetActive(true);
    }

}