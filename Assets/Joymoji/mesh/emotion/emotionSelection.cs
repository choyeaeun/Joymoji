using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class emotionSelection : MonoBehaviour
{
    private GameObject[] emotionList;
    private int index;

    void Start()
    {
        index = PlayerPrefs.GetInt("EmotionSelected");

        emotionList = new GameObject[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
            emotionList[i] = transform.GetChild(i).gameObject;

        foreach (GameObject go in emotionList)
            go.SetActive(false);

        if (emotionList[index])
            emotionList[index].SetActive(true);
    }

    private void EmotionSwitch(int index)
    {
        foreach (GameObject go in emotionList)
            go.SetActive(false);
        emotionList[index].SetActive(true);

        PlayerPrefs.SetInt("EmotionSelected", index);
    }

    public void heartEmo()
    {
        EmotionSwitch(1);
    }
    public void cloudEmo()
    {
        EmotionSwitch(2);
    }
    public void ghostEmo()
    {
        EmotionSwitch(3);
    }
    public void BBooEmo()
    {
        EmotionSwitch(4);
    }
    public void musicEmo()
    {
        EmotionSwitch(5);
    }
    public void questionEmo()
    {
        EmotionSwitch(6);
    }
    public void normalEmo()
    {
        EmotionSwitch(0);
    }

}
