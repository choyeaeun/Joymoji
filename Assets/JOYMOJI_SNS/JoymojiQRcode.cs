using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class JoymojiQRcode : MonoBehaviour
{
    void Start()
    {
        
        //gameObject.SetActive(false);
        Debug.Log("텍스쳐:false");

    }


    public void QRcodeTexture(byte[] data)
    {
        //gameObject.SetActive(true);
        

        Debug.Log("텍스쳐:true");

        Texture2D sampleTexture = new Texture2D(2, 2);

        bool isLoaded = sampleTexture.LoadImage(data);

        Renderer renderer = GetComponent<Renderer>();

        if (isLoaded)
        {
            Debug.Log("큐알코드:success");
            renderer.material.mainTexture = sampleTexture;
           
        }

    }

   
} 






   