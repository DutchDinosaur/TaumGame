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

    private Transform lastClosestItem;
    private Transform lastClosestChest;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = gameObject.GetComponentInChildren<Animator>();
        spriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();

        if (inventory == null) { inventory = GetComponent<Inventory>(); }
    }

    private void Update()
    {
        Move();

        transform.GetChild(0).LookAt(GameObject.FindGameObjectWithTag("MainCamera").transform);

        animationStuff();
        interactWithStuff();
    }

    void Move()
    {
        //Movement
        float currentSpeed = speed;
        if (inputManager.instance.movementDirection == new Vector2(1,1) || inputManager.instance.movementDirection == new Vector2(-1, -1) || inputManager.instance.movementDirection == new Vector2(-1, 1) || inputManager.instance.movementDirection == new Vector2(1, -1))
        {

        }

        rb.MovePosition(new Vector3(transform.position.x + inputManager.instance.movementDirection.x * currentSpeed * Time.deltaTime, transform.position.y + inputManager.instance.movementDirection.y * currentSpeed * Time.deltaTime, transform.position.z)); // stuttery
    }

    void animationStuff()
    {
        if (inputManager.instance.movementDirection.x != 0 || inputManager.instance.movementDirection.y != 0)
        {
            animator.SetBool("Sideways", true);
            animator.SetBool("Running", true);

            if (inputManager.instance.movementDirection.x > 0)
            {
                spriteRenderer.flipX = false;
            }
            else if (inputManager.instance.movementDirection.x < 0) { spriteRenderer.flipX = true; }

        }
        else { animator.SetBool("Running", false); }
    }

    void interactWithStuff()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position - new Vector3(0, .6f, 0), 1);

        List<Transform> Chests = new List<Transform>();
        List<Transform> Items = new List<Transform>();

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].transform.tag == "Item")
            {
                Items.Add(colliders[i].transform);
            }
            if (colliders[i].transform.tag == "Chest")
            {
                Chests.Add(colliders[i].transform);
            }
        }

        if (Chests.Count > 0)
        {
            interactWithChests(Chests);
        } else if (Items.Count > 0) { interactWithItems(Items); }


        if (Chests.Count == 0 && lastClosestChest != null)
        {
            lastClosestChest.GetComponent<Chest>().Highlight(false);
            lastClosestChest = null;
        }
        if (Items.Count == 0 && lastClosestItem != null)
        {
            lastClosestItem.GetComponent<pickup>().Highlight(false);
            lastClosestItem = null;
        }
    }

    void interactWithItems(List<Transform> Items)
    {
        float distance = 0;
        int closestItem = 0;
        for (int i = 0; i < Items.Count; i++)
        {
            float d = Vector3.Distance(transform.position, Items[i].transform.position);
            if (d < distance)
            {
                distance = d;
                closestItem = i;
            }
        }

        pickup item = Items[closestItem].GetComponent<pickup>();

        if (lastClosestItem != Items[closestItem])
        {
            //stop highlighting opd item
            if (lastClosestItem != null)
            {
                lastClosestItem.GetComponent<pickup>().Highlight(false);
            }
            
            //highlight new item
            item.Highlight(true);
        }
        lastClosestItem = Items[closestItem];


        if (inputManager.instance.interactButton == true)
        {
            inventory.Add(item.item);
            Destroy(item.gameObject);
        }
    }

    void interactWithChests(List<Transform> Chests)
    {
        float distance = 0;
        int closestChest = 0;
        for (int i = 0; i < Chests.Count; i++)
        {
            float d = Vector3.Distance(transform.position, Chests[i].transform.position);
            if (d < distance)
            {
                distance = d;
                closestChest = i;
            }
        }

        if (lastClosestChest != Chests[closestChest])
        {
            if (lastClosestChest != null)
            {
                lastClosestChest.GetComponent<Chest>().Highlight(false);
            }

            Chests[closestChest].GetComponent<Chest>().Highlight(true);
        }
        lastClosestChest = Chests[closestChest];

        if (inputManager.instance.interactButton == true)
        {
            Chests[closestChest].GetComponent<Chest>().Open();
            lastClosestChest = null;
        }
    }
}