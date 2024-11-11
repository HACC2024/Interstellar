using UnityEngine;
using UnityEngine.UI;

public class ModelButton : MonoBehaviour
{
    public ModelSpawner modelSpawner;  // Reference to the ModelSpawner
    public string birdName;            // The name of the bird to spawn

    // This method will be assigned to the button's onClick event
    public void OnButtonPress()
    {
        if (modelSpawner != null && !string.IsNullOrEmpty(birdName))
        {
            modelSpawner.SpawnBirdByName(birdName); // Spawn the bird based on birdName
        }
        else
        {
            Debug.LogWarning("ModelSpawner or birdName is not assigned.");
        }
    }
}
