using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class StringInteraction : XRBaseInteractable //String is able to be interacted with
{
    [SerializeField] public Transform stringStartPoint;
    [SerializeField] public Transform stringEndPoint;

    private XRBaseInteractor stringInteractor = null; 
    private Vector3 pullPosition;
    private Vector3 pullDirection;
    private Vector3 targetDirection;

    public float PullAmount { get; private set; } = 0.0f;
    public Vector3 StringStartPoint { get => stringStartPoint.localPosition;}
    public Vector3 StringEndPoint { get => stringEndPoint.localPosition; }

    protected override void Awake()
    {
        base.Awake();
    }

    //Called when the VR hands grab something
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        this.stringInteractor = args.interactor;
    }

    //Called when the object that is in the hands are released
    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        this.stringInteractor = null;
        this.PullAmount = 0f;
    }

    //Same as update function
    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);
        
        if(updatePhase == XRInteractionUpdateOrder.UpdatePhase.Dynamic && isSelected)
        {
            this.pullPosition = this.stringInteractor.transform.position;
            this.PullAmount = CalculatePull(this.pullPosition);
						// Debug.Log("<<<<< Pull amount is "+ PullAmount+" >>>>>");
        }           
    }

    //Calculates the pull amount and keeps the direction front to back instead of allowing the pull to go side to side
    private float CalculatePull(Vector3 pullPosition)
    {
        this.pullDirection = pullPosition - stringStartPoint.position;
        this.targetDirection = stringEndPoint.position - stringStartPoint.position;
        float maxLength = targetDirection.magnitude;
        
        targetDirection.Normalize();

        float pullValue = Vector3.Dot(pullDirection, targetDirection) / maxLength;
        return Mathf.Clamp(pullValue, 0, 1);
    }
}