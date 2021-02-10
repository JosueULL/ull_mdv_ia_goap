using UnityEngine;

public class GAGoToVictim : GActionGoToTarget
{
    private GameObject victim;
    public override bool PostPerform()
    {
        return base.PostPerform();
    }

    public override bool PrePerform()
    {
        victim = inventory.GetItem("Victim");
        if (victim)
        {
            target = victim;
            return base.PrePerform();
        }
        else
        {
            beliefs.RemoveState("FoundVictim");
            return false;
        }
    }

    public override void Perform()
    {
        GameObject victim = inventory.GetItem("Victim");
        if (victim)
        {
            Debug.DrawLine(transform.position, victim.transform.position);
            target = victim;

        }
        else if (!victim || !victim.activeSelf)
        {
            running = false;
        }

        base.Perform();
    }

}
