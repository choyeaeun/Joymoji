using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class e_confirm : MonoBehaviour
{
    private Button Btn;
    // Use this for initialization
    void Start()
    {
        Btn = GetComponent<Button>();

        Btn.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("BodyScene");
        });
    }
}
