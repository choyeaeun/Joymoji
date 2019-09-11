using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;

public class JoyHomeUploadImg : MonoBehaviour
{
    string JoyHomeAddress = "http://15.164.204.212:5252/upload";
    string realtime = null; //시간
    string JoyHomeLanguage = "ko";
    string textToDisplay = "Joymoji";


    public void SendToHome()
    {
        StartCoroutine(UploadPNG());
    }

    IEnumerator UploadPNG()
    {
        yield return new WaitForEndOfFrame();

        byte[] fileimage = File.ReadAllBytes("C:/Users/DS/Desktop/GIT_3_Backup/JoymojiImage/image__001.png"); 
        int width = Screen.width;
        int height = Screen.height;
        Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);
        tex.LoadImage(fileimage);
        tex.Apply();


        byte[] bytes = tex.EncodeToPNG();
        Object.Destroy(tex); //texture는 날림

        System.DateTime time = System.DateTime.Now; //시간
        realtime = time.ToString("hh/mm/ss"); //시간
        Debug.Log(realtime);
      

        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormFileSection("img", bytes, realtime + ".png", "image/png"));
        var www = UnityWebRequest.Post(JoyHomeAddress, formData);
       //둘다 ok
     
      /*
        WWWForm form = new WWWForm();
        form.AddBinaryData("img", bytes,"time.png", "image/png");
        var www = UnityWebRequest.Post("http://15.164.204.212:5252/upload/single", form);
     */

        yield return www.SendWebRequest();


        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Finished Uploading Img");
            //////////////
            byte[] results = www.downloadHandler.data; //여기서 HTTP한테 받아옴 result넘겨
            Debug.Log(results);
           
            GameObject.FindWithTag("QRcode").SendMessage("QRcodeTexture", results); //QR코드 texture띄우는 곳에 sendmessage

           

        }
    }
}
