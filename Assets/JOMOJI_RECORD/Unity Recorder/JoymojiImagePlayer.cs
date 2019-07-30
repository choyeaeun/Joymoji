using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

//using UnityEngine.UI;

public class JoymojiImagePlayer : MonoBehaviour
{

    void Start()
    {

        try
        {
            byte[] byteArray = File.ReadAllBytes(@"C:/Users/DS/Desktop/GITHUB_3/JoymojiImage/image__001.png");

            Texture2D sampleTexture = new Texture2D(2, 2);

            bool isLoaded = sampleTexture.LoadImage(byteArray);
            Renderer renderer = GetComponent<Renderer>();
            if (isLoaded)
            {
                Debug.Log("success");
                renderer.material.mainTexture = sampleTexture;
            }

        }

        catch (FileNotFoundException fe)
        {
            Debug.Log("image load fail");
        }


    }
}



