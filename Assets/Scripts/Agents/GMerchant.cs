using UnityEngine;

public class GMerchant : GAgent
{
    public int MinLoot = 1;
    public int MaxLoot = 5;

    void Awake()
    {
        GetComponent<Loot>().Value = Random.Range(MinLoot, MaxLoot);
    }
}
