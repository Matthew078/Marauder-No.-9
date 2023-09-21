using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float moveForce;
    private float horizontalKey;


    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
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
    }

    void FixedUpdate()
    {
        // Apply force
        Vector2 force =  new Vector2(horizontalKey, 0f).normalized * moveForce;
        rb.AddForce(force);
    }

}
