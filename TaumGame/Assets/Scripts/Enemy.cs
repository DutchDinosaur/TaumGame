using UnityEngine;
using UnityEditor.Animations;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Stuff/Enemy")]
public class Enemy : ScriptableObject
{

    new public string name = "New Enemy";

    public AnimatorController AnimatorController;

    public int Health;
    public float DamageMultiplier;

}
