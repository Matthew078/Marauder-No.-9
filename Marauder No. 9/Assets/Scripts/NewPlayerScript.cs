using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NewPlayerScript : MonoBehaviour
{
    [Header("Player Scripts")]
    [SerializeField]
    private PlayerInputScript playerInputScript;
    [SerializeField]
    private PlayerMovementScript playerMovementScript;
    [SerializeField]
    private GroundCheck groundCheck;
    [Header("Health Settings")]
    [SerializeField]
    private int health;
    [Header("Objects")]
    [SerializeField]
    private GameObject gun;
    [SerializeField]
    private UIManager uiManager;

    public PlayerInputScript pi { get { return playerInputScript; } }
    public GroundCheck gc { get { return groundCheck; } }
    public Rigidbody rb { get; private set; }
    //components
    
    //public new Animation ;
    // make input script seperate so main script carries game data info, make audio script too, put scripts into seperate game obj
    private void Awake()
    {
       rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (pi.inputFire && gun)
        {
            gun.GetComponent<GunScript>().FireGun(true, rb.velocity.x);
        }
        if (pi.inputInteract)
        {
            swapGun();
        }
        uiManager.SetHealth(health/100);
    }

    private void swapGun()
    {
        //get colliders of immediate vicinity
        Collider[] colliders = Physics.OverlapBox(transform.position, new Vector3(.5f, .5f, .5f));
        colliders = colliders.OrderBy(c => (transform.position - c.transform.position).sqrMagnitude).ToArray();  //order colliders by proximity
        int i = 0;
        while (i < colliders.Length)
        {
            if (colliders[i].gameObject != gun.gameObject && colliders[i].gameObject.tag == "Weapon")
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
                //gun.transform.rotation = Quaternion.identity;
                break;
            }
            i++;
        }
    }
}
