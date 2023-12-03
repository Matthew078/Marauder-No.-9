using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    // Fields
    [SerializeField] float smooth;
    [SerializeField] Vector3 maxPosition, minPosition;
    private Transform player;
    private Vector3 velocity;
    public Vector3 startPos;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Robot_Soldier_White").transform;
        velocity = Vector3.zero;
        transform.position = new Vector3(startPos.x, startPos.y, transform.position.z);
        // Application.targetFrameRate = 60;
    }

    // LateUpdate is called once per frame after Update
    void LateUpdate()
    {
        if (transform.position != player.transform.position)
        {
            Vector3 desiredPosition = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);

            desiredPosition.x = Mathf.Clamp(desiredPosition.x, minPosition.x, maxPosition.x);
            desiredPosition.y = Mathf.Clamp(desiredPosition.y, minPosition.y, maxPosition.y) + 1.5f;

            Vector3 smoothPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smooth);

            transform.position = smoothPosition;



            /*Vector3 cameraPosition = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
            transform.position = Vector3.SmoothDamp(transform.position, cameraPosition, ref velocity, smooth * Time.deltaTime);*/
        }
    }
}
