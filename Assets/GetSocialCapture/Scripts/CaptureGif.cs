using System;
using System.Linq;
using GetSocialSdk.Capture.Scripts;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class CaptureGif : MonoBehaviour
{
    private GetSocialCapture capture;
    public GetSocialCapturePreview capturePreview;

    void Awake()
    {
        capture = GetComponent<GetSocialCapture>();
    }

    void Start()
    {
        capturePreview.Play();
    }

    public void ShareResult()
    {
        Debug.Log("Starting gif generation");
        Action<byte[]> result = bytes =>
        {

        };
        capture.GenerateCapture(result);
    }
}
