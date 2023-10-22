using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempGrenadeThrow : MonoBehaviour
{
    [SerializeField] private NewPlayerScript p;
    public List<GameObject> grenade_list = new List<GameObject>();
    public GameObject grenade;
    public float throwForce = 100;
    public float throwDelay = 1;
    public float throwTimer;
    public float grenadeDuration = 3;

    // Start is called before the first frame update
    void Start()
    {
        throwTimer = throwDelay + 1;
    }

    // Update is called once per frame
    void Update()
    { 
        timeOutGrenades();
        if (throwTimer <= throwDelay)
        {
            throwTimer += Time.deltaTime;
        }

        //TEST CODE
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
        GameObject clone = Instantiate(grenade, this.transform.position + this.transform.right, Quaternion.identity);
        grenade_list.Add(clone);
        clone.GetComponent<Rigidbody>().AddForce(transform.right * throwForce);
    }
}
