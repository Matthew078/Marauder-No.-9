using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float moveForce;
    public float jumpForce;
    private bool isHovering;
    private float horizontalKey;
    private bool playerDirection;

    public GameObject gun;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerDirection = true;
        isHovering = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Horizontal Movement input
        horizontalKey = Input.GetAxis("Horizontal");

        //Firing Gun and Hovering Input
        if (horizontalKey < 0) { playerDirection = false; }
        else if (horizontalKey > 0) { playerDirection = true; }
        if (Input.GetButton("Fire1"))
        {
            if (gun) 
            { 
                gun.GetComponent<GunScript>().FireGun(playerDirection, rb.velocity.x);
            }

            if (!isGrounded() && rb.velocity.y < 1)
            {
                isHovering = true;
            }
        }
        else
        {
            isHovering = false;
        }

        //Swapping Guns input
        if (Input.GetKeyDown("e"))
        {
            //get colliders of immediate vicinity
            Collider[] colliders = Physics.OverlapBox(transform.position, new Vector3(.5f, .5f, .5f));
            colliders = colliders.OrderBy(c => (transform.position - c.transform.position).sqrMagnitude).ToArray();  //order colliders by proximity
            int i = 0;
            while (i < colliders.Length)
            {
                if (colliders[i].gameObject.tag == "Weapon")
                {
                    //get rid of old gun
                    if (gun)
                    {
                        gun.transform.parent = null;
                        gun.transform.position = this.transform.position + new Vector3(0, 1, 0);
                        gun.GetComponent<Rigidbody>().isKinematic = false;
                    }
                    //set new gun
                    gun = colliders[i].gameObject;
                    colliders[i].gameObject.transform.parent = this.transform;
                    gun.GetComponent<Rigidbody>().isKinematic = true;
                    gun.transform.position = this.transform.position + new Vector3(0, 3, 0);
                    gun.transform.rotation = Quaternion.Euler(0 ,0 ,0);
                    break;
                }
                i++;
            }
        }

        //Jumping input
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded())
            {
                rb.AddForce(Vector3.up * jumpForce);
            }
        }
    }

    void FixedUpdate()
    {
        // Horizontal Force
        Vector2 force = new Vector2(horizontalKey, 0f).normalized * moveForce;
        rb.AddForce(force);

        //Hovering Force
        if (isHovering)
        {
            rb.AddForce(Vector3.up * 9.5f);
        }
    }

    //Checks if player is grounded
    bool isGrounded()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position - new Vector3(0, .5f, 0), new Vector3(.5f, .5f, .5f));
        int i = 0;

        while (i < colliders.Length)
        {
            if (colliders[i].gameObject.tag == "Ground")
            {
                return true;
            }
            i++;
        }
        return false;
    }
}
