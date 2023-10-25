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
    [Header("Health Settings")]
    [SerializeField]
    private int health;
    [Header("Objects")]
    [SerializeField]
    private UIManager uiManager;

    public PlayerInputScript pi { get { return playerInputScript; } }
    public GroundCheck gc { get { return groundCheck; } }
    public Rigidbody rb { get; private set; }
    //components
    
    //public new Animation ;
    // make input script seperate so main script carries game data info, make audio script too, put scripts into seperate game obj
    private void Awake()
    {
       rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        uiManager.SetHealth(health / 100);
    }


}
