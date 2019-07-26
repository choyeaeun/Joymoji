using System;
using System.Linq;
using GetSocialSdk.Capture.Scripts;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Test : MonoBehaviour
{
    private GetSocialCapture capture;
    public GetSocialCapturePreview capturePreview;

    public int timeLeft = 7;
    public int gifTime = 5;
    public Text countdownText;
    private GameObject[] BtnObj;

    private bool startBool = false;

    void Awake()
    {
        capture = GetComponent<GetSocialCapture>();
        DontDestroyOnLoad(this.gameObject);
    }
    
    void Start()
    {
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

    public void StartCapture()
    {
        StartCoroutine("Time");
        capture.StartCapture();
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
            SceneManager.LoadScene("ShareScene");
        }
        //다음 씬으로 넘어가서 ShareResult를 쓰면 Null이어서 저장이 안 됨. 여기서는 DontDestroy때문에 다음씬에서도 저장됨.
        if (Input.GetMouseButtonDown(0))
            ShareResult();
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
