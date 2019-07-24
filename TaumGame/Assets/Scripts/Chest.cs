using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public List<Item> items = new List<Item>();
    [SerializeField]
    private Sprite chestSprite;
    [SerializeField]
    private Sprite highlightChestSprite;
    [SerializeField]
    private Sprite openChestSprite;
    [SerializeField]
    private GameObject itemPrefab;
    [SerializeField]
    private Transform itemPartent;

    private SpriteRenderer SpriteRenderer;

    private void Start()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        SpriteRenderer.sprite = chestSprite;
    }

    public void Open()
    {
        SpriteRenderer.sprite = openChestSprite;

        //drop items
        SpawnItems(ItemDropPosses(items.Count));
        Destroy(GetComponent<SphereCollider>());
        Destroy(this);
    }

    public void Highlight(bool b)
    {
        if (b)
        {
            SpriteRenderer.sprite = highlightChestSprite;
        }
        else { SpriteRenderer.sprite = chestSprite; }
    }

    private Vector2[] ItemDropPosses (int amount)
    {
        Vector2[] Posses = new Vector2[amount];

        for (int i = 0; i < amount; i++)
        {
            float dir = ((2 * Mathf.PI) / amount) * i;

            Posses[i] = new Vector2(Mathf.Cos(dir), Mathf.Sin(dir));
        }

        return Posses;
    }

    private void SpawnItems(Vector2[] Posses)
    {
        for (int i = 0; i < Posses.Length; i++)
        {
            Instantiate(itemPrefab,new Vector3(transform.position.x + Posses[i].x,transform.position.y + Posses[i].y,-0.3f),Quaternion.Euler(-43,0,0 ),itemPartent);
        }
    }
}