using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    // Fields
    [Header("Player Scripts")]
    [SerializeField]
    private PlayerInputScript playerInputScript;
    [SerializeField]
    private PlayerMovementScript playerMovementScript;
    [SerializeField]
    private GroundCheck groundCheck;
    [Header("Health Settings")]
    [SerializeField]
    private int health;
    [SerializeField]
    private int coins;
    [Header("Objects")]
    [SerializeField]
    private UIManager uiManager;
    [SerializeField]
    private GameManager gm;
    [SerializeField]
    private GameObject shield1;
    [SerializeField]
    private GameObject shield2;
    public PlayerInputScript pi { get { return playerInputScript; } }
    public GroundCheck gc { get { return groundCheck; } }
    public Rigidbody rb { get; private set; } // Component
    public Animator a { get; private set; } // Component

    // Awake is called when an enabled script instance is being loaded
    private void Awake()
    {
       rb = GetComponent<Rigidbody>();
       a = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        shield2.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {
        // Reset player
        if (pi.inputReset)
        {
            transform.position = new Vector3(23.5f, 19f, 4.5f);
        }
        // Face player right using front shield object
        if (rb.velocity.x > 1.5f)
        {
            shield1.SetActive(true);
            shield2.SetActive(false);
            transform.rotation = Quaternion.Euler(transform.rotation.x, 90, transform.rotation.z);
        }
        // Face player left using the back shield object 
        else if (rb.velocity.x < -1.5f)
        {
            shield1.SetActive(false);
            shield2.SetActive(true);
            transform.rotation = Quaternion.Euler(transform.rotation.x, 270, transform.rotation.z);
        }
        // Manage audio
        if (Mathf.Abs(playerInputScript.inputHorizontal) > .1f && groundCheck.IsGrounded())
        {
            SoundManager.Instance.playStateSound("Player_Walk");
        }
        else if (playerInputScript.inputFire && !groundCheck.IsGrounded())
        {
            SoundManager.Instance.playStateSound("Hover");
        }
        else
        {
            SoundManager.Instance.stopStateSound();
        }
        // Manage ui
        uiManager.SetHealth(health);
        uiManager.SetGold(coins);
    }
    // OnTriggerEnter is called when a GameObject collides with another GameObject
    // @param other is the Collider from a GameObject this instance collided with
    private void OnTriggerEnter(Collider other)
    {
        // Collect loot
        if(other.gameObject.tag == "Loot")
        {
            Destroy(other.gameObject);
            coins += 1;
        }
        // Take damage from bullet
        if(other.gameObject.tag == "Bullet")
        {
            SoundManager.Instance.playSound("Player_Damage");
            health -= other.gameObject.GetComponent<BulletScript>().damage;
            Destroy(other.gameObject);
            if (health <= 0)
            {
                gm.LoadLevel("Main");
            }
            
        }
        // Level complete when the player reaches the end
        if(other.gameObject.tag == "End")
        {
            gm.NextLevel();
        }
    }

    
}
