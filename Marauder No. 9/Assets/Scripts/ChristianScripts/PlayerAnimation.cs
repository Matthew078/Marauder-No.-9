using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField]
    private NewPlayerScript p;

    Animator playerAnim;

    // Start is called before the first frame update
    void Start()
    {
        playerAnim = p.a;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Player Walking Animations
        float move = p.pi.inputHorizontal;
        //Debug.Log(move);
        playerAnim.SetFloat("speed", Mathf.Abs(move));

        //Player Jump Animations
        bool isJumping = !p.gc.IsGrounded();
        //if (isJumping) { Debug.Log(isJumping); }
        playerAnim.SetBool("jumping", isJumping);

        //Player Grenade Animations
        /*
        bool grenadeRoll = p.pi.inputGrenade;
        if (grenadeRoll) { Debug.Log(grenadeRoll); }
        playerAnim.SetBool("grenadeRoll", grenadeRoll);
        */
    }
}
