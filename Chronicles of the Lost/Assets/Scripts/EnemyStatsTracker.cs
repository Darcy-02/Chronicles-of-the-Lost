using UnityEngine;

public class EnemyStatsTracker : MonoBehaviour
{
    [SerializeField] private FighterHealth enemyHealth;

    private void OnEnable()
    {
        if (enemyHealth == null)
            enemyHealth = GetComponent<FighterHealth>();

        if (enemyHealth != null)
            enemyHealth.OnDefeated += TrackDefeat;
    }

    private void OnDisable()
    {
        if (enemyHealth != null)
            enemyHealth.OnDefeated -= TrackDefeat;
    }

    private void TrackDefeat()
    {
        if (GameDataManager.Instance == null)
            return;

        GameDataManager.Instance.CurrentData.enemiesDefeated++;
        GameDataManager.Instance.SaveGame();

        Debug.Log("Enemies defeated: " +
            GameDataManager.Instance.CurrentData.enemiesDefeated);
    }
}