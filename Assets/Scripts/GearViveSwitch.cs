#if (UNITY_STANDALONE || UNITY_EDITOR)
using UnityEngine;
#if (UNITY_EDITOR && !UNITY_STANDALONE)
using UnityEditor;
#endif
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Executes in edit mode, ensures the objects for the selected platform are active and the others inactive,
/// it also positions the GearVR camera on the positioning object (whatever this script is attached to)
/// </summary>
[ExecuteInEditMode]
public class GearViveSwitch : MonoBehaviour
{
    public enum Platform
    {
        HTCVive, GearVR
    }
    
    private Camera gearVRCam = null;
    public Platform platform;
    //SteamVR, player camera rig, etc, objects that use vive/steamvr specific stuff
    public List<GameObject> htcViveObjects = new List<GameObject>();
    //The camera used for the gearvr/ovrplayercontroller, objects that are only needed when running on GearVR
    public List<GameObject> gearVrObjects = new List<GameObject>();

    void Start()
    {
        for (int i = 0; i < gearVrObjects.Count; i++)
        {
            Camera gearObjectCam = gearVrObjects[i].GetComponent<Camera>();
            if (gearObjectCam != null)
            {
                gearVRCam = gearObjectCam;
            }
        }
    }

    void Update()
    {
        enableAndDisableComponents(platform == Platform.HTCVive);
    }

    public void enableAndDisableComponents(bool viveEnabled)
    {
        for (int i = 0; i < htcViveObjects.Count; i++)
            htcViveObjects[i].SetActive(viveEnabled);
        for (int i = 0; i < gearVrObjects.Count; i++)
            gearVrObjects[i].SetActive(!viveEnabled);

        if (!viveEnabled) positionGearVRCamera();
    }

    //This is assuming one camera, if you have multiple in your gearvr array you may need to change this somewhat
    private void positionGearVRCamera()
    {
        if(gearVRCam != null)
        {
            gearVRCam.transform.position = this.transform.position;
            gearVRCam.transform.rotation = this.transform.rotation;
        }
    }
}
#endif