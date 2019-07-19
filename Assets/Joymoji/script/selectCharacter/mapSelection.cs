using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mapSelection : MonoBehaviour
{
    private GameObject[] mapList;
    private int index;

    void Start()
    {
        index = PlayerPrefs.GetInt("MapSelected");

        mapList = new GameObject[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
            mapList[i] = transform.GetChild(i).gameObject;

        foreach (GameObject go in mapList)
            go.SetActive(false);

        if (mapList[index])
            mapList[index].SetActive(true);
    }

    public void ToggleLeft()
    {
        mapList[index].SetActive(false);

        index--;

        if (index < 0)
            index = mapList.Length - 1;

        mapList[index].SetActive(true);
    }

    public void ToggleRight()
    {
        mapList[index].SetActive(false);
        index++;

        if (index == mapList.Length)
            index = 0;

        mapList[index].SetActive(true);
    }

    public void ConfirmButton()
    {
        PlayerPrefs.SetInt("MapSelected", index);
        SceneManager.LoadScene("5_confirm");
    }
    public void LastConfirmButton()
    {
        PlayerPrefs.SetInt("MapSelected", index);
        SceneManager.LoadScene("BodyScene");
    }

    public void BackBtn()
    {
        SceneManager.LoadScene("3_B_character");
    }

    public void ConfirmBackBtn()
    {
        SceneManager.LoadScene("4_B_background");
    }
}
