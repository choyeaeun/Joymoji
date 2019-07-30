//#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Recorder;
using UnityEditor.Recorder.Input;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class JomojiRecord : MonoBehaviour
{

    RecorderController m_RecorderController;
    public int timeLeft = 7;
    public int MovieTime = 5;
    public Text countdownText;
    private bool startBool = false;
    //private GameObject[] BtnObj;

    // void OnMouseEnter()
    //void OnTriggerStay2D(Collider2D col)
    void Start()
    {
        Debug.Log("cliclk");
        countdownText.text = " ";
       // BtnObj = GameObject.FindGameObjectsWithTag("UI");

        // OnEnable();
        // m_RecorderController.StartRecording();

        var controllerSettings = ScriptableObject.CreateInstance<RecorderControllerSettings>();
        m_RecorderController = new RecorderController(controllerSettings);

        var mediaOutputFolder = Application.dataPath + "../JoymojiMovie";
       
        // Video
        var videoRecorder = ScriptableObject.CreateInstance<MovieRecorderSettings>();
        videoRecorder.name = "JOYMOJIMOVIE";
        videoRecorder.enabled = true;

        videoRecorder.outputFormat = VideoRecorderOutputFormat.MP4;
        videoRecorder.videoBitRateMode = VideoBitrateMode.Low;

        videoRecorder.imageInputSettings = new GameViewInputSettings
        {
            outputWidth = 1920,
            outputHeight = 1080
        };

        videoRecorder.audioInputSettings.preserveAudio = false;

        videoRecorder.outputFile = mediaOutputFolder + "/MOVIE_" + DefaultWildcard.Take;
 

        //Animation& image Sequence 생략

        // Setup Recording
        controllerSettings.AddRecorderSettings(videoRecorder);
   

        controllerSettings.SetRecordModeToManual();
        controllerSettings.frameRate = 30.0f;

        Options.verboseMode = false;
       // m_RecorderController.StartRecording();

    }

    //void OnMouseExit()
   /* void OnTriggerExit2D(Collider2D col)
    {
        Debug.Log("Uncliclk");
        //OnDisable();
        m_RecorderController.StopRecording();
       

    }*/ //레코딩 중지 트리거 이용

 
    void Update()
    {
        if ((timeLeft <= 5) && (timeLeft > 0))
            countdownText.text = ("" + timeLeft);

        if (timeLeft == 0)
        {
            countdownText.text = " ";
            StopCoroutine("LoseTime");
            startBool = true;
            if (startBool == true)
            {
                Debug.Log("start capture call");
                StartCapture();
                startBool = false;
                timeLeft = 6;
            }
        }

        if (MovieTime == 0)
        {
            StopCoroutine("Time");
            MovieTime = 5;
            //capture.StopCapture();
            m_RecorderController.StopRecording();
            //레코딩 중지
            SceneManager.LoadScene("ShareScene");
        }
        //다음 씬으로 넘어가서 ShareResult를 쓰면 Null이어서 저장이 안 됨. 여기서는 DontDestroy때문에 다음씬에서도 저장됨.
        //if (Input.GetMouseButtonDown(0))
            //ShareResult();
    }



    public void StartTimer()
    {
        StartCoroutine("LoseTime");
      
    }

    public void StartCapture()
    {
        Debug.Log("start capture play");
        m_RecorderController.StartRecording();    //레코딩 시작
        StartCoroutine("Time");
        //capture.StartCapture();
      
    

    }

    IEnumerator LoseTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            timeLeft--;
        }
    }
    IEnumerator Time()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            MovieTime--;
        }
    }


}

//#endif

//1.자동으로 다음 씬 넘어가기
//2. 지정한 초 만큼 레코딩하기 

