using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    private int hitCount = 0;
    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
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

    private void OnDestroy()
    {
        GameManager.instance.enemyCount--;
        GameManager.instance.enemyDestroyedCount++;
    }
}
