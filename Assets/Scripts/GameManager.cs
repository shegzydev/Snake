using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] Text scoreText;
    [SerializeField] Text endText;

    [SerializeField] GameObject endScreen;

    [SerializeField] Button replayButton;
    [SerializeField] Button mainMenuButton;

    Snake snake;

    void Start()
    {
        snake = FindObjectOfType<Snake>();

        replayButton.onClick.AddListener(delegate
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        });

        mainMenuButton.onClick.AddListener(delegate
        {
            SceneManager.LoadScene(0);
        });
    }

    void Update()
    {
        scoreText.text = $"Score:{snake.GetScore}";
    }

    public void ShowEndScreen()
    {
        StartCoroutine(Show());
    }
    IEnumerator Show()
    {
        yield return new WaitForSeconds(1.0f);
        endText.text = $"You Scored {snake.GetScore}";
        endScreen.SetActive(true);
    }
}
