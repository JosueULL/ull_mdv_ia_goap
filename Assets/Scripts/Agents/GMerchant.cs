using UnityEngine;

public class GMerchant : GAgent
{
    void Awake()
    {
        goals.Add(new SubGoal("PickupCargo", 1, false), 1);

        GetComponent<Loot>().Value = Random.Range(1, 5);
    }
}
