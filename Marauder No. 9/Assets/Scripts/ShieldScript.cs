using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldScript : MonoBehaviour
{
    [SerializeField] private MeshRenderer shieldMesh;
    [SerializeField] private float regenRate = .25f;
    //These Variables are only public for testing purposes
    public float shieldTimer;              //Time the shield has spent On, used for reflecting mechanic
    public float shieldHealth;          
    public bool shieldOn;
    public bool isBroken;
    public float delayIfBroken;            //Amount of Time the shield is broken

    //TEMPORARY TEST OBJECTS, NEED TO BE DELETED
    public GameObject healthbar;
    
    // Start is called before the first frame update
    void Start()
    {
        shieldMesh = GetComponent<MeshRenderer>();
        shieldMesh.enabled = false;
        shieldHealth = 100;
        shieldTimer = 0f;
        shieldOn = false;
        isBroken = false;
        delayIfBroken = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        //TEST CODE (WILL BE DELETED)
        if (Input.GetKeyDown("q"))
        {
            deployShield();
        }
        if (Input.GetKeyUp("q"))
        {
            deactivateShield();
        }
        healthbar.transform.localScale = new Vector3(shieldHealth / 10, 1f, 1f);
    }

    private void FixedUpdate()
    {
        updateShieldStats();
    }
    public void deployShield()
    {
        if (!isBroken)
        {
            shieldOn = true;
            shieldMesh.enabled = true;
        }
    }

    public void deactivateShield()
    {
        shieldOn = false;
        shieldTimer = 0f;
        shieldMesh.enabled = false;
    }

    private void breakShield()
    {
        deactivateShield();
        isBroken = true;
    }

    private void updateShieldStats()
    {
        //if Shield is broken, run these conditions
        if (isBroken && delayIfBroken < 3f)
        {
            delayIfBroken += Time.deltaTime;
        }
        else if (isBroken)
        {
            isBroken = false;
            delayIfBroken = 0f;
        }
        
        //Shield On vs Shield Off
        if(shieldOn)
        {
            shieldHealth -= Time.deltaTime * 33.33f;
            shieldTimer += Time.deltaTime;
        }
        else
        {
            if (shieldHealth < 100f)
            {
                shieldHealth += regenRate;
            }
        }

        //break Shield
        if (shieldHealth < 0)
        {
            breakShield();
        }
    }

    //BUG: Trigger box only detects colliders when the shield is activated before the bullet enters the sheild(due to onTriggerEnter)
    //This can mitigated by using onTriggerStay, but a variable in a bullet called "Reflected" would need to exist so the shield only flips the velocity once.
    private void OnTriggerEnter(Collider other)
    {
        //Reflect bullet if Timer < .25, otherwise delete bullet
        if (other.gameObject.tag == "Bullet" && shieldOn)
        {
            if (shieldTimer < .25f)
            {
                Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
                rb.velocity = new Vector3(-rb.velocity.x, -rb.velocity.y, -rb.velocity.z);
            }
            else
            {
                Destroy(other.gameObject);
            }
        }
    }
}
