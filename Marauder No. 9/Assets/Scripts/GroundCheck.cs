using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{ 

    private bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        isGrounded = false;   
    }

    // Update is called once per frame
    void Update()
    {
            
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }    
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }

    public bool IsGrounded() { return isGrounded; }

}
