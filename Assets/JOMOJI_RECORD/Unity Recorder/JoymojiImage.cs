#if UNITY_EDITOR
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

    //private bool startBool = false;
    //string param = "MoviePlayer";
    private float time =1f;

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
        Im_RecorderController.StartRecording();

        Debug.Log("이미지 startcapture");
       

    }

    void StopCapture()
    {
        Im_RecorderController.StopRecording();
        Debug.Log("이미지 찍히고 stoprecord");
        SceneManager.LoadScene("ShareScene");

    }


    

}
#endif