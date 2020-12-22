using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    private Animator animator;
    private Rigidbody2D playerRB;
    private static bool playerExists;

    private bool isMoving;
    public Vector2 lastDirection;
    public string startingPoint;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerRB = GetComponent<Rigidbody2D>();
        
        if (!playerExists)
        {
            playerExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        isMoving = false;

        //Movement with Rigidbody2D
        if(Input.GetAxisRaw("Horizontal") > 0.5f || Input.GetAxisRaw("Horizontal") < -0.5f)
        {
            //transform.Translate(new Vector3((Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime), 0f, 0f));
            playerRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, playerRB.velocity.y);
            lastDirection = new Vector2(Input.GetAxisRaw("Horizontal"), 0f);
            isMoving = true;
        }
        if (Input.GetAxisRaw("Vertical") > 0.5f || Input.GetAxisRaw("Vertical") < -0.5f)
        {
            //transform.Translate(new Vector3(0f, (Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime), 0f));
            playerRB.velocity = new Vector2(playerRB.velocity.x, Input.GetAxisRaw("Vertical") * moveSpeed);
            lastDirection = new Vector2(0f, Input.GetAxisRaw("Vertical"));
            isMoving = true;
        }

        //Stopping movement with Rigidbody2D
        if (Input.GetAxisRaw("Horizontal") < 0.5f && Input.GetAxisRaw("Horizontal") > -0.5f)
        {
            playerRB.velocity = new Vector2(0f, playerRB.velocity.y);
        }
        if (Input.GetAxisRaw("Vertical") < 0.5f && Input.GetAxisRaw("Vertical") > -0.5f)
        {
            playerRB.velocity = new Vector2(playerRB.velocity.x, 0f);
        }

        animator.SetFloat("moveX", Input.GetAxisRaw("Horizontal"));
        animator.SetFloat("moveY", Input.GetAxisRaw("Vertical"));
        animator.SetFloat("lastMoveX", lastDirection.x);
        animator.SetFloat("lastMoveY", lastDirection.y);
        animator.SetBool("isMoving", isMoving);
    }
}
