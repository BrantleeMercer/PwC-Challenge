using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRGrabInteractable))] // To ensure that the component has a XRGrabIntractable script attached
public class ArrowInteraction : MonoBehaviour
{
    private XRGrabInteractable xRGrabInteractable = null;
    private bool inAir = false ;
    private Vector3 lastPosition = Vector3.one;
    private Rigidbody arrowRigidBody = null;

    [SerializeField] private float speed;
    [SerializeField] private Transform tipPosition;

    private void Awake()
    {
        this.arrowRigidBody = GetComponent<Rigidbody>();
        this.inAir = false;
        this.lastPosition = Vector3.zero;
        this.xRGrabInteractable = GetComponent<XRGrabInteractable>();
        this.arrowRigidBody.interpolation = RigidbodyInterpolation.Interpolate;
    }         

    private void FixedUpdate()
    {
        if(this.inAir)
        {
            this.CheckCollision();
            this.lastPosition = tipPosition.position;
        }
    }

    private void CheckCollision()
    {
        if (Physics.Linecast(lastPosition, tipPosition.position, out RaycastHit hitInfo))
        {
            if (hitInfo.transform.TryGetComponent(out Rigidbody body))
            {
                this.arrowRigidBody.interpolation = RigidbodyInterpolation.None;
                this.transform.parent = hitInfo.transform;
                body.AddForce(arrowRigidBody.velocity, ForceMode.Impulse);
            }
            this.StopArrow();
        }
    }

    private void StopArrow()
    {
        this.inAir = false;
    }


    public void ReleaseArrow(float value)
    {
        this.inAir = true;
        MaskAndFire(value);
        this.lastPosition = tipPosition.position;
    }

    private void MaskAndFire(float power)
    {
        this.xRGrabInteractable.colliders[0].enabled = false;
        Vector3 force = -transform.right * power * speed;
        this.arrowRigidBody.AddForce(force, ForceMode.Impulse);
        Debug.Log($"Mask and fire : force = {force}");
    }

}