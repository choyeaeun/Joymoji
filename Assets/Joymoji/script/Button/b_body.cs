using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class b_body : MonoBehaviour
{
    private Button _button1;
    public GameObject buttonObj;

    void Start()
    {
        _button1 = GetComponent<Button>();

        _button1.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("3_B_character");
        });
    }

    void OnTriggerStay2D(Collider2D col)
    {
        buttonObj.GetComponent<Animation>().Play();
    }
}
