using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    private GameObject[] PlayerList;
    private int index;

    // Start is called before the first frame update
    void Start()
    {
     
        index = PlayerPrefs.GetInt("FileExist");

        PlayerList = new GameObject[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
            PlayerList[i] = transform.GetChild(i).gameObject;

        foreach (GameObject go in PlayerList)
            go.SetActive(true); 
         
    

        //받은 놈과 못받은 놈의 구별 
        //각자 sendmessage를 받으려고 하면 image video둘중 하나 선택시 다른 하나는 message를 못받음
        //따라서 하나의 스크립트에서 image와 video를 둘다 관리 해야됨

        //다 true 인데 sendmessage 못받으면 false로 돌려 
        //sendmessage로 받는애는 true로 돌리고 못받는애는 false로 돌려
        
        //if sendmessage로 movie왔다 (그대로 setActive유지 player에게 메세지 전송)
        //img에게는  sendmessage로 image에게 
    
        //FileExist(index);
    }

   /* public void FileExist( int i)
    {
        PlayerList[i].SetActive(true);
    }*/

    public void ListActiveControl(string tag)
    {
        if(tag == "MoviePlayer")
        {
            GameObject.FindWithTag(tag).SendMessage("videoInit");
        }

        else
        {
            GameObject.FindWithTag(tag).SendMessage( "imgInit");
        }
        

    }



}
