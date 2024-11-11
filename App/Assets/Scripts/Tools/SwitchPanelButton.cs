using UnityEngine;

public class SwitchPanelButton : MonoBehaviour
{
    // Array to hold all panels we want to toggle between
    public GameObject[] panels;

    // Method to switch to a specific panel by index
    public void ShowPanel(int panelIndex)
    {
        // Check if the index is valid
        if (panelIndex < 0 || panelIndex >= panels.Length)
        {
            Debug.LogWarning("Panel index out of range.");
            return;
        }

        // Deactivate all panels
        foreach (GameObject panel in panels)
        {
            panel.SetActive(false);
        }

        // Activate the selected panel
        panels[panelIndex].SetActive(true);
    }
}
