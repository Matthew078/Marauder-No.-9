using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponControl : MonoBehaviour
{
    [SerializeField]
    private GameObject gun;
    [SerializeField]
    private PlayerScript p;
    [SerializeField]
    private Rigidbody playerRB;
    [SerializeField]
    private GameObject wrist;
        [SerializeField]
    private GameObject body;
    private bool facingForwards;
    private bool hasGun;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = p.gameObject.GetComponent<Rigidbody>();
        p.a.SetBool("hasGun", false);
    }

    // Update is called once per frame
    void Update()
    {
        //player input
        hasGun = p.a.GetBool("hasGun");
        if (p.pi.inputFire && gun)
        {
            gun.GetComponent<GunScript>().onClick(playerRB.velocity.x, "PlayerBullet");
        }

        if (p.pi.inputFireUp && gun)
        {
            gun.GetComponent<GunScript>().onFireUp();
        }

        if (p.pi.inputInteract)
        {
            swapGun();
        }

        if (playerRB.velocity.x > 1.5f)
        {
            facingForwards = true;
        }
        else if (playerRB.velocity.x < -1.5f)
        {
            facingForwards = false;
        }
    }

    //switch current gun to one near the player
    private void swapGun()
    {
        //get colliders of immediate vicinity
        Collider[] colliders = Physics.OverlapBox(transform.position, new Vector3(.3f, 2f, 1f));
        colliders = colliders.OrderBy(c => (transform.position - c.transform.position).sqrMagnitude).ToArray();  //order colliders by proximity

        //loop through all colliders of immediat vicinity
        int i = 0;
        while (i < colliders.Length)
        {
            if (colliders[i].gameObject != gun.gameObject && colliders[i].gameObject.tag == "Weapon" && p.gc.IsGrounded())
            {
                removeGun();
                if (hasGun == false)
                {
                    StartCoroutine(GunPickupTimer());
                }
                else
                {
                    pickUpGun(colliders[i].gameObject);
                }
                break;
            }
            i++;
        }
    }
    private IEnumerator GunPickupTimer()
    {
        p.a.SetBool("hasGun", true);
        yield return new WaitForSeconds(0.13f);
        swapGun();     
    }



    //get rid of current gun
    private void removeGun()
    {
        if (gun)
        {
            gun.transform.parent = null;
            gun.transform.position = this.transform.position + new Vector3(0, 1, 0);
            gun.GetComponent<Rigidbody>().isKinematic = false;
        }
    }

    //sets gun object as player's current gun
    private void pickUpGun(GameObject newGun)
    {        //set new gun
        if (p.gc.IsGrounded())
        {
            gun = newGun;
            gun.GetComponent<Rigidbody>().isKinematic = true;
            newGun.gameObject.transform.parent = wrist.gameObject.transform;
            if (facingForwards == true)
            {
                gun.transform.position = this.transform.position + new Vector3(0.3f, 0.12f, 0);
                gun.transform.rotation = Quaternion.Euler(0,85,0);
            }
            else
            {   
                gun.transform.position = this.transform.position + new Vector3(-0.3f, 0.12f, 0);
                gun.transform.rotation = Quaternion.Euler(0,-90,0);
            }
        }
    }
}
