using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class howto : MonoBehaviour
{
    private Button _button;
    public GameObject buttonObj;

    // Use this for initialization
    void Start()
    {

        _button = GetComponent<Button>();

        _button.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("howto");
        });

    }
    void OnTriggerStay2D(Collider2D col)
    {
        buttonObj.GetComponent<Animation>().Play();
    }

}