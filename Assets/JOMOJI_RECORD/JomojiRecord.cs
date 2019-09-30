/*#if UNITY_EDITOR

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
   
    
    private float time = 5f;
    public int timeLeft = 7;
    public Text countdownText;
    private GameObject[] BtnObj;
    bool call = false;

    void Awake()
    {
        
        //countdownText.text = " ";
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

    void Start()
    {
        //countdownText.SetActive(true);
        countdownText.text = " ";
        BtnObj = GameObject.FindGameObjectsWithTag("UI");
    }

    public void StartTimer()
    {
        StartCoroutine("LoseTime");

        if (BtnObj != null)
        {
            foreach (GameObject go in BtnObj)
                go.SetActive(false);
        }
    }
    void Update()
    {
        if ((timeLeft <= 5) && (timeLeft > 0))
        {
            countdownText.text = ("" + timeLeft);
        }
        else if (timeLeft == 0)
        {
            countdownText.text = " ";
            StopCoroutine("LoseTime");
            if (call == false)
            {
                call = true;
                StartCapture();

            }
          
        }
        else
        {
            //countdownText.SetActive(false);
        }
    }

    IEnumerator LoseTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            timeLeft--;
        }
    }

    IEnumerator Timer()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);

            if (time > 0)
            {
                time--;
                Debug.Log(time);

            }
            else
            {
                gameObject.SendMessage("StopCapture");
            }
            
        }

    }

    public void StartCapture()
    {
        StartCoroutine(Timer());
        Debug.Log("start capture play");
        m_RecorderController.StartRecording();    //레코딩 시작
       // StartCoroutine("Time");

    }

    public void StopCapture()
    {
        m_RecorderController.StopRecording();
        //레코딩 중지
        SceneManager.LoadScene("ShareScene_mp4");
    }

}

#endif
*/
//1.자동으로 다음 씬 넘어가기
//2. 지정한 초 만큼 레코딩하기 

