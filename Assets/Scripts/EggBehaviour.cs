using UnityEngine;
using UnityEngine.Experimental.AI;

public class EggBehaviour : MonoBehaviour
{
    public float speed = 40f;
    private float worldBoundY;
    private float worldBoundX;
    private bool hasHit = false;

    private void Start()
    {
        worldBoundY = Camera.main.orthographicSize;
        worldBoundX = worldBoundY * Camera.main.aspect;
    }
    void Update()
    {
        if (hasHit) return;

        transform.position += transform.up * speed * Time.deltaTime;

        Vector3 pos = transform.position;
        if (Mathf.Abs(pos.x) > worldBoundX || Mathf.Abs(pos.y) > worldBoundY)
        {
            Destroy(gameObject);
            GameManager.instance.eggCount--;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (hasHit) return;

        if (other.CompareTag("Enemy"))
        {
            EnemyBehaviour eb = other.GetComponent<EnemyBehaviour>();
            if (eb != null)
            {
                eb.TakeDamage();
            }

            hasHit = true;
            Destroy(gameObject);
            GameManager.instance.eggCount--;
        }
    }

}
