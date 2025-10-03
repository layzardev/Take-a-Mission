using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Spawn")]
    public Transform spawnPoint;     // drag spawn point dari scene
    public GameObject playerPrefab;  // prefab player

    void Start()
    {
        if (playerPrefab != null && spawnPoint != null)
        {
            Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity);
        }
        else
        {
            Debug.LogError("SpawnManager: SpawnPoint atau PlayerPrefab belum di-assign!");
        }
    }
}
