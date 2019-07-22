using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private inputManager inputManager;
    private Rigidbody2D rb;

    void Start()
    {
        inputManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<inputManager>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        transform.LookAt(GameObject.FindGameObjectWithTag("MainCamera").transform);
    }

    void FixedUpdate()
    {
        rb.AddForce(new Vector2(inputManager.movementDirection.x * speed, inputManager.movementDirection.y * speed));
    }
}