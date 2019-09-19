using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;

public class JoyHomeUploadAvi : MonoBehaviour
{
    string JoyHomeAddress = "http://15.164.204.212:5252/upload";
    string realtime = null; //시간
    string JoyHomeLanguage = "ko";
    string textToDisplay = "Joymoji";


    /* public void sendToHome()
     {
         StartCoroutine(UploadPNG());
     }*/

    /*void OnMouseDown()
    {
        StartCoroutine(UploadAVI());
    }*///190917

    public void SendToHome()
    {
        StartCoroutine(UploadAVI());
    } //190917추가


    IEnumerator UploadAVI()
    {
        yield return new WaitForEndOfFrame();

       
        byte[] filedata = File.ReadAllBytes("/Users/DS/Desktop/GITHUB_4/JoymojiMovie/MOVIE_001.mp4"); ////190717파일경로변경

        System.DateTime time = System.DateTime.Now; //시간
        realtime = time.ToString("hh/mm/ss"); //시간

        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormFileSection("img", filedata, realtime + ".mp4", "video/mp4"));
        var www = UnityWebRequest.Post(JoyHomeAddress, formData);

        yield return www.SendWebRequest();


        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Finished Uploading mp4");
            byte[] results = www.downloadHandler.data; //여기서 HTTP한테 받아옴 result넘겨

            GameObject.FindWithTag("QRcode").SendMessage("QRcodeTexture", results); //QR코드 texture띄우는 곳에 sendmessage//190717추가
            GameObject.FindWithTag("MoviePlayer").SendMessage("videoInit"); //생성된 이미지 파일 삭제 sendmessage//190717추가
        }






    }
}
