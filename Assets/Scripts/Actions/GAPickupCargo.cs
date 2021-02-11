using UnityEngine;

public class GAPickupCargo : GActionGoToTarget
{
    public GKey CanSeeCargoKey;
    public GInventoryKey CargoKey;

    private Cargo mCargo;

    public override void Awake()
    {
        base.Awake();
        mCargo = GetComponent<Cargo>();
    }

    public override bool PostPerform()
    {
        Inventory.RemoveItem(CargoKey);
        Beliefs.RemoveState(CanSeeCargoKey);
        return base.PostPerform();
    }

    public override bool PrePerform()
    {
        GameObject cargo = Inventory.GetItem(CargoKey);
        Target = cargo;
        return base.PrePerform();
    }

    public override void Perform()
    {
        GameObject cargo = Inventory.GetItem(CargoKey);
        if (!cargo)
        {
            Target = null;
            Running = false;
        }

        base.Perform();

        if (!Running && cargo) // Reached the cargo
        {
            Loot loot = cargo.GetComponent<Loot>();
            mCargo.Add(loot.Value);
            Destroy(cargo);
        }
    }
}
