using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;

public class JoyHomeUploadGIF : MonoBehaviour
{
    string JoyHomeAddress = "http://15.164.204.212:5252/upload";
    string realtime = null; //시간

    /* public void sendToHome()
     {
         StartCoroutine(UploadPNG());
     }*/

    void OnMouseDown()
    {
        StartCoroutine(UploadGIF());
    }



    IEnumerator UploadGIF()
    {
        yield return new WaitForEndOfFrame();
        /// <summary>
        /// 여기가 추가 부분 downloadhandler 쓰기위한 텍스쳐
        /// </summary>
        Texture2D sampleTexture = new Texture2D(2, 2);
      ////

        byte[] fileGIF = File.ReadAllBytes("C:/Users/DS/Desktop/GIT_3_Backup/Assets/gifresult/test.gif");
        System.DateTime time = System.DateTime.Now; //시간
        realtime = time.ToString("hh/mm/ss"); //시간



        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormFileSection("img", fileGIF, realtime + ".gif", "image/gif"));
        var www = UnityWebRequest.Post(JoyHomeAddress, formData);
        www.downloadHandler = new DownloadHandlerBuffer();

        yield return www.SendWebRequest();


        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Finished Uploading GIF");
            //////////////
            byte[] results = www.downloadHandler.data; //여기서 HTTP한테 받아옴 result넘겨
            Debug.Log(results);
            //bool isLoaded = sampleTexture.LoadImage(results);
            // Renderer renderer = GetComponent<Renderer>();
            GameObject.FindWithTag("QRcode").SendMessage("QRcodeTexture", results); //QR코드 texture띄우는 곳에 sendmessage

            /*if (isLoaded)
            {
               Debug.Log("큐알코드:success");
               renderer.material.mainTexture = sampleTexture;
                GameObjtect.FindWithTag("QRcode").SendMessage("QRcodeTexture", results);  //QR코드 texture띄우는 곳에 sendmessage

            }

            else
            {
                Debug.Log("큐알코드:fail");

            }*/
            //sampleTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;


        }
    }
}
