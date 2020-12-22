using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    private PlayerController player;
    private cameraController playerCamera;
    public Vector2 startingDirection;
    public string pointName;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        if(player != null)
        {
            SetLocation();
        }
    }

    void SetLocation()
    {
        if (player.startingPoint == pointName)
        {
            player.transform.position = transform.position;
            player.lastDirection = startingDirection;

            playerCamera = FindObjectOfType<cameraController>();
            playerCamera.transform.position = new Vector3(transform.position.x, transform.position.y, playerCamera.transform.position.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
