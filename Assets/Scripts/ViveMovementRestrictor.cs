using UnityEngine;
using System.Collections;

/// <summary>
/// Restricts the movement of the Vive to rotational to give a representation of GearVR capabilities
/// </summary>
public class ViveMovementRestrictor : MonoBehaviour
{
    private Vector3 hmdPos;
    public bool activated = true;
    public Transform playerCamera;
    public Transform cameraPositionObj;

    void LateUpdate()
    {
        if (activated)
        { 
            hmdPos = playerCamera.localPosition;
            transform.position = cameraPositionObj.position - hmdPos;
        }
    }
}