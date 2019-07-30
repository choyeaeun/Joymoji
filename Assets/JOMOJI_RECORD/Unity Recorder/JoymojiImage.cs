using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Recorder;
using UnityEditor.Recorder.Input;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class JoymojiImage : MonoBehaviour
{

    RecorderController Im_RecorderController;
    public int timeLeft = 7;
    public int MovieTime = 5;
    public Text countdownText;
    private bool startBool = false;

    // void OnMouseEnter()
    // void OnTriggerStay2D(Collider2D col)
    void Start()
    {
        Debug.Log("cliclk");
      
        var controllerSettings = ScriptableObject.CreateInstance<RecorderControllerSettings>();
        Im_RecorderController = new RecorderController(controllerSettings);

        var mediaOutputFolder = Application.dataPath + "../JoymojiImage";

        // Image Sequence
        var imageRecorder = ScriptableObject.CreateInstance<ImageRecorderSettings>();
        imageRecorder.name = "My Image Recorder";
        imageRecorder.enabled = true;

        imageRecorder.outputFormat = ImageRecorderOutputFormat.PNG;
        imageRecorder.captureAlpha = true;

        //imageRecorder.outputFile = mediaOutputFolder + "/image_" + DefaultWildcard.Frame + "_" + DefaultWildcard.Take;
        imageRecorder.outputFile = mediaOutputFolder + "/image_"  + "_" + DefaultWildcard.Take;

        imageRecorder.imageInputSettings = new CameraInputSettings
        {
            source = ImageSource.MainCamera,
            outputWidth = 1920,
            outputHeight = 1080,
            captureUI = true
        };

        // Setup Recording
        controllerSettings.AddRecorderSettings(imageRecorder);

        controllerSettings.SetRecordModeToManual();
        controllerSettings.frameRate = 30.0f;

        Options.verboseMode = false;
       // Im_RecorderController.StartRecording();

    }



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
            Im_RecorderController.StopRecording();
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
        Im_RecorderController.StartRecording();   //레코딩 시작
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
