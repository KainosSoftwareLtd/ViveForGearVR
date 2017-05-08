using UnityEngine;
using WindowsInput;
using System.Collections;
using System.Runtime.InteropServices;
using System;
using Valve.VR;

/// <summary>
/// Uses Input Simulator to emulate mouse clicks/movement when using the Vive controller.
/// See https://inputsimulator.codeplex.com/ for more examples of how to use the input simulator if you need different inputs than those used here.
/// </summary>
public class ViveToGearVRInputs : MonoBehaviour
{
    public GameObject leftController;
    public GameObject rightController;
    private SteamVR_Controller.Device rightDevice;
    private SteamVR_Controller.Device leftDevice;

    [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
    private static extern void mouse_event(uint dwFlags, int dx, int dy, int dwData, int dwExtraInfo);
    [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
    private static extern bool SetCursorPos(int X, int Y);

    private const int MOUSE_EVENT_ABSOLUTE = 0x8000;
    private const int MOUSE_EVENT_LEFTDOWN = 0x0002;
    private const int MOUSE_EVENT_LEFTUP = 0x0004;

    void Start()
    {
        if(leftController != null)
        {
            SteamVR_TrackedController trackedLeftController = leftController.GetComponent<SteamVR_TrackedController>();
            if (trackedLeftController != null)
            {
                trackedLeftController.TriggerUnclicked += new ClickedEventHandler(mouseUp);
                trackedLeftController.TriggerClicked += new ClickedEventHandler(mouseDown);
            }
        }
        if (rightController != null)
        {
            SteamVR_TrackedController trackedRightController = rightController.GetComponent<SteamVR_TrackedController>();
            if (trackedRightController != null)
            {
                trackedRightController.TriggerUnclicked += new ClickedEventHandler(mouseUp);
                trackedRightController.TriggerClicked += new ClickedEventHandler(mouseDown);
            }
        }
    }

    void Update()
    {
        mouseTrack();
    }

    private void mouseUp(object sender, ClickedEventArgs e)
    {
        mouse_event(MOUSE_EVENT_ABSOLUTE | MOUSE_EVENT_LEFTUP, 0, 0, 0, 0);
    }

    private void mouseDown(object sender, ClickedEventArgs e)
    {
        mouse_event(MOUSE_EVENT_ABSOLUTE | MOUSE_EVENT_LEFTDOWN, 0, 0, 0, 0);
    }

    private SteamVR_Controller.Device getControllerDevice(SteamVR_TrackedObject trackedObj)
    {
        int index = trackedObj != null ? (int)trackedObj.index : -1;
        return index > -1 ? SteamVR_Controller.Input(index) : null;
    }

    //Vive pad coords scale is -1...1, -1 being left/bottom
    private void setMousePosToVivePadPos(SteamVR_Controller.Device device)
    {
        Vector2 touchpadPos = device.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad);
        SetCursorPos((int)(Screen.width * touchpadPos.x * 2), (int)-(Screen.height * touchpadPos.y * 2));
    }

    /// <summary>
    /// We need to get and check the devices here because the controllers
    /// often disconnect/reconnect causing issues if you don't check for their prescence before use.
    /// </summary>
    private void mouseTrack()
    {
        if (rightDevice == null || leftDevice == null)
        {
            if (rightController != null)
                rightDevice = getControllerDevice(rightController.GetComponent<SteamVR_TrackedObject>());

            if (leftController != null)
                leftDevice = getControllerDevice(leftController.GetComponent<SteamVR_TrackedObject>());
        }

        if (rightDevice != null && rightDevice.GetTouch(SteamVR_Controller.ButtonMask.Touchpad))
            setMousePosToVivePadPos(rightDevice);
        else if (leftDevice != null && leftDevice.GetTouch(SteamVR_Controller.ButtonMask.Touchpad))
            setMousePosToVivePadPos(leftDevice);
    }
}