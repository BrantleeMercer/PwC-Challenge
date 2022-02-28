using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(StringInteraction))]
public class SocketInteraction : XRSocketInteractor //To interact with the socket set up on the bow string
{

    private XRBaseInteractor handHoldingArrow = null;
    private XRBaseInteractable currentArrow = null;
    private StringInteraction stringInteraction = null;
    private BowInteraction bowInteraction = null;
    private ArrowInteraction currentArrowInteraction = null;
    private InputDevice _rightController;
   
    protected override void Awake()
    {
        base.Awake();
        this.stringInteraction = GetComponent<StringInteraction>();
        this.bowInteraction = GetComponentInParent<BowInteraction>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        stringInteraction.selectExited.AddListener(ReleasaeArrow);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        stringInteraction.selectExited.RemoveListener(ReleasaeArrow);
    }

    protected override void OnHoverEntered(HoverEnterEventArgs args)
    {
        base.OnHoverEntered(args);
        this.handHoldingArrow = args.interactable.selectingInteractor;
        if (args.interactable.tag == "Arrow" && bowInteraction.BowHeld)
        {
            interactionManager.SelectExit(handHoldingArrow, args.interactable);
            interactionManager.SelectEnter(handHoldingArrow, stringInteraction);
        }
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        StoreArrow(args.interactable);

    }

    private void StoreArrow(XRBaseInteractable interactable)
    {
        if (interactable.tag == "Arrow")
        {
            this.currentArrow = interactable;
            this.currentArrowInteraction = currentArrow.gameObject.GetComponent<ArrowInteraction>();
            
            var devices = new List<UnityEngine.XR.InputDevice>();
            var desiredCharacteristics = UnityEngine.XR.InputDeviceCharacteristics.Right | UnityEngine.XR.InputDeviceCharacteristics.Controller;
            UnityEngine.XR.InputDevices.GetDevicesWithCharacteristics(desiredCharacteristics, devices);
            
            _rightController = devices[0];
            
        }
    }

    private void ReleasaeArrow(SelectExitEventArgs arg0)
    {
        if (currentArrow && bowInteraction.BowHeld)
        {
            ForceDetach();
            ReleaseArrowFromSocket();
            ClearVariables();
        }
    }

    public override XRBaseInteractable.MovementType? selectedInteractableMovementTypeOverride
    {
        get { return XRBaseInteractable.MovementType.Instantaneous; }
    }

    private void ForceDetach()
    {
        interactionManager.SelectExit(this, currentArrow);
    }

    private void ReleaseArrowFromSocket()
    {
        currentArrowInteraction.ReleaseArrow(stringInteraction.PullAmount);
    }

    private void ClearVariables()
    {
        this.currentArrow = null;
        this.currentArrowInteraction = null;
        this.handHoldingArrow = null;
    }
}