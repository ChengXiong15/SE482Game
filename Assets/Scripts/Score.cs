using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{
    public static int score;
    public static int highScore;
    public Text scoreText;

    public Text highScoreText;
    public Scene scene;


    private void Start()
    {
        score = 0;
        highScore = PlayerPrefs.GetInt("High Score" + gameObject.scene.name);
        highScoreText.text = PlayerPrefs.GetInt("High Score" + gameObject.scene.name, 0).ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GetComponent<Animator>().SetTrigger("Open");
            StartCoroutine(openChest());
        }
    }

    IEnumerator openChest(float delay = 1f)
    {
        yield return new WaitForSeconds(delay);
        this.gameObject.SetActive(false);
        addScore();
    }
    public void addScore()
    {
        score+=1;
        scoreText.text = score.ToString();

        if (score > PlayerPrefs.GetInt("High Score" + gameObject.scene.name, 0))
        {
            PlayerPrefs.SetInt("High Score" + gameObject.scene.name, score);
            highScoreText.text = score.ToString();
        }
    }
}
