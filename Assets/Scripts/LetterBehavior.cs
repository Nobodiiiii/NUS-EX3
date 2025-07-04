using UnityEngine;
using TMPro;

public class LetterBehaviour : MonoBehaviour
{
    public string letterName;  // 保存这个对象代表的字母

    private int hitCount = 0;
    private TextMeshPro tmp;

    void Start()
    {
        tmp = GetComponent<TextMeshPro>();
    }
    public void TakeDamage()
    {
        hitCount++;

        Color c = tmp.color;
        c.a -= 0.25f;
        c.a = Mathf.Clamp01(c.a);
        tmp.color = c;

        if (hitCount >= 4)
        {
            Destroy(gameObject);
        }
    }

}