using System.Collections;
using System.Collections.Generic;
using System.Net;
//using System.Numerics;
using UnityEngine;

public class Drawing : MonoBehaviour
{
    public float maxDrawing; // Maximum length of platform the player can use
    public float snapRadius; // Maximum distance to snap from
    public GameObject startMarkerPrefab; // Object to use to mark the start of the platform
    public GameObject pointerMarkerPrefab; // Object to mark snapping
    public GameObject platformPrefab; // To use as platform

    Vector3 platformStart; // 1st chosen end of platform
    GameObject currentStartMarker; // Marks one end
    GameObject pointerMarker; // Marks snapping
    bool drawingPlatform; // Has the starting point been chosen?
    bool snapping; // Currently snapping marker position
    bool snapped; // Start of platform snapped

    List<Vector3> platformPoints; // Pairs of end points of platforms
    List<Vector3[]> rotatedPlatforms;
    public List<Bounds> unrotatedPlatformBounds;
    public List<Matrix4x4> platformBoundRotations;

    /// <summary>
    /// Actions to take before the game starts;
    /// Initializes variables
    /// </summary>
    private void Awake()
    {
        drawingPlatform = false;
        snapping = false;
        platformPoints = new List<Vector3>();
        rotatedPlatforms = new List<Vector3[]>();
        unrotatedPlatformBounds = new List<Bounds>();
        platformBoundRotations = new List<Matrix4x4>();
    }

    /// <summary>
    /// Actions to take before 1st frame;
    /// Sets up the scene
    /// </summary>
    void Start()
    {
        pointerMarker = Instantiate(pointerMarkerPrefab, Vector3.zero, Quaternion.identity);
    }

    /// <summary>
    /// Actions to take every frame
    /// </summary>
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            OnLClick();
        }
        if (Input.GetButtonDown("Fire2"))
        {
            OnRClick();
        }

        float leastDist = float.PositiveInfinity;
        int argClosestPoint = 0;
        // Iterate through all endpoints to find the closest to the mouse pointer
        for (int i = 0; i < platformPoints.Count; i++)
        {
            float dist = (WorldMousePosition() - platformPoints[i]).magnitude;
            if (dist < leastDist)
            {
                leastDist = dist;
                argClosestPoint = i;
            }
        }

        // If close enouigh, snap to the pointer
        if (leastDist < snapRadius)
        {
            pointerMarker.transform.position = platformPoints[argClosestPoint];
            snapping = true;
        }
        else
        {
            pointerMarker.transform.position = WorldMousePosition();
            snapping = false;
        }
    }

    /// <summary>
    /// Actions to take on a right click
    /// </summary>
    void OnRClick()
    {
        drawingPlatform = false;
        Destroy(currentStartMarker);
    }

    /// <summary>
    /// Actions to take on left click
    /// </summary>
    void OnLClick()
    {
        Vector3 endPostition = pointerMarker.transform.position; // Position to place the platform end

        if (!drawingPlatform) // Start of platoform
        {
            platformStart = endPostition;
            currentStartMarker = Instantiate(startMarkerPrefab, endPostition, Quaternion.identity); // Place marker
            drawingPlatform = true;
            snapped = snapping;
        }
        else
        {
            Vector3 platformEnd = endPostition;
            Vector3 platformVec = platformEnd - platformStart; // Direction... and MAGNITUDE of platform
            Destroy(currentStartMarker);

            float platformLength = platformVec.magnitude;
            Vector3 platformCentre = platformStart + platformVec / 2;

            float platformAngle = -Vector3.SignedAngle(platformVec, Vector3.right, Vector3.forward);
            Quaternion platformOrientation = Quaternion.Euler(0, 0, platformAngle);
            Matrix4x4 rotation = Matrix4x4.Rotate(platformOrientation);

            GameObject newPlatform = Instantiate(platformPrefab, platformCentre, Quaternion.identity); // Create and scale platform, get bounds, then rotate
            newPlatform.transform.localScale = new Vector3(platformLength, 0.05f, 20);
            Bounds bounds = newPlatform.GetComponent<Collider>().bounds;
            newPlatform.transform.rotation = platformOrientation;

            // If platform has been made at a new place, add to list for snapping
            if (!snapped)
            {
                platformPoints.Add(platformStart);
            }
            if (!snapping)
            {
                platformPoints.Add(platformEnd);
            }

            // Endpoints of all platforms for networking

            // Bounds and rotations for fluid simulations
            unrotatedPlatformBounds.Add(bounds);
            platformBoundRotations.Add(rotation);

            drawingPlatform = false;
        }
    }

    /// <summary>
    /// Gets the position of the mouse pointer in the scene, with z = 0
    /// </summary>
    /// <returns>The mouse position</returns>
    Vector3 WorldMousePosition()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        return pos;
    }
}
