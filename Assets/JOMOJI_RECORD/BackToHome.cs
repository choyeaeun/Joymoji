using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class BackToHome : MonoBehaviour
{
   
    public void DeleteFileSendMessage()
    {
        if (GameObject.FindWithTag("ImagePlayer"))
        {
            GameObject.FindWithTag("ImagePlayer").SendMessage("imgInit");
        }
       
       
    }

    
}
