using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempLoot : MonoBehaviour
{
    Rigidbody rb;
    private bool falling;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        falling = true;
    }

    // Update is called once per frame
    void Update()
    {
        stopFalling();
    }

    private void stopFalling()
    {
        if (falling)
        {
            Collider[] cols = Physics.OverlapBox(this.transform.position + Vector3.down/2, this.transform.localScale / 2);
            int i = 0;
            while (i < cols.Length)
            {
                if (cols[i].gameObject.tag == "Ground")
                {
                    falling = false;
                    rb.isKinematic = true;
                }
                i++;
            }
        }
    }
}