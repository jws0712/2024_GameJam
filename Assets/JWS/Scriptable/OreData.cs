using UnityEngine;

[CreateAssetMenu(fileName = "OreData", menuName = "Scriptable Object Asset/OreData")]
public class OreData : ScriptableObject
{
    [Header("OreSetting")]
    [SerializeField] private Sprite oreSprite;
    [SerializeField] private float oreHp;
    [SerializeField] private float coinCount;

    public Sprite OreSprite => oreSprite;
    public float OreHp => oreHp;
    public float CoinCount => coinCount;

}
