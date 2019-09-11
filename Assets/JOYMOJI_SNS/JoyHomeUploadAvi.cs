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

    void OnMouseDown()
    {
        StartCoroutine(UploadAVI());
    }



    IEnumerator UploadAVI()
    {
        yield return new WaitForEndOfFrame();

        byte[] filedata = File.ReadAllBytes("/Users/DS/Desktop/GIT_3_Backup/JoymojiMovie/MOVIE_001.mp4");

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
        }
    }
}
