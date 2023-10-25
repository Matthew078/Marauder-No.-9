using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponControl : MonoBehaviour
{
    [SerializeField]
    private GameObject gun;
    [SerializeField]
    private NewPlayerScript p;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private Rigidbody playerRB;

    private bool facingForwards;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = player.GetComponent<Rigidbody>();
        p = player.GetComponent<NewPlayerScript>();
        facingForwards = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerRB.velocity.x < 0)
        {
            facingForwards = false;
        }
        else if (playerRB.velocity.x > 0)
        {
            facingForwards = true;
        }

        if (p.pi.inputFire && gun)
        {
            gun.GetComponent<GunScript>().FireGun(facingForwards, playerRB.velocity.x);
        }

        if (p.pi.inputInteract)
        {
            swapGun();
        }
    }

    private void swapGun()
    {
        //get colliders of immediate vicinity
        Collider[] colliders = Physics.OverlapBox(transform.position, new Vector3(.5f, .5f, .5f));
        colliders = colliders.OrderBy(c => (transform.position - c.transform.position).sqrMagnitude).ToArray();  //order colliders by proximity

        //loop through all colliders of immediat vicinity
        int i = 0;
        while (i < colliders.Length)
        {
            if (colliders[i].gameObject != gun.gameObject && colliders[i].gameObject.tag == "Weapon")
            {
                removeGun();
                pickUpGun(colliders[i].gameObject);
                break;
            }
            i++;
        }
    }

    private void removeGun()
    {
        //get rid of current gun
        if (gun)
        {
            gun.transform.parent = null;
            gun.transform.position = this.transform.position + new Vector3(0, 1, 0);
            gun.GetComponent<Rigidbody>().isKinematic = false;
        }
    }

    private void pickUpGun(GameObject newGun)
    {
        //set new gun
        gun = newGun;
        newGun.gameObject.transform.parent = player.transform;
        gun.GetComponent<Rigidbody>().isKinematic = true;
        gun.transform.position = this.transform.position + new Vector3(0, 3, 0);
        //gun.transform.rotation = Quaternion.identity;
    }
}
