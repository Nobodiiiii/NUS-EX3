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
    private Vector2[] letterPos = new Vector2[] {
    new Vector2(-70, 70),
    new Vector2(70, -70),
    new Vector2(30, 0),
    new Vector2(-70, -70),
    new Vector2(70, 70),
    new Vector2(-30, 0)
    };


//字母是否显示
public static bool isShowed = true;
    public static bool lastH = false;

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
            GameObject go = SpawnLetter(letter , 0 , 0 );
            letterMap[letter] = go; 
        }
    }

    void Update()
    {
        ToggleMode();
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
    GameObject SpawnLetter(string letter , float offsetX, float offsetY)
    {
        int i = System.Array.IndexOf(lettersNames, letter);
        if (i < 0 || i >= letterPos.Length)
        {
            Debug.LogWarning("Letter not found in base list: " + letter);
            i = 0;
        }

        Vector2 basePos = letterPos[i];

        // 在基础位置上进行 ±15 范围的随机偏移
        Vector3 position = new Vector3(basePos.x + offsetX, basePos.y + offsetY, 0);

        GameObject letterGO = Instantiate(letterPrefab, position, Quaternion.identity);
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
                float offsetX = Random.Range(-15f, 15f);
                float offsetY = Random.Range(-15f, 15f);
                GameObject go = SpawnLetter(letter, offsetX, offsetY);
                letterMap[letter] = go;
            }
        }
    }
    //切换是否显示字母
    private void ToggleMode()
    {
        if ((Input.GetKey(KeyCode.H) ^ lastH) && !lastH)//检测上升沿
        {
            isShowed = !isShowed;
            for (int i = 0; i < lettersNames.Length; i++)
            {
                letterMap[lettersNames[i]].GetComponent<TextMeshPro>().enabled = isShowed;
            }
        }
        lastH = Input.GetKey(KeyCode.H);
    }
}
