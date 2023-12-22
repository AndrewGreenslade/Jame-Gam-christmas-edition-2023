using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int CountdownTime;
    public int spawnDelay;
    public int spawnRate;
    GameObject[] spawnPoints;
    public GameObject enemyPrefab;
    Anvil anvil;

    [HideInInspector]
    public AnvilUI anvilUI;
    float nextSpawn;
    int _kills;

    public int kills
    {
        get { return _kills; }
        set
        {
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
        nextSpawn = Time.time + spawnDelay;
    }

    private void Update()
    {
        if (nextSpawn < Time.time)
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
        Instantiate(
            enemyPrefab,
            spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position,
            Quaternion.identity
        );
        nextSpawn = Time.time + spawnRate;
    }

    public Anvil Anvil
    {
        get { return anvil; }
        set { anvil = value; }
    }
}
