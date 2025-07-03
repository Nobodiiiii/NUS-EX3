using TMPro;
using UnityEngine;

public class StatusUI : MonoBehaviour
{
    public TMP_Text heroControlModeText;
    public TMP_Text heroCollisionCountText;
    public TMP_Text eggCountText;
    public TMP_Text enemyCountText;
    public TMP_Text enemyDestroyedCountText;
    public TMP_Text enemyMode;

    void Update()
    {
        GameManager gm = GameManager.instance;

        heroControlModeText.text = "Hero Control Mode: " + gm.heroControlMode;
        heroCollisionCountText.text = "Hero Collisions: " + gm.heroCollisionCount;
        eggCountText.text = "Eggs in World: " + gm.eggCount;
        enemyCountText.text = "Enemies in World: " + gm.enemyCount;
        enemyDestroyedCountText.text = "Enemies Destroyed: " + gm.enemyDestroyedCount;
        enemyMode.text = "Enemy movement mode: " + EnemyBehaviour.randomTrace;
    }
}
