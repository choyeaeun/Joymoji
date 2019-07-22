using System;
using System.Linq;
using GetSocialSdk.Capture.Scripts;
//using GetSocialSdk.Core;
//using GetSocialSdk.Ui;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Test : MonoBehaviour
{
    private GetSocialCapture capture;
    public GetSocialCapturePreview capturePreview;

    public int timeLeft = 6;
    public int gifTime = 5;
    public Text countdownText;
    private GameObject BtnObj;

    private bool startBool = false;

    void Awake()
    {
        capture = GetComponent<GetSocialCapture>();
    }
    
    void Start()
    {
        countdownText.text = " ";
        BtnObj = GameObject.FindWithTag("UI");
    }

    public void StartTimer()
    {
        StartCoroutine("LoseTime");
        //timeLeft = 5;
    }

    public void StartCapture()
    {
        StartCoroutine("Time");
        capture.StartCapture();
        BtnObj.setActive(false);
        //gifTime += 5;
    }
    public void ShareResult()
    {
        Debug.Log("Starting gif generation");
        Action<byte[]> result = bytes =>
        {

        };
        capture.GenerateCapture(result);
    }
    void Update()
    {
        if((timeLeft <= 5)&&(timeLeft > 0))
            countdownText.text = ("" + timeLeft);

        if (timeLeft == 0)
        {
            countdownText.text = " ";
            StopCoroutine("LoseTime");
            startBool = true;
            if (startBool == true)
            {
                Debug.Log("start capture");
                StartCapture();
                startBool = false;
                timeLeft = 6;
            }
        }
        if (gifTime == 0)
        {
            StopCoroutine("Time");
            gifTime = 5;
            capture.StopCapture();
            capturePreview.Play();
            BtnObj.setActive(true);
        }
        if (Input.GetMouseButtonDown(1))
        {
            ShareResult();
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
    IEnumerator Time()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            gifTime--;
        }
    }
}
