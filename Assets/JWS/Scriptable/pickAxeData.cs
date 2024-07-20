using UnityEngine;

[CreateAssetMenu(fileName = "pickAxeData", menuName = "Scriptable Object Asset/pickAxeData")]
public class pickAxeData : ScriptableObject
{
    [Header("OreSetting")]
    [SerializeField] private Sprite pickAxeSprite;
    [SerializeField] private float pickAxeDamage;

    public Sprite PickAxeSprite => pickAxeSprite;
    public float PickAxeDamage => pickAxeDamage;

}
