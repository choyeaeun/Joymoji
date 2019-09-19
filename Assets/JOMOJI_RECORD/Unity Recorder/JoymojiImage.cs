#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Recorder;
using UnityEditor.Recorder.Input;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;


public class JoymojiImage : MonoBehaviour
{

    RecorderController Im_RecorderController;

    //private bool startBool = false;
    //string param = "MoviePlayer";
    private float time =1f;
    public Text countdownText;
    private int timeLeft = 6;
    private GameObject[] BtnObj;
    bool call = false;

    void Awake() //Awake로 초기화 하는게 더 good
    {
        
        var controllerSettings = ScriptableObject.CreateInstance<RecorderControllerSettings>();
        Im_RecorderController = new RecorderController(controllerSettings);

        var mediaOutputFolder = Application.dataPath + "../JoymojiImage";

        // Image Sequence
        var imageRecorder = ScriptableObject.CreateInstance<ImageRecorderSettings>();
        imageRecorder.name = "My Image";
        imageRecorder.enabled = true;

        imageRecorder.outputFormat = ImageRecorderOutputFormat.PNG;
        imageRecorder.captureAlpha = true;

       // imageRecorder.outputFile = mediaOutputFolder + "/image_" + DefaultWildcard.Frame + "_" + DefaultWildcard.Take;
         imageRecorder.outputFile = mediaOutputFolder + "/image_" + "_" + DefaultWildcard.Take;

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
    
    IEnumerator Timer(){
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
                //gameObject.SendMessage("StopCapture");
                StopCapture();
            }

        }

    }

    public void StartCapture()
    {
        StartCoroutine(Timer());
        Im_RecorderController.StartRecording();

        Debug.Log("이미지 startcapture");
       

    }

    void StopCapture()
    {
        Im_RecorderController.StopRecording();
        Debug.Log("이미지 찍히고 stopCapture");
        SceneManager.LoadScene("ShareScene_img");

    }


    

}
#endif