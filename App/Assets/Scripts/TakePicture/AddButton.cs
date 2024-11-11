using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AddButton : MonoBehaviour
{
    public Transform scrollViewContent;         // Reference to the scroll view's content area
    public GameObject buttonPrefab;             // Prefab of the button to add to the scroll view
    public ModelSpawner modelSpawner;           // Reference to the ModelSpawner
    // public BirdInfoManager birdInfoManager;     // Reference to the BirdInfoManager

    // Method to add a button to the scroll view with text, image, and full bird data
    public void AddButtonToScrollView(string buttonText, Sprite birdImage, Bird bird)
    {
        // Instantiate the button prefab as a child of the content area
        GameObject newButton = Instantiate(buttonPrefab, scrollViewContent);
        newButton.transform.localScale = Vector3.one; // Reset scale if necessary

        // Set the button text if the prefab has a TMP_Text component
        TMP_Text textComponent = newButton.GetComponentInChildren<TMP_Text>();
        if (textComponent != null)
        {
            textComponent.text = buttonText;
        }

        // Set the image for the button
        Image imageComponent = newButton.transform.Find("BirdPic")?.GetComponent<Image>();
        if (imageComponent != null && birdImage != null)
        {
            imageComponent.sprite = birdImage;
            imageComponent.enabled = true; // Ensure the image component is enabled
        }
        else
        {
            Debug.LogWarning("Failed to find Image component or birdImage is null.");
        }

        // Add the OnClick listener to the button to handle bird info and spawning
        Button button = newButton.GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(() => OnButtonPress(bird));
        }
        else
        {
            Debug.LogWarning("Button component is not attached to the button prefab.");
        }

        newButton.SetActive(true);
    }

    // Method called when a button is pressed to display bird info and spawn the model
    private void OnButtonPress(Bird bird)
    {
        // if (birdInfoManager != null)
        // {
        //     birdInfoManager.DisplayBirdInfo(bird.birdName, bird.birdImage, bird.birdDescription); // Show bird information on the info panel
        // }
        // else
        // {
        //     Debug.LogWarning("BirdInfoManager is not assigned.");
        // }

        if (modelSpawner != null && !string.IsNullOrEmpty(bird.birdName))
        {
            modelSpawner.SpawnBirdByName(bird.birdName); // Spawn the bird model in the scene
        }
        else
        {
            Debug.LogWarning("ModelSpawner is not assigned or bird name is empty.");
        }
    }
}
