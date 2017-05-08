Quick Setup
-----------
This assumes you are starting with a project that is already set up and deploying to Android and you have SteamVR installed in Steam for the Vive.
In order for your project to run on the Vive you must have 'Virtual Reality Supported' ticked and OpenVR in the virtual reality SDKs box.

1. Import this asset into your Unity project.

2. Drag [SteamVR] and [CameraRig] from the SteamVR/Prefabs folder into your scene. [CameraRig] will be used as the Vive player camera.

3. Drag the 'HMDPositionAndPlatformSwitch' asset into the scene. This asset is used to position the HMD - move it around to change the position of the player camera rather than the camera object itself. 

4. The script attached to 'HMDPositionAndPlatformSwitch' has 2 arrays: 'HtcViveObjects' and 'GearVRObjects', drag any objects in your scene that are specific to either (eg: SteamVR, the cameras for Gear and Vive).

5. Add the 'ViveMovementRestrictor' script to your Vive player CameraRig (if you're using the default prefab in the steamvr plugin), into the 'playerCamera' variable place the 'Camera (eye)' object of the [CameraRig], and into the 'cameraPositionObj' place the 'HMDPositionAndPlatformSwitch'.

6. Attach the 'ViveToGearVRInputs' script to the Vive player CameraRig (it doesn't have to go here, but I find it convenient to keep the Vive scripts together), into the left and right controller variables drag the right and left controller objects. By default this script emulates a mouse left click with the trigger and mouse movement with the pad of the Vive controller.

7. That's it, now just switch platform using the platform drop down in the 'HMDPositionAndPlatformSwitch' object, and if you want to move the HMD in the scene move this object as well. If you switch platform to HTC Vive and hit play it should start running on the Vive.

I find the chaperone very distracting when using the Vive for GearVR development purposes, so I highly recommend turning it off/to developer mode.