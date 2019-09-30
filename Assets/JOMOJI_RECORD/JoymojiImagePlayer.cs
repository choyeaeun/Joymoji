using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class JoymojiImagePlayer : MonoBehaviour
{

    void Start()
    {
        gameObject.SetActive(false);

        try
        {

            //byte[] byteArray = File.ReadAllBytes(@"C:/Users/DS/Desktop/GITHUB_4_backup/Assets/JoymojiImage/image__001.png" );
            byte[] byteArray = File.ReadAllBytes(Application.dataPath + "./JoymojiImage/image__001.png");
            //GameObject.FindWithTag("FileExist").SendMessage("FileExist", 0); //PlayerControl.cs

            Texture2D sampleTexture = new Texture2D(2, 2);

            bool isLoaded = sampleTexture.LoadImage(byteArray);
            Renderer renderer = GetComponent<Renderer>();
            if (isLoaded)
            {
                gameObject.SetActive(true);
                Debug.Log("이미지존재/텍스쳐ok/SetActive-True");
                renderer.material.mainTexture = sampleTexture;
            }



        }

        catch (FileNotFoundException fe)
        {
            Debug.Log("image load fail");
        }


    }



    void imgInit()
    {
        //gameObject.SetActive(false); //sendmessage추가 부분 0906
        try
        {
            gameObject.SetActive(false);
            System.IO.File.Delete(Application.dataPath + "./JoymojiImage/image__001.png");
        }

        catch (FileNotFoundException fe)
        {
            Debug.Log("지울이미지 없음");
        }
    }


}


//#endif

