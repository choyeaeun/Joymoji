using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using System.IO;

public class JoymojiMoviePlayer : MonoBehaviour
{
  
    FileInfo File ;
 
    void Start()
    {
        var videoPlayer = gameObject.AddComponent<UnityEngine.Video.VideoPlayer>();

        try
        {
            
            videoPlayer.playOnAwake = true;
            videoPlayer.renderMode = UnityEngine.Video.VideoRenderMode.MaterialOverride;
            videoPlayer.targetMaterialRenderer = GetComponent<Renderer>();
            videoPlayer.targetMaterialProperty = "_MainTex";
            videoPlayer.isLooping = true;

            videoPlayer.url = "/Users/DS/Desktop/GITHUB_3/JoymojiMovie/MOVIE_001.mp4";
         //videoPlayer.url = "/Users/DS/Desktop/GITHUB_2/JoymojiImage/image__001.PNG";
         // videoPlayer.Play();
         File = new FileInfo(videoPlayer.url); //file 경로 확인

            // if (videoPlayer.url == null)
            // if(!(File.Exists(videoPlayer.url)))

            Debug.Log("Video File Exist");
            videoPlayer.Play();
        }

        catch (FileNotFoundException fe)
        {
            Debug.Log("Video File Does Not Exist!");
            videoPlayer.Stop();
        }

    }


    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        vp.playbackSpeed = vp.playbackSpeed / 10.0f;
       // vp.playbackSpeed =1.0f;
    }

}



