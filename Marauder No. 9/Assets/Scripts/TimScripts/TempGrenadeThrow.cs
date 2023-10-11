using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempGrenadeThrow : MonoBehaviour
{
    public List<GameObject> grenade_list = new List<GameObject>();
    public GameObject grenade;
    public float throwForce = 100;
    public float throwDelay;
    public float throwTimer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void detonateGrenades()
    {
        foreach(GameObject clone in new List<GameObject>(grenade_list))
        {
            //clone.GetComponent<TempGrenade>().detonate();
        }
    }

    void throwGrenade()
    {
        GameObject clone = Instantiate(grenade, this.transform.position + this.transform.forward, Quaternion.identity);
        grenade_list.Add(clone);
        clone.GetComponent<Rigidbody>().AddForce(transform.forward * throwForce);
    }
}
