using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinText : MonoBehaviour
{
    public GameObject uiObject;


    void Start()
    {
        uiObject.SetActive(false);
    }

    private void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            uiObject.SetActive(true);
            StartCoroutine("Wait");
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(5);
        uiObject.SetActive(false);
        SceneManager.LoadScene(0);
    }
}
