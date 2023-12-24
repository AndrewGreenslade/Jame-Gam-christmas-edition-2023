using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int CountdownTime;
    public int spawnDelay;
    public float spawnRate;
    public int maxToys;
    public int increaseEnemiesSpawnRateSeconds;
    public bool hardmode;
    GameObject[] spawnPoints;
    public GameObject enemyPrefab;
    Anvil anvil;

    [HideInInspector]
    public AnvilUI anvilUI;
    float nextSpawn;
    int _kills;
    bool gameOver;
    List<GameObject> enemies = new List<GameObject>();
    public GameObject gameOverCamera;

    public int kills
    {
        get { return _kills; }
        set
        {
            if (gameOver) { return; }
            _kills = value;
            anvilUI.UpdateToyList();
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        spawnPoints = GameObject.FindGameObjectsWithTag("Spawnpoint");
        gameOverCamera.SetActive(false);
        nextSpawn = Time.time + spawnDelay;
    }

    private void Start()
    {
        GameOverUI.Instance.gameObject.SetActive(false);
        StartCoroutine(IncreaseSpawnRate());
    }

    public void GameOver()
    {
        gameOver = true;
        gameOverCamera.SetActive(true);
        GameOverUI.Instance.gameObject.SetActive(true);
        GameOverUI.Instance.skippableTime = Time.time + 2f;
        GameOverUI.Instance.scoreText.text = kills.ToString();
        Player.Instance.gameObject.SetActive(false);
        PlayerUI.Instance.gameObject.SetActive(false);
        anvilUI.gameObject.SetActive(false);
        StartCoroutine(KillAllSlowly());
    }

    IEnumerator KillAllSlowly()
    {
        foreach (GameObject e in enemies)
        {
            e.GetComponent<Enemy>().TakeDamage(1000);
            yield return new WaitForSeconds(0.2f);
        }
        yield return null;
    }

    IEnumerator IncreaseSpawnRate()
    {
        yield return new WaitForSeconds(increaseEnemiesSpawnRateSeconds);
        spawnRate -= 0.1f;
        if (spawnRate <= 0.5f) { yield return null; }
        else
        {
            StartCoroutine(IncreaseSpawnRate());
        }
    }

    public bool isGameOver { get { return gameOver; } }

    private void Update()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i] == null)
            {
                enemies.RemoveAt(i);
            }
        }
        if (nextSpawn < Time.time && !gameOver)
        {
            Spawn();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            kills++;
        }
    }

    private void Spawn()
    {
        var temp = Instantiate(
            enemyPrefab,
            spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position,
            Quaternion.identity
        );
        temp.name = string.Format("Grinch '{0}'", Random.Range(0, 10000));
        enemies.Add(temp);
        nextSpawn = Time.time + spawnRate;
    }

    public Anvil Anvil
    {
        get { return anvil; }
        set { anvil = value; }
    }
}
