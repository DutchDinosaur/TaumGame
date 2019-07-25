using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "New Item", menuName = "Stuff/Item")]
public class Item : ScriptableObject
{

    new public string name = "New Item";

    public Sprite icon = null;
    public Sprite highlightIcon = null;

    public bool isFood = false;
}