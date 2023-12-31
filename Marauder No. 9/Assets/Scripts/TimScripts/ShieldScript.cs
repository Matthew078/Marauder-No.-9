using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class ShieldScript : MonoBehaviour
{
    [SerializeField] private MeshRenderer shieldMesh;
    [SerializeField] private Material shieldMat;
    [SerializeField] private float regenRate = .25f;
    [SerializeField] private PlayerScript p;
    public List<GameObject> bullets = new List<GameObject>();
    //These Variables are only public for testing purposes
    public float shieldTimer;              //Time the shield has spent On, used for reflecting mechanic
    public float shieldHealth;          
    public bool shieldOn;
    public bool isBroken;
    public float delayIfBroken;            //Amount of Time the shield is broken
    
    // Start is called before the first frame update
    void Start()
    {
        shieldMesh = GetComponent<MeshRenderer>();
        shieldMesh.enabled = false;
        shieldHealth = 100;
        shieldTimer = 0f;
        shieldOn = false;
        isBroken = false;
        delayIfBroken = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        //Input
        if (p.pi.inputDefenseDown)
        {
            deployShield();
        }
        if (p.pi.inputDefenseUp)
        {
            deactivateShield();
        }

        if (shieldOn)
        {
            SoundManager.Instance.playShieldSound();
        }
        else
        {
            SoundManager.Instance.stopShieldSound();
        }
        updateMatAlpha();
        reflectBullets();
    }

    private void FixedUpdate()
    {
        updateShieldStats();
    }

    //Change colors
    void updateMatAlpha()
    {
        UnityEngine.Color color = new UnityEngine.Color(1f , 2 * (shieldHealth / 100), 2 * (shieldHealth / 100));
        shieldMat.color = color;
    }

    //Called when button is pressed to turn on shield
    public void deployShield()
    {
        if (!isBroken)
        {
            shieldOn = true;
            shieldMesh.enabled = true;
        }
    }

    public void deactivateShield()
    {
        shieldOn = false;
        shieldTimer = 0f;
        shieldMesh.enabled = false;
    }

    //called when shield reaches 0 health
    private void breakShield()
    {
        SoundManager.Instance.playSound("Shield_Break");
        deactivateShield();
        isBroken = true;
    }

    //controls interaction between shield and bullets
    private void reflectBullets()
    {
        if (shieldOn)
        {
            foreach (GameObject bullet in new List<GameObject>(bullets))
            {
                bullets.Remove(bullet);
                if (shieldTimer < .25f && bullet)
                {
                    bullet.gameObject.tag = "PlayerBullet";
                    Rigidbody rb = bullet.gameObject.GetComponent<Rigidbody>();
                    rb.velocity = new Vector3(-rb.velocity.x, -rb.velocity.y, -rb.velocity.z);
                }
                else
                {
                    Destroy(bullet);
                }
            }
        }
    }

    //used to update shield health and state
    private void updateShieldStats()
    {
        //if Shield is broken, run these conditions
        if (isBroken && delayIfBroken < 3f)
        {
            delayIfBroken += Time.deltaTime;
        }
        else if (isBroken)
        {
            isBroken = false;
            delayIfBroken = 0f;
        }
        
        //Shield On vs Shield Off
        if(shieldOn)
        {
            shieldHealth -= Time.deltaTime * 33.33f;
            shieldTimer += Time.deltaTime;
        }
        else
        {
            if (shieldHealth < 100f)
            {
                shieldHealth += regenRate;
            }
        }

        //break Shield
        if (shieldHealth < 0)
        {
            breakShield();
        }
    }

    //attempt to fix alpha bug (failed)
    private void OnDisable()
    {
        UnityEngine.Color color = shieldMat.color;
        color.a = (1);
        shieldMat.color = color;
    }

    //put bullets in list when overlapping
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            bullets.Add(other.gameObject);
        }
    }
    
    //remove bullets from list when overlapping
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Bullet" && bullets.Contains(other.gameObject))
        {
            bullets.Remove(other.gameObject);
        }
    }
}
