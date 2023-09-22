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

        //Swapping Guns
        if (Input.GetKeyDown("e"))
        {
            //get colliders of immediate vicinity
            Collider[] colliders = Physics.OverlapBox(transform.position, new Vector3(1f, 1f, 1f));
            int i = 0;
            while (i < colliders.Length)
            {
                if (colliders[i].gameObject.tag == "Weapon")
                {
                    //get rid of old gun
                    if (gun)
                    {
                        gun.transform.parent = null;
                        gun.transform.position = this.transform.position;
                    }
                    //set new gun
                    gun = colliders[i].gameObject;
                    colliders[i].gameObject.transform.parent = this.transform;
                    gun.transform.position = this.transform.position + new Vector3(0, 3, 0);
                }
                i++;
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
