using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputScript : MonoBehaviour
{
    [Header("Input Configurations")]
    [SerializeField]
    private string horizontalAxis = "Horizontal";
    [SerializeField]
    private string verticalAxis = "Vertical";
    [SerializeField]
    private string interactButton = "e";

    public float inputHorizontal { get; private set; }
    public float inputVertical { get; private set; }
    public bool inputInteract { get; private set; }
    public bool inputDefenseDown { get; private set; }

    public bool inputDefenseUp { get; private set; }
    public bool inputGrenade { get; private set; }

    // Start is called before the first frame update
    private void Start()
    {
        inputHorizontal = 0;
        inputVertical = 0;
    }

    // Update is called once per frame
    private void Update()
    {
        inputHorizontal = Input.GetAxis(horizontalAxis);
        inputVertical = Input.GetAxisRaw(verticalAxis);
        inputInteract = Input.GetKeyDown(interactButton);
        inputDefenseDown = Input.GetKeyDown("q");
        inputDefenseUp = Input.GetKeyUp("q");
        inputGrenade = Input.GetKeyDown("g");
    }
}
