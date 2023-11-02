using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    }

    void Update()
    {
        scoreText.text = $"Score:{snake.GetScore}";
    }

    public void ShowEndScreen()
    {
        endText.text = $"You Scored {snake.GetScore}";
        endScreen.SetActive(true);
    }
}
