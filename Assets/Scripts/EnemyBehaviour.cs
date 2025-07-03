using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    private int hitCount = 0;
    private SpriteRenderer sr;
    [SerializeField]
    //飞机移速
    private float speed = 40f;
    [SerializeField]
    //飞机转向速率
    private float rotateRate = 10f;
    [SerializeField]
    //切换是否随机路径
    private bool randomTrace = false;

    [SerializeField]
    //追踪的路径点索引
    private int traceIndex = 0;
    [SerializeField]
    //路径点的最大索引
    //如果路径点数量改变，需要修改此值
    private int MAXIndex = 6;

    private string[] wayPoints = { "A", "B", "C", "D", "E", "F" };

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        Vector3 p = FindCoord(wayPoints[traceIndex]);
        PointAtPosition(p, rotateRate * Time.deltaTime);
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
    public Vector3 FindCoord(string name)
    {
        GameObject target = GameObject.Find(name);
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
        transform.up = Vector3.LerpUnclamped(transform.up, v, r);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision with " + collision.gameObject.name);
        // 检测是否到达路径点并更新下一目标
        if (collision.gameObject.name == wayPoints[traceIndex])
        {
            Debug.Log("Reached " + wayPoints[traceIndex]);
            traceIndex = (traceIndex + 1) % MAXIndex;
        }
    }
}
