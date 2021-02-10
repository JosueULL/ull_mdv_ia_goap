using UnityEngine;

public class GAAttackVictim : GAction
{
    public float TimeBetweenAttacks = 3;
    private float mLastAttack;

    public override void Perform()
    {
    }

    public override bool PostPerform()
    {
        return true;
    }

    public override bool PrePerform()
    {
        GameObject victim = inventory.GetItem("Victim");
        if (victim)
        {
            if (victim.activeSelf)
            {
                if ((Time.time - mLastAttack) > TimeBetweenAttacks)
                {
                    Health health = victim.GetComponent<Health>();
                    health.ReduceHealth(1);
                    if (health.CurrentAmount <= 0)
                    {
                        Loot loot = victim.GetComponent<Loot>();
                        Cargo cargo = agent.GetComponent<Cargo>();
                        if (loot && cargo)
                        {
                            cargo.Add(loot.Value);
                            
                        }

                        victim.gameObject.SetActive(false);
                    }
                    mLastAttack = Time.time;
                }
            }

            if (!victim.activeSelf)
            {
                inventory.RemoveItem("Victim");
            }
        }

        return false;
    }
}
