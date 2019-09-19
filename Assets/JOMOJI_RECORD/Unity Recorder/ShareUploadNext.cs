using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShareUploadNext : MonoBehaviour
{
    public void shareUploadButton()
    {
       GameObject.FindWithTag("MoviePlayer").SendMessage("videoInit");
       GameObject.FindWithTag("ImagePlayer").SendMessage("imgInit");

       // SceneManager.LoadScene("S_QRcodeScene");
    }



}
