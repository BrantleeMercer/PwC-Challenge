using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;


public class TestScript : MonoBehaviour
{
    private void OnCollisionEnter(Collision other) {
        
        if (other.gameObject.tag.Equals("Arrow"))
        {
            other.transform.SetParent(this.transform);
            Rigidbody arrowRB = other.gameObject.GetComponent<Rigidbody>();
            arrowRB.velocity = Vector3.zero;
            arrowRB.useGravity = false;
            arrowRB.isKinematic = true;
            arrowRB.detectCollisions = false;
        }
    }
}
