using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float moveForce;
    private float horizontalKey;
    private bool playerDirection;

    public GameObject gun;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerDirection = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Player input
        horizontalKey = Input.GetAxis("Horizontal");

        //Firing Gun
        if (horizontalKey < 0) { playerDirection = false; }
        else if (horizontalKey > 0) { playerDirection = true; }
        if (Input.GetButton("Fire1"))
        {
            if (gun) 
            { 
                gun.GetComponent<GunScript>().FireGun(playerDirection, rb.velocity.x);
            }
        }
    }

    void FixedUpdate()
    {
        // Horizontal Force
        Vector2 force = new Vector2(horizontalKey, 0f).normalized * moveForce;
        rb.AddForce(force);
    }
}
