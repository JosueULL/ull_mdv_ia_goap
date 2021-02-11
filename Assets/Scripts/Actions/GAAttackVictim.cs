using UnityEngine;

public class GAAttackVictim : GAction
{
    public GKey FoundVictimKey;
    public GInventoryKey VictimKey;
    public int Damage = 1;
    public float TimeBetweenAttacks = 3;
    
    private float mLastAttack;

    private Cargo mCargo;

    public override void Awake()
    {
        base.Awake();
        mCargo = GetComponent<Cargo>();
    }

    public override void Perform()
    {
        // Nothing...
    }

    public override bool PostPerform()
    {
        return true;
    }

    public override bool PrePerform()
    {
        GameObject victim = Inventory.GetItem(VictimKey);
        if (victim)
        {
            if (victim.activeSelf)
            {
                if ((Time.time - mLastAttack) > TimeBetweenAttacks)
                {
                    Health health = victim.GetComponent<Health>();
                    health.ReduceHealth(Damage);
                    if (health.CurrentAmount <= 0)
                    {
                        Loot loot = victim.GetComponent<Loot>();
                        if (loot && mCargo)
                        {
                            mCargo.Add(loot.Value);
                        }

                        victim.gameObject.SetActive(false);
                    }
                    mLastAttack = Time.time;
                }
            }

            if (!victim.activeSelf)
            {
                Inventory.RemoveItem(VictimKey);
                Beliefs.RemoveState(FoundVictimKey);
            }
        }

        return false;
    }
}
