using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// Tutorial Article for the template of this code: https://gamedevacademy.org/educational-games-math-tutorial/ 
    /// </summary>
    public enum PlayerState
    {
        Idle, //0
        Jump, //1
        Drive, //2
        Crash, //3
    }

    public PlayerState curState;// current player state
    public float moveSpeed;// force applied horizontally when moving
    public float jumpSpeed;// force applied upwards when jumping
    public bool grounded;// is the player currently on the ground?
    public float crashDuration;// duration of crash
    private float crashStartTime;// time that the player was crash components
    public Rigidbody2D rb;// Rigidbody2D component
    public Animator anim;// Animator component

    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;

    AudioSource playSFX; //soundfx component
    public AudioClip jumpSFX, crashSFX; //sfx clips
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        objectWidth = transform.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        objectHeight = transform.GetComponent<SpriteRenderer>().bounds.size.y / 2;
        playSFX = GetComponent<AudioSource>();
    }

    void LateUpdate()
    {
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x * -1 + objectWidth, screenBounds.x - objectWidth);
        viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y * -1 + objectHeight, screenBounds.y - objectHeight);
        transform.position = viewPos;
    }

    // Update is called once per frame
    void Update()
    {
        // shoot a raycast down underneath the player
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 1.45f), Vector2.down, 1.0f);
        // did we hit anything?
        if (hit.collider != null)
        {
            // was it the floor?
            if (hit.collider.CompareTag("Floor"))
            {
                grounded = true;
            }
        }
        else
        {
            grounded = false;
        }

        if (curState != PlayerState.Crash)
        {
            // jump
            if (CrossPlatformInputManager.GetButtonDown("Jump") && !PauseMenu.isPause)
                Jump();
        }
    }

    private void FixedUpdate()
    {
        CheckInputs();
        // is the player stunned?
        if (curState == PlayerState.Crash)
        {
            // has the player been stunned for the duration?
            if (Time.time - crashStartTime >= crashDuration)
            {
                curState = PlayerState.Idle;
            }
        }
    }

    // sets the player's state
    void SetState()
    {
        // don't worry about changing states if the player's crashed
        if (curState != PlayerState.Crash)
        {
            // idle
            if (rb.velocity.magnitude == 0 && grounded)
                curState = PlayerState.Idle;
            // Drive
            if (rb.velocity.x != 0 && grounded)
                curState = PlayerState.Drive;
            // jump
            if (rb.velocity.magnitude != 0 && !grounded)
                curState = PlayerState.Jump;
        }
        // tell the animator we've changed states
        anim.SetInteger("State", (int)curState);
    }

    // moves the player horizontally
    void Move()
    {
        // get horizontal axis (A & D, Left Arrow & Right Arrow)
        float dir = CrossPlatformInputManager.GetAxis("Horizontal");

        // flip player to face the direction they're moving
        if (dir > 0)
            transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        else if (dir < 0)
            transform.localScale = new Vector3(-1.5f, 1.5f, 1.5f);
        // set rigidbody horizontal velocity
        rb.velocity = new Vector2(dir * moveSpeed, rb.velocity.y);
    }

    // adds force upwards to player
    void Jump()
    {
        if (grounded)
        {
            // add force upwards
            rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
            playSFX.PlayOneShot(jumpSFX); //play jump fx
        }
    }

    // checks for user input to control player
    void CheckInputs()
    {
        if (curState != PlayerState.Crash)
        {
            // movement
            Move();
        }
        // update our current state
        SetState();
    }

    // called when the player gets crash
    public void Crash()
    {
        curState = PlayerState.Crash;
        rb.velocity = Vector2.down * 3;
        crashStartTime = Time.time;
        playSFX.PlayOneShot(crashSFX); //play the crash sfx
    }
}
