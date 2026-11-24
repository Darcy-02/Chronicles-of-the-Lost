using UnityEngine;

public class OpponentSpawner : MonoBehaviour
{
    [Header("Opponent Settings")]
    [SerializeField] private GameObject opponentPrefab;
    [SerializeField] private Transform spawnPoint;

    [Header("Match Settings")]
    [SerializeField] private MatchManager matchManager;

    private GameObject currentOpponent;

    public void SpawnNextOpponent()
    {
        Debug.Log("SPAWNER: SpawnNextOpponent called.");

        if (opponentPrefab == null || spawnPoint == null)
        {
            Debug.LogError("SPAWNER: Prefab or spawn point missing!");
            return;
        }

        if (matchManager == null)
        {
            Debug.LogError("SPAWNER: MatchManager is missing!");
            return;
        }

        if (currentOpponent != null)
            Destroy(currentOpponent);

        currentOpponent = Instantiate(
            opponentPrefab,
            spawnPoint.position,
            spawnPoint.rotation
        );

        Transform zombieModel = currentOpponent.GetComponentInChildren<Transform>(true);

        foreach (Transform child in currentOpponent.GetComponentsInChildren<Transform>(true))
        {
            if (child.name == "skeletonZombie")
            {
                zombieModel = child;
                break;
            }
        }

        if (zombieModel != null)
        {
            zombieModel.gameObject.SetActive(true);
            Debug.Log("SPAWNER: Zombie model enabled!");
        }
        else
        {
            Debug.LogError("SPAWNER: Zombie child not found!");
        }


        FighterBase newFighter =
    currentOpponent.GetComponentInChildren<FighterBase>(true);

        Debug.Log("FOUND FIGHTER: " +
    (newFighter != null ? newFighter.name : "NULL"));

        if (newFighter == null)
        {
            Debug.LogError("FIGHTER NOT FOUND. Checking spawned hierarchy...");

            foreach (var component in
                currentOpponent.GetComponentsInChildren<MonoBehaviour>(true))
            {
                Debug.Log("COMPONENT: " + component.GetType().Name +
                          " ON: " + component.gameObject.name);
            }

            return;
        }

        FighterCombat playerCombat =
            FindFirstObjectByType<PlayerController>()
            ?.GetComponent<FighterCombat>();

        FighterCombat opponentCombat =
            currentOpponent.GetComponent<FighterCombat>();

        if (playerCombat != null)
            playerCombat.SetOpponent(newFighter);

        if (opponentCombat != null && playerCombat != null)
            opponentCombat.SetOpponent(
                playerCombat.GetComponent<FighterBase>()
            );

        Time.timeScale = 1f;

        Debug.Log("SPAWNER: Starting new match.");

        matchManager.SetNewOpponent(newFighter);
        matchManager.StartNewMatch();
    }
}