using UnityEngine;

public class BirdAppPanel : Panel
{
    // Add logic specific to BirdApp here
    public override void Open()
    {
        base.Open();
        // Additional logic to open the BirdApp panel
        Debug.Log("BirdApp Panel Opened");
    }

    public override void Close()
    {
        base.Close();
        // Additional logic to close the BirdApp panel
        Debug.Log("BirdApp Panel Closed");
    }
}
