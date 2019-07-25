using UnityEngine;

[CreateAssetMenu(fileName = "New Attack", menuName = "Stuff/Attack")]
public class Attack : ScriptableObject
{

    new public string name = "New Attack";

    public int Damage;
    public int AnimationIndex;

}