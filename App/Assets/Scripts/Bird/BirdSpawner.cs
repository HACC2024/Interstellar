using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BirdSpawner : MonoBehaviour
{
    public Bird[] birdPrefabs; // Array of bird prefabs
    private List<Bird> activeBirds = new List<Bird>(); // Tracks active birds in the scene
    private const float spawnDistanceFromOrigin = 1.5f; // 1.5 meters from (0,0)
    private const float respawnDelay = 5f; // Delay in seconds before a new bird can spawn

    private void Update()
    {
        if (activeBirds.Count < 3)
        {
            StartCoroutine(SpawnBirdWithDelay());
        }
    }

    IEnumerator SpawnBirdWithDelay()
    {
        yield return new WaitForSeconds(respawnDelay);

        while (activeBirds.Count < 3)
        {
            SpawnBirdsNearOrigin();
        }
    }

    void SpawnBirdsNearOrigin()
    {
        ShuffleBirdPrefabs();

        foreach (Bird birdPrefab in birdPrefabs)
        {
            if (activeBirds.Count >= 3) break;

            // Check spawn chance based on rarity
            if (ShouldSpawnBasedOnRarity(birdPrefab.rarity))
            {
                SpawnBirdNearOrigin(birdPrefab);
            }
        }
    }

    bool ShouldSpawnBasedOnRarity(Bird.Rarity rarity)
    {
        // Define spawn chances for each rarity level
        float spawnChance;
        switch (rarity)
        {
            case Bird.Rarity.Rare:
                spawnChance = 0.1f; // 10% chance for Rare
                break;
            case Bird.Rarity.Uncommon:
                spawnChance = 0.3f; // 30% chance for Uncommon
                break;
            default:
                spawnChance = 0.6f; // 60% chance for Common
                break;
        }

        // Return true if a random value falls within the spawn chance
        return Random.value < spawnChance;
    }

    void SpawnBirdNearOrigin(Bird birdPrefab)
    {
        Vector2 spawnOffset = Random.insideUnitCircle * spawnDistanceFromOrigin;
        Vector3 birdPosition = new Vector3(spawnOffset.x, 0, spawnOffset.y);

        Bird spawnedBird = Instantiate(birdPrefab, birdPosition, Quaternion.identity);
        activeBirds.Add(spawnedBird);
    }

    public void CaptureBird(Bird bird)
    {
        activeBirds.Remove(bird);
        Destroy(bird.gameObject); // Remove bird from scene after capturing
        StartCoroutine(SpawnBirdWithDelay());
    }

    void ShuffleBirdPrefabs()
    {
        for (int i = birdPrefabs.Length - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            Bird temp = birdPrefabs[i];
            birdPrefabs[i] = birdPrefabs[j];
            birdPrefabs[j] = temp;
        }
    }
}
