using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;


public class TestScript : MonoBehaviour
{
    private int _hitCounter = 0;
    private UIManager _uiManager;

    private void Start() 
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if(_uiManager == null)
        {
            Debug.LogError("_uiManager :: TestScript == null");
        }
    }

    private void OnCollisionEnter(Collision other) {
        
        if (other.gameObject.tag.Equals("Arrow"))
        {
            other.transform.SetParent(this.transform);

            Rigidbody arrowRB = other.gameObject.GetComponent<Rigidbody>();
            arrowRB.velocity = Vector3.zero;
            arrowRB.useGravity = false;
            arrowRB.isKinematic = true;
            arrowRB.detectCollisions = false;
            
            _hitCounter++;
            _uiManager.UpdateHitCounter(_hitCounter);
        }
    }
}
