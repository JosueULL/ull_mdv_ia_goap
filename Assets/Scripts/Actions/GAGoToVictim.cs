using UnityEngine;

public class GAGoToVictim : GActionGoToTarget
{
    public GKey FoundVictimKey;
    public GInventoryKey VictimKey;

    private GameObject mVictim;

    public override bool PostPerform()
    {
        return base.PostPerform();
    }

    public override bool PrePerform()
    {
        mVictim = Inventory.GetItem(VictimKey);
        if (mVictim)
        {
            Target = mVictim;
            return base.PrePerform();
        }
        else
        {
            Beliefs.RemoveState(FoundVictimKey);
            return false;
        }
    }

    public override void Perform()
    {
        GameObject victim = Inventory.GetItem(VictimKey);
        if (victim)
        {
            Debug.DrawLine(transform.position, victim.transform.position);
            Target = victim;

        }
        else if (!victim || !victim.activeSelf)
        {
            Running = false;
        }

        base.Perform();
    }

}
