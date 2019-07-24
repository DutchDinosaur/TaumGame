using UnityEngine;

public class pickup : MonoBehaviour
{
    public Item item;
    private SpriteRenderer sprite;

    private bool highlighted = false;

    private void Start()
    {
        if (item == null)
        {
            return;
        }

        sprite = GetComponent<SpriteRenderer>();
        sprite.sprite = item.icon;
    }

    private void LateUpdate()
    {
        if (highlighted)
        {
            sprite.sprite = item.highlightIcon;
        }
        else { sprite.sprite = item.icon; }

        highlighted = false;
    }

    public void Highlight()
    {
        highlighted = true;
    }
}