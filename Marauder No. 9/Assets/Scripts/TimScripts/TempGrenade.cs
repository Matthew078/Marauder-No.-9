using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempGrenade : MonoBehaviour
{
    public float explosionForce = 100;
    public float explosionRadius = 5;
    public float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
    }

    public void detonate()
    {
        Collider[] cols = Physics.OverlapSphere(this.transform.position, 5f);
        int i = 0;
        while (i < cols.Length)
        {
            if (cols[i].gameObject.tag == "Enemy")
            {
                //cols[i].gameObject.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, this.transform.position, explosionRadius);
                cols[i].GetComponent<TempEnemy>().health -= 1;
            }
            i++;
        }

        Destroy(this.gameObject);
    }
}
