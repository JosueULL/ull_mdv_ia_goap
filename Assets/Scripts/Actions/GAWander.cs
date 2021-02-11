public class GAWander : GActionGoToTarget
{
    public GInventoryKey VictimKey;
    public GInventoryKey CargoKey;

    public override bool PrePerform()
    {
        PickRandomWithTag("WanderPoint");
        return base.PrePerform();
    }

    public override void Perform()
    {
        base.Perform();

        if (Inventory.GetItem(VictimKey) || Inventory.GetItem(CargoKey))
        {
            Running = false;
        }
    }

}
