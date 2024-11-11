using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BirdInfoManager : MonoBehaviour
{
    public Image birdImageUI;           // Reference to the UI Image component for bird image
    public TMP_Text birdNameUI;         // Reference to the UI Text component for bird name
    public TMP_Text birdInfoUI;         // Reference to the UI Text component for bird info

    // Method to display bird information on the Bird Info panel
    public void DisplayBirdInfo(string birdName, Sprite birdImage, string birdInfo)
{
    // Debug.Log($"Displaying Bird Info - Name: {birdName}, Has Image: {birdImage != null}, Description: {birdInfo}");

    if (birdImageUI != null && birdImage != null)
    {
        birdImageUI.sprite = birdImage;
        birdImageUI.enabled = true;
    }
    else
    {
        Debug.LogWarning("Bird image UI or bird image sprite is missing.");
    }

    if (birdNameUI != null)
    {
        birdNameUI.text = birdName;
    }
    else
    {
        Debug.LogWarning("Bird name UI component is missing.");
    }

    if (birdInfoUI != null)
    {
        birdInfoUI.text = birdInfo;
    }
    else
    {
        Debug.LogWarning("Bird info UI component is missing.");
    }
}

}
