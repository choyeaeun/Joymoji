using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class stickerSelection : MonoBehaviour
{
    private GameObject[] stickerList;
    private int index;

    void Start()
    {
        index = PlayerPrefs.GetInt("StickerSelected");

        stickerList = new GameObject[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
            stickerList[i] = transform.GetChild(i).gameObject;

        foreach (GameObject go in stickerList)
            go.SetActive(false);

        if (stickerList[index])
            stickerList[index].SetActive(true);
    }

    public void ToggleLeft()
    {
        stickerList[index].SetActive(false);

        index--;
        if (index < 0)
            index = stickerList.Length - 1;

        stickerList[index].SetActive(true);
    }

    public void ToggleRight()
    {
        stickerList[index].SetActive(false);

        index++;
        if (index == stickerList.Length)
            index = 0;

        stickerList[index].SetActive(true);
    }

    public void ConfirmButton()
    {
        PlayerPrefs.SetInt("StickerSelected", index);
        SceneManager.LoadScene("StickerScene");
    }
    /*
    public void LastConfirmButton()
    {
        PlayerPrefs.SetInt("CharacterSelected", index);
        SceneManager.LoadScene("BodyScene");
    }*/

    public void BackBtn()
    {
        SceneManager.LoadScene("2_choose");
    }
}
