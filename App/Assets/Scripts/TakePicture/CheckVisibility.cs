using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class CheckVisibility: MonoBehaviour
{
    Camera mainCamera;
    Plane[] cameraFrustum;

    Collider birdCollider;

    void Start() 
    {
        mainCamera = Camera.main;
        birdCollider = GetComponent<Collider>();
    }

        // Method to check if the bird is within the camera frustum
    public bool IsInView()
    {
        if (mainCamera == null || birdCollider == null) return false;

        var bounds = birdCollider.bounds;
        Plane[] cameraFrustum = GeometryUtility.CalculateFrustumPlanes(mainCamera);
        return GeometryUtility.TestPlanesAABB(cameraFrustum, bounds);
    }


}
