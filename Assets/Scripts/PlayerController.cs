﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float speed, jumpSpeed;
    [SerializeField] private LayerMask ground;

    private PlayerControl playerControl;

    private Rigidbody2D rb;
    private Collider2D col;

    private Animator animator;

    private SpriteRenderer spriteRenderer;

    private AudioClip deathSound;
    private AudioClip teleportSound;

    private int nextSceneToLoad;
    private int restartScene;

    private void Awake()
    {
        playerControl = new PlayerControl();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        playerControl.Enable();
    }

    private void OnDisable()
    {
        playerControl.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        deathSound = (AudioClip)Resources.Load("deathSound");
        teleportSound = (AudioClip)Resources.Load("teleportSound");
        playerControl.Land.Jump.performed += _ => Jump();
        nextSceneToLoad = SceneManager.GetActiveScene().buildIndex + 1;
        restartScene = SceneManager.GetActiveScene().buildIndex + 0;
    }

    private void Jump()
    {
        if (IsGrounded())
        {
            rb.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
            animator.SetTrigger("Jump");
        }
    }

    private bool IsGrounded()
    {
        Vector2 topLeftPoint = transform.position;
        topLeftPoint.x -= col.bounds.extents.x;
        topLeftPoint.y += col.bounds.extents.y;

        Vector2 bottomRightPoint = transform.position;
        bottomRightPoint.x += col.bounds.extents.x;
        bottomRightPoint.y -= col.bounds.extents.y;

        return Physics2D.OverlapArea(topLeftPoint, bottomRightPoint, ground);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        // read the movement value
        float movementInput = playerControl.Land.Move.ReadValue<float>();

        // move player
        Vector3 currentPosition = transform.position;
        currentPosition.x += movementInput * speed * Time.deltaTime;
        transform.position = currentPosition;

        // Animation
        if (movementInput != 0) animator.SetBool("Run", true);
        else animator.SetBool("Run", false);

        // Sprite Flip
        if (movementInput == -1)
            spriteRenderer.flipX = true;
        else if (movementInput == 1)
            spriteRenderer.flipX = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            StartCoroutine(playDeath());
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            StartCoroutine(playDeath());
        }


        if (collision.gameObject.tag == "Portal")
        {
            StartCoroutine(playTeleport());
        }
    }

    IEnumerator playDeath()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = deathSound;
        audio.Play();
        yield return new WaitUntil(() => audio.isPlaying == false);
        SceneManager.LoadScene(restartScene);
    }

    IEnumerator playTeleport()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = teleportSound;
        audio.Play();
        yield return new WaitUntil(() => audio.isPlaying == false);
        SceneManager.LoadScene(nextSceneToLoad);
    }
}
