using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/*
    Code followed from author Ashray Pai bow and arrow in vr tutorital

    https://blog.immersive-insiders.com/bow-and-arrow-in-vr-part2/
*/

public class BowInteraction : XRGrabInteractable //Bow is able to be grabbed.
{
    private LineRenderer bowString; 
    private StringInteraction stringInteraction; 

    [SerializeField] private Transform socketTransform;
    public bool BowHeld { get; private set; }
    

    protected override void Awake()
    {
        base.Awake();
        stringInteraction = GetComponentInChildren<StringInteraction>();
        bowString = GetComponentInChildren<LineRenderer>();
        this.movementType = MovementType.Instantaneous;
    }

    //Returns true on the bow held when the selected object is in the VR Hand
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        BowHeld = true;
        base.OnSelectEntered(args);
    }

    //Sets Bow held to false when the bow is released
    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        BowHeld = false;
        base.OnSelectExited(args);       
    }

    //Acts as update function
    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);
        
        if (updatePhase == XRInteractionUpdateOrder.UpdatePhase.Dynamic)
        {
                UpdateBow(stringInteraction.PullAmount);
        }
    }

    //Creates the bend in the bow string when the bow string is being pulled, also keeps the line straight and not side to side
    private void UpdateBow(float pullAmount)
    {
        float xPositionStart = stringInteraction.stringStartPoint.localPosition.x;
        float xPositionEnd = stringInteraction.stringEndPoint.localPosition.x;

        Vector3 linePosition = Vector3.right * Mathf.Lerp(xPositionStart, xPositionEnd, pullAmount);

        bowString.SetPosition(1, linePosition);
        socketTransform.localPosition = linePosition;
        
    }

}