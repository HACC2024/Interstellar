using UnityEngine;

public class ModelSpawner : MonoBehaviour
{
    public Transform spawnLocation; // Reference to the spawner's position
    public GameObject[] birdPrefabs; // Array of all available bird prefabs

    private GameObject currentBirdInstance; // The currently spawned bird instance

    // Method to spawn a bird model based on its name
    public void SpawnBirdByName(string birdName)
    {
        // Destroy the currently spawned bird, if it exists
        if (currentBirdInstance != null)
        {
            Destroy(currentBirdInstance);
        }

        // Find the bird model prefab with the specified birdName
        GameObject birdPrefab = GetBirdPrefabByName(birdName);
        if (birdPrefab != null)
        {
            currentBirdInstance = Instantiate(birdPrefab, spawnLocation.position, spawnLocation.rotation);
        }
        else
        {
            Debug.LogWarning("Bird prefab with name " + birdName + " not found.");
        }
    }

    // Helper method to find a bird prefab by name
    private GameObject GetBirdPrefabByName(string birdName)
    {
        foreach (GameObject prefab in birdPrefabs)
        {
            Bird birdComponent = prefab.GetComponent<Bird>();
            if (birdComponent != null && birdComponent.birdName == birdName)
            {
                return prefab;
            }
        }
        return null; // Return null if no matching prefab is found
    }
}
