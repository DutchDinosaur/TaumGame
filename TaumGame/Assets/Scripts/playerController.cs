using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private Inventory inventory;

    private Rigidbody rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = gameObject.GetComponentInChildren<Animator>();
        spriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();

        if (inventory == null) { inventory = GetComponent<Inventory>(); }
    }

    private void Update()
    {
        transform.GetChild(0).LookAt(GameObject.FindGameObjectWithTag("MainCamera").transform);

        if (inputManager.instance.movementDirection.x != 0 || inputManager.instance.movementDirection.y != 0)
        {
            animator.SetBool("Sideways", true);
            animator.SetBool("Running", true);

            if (inputManager.instance.movementDirection.x > 0)
            {
                spriteRenderer.flipX = false;
            }
            else if (inputManager.instance.movementDirection.x < 0) { spriteRenderer.flipX = true; }

        } else { animator.SetBool("Running", false); }

        interactWithStuff();
    }

    void FixedUpdate()
    {
        rb.AddForce(new Vector2(inputManager.instance.movementDirection.x * speed, inputManager.instance.movementDirection.y * speed));
    }

    void interactWithStuff()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position - new Vector3(0, .6f, 0), 1);
        List<Transform> Items = new List<Transform>();
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].transform.tag == "Item")
            {
                Items.Add(colliders[i].transform);
            }
        }

        if (Items.Count == 0) { return; }

        float distance = 0;
        int closestCollider = 0;
        for (int i = 0; i < Items.Count; i++)
        {
            float d = Vector3.Distance(transform.position, colliders[i].transform.position);
            if (d < distance)
            {
                distance = d;
                closestCollider = i;
            }
        }

        pickup item = Items[closestCollider].GetComponent<pickup>();
        item.Highlight();
        if (inputManager.instance.interactButton == true)
        {
            inventory.Add(item.item);
            Destroy(item.gameObject);
        }
    }
}