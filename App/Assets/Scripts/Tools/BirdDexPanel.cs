using UnityEngine;

public class BirdDexPanel : Panel
{
    // Add logic specific to BirdDex here
    public override void Open()
    {
        base.Open();
        // Additional logic to open the BirdDex panel
        Debug.Log("BirdDex Panel Opened");
    }

    public override void Close()
    {
        base.Close();
        // Additional logic to close the BirdDex panel
        Debug.Log("BirdDex Panel Closed");
    }
}
