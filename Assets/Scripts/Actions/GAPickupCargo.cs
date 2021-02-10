using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GAPickupCargo : GActionGoToTarget
{
    public override bool PostPerform()
    {
        inventory.RemoveItem("TargetCargo");
        beliefs.RemoveState("CanSeeCargo");
        return base.PostPerform();
    }

    public override bool PrePerform()
    {
        GameObject cargo = inventory.GetItem("TargetCargo");
        target = cargo;
        return base.PrePerform();
    }

    public override void Perform()
    {
        GameObject cargo = inventory.GetItem("TargetCargo");
        if (!cargo)
        {
            target = null;
            running = false;
        }

        base.Perform();

        if (!running && cargo) // Reached the cargo
        {
            Cargo agentCargo = agent.GetComponent<Cargo>();
            Loot loot = cargo.GetComponent<Loot>();
            agentCargo.Add(loot.Value);
            Destroy(cargo);
        }
    }
}
