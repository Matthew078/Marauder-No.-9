    using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    [SerializeField]
    private NewPlayerScript p;
    [SerializeField]
    private float speed = 5f;
    [Header("Jump Settings")]
    [SerializeField]
    private float jumpPower = 10f;
    [SerializeField]
    private float jumpMultiplier = 1f;
    [SerializeField]
    private float fallMultiplier = 1f;

    private bool canJump;

    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    private void Start()
    {
        canJump = false;
    }

    // Update is called once per frame
    
    private void Update()
    {
        if (p.gc.IsGrounded() && p.pi.inputVertical > 0.1f)
        {
            canJump = true;
        }
    }

    private void FixedUpdate()
    {
        // movement mechanics
        Vector3 velocity = new Vector3(p.pi.inputHorizontal * speed, p.rb.velocity.y, 0f);
        p.rb.velocity = velocity;
        // jump mechanics       
        if (canJump)
        {
            canJump = false;
            p.rb.velocity = new Vector3(p.rb.velocity.x, jumpPower, 0f);
        }
        if (p.rb.velocity.y < 0)
        {
            p.rb.velocity -= new Vector3(0f, -Physics.gravity.y, 0f) * fallMultiplier * Time.deltaTime;
        }
    }
    
}
