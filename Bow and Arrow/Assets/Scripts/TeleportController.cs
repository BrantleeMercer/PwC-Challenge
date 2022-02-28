using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class TeleportController : MonoBehaviour
{
    [SerializeField] private XRController _teleportRay;
    [SerializeField] private InputHelpers.Button _activateTeleportButton;

    private float _activateThreshold = 0.1f;

    void Update()
    {
        if(_teleportRay)
        {
            _teleportRay.gameObject.SetActive(ActivateTeleport(_teleportRay));
        }
       
    }

    public bool ActivateTeleport(XRController controller)
    {
        InputHelpers.IsPressed(controller.inputDevice, _activateTeleportButton, out bool isActive, _activateThreshold);
        return isActive;
    }
}
