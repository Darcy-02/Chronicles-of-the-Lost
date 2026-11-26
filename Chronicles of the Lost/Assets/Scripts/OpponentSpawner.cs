
using UnityEngine;

public class OpponentSpawner : MonoBehaviour
{
    [Header("Opponent Prefabs")]
    [SerializeField] private GameObject[] opponentPrefabs;

    [Header("Spawn Settings")]
    [SerializeField] private Transform spawnPoint;

    [Header("Match Settings")]
    [SerializeField] private MatchManager matchManager;

    [Header("Health Bar")]
    [SerializeField] private HealthBar player2HealthBar;

    private GameObject currentOpponent;

    public void SpawnOpponent(int levelIndex)
    {
        if (levelIndex < 0 || levelIndex >= opponentPrefabs.Length)
        {
            Debug.LogError("SPAWNER: Invalid opponent level!");
            return;
        }

        if (spawnPoint == null || matchManager == null)
        {
            Debug.LogError("SPAWNER: Missing references!");
            return;
        }

        GameObject prefab = opponentPrefabs[levelIndex];

        if (prefab == null)
        {
            Debug.LogError("SPAWNER: Opponent prefab is missing!");
            return;
        }

        if (currentOpponent != null)
            Destroy(currentOpponent);

        currentOpponent = Instantiate(
            prefab,
            spawnPoint.position,
            spawnPoint.rotation
        );


        FighterAI newAI =
            currentOpponent.GetComponentInChildren<FighterAI>(true);

        FighterHealth opponentHealth =
            currentOpponent.GetComponentInChildren<FighterHealth>(true);

        FighterCombat opponentCombat =
            currentOpponent.GetComponentInChildren<FighterCombat>(true);

        if (newAI == null)
        {
            Debug.LogError("SPAWNER: FighterAI missing!");
            Destroy(currentOpponent);
            return;
        }

        FighterBase newFighter = newAI;


        if (opponentHealth != null)
            opponentHealth.SetHealthBar(player2HealthBar);

        PlayerController player =
            FindFirstObjectByType<PlayerController>();

        if (player == null)
        {
            Debug.LogError("SPAWNER: PlayerController missing!");
            return;
        }

        FighterCombat playerCombat =
            player.GetComponentInChildren<FighterCombat>(true);

        FighterBase playerFighter =
            player.GetComponentInParent<FighterBase>()
            ?? player.GetComponentInChildren<FighterBase>(true);


        if (playerCombat != null)
            playerCombat.SetOpponent(newFighter);

        if (opponentCombat != null && playerFighter != null)
            opponentCombat.SetOpponent(playerFighter);

        Debug.Log("SPAWNER: Combat connected for " + currentOpponent.name);

        Time.timeScale = 1f;

        matchManager.SetNewOpponent(newFighter);
        matchManager.StartNewMatch();

        Debug.Log("SPAWNER: Spawned " + currentOpponent.name);
    }
}
