using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public GameObject bullet;
    [SerializeField] private float bulletBaseForce;
    [SerializeField] private float bulletRange;
    [SerializeField] private float fireRate;
    float fireTimer;
    // Start is called before the first frame update
    void Start()
    {
        fireTimer = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (fireTimer <= fireRate)
        {
            fireTimer += Time.deltaTime;
        }
    }

    public void FireGun(bool forward, float playerVelocity)
    {
        //Control Rate of Fire
        if (fireTimer < fireRate)
        {
            return;
        }
        fireTimer = 0f;

        //Set bullet force and position based on player direction and speed
        Vector3 initialPosition;
        Vector3 bulletForce;
        if (forward)    
        {
            initialPosition = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);

            if (playerVelocity > 0)    { bulletForce = Vector3.right * (bulletBaseForce + playerVelocity * 50); }
            else                        { bulletForce = Vector3.right * (bulletBaseForce); }
        }
        else
        {
            initialPosition = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);

            if (playerVelocity < 0)    { bulletForce = Vector3.left * (bulletBaseForce + Mathf.Abs(playerVelocity * 50)); }
            else                        { bulletForce = Vector3.left * (bulletBaseForce); }
        }

        //Instantiate bullet and set variables
        GameObject clone = Instantiate(bullet, initialPosition, Quaternion.identity);
        clone.GetComponent<Rigidbody>().AddForce(bulletForce);
        clone.GetComponent<BulletScript>().range = bulletRange + Mathf.Abs(playerVelocity) * 1.5f;           //Scale Range with player velocity or else range will appear shorter
        clone.GetComponent<BulletScript>().initialPosition = initialPosition;

        
    }
}
