#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using System.IO;

public class JoymojiMoviePlayer : MonoBehaviour
{
  
    //FileInfo File ;
    
    void Start()
    {
        gameObject.SetActive(false);
     

        try
        {
            if (System.IO.File.Exists("C:/Users/DS/Desktop/GITHUB_3/JoymojiMovie/MOVIE_001.mp4"))
            {
                var videoPlayer = gameObject.AddComponent<UnityEngine.Video.VideoPlayer>();
                videoPlayer.playOnAwake = false;
                Debug.Log("Video File Exist");
                gameObject.SetActive(true); //###

                videoPlayer.url = "/Users/DS/Desktop/GITHUB_3/JoymojiMovie/MOVIE_001.mp4";
                FileInfo File = new FileInfo(videoPlayer.url); //file 경로 확인
                                                      //GameObject.FindWithTag("FileExist").SendMessage("FileExist", 1); //PlayerControl.cs


                videoPlayer.renderMode = UnityEngine.Video.VideoRenderMode.MaterialOverride;
                videoPlayer.targetMaterialRenderer = GetComponent<Renderer>();
                videoPlayer.targetMaterialProperty = "_MainTex";
                videoPlayer.isLooping = true;

                //###  videoPlayer.url = "/Users/DS/Desktop/GITHUB_3/JoymojiMovie/MOVIE_001.mp4";
                //videoPlayer.url = "/Users/DS/Desktop/GITHUB_2/JoymojiImage/image__001.PNG";
                // videoPlayer.Play();
                //###File = new FileInfo(videoPlayer.url); //file 경로 확인

                // if (videoPlayer.url == null)

                videoPlayer.Play();


            }

          
        }

        catch (FileNotFoundException fe)
        {
            Debug.Log("Video File Does Not Exist!");
            //videoPlayer.Stop();
        }
       
    }

    void videoInit()
    {
        //gameObject.SetActive(false); //sendmessage로 추가부분 0906
        try
        {
            System.IO.File.Delete("C:/Users/DS/Desktop/GITHUB_3/JoymojiMovie/MOVIE_001.mp4");
        }

        catch (FileNotFoundException fe)
        {
            Debug.Log("지울 동영상 없음");
        }

       
    }

    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        vp.playbackSpeed = vp.playbackSpeed / 10.0f;
       // vp.playbackSpeed =1.0f;
    }

}

#endif

