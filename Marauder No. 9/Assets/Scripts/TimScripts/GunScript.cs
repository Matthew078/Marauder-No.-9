using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType { Auto, Semi, Burst };
public class GunScript : MonoBehaviour
{
    public GameObject bullet;
    [SerializeField] private float bulletBaseForce;
    [SerializeField] private float bulletRange;
    [SerializeField] private float fireRate;
    [SerializeField] private int damage = 10;
    [SerializeField] private int burstAmount = 3;
    [SerializeField] private WeaponType type = WeaponType.Auto;
    bool canFire;
    bool bursting;
    float fireTimer;
    int burstCount;
    
    void Start()
    {
        fireTimer = 100;
        canFire = true;
        burstCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (fireTimer <= fireRate)
        {
            fireTimer += Time.deltaTime;
        }
    }

    public void onFireUp()
    {
        canFire = true;
    }

    public void onClick(float playerVelocity, string bulletTag)
    {
        //Control Rate of Fire
        if (fireTimer > fireRate && canFire)
        {
            FireGun(playerVelocity, bulletTag);
            if (type == WeaponType.Auto)
            {
                canFire = true;
            }
            else if (type == WeaponType.Semi)
            {
                canFire = false;
            }
            else if (type == WeaponType.Burst)
            {
                burstCount += 1;
                if (burstCount > burstAmount)
                {
                    burstCount = 0;
                    canFire = false;
                }
                else
                {
                    canFire = true;
                }
            }
            fireTimer = 0f;
        }
    }

    public void FireGun(float playerVelocity, string bulletTag)
    {
        //Set bullet force and position based on player direction and speed
        Vector3 initialPosition;
        Vector3 bulletForce;
        initialPosition = transform.position + transform.forward;
        bulletForce = transform.forward * (bulletBaseForce + Mathf.Abs(playerVelocity * 50)); 

        //Instantiate bullet and set variables
        GameObject clone = Instantiate(bullet, initialPosition, Quaternion.identity);
        clone.gameObject.tag = bulletTag;
        clone.GetComponent<Rigidbody>().AddForce(bulletForce);

        //EDIT BULLET SCRIPT
        clone.GetComponent<BulletScript>().range = bulletRange + Mathf.Abs(playerVelocity) * 1.5f;           //Scale Range with player velocity or else range will appear shorter
        clone.GetComponent<BulletScript>().initialPosition = initialPosition;
        clone.GetComponent<BulletScript>().damage = damage;
    }

    public void switchToAuto()
    {
        type = WeaponType.Auto;
    }
}
