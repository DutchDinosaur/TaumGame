using UnityEngine;

public class pickup : MonoBehaviour
{
    public Item item;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        if (item == null)
        {
            return;
        }

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = item.icon;
    }

    public void Highlight(bool b)
    {
        if (b)
        {
            spriteRenderer.sprite = item.highlightIcon;
        }
        else { spriteRenderer.sprite = item.icon; }
    }
}