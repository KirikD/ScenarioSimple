using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayScenarioOneStep : MonoBehaviour
{
    ScenarioStepper SS;
    public AudioSource Audio;
    public Animator Anim;
    public string TextMessage;
    public Animation AnimFBX;

    public int CurStepNum;
    void Start()
    {
        SS = GameObject.Find("ScenarioStepper").GetComponent<ScenarioStepper>();
        // StandardScenarioFunc("__");
        if (GetComponent<AudioSource>() == null)
            this.gameObject.AddComponent<AudioSource>(); 
        audioData = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    [Header("�������� ����� ������ ��� � ����� ���� ����� �� ��� �������")]
    public bool ActivStp = false;
    public bool Next = false; // ���� ������ ������������ �� ���� ����������� ���������
    //��������� ����
    [Header("��������� ��� �����")]
    public bool isAudioPlay; public int SecondsAudPlay = 5;
    [Header("��������� ��� ��������")]
    public bool isAnimatorPlay; public int SecondsAnimPlay = 6;
    [Header("������������ ��� � ������ ���������� ����")]
    public bool isObjActiv; public GameObject PreActivThis; public GameObject ActivThis; public GameObject DisabThis;
    // ��������
    AudioSource audioData;
        public void StandardScenarioFunc(string Data)
    {
        TextMessage = Data;
        ActivStp = true;
        this.gameObject.AddComponent<Outline>().OutlineColor = Color.blue;
        Debug.Log("������ ��� " + Data);
        StartCoroutine(WaitStep()); // ������� ���������� �����
        // if (CurStepNum == SS.step && Next)  Invoke("EndStepTrue", 0.2f);
        // ���� ��� ��������� �� �� ����� ����� �������� ��� ���������
        if (isAudioPlay)
        {
            Invoke("EndStepTrue", audioData.clip.length);
            
            audioData.Play(0);
            Debug.Log(audioData.clip.length + "started " + audioData.name);
        }
        if (isAnimatorPlay)
        {
            Invoke("EndStepTrue", SecondsAnimPlay);
        }
        if (isObjActiv)
        {
            try { PreActivThis.SetActive(true);   } catch { }
         }
    }
    public void SetStp(int stp)
    { CurStepNum = stp; }

    IEnumerator WaitStep()
    {
       // Debug.Log("11111111");
        while (Next == false)
        {
            yield return null; 
        }
        Debug.Log(CurStepNum+" ������������ ��������� ��� "+ SS.step);
        ActivStp = false;
        GetComponent<Outline>().OutlineColor = Color.black;  Invoke(nameof(DestroyOutline),1);
        if (CurStepNum == SS.step && Next)
        {
            SS.SetNextStep(0); // ������������ ��������� ���

        }
        if (isObjActiv)
        {
            try  {    ActivThis.SetActive(true);  } catch { }
            try  {    DisabThis.SetActive(false); } catch { }
    }
    }
    void DestroyOutline() { Destroy(GetComponent<Outline>()); audioData.Stop();  }
    //������� ���������� ���������
    void EndStepTrue() { Next = true; }// �����



}
