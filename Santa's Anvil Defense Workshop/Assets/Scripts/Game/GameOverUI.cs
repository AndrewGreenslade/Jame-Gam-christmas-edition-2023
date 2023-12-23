using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public static GameOverUI Instance;
    public TextMeshProUGUI scoreText;
    public float skippableTime = 100000000;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Update()
    {
        if (Input.anyKey && skippableTime < Time.time)
        {
            SceneManager.LoadScene(0);
        }
    }
}
