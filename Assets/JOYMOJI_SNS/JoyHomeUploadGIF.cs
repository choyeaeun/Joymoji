using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using UnityEngine.UI;

public class JoyHomeUploadGIF : MonoBehaviour
{
    string JoyHomeAddress = "http://15.164.204.212:5252/upload";
    string realtime = null; //시간
    string gifFileName = null;
    string filePath = null;

    public void sendToHome()
     {

        // Debug.Log(Path.GetFileName("C:/ Users / DS / Desktop / GITHUB_4 / Assets / gifresult "));
        
         StartCoroutine(UploadGIF());
     }
    
    void Awake()
    {
        filePath = Application.dataPath + "/gifresult/test.gif";
    }

    IEnumerator UploadGIF()
    {
        yield return new WaitForEndOfFrame();
        /// <summary>
        /// 여기가 추가 부분 downloadhandler 쓰기위한 텍스쳐
        /// </summary>
        Texture2D sampleTexture = new Texture2D(2, 2);
        ////

        //File.GetCreationTime(String)
        byte[] fileGIF = File.ReadAllBytes(filePath); //file명 수정 
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

            GameObject.FindWithTag("QRcode").SendMessage("QRcodeTexture", results); //QR코드 texture띄우는 곳에 sendmessage
            GameObject.FindWithTag("GifPlayer").SendMessage("gifInit"); //생성된 이미지 파일 삭제 sendmessage


        }

  

    }
}
