using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // 游戏状态
    public string heroControlMode = "Mouse";
    public int heroCollisionCount = 0;
    public int eggCount = 0;
    public int enemyCount = 0;
    public int enemyDestroyedCount = 0;

    public static GameManager instance;
    private float worldBoundY;
    private float worldBoundX;

    // 敌人生成
    public int maxEnemyCount = 10;
    public GameObject enemyPrefab;
    private List<GameObject> enemies = new List<GameObject>();

    // 字母生成
    public GameObject letterPrefab;
    private Dictionary<string, GameObject> letterMap = new Dictionary<string, GameObject>();
    private string[] lettersNames = { "A", "B", "C", "D", "E", "F" };

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        worldBoundY = Camera.main.orthographicSize;
        worldBoundX = worldBoundY * Camera.main.aspect;

        // 敌人生成
        for (int i = 0; i < maxEnemyCount; i++)
        {
            SpawnEnemy();
        }

        // 字母生成
        foreach (string letter in lettersNames)
        {
            GameObject go = SpawnLetter(letter);
            letterMap[letter] = go;
        }
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Quit pressed");
            Application.Quit();
        }

        RespawnEnemies();
        RespawnLetters();

    }

    // 敌人生成
    void SpawnEnemy()
    {
        Vector2 pos = new Vector2(
            Random.Range(-worldBoundX * 0.9f, worldBoundX * 0.9f),
            Random.Range(-worldBoundY * 0.9f, worldBoundY * 0.9f)
        );

        GameObject enemy = Instantiate(enemyPrefab, pos, Quaternion.identity);
        enemies.Add(enemy);
    }

    // 字母生成
    GameObject SpawnLetter(string letter)
    {
        float rowYTop = worldBoundY * 0.5f;
        float rowYBottom = -worldBoundY * 0.5f;
        int i = System.Array.IndexOf(lettersNames, letter);

        float y = (i < 3) ? rowYTop : rowYBottom;
        float t = (i % 3) / 2.0f;
        float x = Mathf.Lerp(-worldBoundX * 0.7f, worldBoundX * 0.7f, t);

        x += Random.Range(-30f, 30f);
        y += Random.Range(-20f, 20f);

        Vector3 position = new Vector3(x, y, 0);

        GameObject letterGO = Instantiate(letterPrefab, position, Quaternion.identity);
        // 设置字母名称，便于飞机查找
        letterGO.name = letter;

        var text = letterGO.GetComponent<TextMeshPro>();
        if (text != null)
            text.text = letter;

        var info = letterGO.GetComponent<LetterBehaviour>();
        if (info != null)
            info.letterName = letter;

        return letterGO;
    }

    // 敌人重生
    void RespawnEnemies()
    {
        enemies.RemoveAll(e => e == null);
        while (enemies.Count < maxEnemyCount)
        {
            SpawnEnemy();
        }
        enemyCount = enemies.Count;
    }

    // 字母重生
    void RespawnLetters()
    {
        foreach (string letter in lettersNames)
        {
            if (letterMap[letter] == null)
            {
                GameObject go = SpawnLetter(letter);
                letterMap[letter] = go;
            }
        }
    }
}
