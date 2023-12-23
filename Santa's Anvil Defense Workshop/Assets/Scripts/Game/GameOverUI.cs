using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public static GameOverUI Instance;
    public TextMeshProUGUI scoreText;
    float skippableTime;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        skippableTime = Time.time + 2;
    }

    private void Update()
    {
        scoreText.text = GameManager.Instance.kills.ToString();
        if (Input.anyKey && skippableTime < Time.time)
        {
            SceneManager.LoadScene(0);
        }
    }
}
