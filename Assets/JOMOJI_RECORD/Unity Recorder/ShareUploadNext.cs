using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShareUploadNext : MonoBehaviour
{
    public void shareUploadButton()
    {
        GameObject.FindWithTag("MoviePlayer").SendMessage("imgInit");
        GameObject.FindWithTag("ImagePlayer").SendMessage("imgInitImg");

        SceneManager.LoadScene("S_QRcodeScene");
    }



}
