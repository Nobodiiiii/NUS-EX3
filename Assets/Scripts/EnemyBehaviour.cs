using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyBehaviour : MonoBehaviour
{
    private int hitCount = 0;
    private SpriteRenderer sr;
    [SerializeField]
    //飞机移速
    private float speed = 20f;
    [SerializeField]
    //飞机转向速率
    private float rotateRate = 0.8f;
    [SerializeField]
    //切换是否随机路径
    public static bool randomTrace = false;

    [SerializeField]
    //追踪的路径点索引
    private int traceIndex = 0;
    [SerializeField]
    //路径点的最大索引
    //如果路径点数量改变，需要修改此值
    private int MAXIndex = 6;

    private string[] wayPoints = { "A", "B", "C", "D", "E", "F" };
    //生成随机数
    private static System.Random rand;
    //检测上一帧是否按下J键
    private static bool lastMode = false;

    public float angle;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rand = new System.Random();
    }
    void Update()
    {   
        ToggleMode();//检测是否按下J键修改状态
        Vector3 p = FindCoord(wayPoints[traceIndex], randomTrace);
        PointAtPosition(p, rotateRate);
        transform.position += transform.up * speed * Time.deltaTime;
    }
    public void TakeDamage()
    {
        hitCount++;

        Color c = sr.color;
        c.a *= 0.8f;
        sr.color = c;

        if (hitCount >= 4)
        {
            Destroy(gameObject);
        }
    }
    //根据名称查找坐标
    public Vector3 FindCoord(string name,bool randomTrace)
    {
        GameObject target;
        target = GameObject.Find(name);
        if (target != null)
        {
            return target.transform.position;
        }
        return Vector3.zero;
    }

    private void OnDestroy()
    {
        GameManager.instance.enemyCount--;
        GameManager.instance.enemyDestroyedCount++;
    }
    private void PointAtPosition(Vector3 p, float r)
    {
        Vector3 v = p - transform.localPosition;
        angle = Vector3.SignedAngle(transform.up,v,Vector3.forward);
        transform.Rotate(0, 0, angle * rotateRate*Time.deltaTime);
    }
    private static void ToggleMode()
    {
        if (Input.GetKey(KeyCode.J)^lastMode&&!lastMode)//检测上升沿
        {
            randomTrace = !randomTrace;
        }
        lastMode = Input.GetKey(KeyCode.J);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision with " + collision.gameObject.name);
        // 检测是否到达路径点并更新下一目标
        if (collision.gameObject.name == wayPoints[traceIndex])
        {
            Debug.Log("Reached " + wayPoints[traceIndex]);
            int randNum = rand.Next(0, wayPoints.Length);
            randNum = randNum == traceIndex ? (randNum + 1)%MAXIndex : randNum;
            traceIndex = randomTrace? randNum : (traceIndex + 1) % MAXIndex;
        }
    }

}
