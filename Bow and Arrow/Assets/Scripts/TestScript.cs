using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;


public class TestScript : MonoBehaviour
{
    private InputDevice _rightController;
    private InputDevice _leftController;

    // Start is called before the first frame update
    void Start()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDeviceCharacteristics right = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(right, devices);

        if(devices.Count > 0)
        {
            _rightController = devices[0];
        } 

        Debug.Log(_rightController.characteristics);      


    }

    // Update is called once per frame
    void Update()
    {
        _rightController.TryGetFeatureValue(CommonUsages.primaryButton, out bool rightPressed);
        if(rightPressed)
        {
            Debug.Log("Right trigger pressed");
        }
        
    }
}
