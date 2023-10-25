using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempGrenadeThrow : MonoBehaviour
{
    [SerializeField] private NewPlayerScript p;
    [SerializeField] private List<GameObject> grenade_list = new List<GameObject>();
    [SerializeField] private GameObject grenade;
    [SerializeField] private float throwForce = 100;
    [SerializeField] private float throwDelay = 1;
    [SerializeField] private float grenadeDuration = 3;
    public float throwTimer;
    private Rigidbody playerRB;
    private bool facingForwards;
    
    // Start is called before the first frame update
    void Start()
    {
        throwTimer = throwDelay + 1;
        playerRB = p.gameObject.GetComponent<Rigidbody>();
        facingForwards = true;
    }

    // Update is called once per frame
    void Update()
    { 
        timeOutGrenades();

        //Update Direction
        if (playerRB.velocity.x < 0)
        {
            facingForwards = false;
        }
        else if (playerRB.velocity.x > 0)
        {
            facingForwards = true;
        }

        //UpdateTimer
        if (throwTimer <= throwDelay)
        {
            throwTimer += Time.deltaTime;
        }

        //Input
        if (p.pi.inputGrenade)
        {
            onClick();
        }
    }

    public void onClick()
    {
        if (throwTimer <= throwDelay)
        {
            detonateGrenades();
        }
        else
        {
            throwGrenade();
            throwTimer = 0;
        }
    }

    void timeOutGrenades()
    {
        foreach (GameObject clone in new List<GameObject>(grenade_list))
        {
            if (clone.GetComponent<TempGrenade>().timer > grenadeDuration)
            {
                grenade_list.Remove(clone);
                clone.GetComponent<TempGrenade>().detonate();
            }
        }
    }
    void detonateGrenades()
    {
        foreach(GameObject clone in new List<GameObject>(grenade_list))
        {
            grenade_list.Remove(clone);
            clone.GetComponent<TempGrenade>().detonate();
        }
    }

    void throwGrenade()
    {
        
        if (facingForwards)
        {
            GameObject clone = Instantiate(grenade, this.transform.position + Vector3.right, Quaternion.identity);
            grenade_list.Add(clone);
            clone.GetComponent<Rigidbody>().AddForce(Vector3.right * throwForce);
        }
        else
        {
            GameObject clone = Instantiate(grenade, this.transform.position - Vector3.right, Quaternion.identity);
            grenade_list.Add(clone);
            clone.GetComponent<Rigidbody>().AddForce(-Vector3.right * throwForce);
        }
    }
}
