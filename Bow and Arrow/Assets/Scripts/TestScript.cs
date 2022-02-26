using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;


public class TestScript : MonoBehaviour
{
    private ActionBasedController controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<ActionBasedController>();

        controller.selectAction.action.performed += SelectActivated;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SelectActivated(InputAction.CallbackContext context)
    {
        Debug.Log("select button pressed");
    }
}
