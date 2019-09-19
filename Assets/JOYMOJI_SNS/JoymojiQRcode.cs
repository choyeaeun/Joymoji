using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class JoymojiQRcode : MonoBehaviour
{
   
    public void QRcodeTexture(byte[] data)
    {
        //gameObject.SetActive(true);
        
       GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        plane.transform.position = new Vector3(0, 0, 34.6f);
        plane.transform.localScale = new Vector3(3.0f, 3.0f, 3.0f);
        plane.transform.localEulerAngles = new Vector3(-90.0f, 0, 0);
        plane.transform.localRotation = Quaternion.Euler(-90.0f, 0, 0);
    
        Texture2D sampleTexture = new Texture2D(2, 2);

        bool isLoaded = sampleTexture.LoadImage(data);

        if (isLoaded)
        {
            Debug.Log("data는 들어옴");    
        }

        //Renderer renderer = GetComponent<Renderer>();
        Renderer renderer = plane.GetComponent<Renderer>();



        if (isLoaded)
        {
            Debug.Log("큐알코드:success");
            renderer.material.mainTexture = sampleTexture;
            
        }

    }


}
