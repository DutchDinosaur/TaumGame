﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private inputManager inputManager;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        inputManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<inputManager>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        transform.LookAt(GameObject.FindGameObjectWithTag("MainCamera").transform);

        if (inputManager.movementDirection.x != 0 || inputManager.movementDirection.y != 0)
        {
            animator.SetBool("Sideways", true);
            animator.SetBool("Running", true);

            if (inputManager.movementDirection.x > 0)
            {
                spriteRenderer.flipX = false;
            }
            else if (inputManager.movementDirection.x < 0) { spriteRenderer.flipX = true; }

        } else { animator.SetBool("Running", false); }

    }

    void FixedUpdate()
    {
        rb.AddForce(new Vector2(inputManager.movementDirection.x * speed, inputManager.movementDirection.y * speed));
    }
}