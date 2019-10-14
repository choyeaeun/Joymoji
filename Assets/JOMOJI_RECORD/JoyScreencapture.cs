using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;

public class JoyScreencapture : MonoBehaviour
{
    private float time = 1f;
    public Text countdownText;
    private int timeLeft = 7;
    private GameObject[] BtnObj;
    bool call = false;
    string filename = "image__001.png";
    string mediaOutputFolder = null;

    void Awake()
    {
        //var mediaOutputFolder = Application.dataPath + "../JoymojiImage";
        System.IO.Directory.CreateDirectory(Application.dataPath + "./JoymojiImage");
        mediaOutputFolder = Application.dataPath + "./JoymojiImage";
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
                //gameObject.SendMessage("StopCapture");
                StopCapture();
            }

        }

    }

    public void StartCapture()
    {

        Debug.Log(Application.dataPath);
        StartCoroutine(Timer());
        //Im_RecorderController.StartRecording();

        ScreenCapture.CaptureScreenshot(mediaOutputFolder + "/" + filename);
        Debug.Log("스크립트이미지 startcapture");


    }

    void StopCapture()
    {

        Debug.Log("이미지 찍히고 stopCapture");
        SceneManager.LoadScene("ShareScene_img");

    }





}
