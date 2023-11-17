using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NewPlayerScript : MonoBehaviour
{
    [Header("Player Scripts")]
    [SerializeField]
    private PlayerInputScript playerInputScript;
    [SerializeField]
    private PlayerMovementScript playerMovementScript;
    [SerializeField]
    private GroundCheck groundCheck;
    [SerializeField]
    private GameObject shield1;
    [SerializeField]
    private GameObject shield2;
    [Header("Health Settings")]
    [SerializeField]
    private int health;
    [SerializeField]
    private int coins;
    [Header("Objects")]
    [SerializeField]
    private UIManager uiManager;

    public PlayerInputScript pi { get { return playerInputScript; } }
    public GroundCheck gc { get { return groundCheck; } }
    public Rigidbody rb { get; private set; }
    public Animator a { get; private set; }
    //components
    
    //public new Animation ;
    // make input script seperate so main script carries game data info, make audio script too, put scripts into seperate game obj
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
        if (rb.velocity.x > 1f)
        {
            shield1.SetActive(true);
            shield2.SetActive(false);
            transform.rotation = Quaternion.Euler(transform.rotation.x, 90, transform.rotation.z);
        }
        else if (rb.velocity.x < -1f)
        {
            shield1.SetActive(false);
            shield2.SetActive(true);
            transform.rotation = Quaternion.Euler(transform.rotation.x, 270, transform.rotation.z);
        }

        uiManager.SetHealth(health);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Loot")
        {
            Destroy(other.gameObject);
            coins += 1;
        }

        if(other.gameObject.tag == "Bullet")
        {
            SoundManager.Instance.playSound("Player_Damage");
            health -= other.gameObject.GetComponent<BulletScript>().damage;
            Destroy(other.gameObject);
        }
    }

    
}
