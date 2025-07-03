using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public string heroControlMode = "Mouse";
    public int heroCollisionCount = 0;
    public int eggCount = 0;
    public int enemyCount = 0;
    public int enemyDestroyedCount = 0;

    public static GameManager instance;

    public int maxEnemyCount = 10;
    public GameObject enemyPrefab;
    private List<GameObject> enemies = new List<GameObject>();

    private float spawnBound = 90f;
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        for (int i = 0; i < maxEnemyCount; i++)
        {
            SpawnEnemy();
        }
    }
    void Update()
    {
        enemies.RemoveAll(e => e == null);

        while (enemies.Count < maxEnemyCount)
        {
            SpawnEnemy();
        }

        GameManager.instance.enemyCount = enemies.Count;

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Quit pressed");
            Application.Quit();
        }
    }

    void SpawnEnemy()
    {
        Vector2 pos = new Vector2(
            Random.Range(-spawnBound, spawnBound),
            Random.Range(-spawnBound, spawnBound)
        );

        GameObject enemy = Instantiate(enemyPrefab, pos, Quaternion.identity);
        enemies.Add(enemy);
    }
}
