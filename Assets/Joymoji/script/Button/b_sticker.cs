using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class b_sticker : MonoBehaviour
{
    private Button _button1;
    // Use this for initialization
    void Start()
    {
        _button1 = GetComponent<Button>();

        _button1.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("3_A_sticker");
        });
    }
}
