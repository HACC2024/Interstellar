using UnityEngine;

public class BirdCapture : MonoBehaviour
{
    public int points; // Points for capturing this bird
    public float captureRange = 10f; // Max range for capturing the bird
    Camera arCamera;

    void Start()
    {
        arCamera = GetComponent<Camera>();
    }

    public bool IsBirdInView()
    {
        if (arCamera == null) return false;
        Collider birdCollider = GetComponent<Collider>();
        if (birdCollider == null)
        {
            Debug.LogWarning("No Collider found on the bird.");
            return false;
        }
        return IsVisible(transform.position, birdCollider.bounds.size, arCamera);
    }

    bool IsVisible(Vector3 pos, Vector3 boundSize, Camera camera)
    {
        var bounds = new Bounds(pos, boundSize);
        var planes = GeometryUtility.CalculateFrustumPlanes(camera);
        return GeometryUtility.TestPlanesAABB(planes, bounds);
    }

    public bool IsWithinCaptureRange()
    {
        if (arCamera == null) return false;
        float distance = Vector3.Distance(arCamera.transform.position, transform.position);
        return distance <= captureRange;
    }

    public void CaptureBird()
    {
        Debug.Log("Bird captured! Points awarded: " + points);
        PlayerScore.Instance.AddPoints(points);
        // BirdDexUIManager.Instance.AddBirdToUI(GetComponent<Bird>()); // Add bird to UI
        Destroy(gameObject); // Remove bird from scene after capture
    }
}
