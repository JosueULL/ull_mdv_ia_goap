public class GAWander : GActionGoToTarget
{
    public override bool PrePerform()
    {
        PickRandomWithTag("WanderPoint");
        return base.PrePerform();
    }

    public override void Perform()
    {
        base.Perform();

        if (inventory.GetItem("Victim") || inventory.GetItem("TargetCargo"))
        {
            running = false;
        }
    }

}
