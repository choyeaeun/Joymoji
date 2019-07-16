using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class a_start : MonoBehaviour
{
    private Button _button;
    public GameObject coin;

    // Use this for initialization
    void Start()
    {

        _button = GetComponent<Button>();

        _button.onClick.AddListener(() =>
        {
            //coin.animation
            SceneManager.LoadScene("2_choose");
        });
    }

}