    using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    // Fields
    [SerializeField]
    private PlayerScript p;
    [SerializeField]
    public float speed = 5f;
    [Header("Jump Settings")]
    [SerializeField]
    private float jumpPower = 10f;
    [SerializeField]
    private float jumpMultiplier = 1f;
    [SerializeField]
    private float fallMultiplier = 1f;
    private bool hover;
    private bool canJump;
    private float hoverTimer;
    public ParticleSystem jetfire;


    // Awake is called when an enabled script instance is being loaded
    private void Awake()
    {
        // Reference PlayerScript for components
    }
    // Start is called before the first frame update
    private void Start()
    {
        canJump = false;
        p.rb.isKinematic = false;
        hover = false;
        hoverTimer = 0f;
        jetfire.Stop();
    }

    // Update is called once per frame
    private void Update()
    {
        // Check if player is able to jump
        print(hoverTimer);
        if (p.gc.IsGrounded() && p.pi.inputVertical > 0.1f)
        {
            canJump = true;
        }
        // Check if player should hover
        if (!p.gc.IsGrounded() && p.pi.inputHover)
        {
            fallMultiplier = -2.5f;
            hover = true;
            speed = 2.5f;
            if (!jetfire.isPlaying)
            {
                jetfire.Play();
            }
        }
        else
        {
            fallMultiplier = 2;
            hover = false;
            hoverTimer = 0f;
            speed = 4.3f;
            jetfire.Stop();
        }
        if (hover == true)
        {
            hoverTimer += Time.deltaTime;
        }
        if (hoverTimer >= 2.3f)
        {
            hover = false;
            fallMultiplier = 2;
        }        
    }

    // FixedUpdate is called once per physics frame
    private void FixedUpdate()
    {
        // Movement mechanics
        Vector3 velocity = new Vector3(p.pi.inputHorizontal * speed, p.rb.velocity.y, 0f);
        p.rb.velocity = velocity;
        // Jump mechanics       
        if (canJump)
        {
            SoundManager.Instance.playSound("Player_Jump");
            canJump = false;
            p.rb.velocity = new Vector3(p.rb.velocity.x, jumpPower, 0f);
        }
        if (p.rb.velocity.y < 0)
        {
            p.rb.velocity -= new Vector3(0f, -Physics.gravity.y, 0f) * fallMultiplier * Time.deltaTime;
        }
    }
}
