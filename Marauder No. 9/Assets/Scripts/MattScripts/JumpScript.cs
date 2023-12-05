using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScript : MonoBehaviour
{
    public GroundCheck groundCheck;
    public float jumpPower = 10f;
    public float jumpMultiplier = 1f;
    public float fallMultiplier = 1f;
    private bool jump;


    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        jump = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Jump") && groundCheck.IsGrounded())
        {
            jump = true;
        }
        if (Input.GetKeyDown("h"))
        {
            rb.isKinematic = true;
        }
        if (Input.GetKeyUp("h")) 
        {
            rb.isKinematic = false;
        }
    }

    void FixedUpdate()
    {
        if (jump)
        {
            jump = false;
            rb.velocity = new Vector3(rb.velocity.x, jumpPower, 0f);
        }

            if (rb.velocity.y < 0)
            {
                rb.velocity -= new Vector3(0f, -Physics.gravity.y, 0f) * fallMultiplier * Time.deltaTime;

            }      
    }
}
