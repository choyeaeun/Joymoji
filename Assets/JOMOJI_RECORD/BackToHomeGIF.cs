using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class BackToHomeGIF : MonoBehaviour
{
    public void DeleteFileSendMessage()
    {
        if (GameObject.FindWithTag("GifPlayer"))
        {
            GameObject.FindWithTag("GifPlayer").SendMessage("gifInit");
        }
        
    }


}
