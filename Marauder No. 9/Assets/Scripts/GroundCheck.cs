using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{ 
    // fields
    private bool isGrounded;

    // IsGrounded method @return isGrounded field  
    public bool IsGrounded() { 
        return isGrounded; 
    }

    // Start is called before the first frame update
    void Start()
    {
        isGrounded = true;   
    }
    // OnTriggerEnter is called when a GameObject collides with another GameObject
    // @param other is the Collider from a GameObject this instance collided with
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ground" && !isGrounded)
        {
            SoundManager.Instance.playSound("Player_Land");
        }
    }
    // OnTriggerStay is called nearly every frame a GameObject is overlapping another GameObject
    // @param other is the Collider from a GameObject this instance collided with
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }    
    }
    // OnTriggerExit is called when a GameObject stops overlapping another GameObject
    // @param other is the Collider from a GameObject this instance collided with
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }
}
